using System;
using System.Collections.Generic;

namespace AOC.Challenges {
    public class D4 {
        public static void Run() {
            int numRangesThatEncloseTheOther = Part1GetNumRangesThatEncloseTheOther();
            Console.WriteLine("Pt 1 # ranges that enclose the other: " + numRangesThatEncloseTheOther);
            Console.WriteLine();

            numRangesThatEncloseTheOther = Part2GetNumRangesThatOverlap();
            Console.WriteLine("Pt 2 # ranges that overlap: " + numRangesThatEncloseTheOther);
            Console.WriteLine();
        }

        private static int Part1GetNumRangesThatEncloseTheOther() {
            int numRangesThatEncloseTheOther = 0;
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D4\\D4_Input.txt");
            foreach (string input in inputs) {
                string[] minMaxRanges = input.Split('-', ',');
                int min1 = int.Parse(minMaxRanges[0]);
                int max1 = int.Parse(minMaxRanges[1]);
                int min2 = int.Parse(minMaxRanges[2]);
                int max2 = int.Parse(minMaxRanges[3]);

                if ((min1 >= min2 && max1 <= max2) || (min1 <= min2 && max1 >= max2)) {
                    numRangesThatEncloseTheOther++;
                }
            }

            return numRangesThatEncloseTheOther;
        }

        private static int Part2GetNumRangesThatOverlap() {
            int numRangesThatOverlap = 0;
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D4\\D4_Input.txt");
            foreach (string input in inputs) {
                string[] minMaxRanges = input.Split('-', ',');
                int min1 = int.Parse(minMaxRanges[0]);
                int max1 = int.Parse(minMaxRanges[1]);
                int min2 = int.Parse(minMaxRanges[2]);
                int max2 = int.Parse(minMaxRanges[3]);

                if ((min1 >= min2 && min1 <= max2) || (max1 >= min2 && max1 <= max2) ||
                    (min2 >= min1 && min2 <= max1) || (max2 >= min1 && max2 <= max1)) {
                    numRangesThatOverlap++;
                }
            }

            return numRangesThatOverlap;
        }
    }
}
