using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Challenges {
    public class D3 {
        public static void Run() {
            int prioritiesSum = Part1CalcPrioritiesSum();
            Console.WriteLine("Pt 1 Priorities Sum: " + prioritiesSum);
            Console.WriteLine();

            prioritiesSum = Part2CalcPrioritiesSum();
            Console.WriteLine("Pt 2 Priorities Sum: " + prioritiesSum);
            Console.WriteLine();
        }

        private static int Part1CalcPrioritiesSum() {
            int prioritiesSum = 0;
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D3\\D3_Input.txt");
            foreach (string input in inputs) {
                int length = input.Length;
                string sack1 = input.Substring(0, length / 2);
                string sack2 = input.Substring(length / 2);

                char sharedChar = ' ';
                foreach (char c in sack1) {
                    if (sack2.Contains(c)) {
                        sharedChar = c;
                        break;
                    }
                }

                int priorityValue;
                if (sharedChar >= 'a' && sharedChar <= 'z') {
                    priorityValue = sharedChar - 'a' + 1;
                }
                else {
                    priorityValue = sharedChar - 'A' + 27;
                }
                prioritiesSum += priorityValue;
            }

            return prioritiesSum;
        }

        private static int Part2CalcPrioritiesSum() {
            int prioritiesSum = 0;
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D3\\D3_Input.txt");
            for (int s = 0; s < inputs.Count() - 2; s += 3) {
                string sack1 = inputs.ElementAt(s);
                string sack2 = inputs.ElementAt(s + 1);
                string sack3 = inputs.ElementAt(s + 2);

                char sharedChar = ' ';
                foreach (char c in sack1) {
                    if (sack2.Contains(c) && sack3.Contains(c)) {
                        sharedChar = c;
                        break;
                    }
                }

                int priorityValue;
                if (sharedChar >= 'a' && sharedChar <= 'z') {
                    priorityValue = sharedChar - 'a' + 1;
                }
                else {
                    priorityValue = sharedChar - 'A' + 27;
                }
                prioritiesSum += priorityValue;
            }

            return prioritiesSum;
        }
    }
}
