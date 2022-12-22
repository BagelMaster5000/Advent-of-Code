using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Challenges {
    public class D13 {
        public abstract class CompareElement { }

        public class CompareList : CompareElement {
            public List<CompareElement> value;
            public CompareList(List<CompareElement> setValue) {
                value = setValue;
            }

            override public string ToString() {
                string returnString = "[";
                for (int i = 0; i < value.Count; i++) {
                    returnString += value[i].ToString();
                    if (i < value.Count - 1) {
                        returnString += ", ";
                    }
                }
                returnString += "]";

                return returnString;
            }
        }

        public class CompareInt : CompareElement {
            public int value;
            public CompareInt(int setValue) {
                value = setValue;
            }

            public override string ToString() {
                return value.ToString();
            }
        }

        public static void Run() {
            int sumOfCorrectOrderPairIndexes = Part1CalcSumOfCorrectOrderPairIndexes();
            Console.WriteLine("Pt 1 sum of correct ordered pair indexes: " + sumOfCorrectOrderPairIndexes);
            Console.WriteLine();

            List<CompareList> allLists = Part2CreateAndSortAllLists();
            //Console.WriteLine("Pt 2 sorted lists:");
            //foreach (CompareList list in allLists) {
            //    Console.WriteLine(list.ToString());
            //}
            //Console.WriteLine();

            int productOfDividerPacketIndexes = Part2CalcProductOfDividerPacketIndexes(allLists);
            Console.WriteLine("Pt 2 product of divider packet indexes: " + productOfDividerPacketIndexes);
            Console.WriteLine();
        }

        #region Part 1
        private static int Part1CalcSumOfCorrectOrderPairIndexes() {
            CompareList list1, list2;
            int pairIndex = 1;
            int sumOfCorrectOrderPairIndexes = 0;
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D13\\D13_Input.txt");
            for (int i = 0; i < inputs.Count(); i += 3, pairIndex++) {
                list1 = CreateCompareList(inputs.ElementAt(i));
                list2 = CreateCompareList(inputs.ElementAt(i + 1));

                int comparison = CompareElements(list1, list2);
                bool listsInCorrectOrder = comparison <= 0;
                if (listsInCorrectOrder) {
                    sumOfCorrectOrderPairIndexes += pairIndex;
                }
            }

            return sumOfCorrectOrderPairIndexes;
        }
        #endregion

        #region Part 2

        private static List<CompareList> Part2CreateAndSortAllLists() {
            List<CompareList> allLists = Part2CreateAllLists();
            allLists.Add(CreateCompareList("[[2]]"));
            allLists.Add(CreateCompareList("[[6]]"));

            Part2SortAllLists(allLists);

            return allLists;
        }
        private static List<CompareList> Part2CreateAllLists() {
            List<CompareList> allLists = new List<CompareList>();
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D13\\D13_Input.txt");
            foreach (string input in inputs) {
                if (string.IsNullOrWhiteSpace(input)) {
                    continue;
                }

                CompareList curList = CreateCompareList(input);
                allLists.Add(curList);
            }

            return allLists;
        }
        private static void Part2SortAllLists(List<CompareList> allLists) {
            for (int l = 0; l < allLists.Count(); l++) {
                int minIndex = l;
                for (int c = l + 1; c < allLists.Count(); c++) {
                    CompareList curList = allLists[c];
                    int comparison = CompareElements(curList, allLists[minIndex]);
                    if (comparison < 0) {
                        minIndex = c;
                    }
                }

                // Swap
                if (l != minIndex) {
                    CompareList tempList = allLists[l];
                    allLists[l] = allLists[minIndex];
                    allLists[minIndex] = tempList;
                }
            }
        }

        private static int Part2CalcProductOfDividerPacketIndexes(List<CompareList> allLists) {
            int dividerPacket1Index = -1, dividerPacket2Index = -1;
            for (int l = 0; l < allLists.Count; l++) {
                CompareList curList = allLists[l];
                if (curList.ToString() == "[[2]]") {
                    dividerPacket1Index = l + 1;
                }
                else if (curList.ToString() == "[[6]]") {
                    dividerPacket2Index = l + 1;
                }
            }
            int productOfDividerPacketIndexes = dividerPacket1Index * dividerPacket2Index;
            return productOfDividerPacketIndexes;
        }
        #endregion

        #region Helpers
        private static int CompareElements(CompareElement e1, CompareElement e2) {
            bool e1IsList = e1.GetType() == typeof(CompareList);
            bool e2IsList = e2.GetType() == typeof(CompareList);

            if (!e1IsList && !e2IsList) { return CompareIntAndInt(e1, e2); }
            else if (!e1IsList && e2IsList) { return CompareIntAndList(e1, e2); }
            else if (e1IsList && !e2IsList) { return CompareListAndInt(e1, e2); }
            else { return CompareListAndList(e1, e2); }
        }
        private static int CompareIntAndInt(CompareElement e1, CompareElement e2) {
            return ((CompareInt)e1).value - ((CompareInt)e2).value;
        }
        private static int CompareIntAndList(CompareElement e1, CompareElement e2) {
            List<CompareElement> list1Value = new List<CompareElement>();
            list1Value.Add(e1);
            CompareList list1 = new CompareList(list1Value);
            return CompareElements(list1, (CompareList)e2);
        }
        private static int CompareListAndInt(CompareElement e1, CompareElement e2) {
            List<CompareElement> list2Value = new List<CompareElement>();
            list2Value.Add(e2);
            CompareList list2 = new CompareList(list2Value);
            return CompareElements((CompareList)e1, list2);
        }
        private static int CompareListAndList(CompareElement e1, CompareElement e2) {
            CompareList l1 = (CompareList)e1, l2 = (CompareList)e2;

            for (int e = 0; e < l1.value.Count() && e < l2.value.Count(); e++) {
                int comparison = CompareElements(l1.value.ElementAt(e), l2.value.ElementAt(e));
                if (comparison != 0) {
                    return comparison;
                }
            }

            if (l1.value.Count() != l2.value.Count()) {
                return l1.value.Count() - l2.value.Count();
            }

            return 0;
        }

        private static CompareList CreateCompareList(string input) {
            List<CompareElement> list = new List<CompareElement>();

            string buffer = "";
            int i = 1;
            while (i < input.Length - 1) {
                bool foundNumber = false;
                while (input[i] >= '0' && input[i] <= '9') {
                    foundNumber = true;

                    buffer += input[i];
                    i++;
                }
                if (foundNumber) {
                    int newIntValue = int.Parse(buffer);
                    CompareInt newInt = new CompareInt(newIntValue);
                    list.Add(newInt);

                    buffer = "";
                }

                bool foundList = false;
                if (input[i] == '[') {
                    foundList = true;

                    buffer += input[i];
                    i++;
                    int numLeftBrackets = 1;
                    int numRightBrackets = 0;
                    while (numLeftBrackets != numRightBrackets) {
                        if (input[i] == '[') {
                            numLeftBrackets++;
                        }
                        else if (input[i] == ']') {
                            numRightBrackets++;
                        }
                        buffer += input[i];
                        i++;
                    }
                }
                if (buffer != "") {
                    CompareList newList = CreateCompareList(buffer);
                    list.Add(newList);

                    buffer = "";
                }

                if (!foundList && !foundNumber) {
                    i++;
                }
            }

            CompareList returnList = new CompareList(list);
            return returnList;
        }
    }
    #endregion
}
