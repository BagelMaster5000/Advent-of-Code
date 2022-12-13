using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AOC.Challenges {
    public class D11 {
        public struct WorryOperation {
            public char wOperator;
            public int wOperand;
            public WorryOperation(char setOperator, int setOperand) {
                wOperator = setOperator;
                wOperand = setOperand;
            }
        }

        public class Item {
            public BigInteger worryLevel;
            public Item(int setWorryLevel) {
                worryLevel = setWorryLevel;
            }
        }

        public class Monkey {
            List<Item> items;
            int numInspectedItems;
            public Item TakeFirstItem() {
                Item takeItem = items[0];
                items.RemoveAt(0);
                numInspectedItems++;

                return takeItem;
            }
            public void AddItem(Item addItem) {
                items.Add(addItem);
            }
            public Item GetItem(int index) => items[index];
            public int GetNumItems() => items.Count;
            public int GetInspectedItems() => numInspectedItems;

            WorryOperation worryOperation;
            public int ProcessWorryOperationAndGetNewWorryLevel(int startWorryLevel) {
                int returnWorryLevel = 0;

                if (worryOperation.wOperand == -1) {
                    switch (worryOperation.wOperator) {
                        case '+': returnWorryLevel = startWorryLevel + startWorryLevel; break;
                        case '-': returnWorryLevel = startWorryLevel - startWorryLevel; break;
                        case '*': returnWorryLevel = startWorryLevel * startWorryLevel; break;
                        case '/': returnWorryLevel = startWorryLevel / startWorryLevel; break;
                    }
                }
                else {
                    switch (worryOperation.wOperator) {
                        case '+': returnWorryLevel = startWorryLevel + worryOperation.wOperand; break;
                        case '-': returnWorryLevel = startWorryLevel - worryOperation.wOperand; break;
                        case '*': returnWorryLevel = startWorryLevel * worryOperation.wOperand; break;
                        case '/': returnWorryLevel = startWorryLevel / worryOperation.wOperand; break;
                    }
                }

                return returnWorryLevel;
            }
            public void ProcessWorryOperation(Item item) {
                if (worryOperation.wOperand == -1) {
                    switch (worryOperation.wOperator) {
                        case '+': item.worryLevel += item.worryLevel; break;
                        case '-': item.worryLevel -= item.worryLevel; break;
                        case '*': item.worryLevel *= item.worryLevel; break;
                        case '/': item.worryLevel /= item.worryLevel; break;
                    }
                }
                else {
                    switch (worryOperation.wOperator) {
                        case '+': item.worryLevel += worryOperation.wOperand; break;
                        case '-': item.worryLevel -= worryOperation.wOperand; break;
                        case '*': item.worryLevel *= worryOperation.wOperand; break;
                        case '/': item.worryLevel /= worryOperation.wOperand; break;
                    }
                }
            }

            public int testDivisbleNum;
            int ifTrueThrowMonkey, ifFalseThrowMonkey;
            public int TestDivisibleAndGetThrowMonkey(int worryLevel) {
                int throwMonkey = (worryLevel % testDivisbleNum == 0) ? ifTrueThrowMonkey : ifFalseThrowMonkey;
                return throwMonkey;
            }
            public int TestDivisibleAndGetThrowMonkey(BigInteger worryLevel) {

                int throwMonkey = (worryLevel % testDivisbleNum == 0) ? ifTrueThrowMonkey : ifFalseThrowMonkey;
                return throwMonkey;
            }

            public Monkey(List<Item> setItems, char setWorryOperator, int setWorryOperand, int setTestDivisbleNum, int setIfTrueThrowMonkey, int setIfFalseThrowMonkey) {
                items = setItems;
                numInspectedItems = 0;

                worryOperation = new WorryOperation(setWorryOperator, setWorryOperand);

                testDivisbleNum = setTestDivisbleNum;
                ifTrueThrowMonkey = setIfTrueThrowMonkey;
                ifFalseThrowMonkey = setIfFalseThrowMonkey;
            }
        }




        public static List<Monkey> monkeys;

        public static void Run() {
            GenerateMonkeys();
            Pt1ProcessMonkeyThrowing20Rounds();
            Console.WriteLine("Pt 1 Monkey Inspection #s:");
            for (int m = 0; m < monkeys.Count(); m++) {
                Console.WriteLine("Monkey " + m + ": " + monkeys[m].GetInspectedItems());
            }
            Console.WriteLine();

            GenerateMonkeys();
            Pt2ProcessMonkeyThrowing10000Rounds();
            Console.WriteLine("Pt 2 Monkey Inspection #s:");
            for (int m = 0; m < monkeys.Count(); m++) {
                Console.WriteLine("Monkey " + m + ": " + monkeys[m].GetInspectedItems());
            }
            Console.WriteLine();
        }

        private static void Pt1ProcessMonkeyThrowing20Rounds() {
            for (int r = 0; r < 20; r++) {
                for (int m = 0; m < monkeys.Count(); m++) {
                    Monkey curMonkey = monkeys[m];
                    while (curMonkey.GetNumItems() > 0) {
                        Item curItem = curMonkey.TakeFirstItem();

                        int worryLevel = (int)curItem.worryLevel;
                        worryLevel = curMonkey.ProcessWorryOperationAndGetNewWorryLevel(worryLevel);
                        worryLevel /= 3;

                        int throwMonkey = curMonkey.TestDivisibleAndGetThrowMonkey(worryLevel);

                        curItem.worryLevel = worryLevel;
                        monkeys[throwMonkey].AddItem(curItem);
                    }
                }
            }
        }

        private static void Pt2ProcessMonkeyThrowing10000Rounds() {
            int productOfTestValues = 1;
            foreach (Monkey m in monkeys) {
                productOfTestValues *= m.testDivisbleNum;
            }

            for (int r = 0; r < 10000; r++) {
                for (int m = 0; m < monkeys.Count(); m++) {
                    Monkey curMonkey = monkeys[m];
                    while (curMonkey.GetNumItems() > 0) {
                        Item curItem = curMonkey.TakeFirstItem();
                        curMonkey.ProcessWorryOperation(curItem);

                        curItem.worryLevel %= productOfTestValues;

                        int throwMonkey = curMonkey.TestDivisibleAndGetThrowMonkey(curItem.worryLevel);
                        monkeys[throwMonkey].AddItem(curItem);
                    }
                }
            }
        }

        #region Helpers
        private static void GenerateMonkeys() {
            monkeys = new List<Monkey>();
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D11\\D11_Input.txt");
            for (int i = 0; i < inputs.Count() - 5; i += 7) {
                string startingItemsLine = inputs.ElementAt(i + 1);
                string[] startingItemsLineSplit = startingItemsLine.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<Item> startingItems = new List<Item>(startingItemsLineSplit.Length);
                for (int s = 2; s < startingItemsLineSplit.Length; s++) {
                    int itemWorryLevel = int.Parse(startingItemsLineSplit[s]);
                    startingItems.Add(new Item(itemWorryLevel));
                }

                string operationLine = inputs.ElementAt(i + 2);
                string[] operationLineSplit = operationLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                char operation = (operationLineSplit[4])[0];
                int operand = -1;
                if (operationLineSplit[5] != "old") {
                    operand = int.Parse(operationLineSplit[5]);
                }

                string divisibleLine = inputs.ElementAt(i + 3);
                string[] divisibleLineSplit = divisibleLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int testDivisibleNum = int.Parse(divisibleLineSplit[3]);

                string trueThrowMonkeyLine = inputs.ElementAt(i + 4);
                string[] trueThrowMonkeyLineSplit = trueThrowMonkeyLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int trueThrowMonkey = int.Parse(trueThrowMonkeyLineSplit[5]);

                string falseThrowMonkeyLine = inputs.ElementAt(i + 5);
                string[] falseThrowMonkeyLineSplit = falseThrowMonkeyLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int falseThrowMonkey = int.Parse(falseThrowMonkeyLineSplit[5]);

                Monkey newMonkey = new Monkey(startingItems, operation, operand, testDivisibleNum, trueThrowMonkey, falseThrowMonkey);
                monkeys.Add(newMonkey);
            }
        }
        #endregion
    }
}
