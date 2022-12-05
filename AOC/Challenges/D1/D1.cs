using System;
using System.Collections.Generic;

namespace AOC.Challenges {
    class D1 {
        public static void Run() {
            Tuple<int, int> maxElfData = Part1CalculateTopElf();
            Console.WriteLine("Max Elf Index: " + maxElfData.Item1);
            Console.WriteLine("Max Elf Calories: " + maxElfData.Item2);
            Console.WriteLine();

            Tuple<int, int>[] maxElfsData = Part2CalculateTopThreeElfs();
            Console.WriteLine("Max Elf Indexes: " + maxElfsData[0].Item1 + ", " + maxElfsData[1].Item1 + ", " + maxElfsData[2].Item1);
            Console.WriteLine("Max Elf Calories: " + maxElfsData[0].Item2 + ", " + maxElfsData[1].Item2 + ", " + maxElfsData[2].Item2);
            int combinedMaxElfCalories = maxElfsData[0].Item2 + maxElfsData[1].Item2 + maxElfsData[2].Item2;
            Console.WriteLine("Combined Max Elf Calories: " + combinedMaxElfCalories);
            Console.WriteLine();
        }

        private static Tuple<int, int> Part1CalculateTopElf() {
            int maxElfIndex = -1, maxElfCalories = -1;
            int curElfIndex = 1, curElfCalories = 0;
            IEnumerable<String> lines = System.IO.File.ReadLines("Challenges\\D1\\D1_Input.txt");
            foreach (String line in lines) {
                if (line.Length > 0) { // If line contains a calorie count
                    int lineCalories = int.Parse(line);
                    curElfCalories += lineCalories;
                }
                else { // If line is blank
                    // Updating the max elf if the current one has more calories
                    if (curElfCalories > maxElfCalories) {
                        maxElfIndex = curElfIndex;
                        maxElfCalories = curElfCalories;
                    }

                    curElfIndex++;
                    curElfCalories = 0;
                }
            }

            return Tuple.Create(maxElfIndex, maxElfCalories);
        }

        private static Tuple<int, int>[] Part2CalculateTopThreeElfs() {
            int[] maxElfIndexes = { -1, -1, -1 }, maxElfCalories = { -1, -1, -1 };
            int curElfIndex = 1, curElfCalories = 0;
            IEnumerable<String> lines = System.IO.File.ReadLines("Challenges\\D1\\D1_Input.txt");
            foreach (String line in lines) {
                if (line.Length > 0) { // If line contains a calorie count
                    int lineCalories = int.Parse(line);
                    curElfCalories += lineCalories;
                }
                else { // If line is blank
                    // Finding smallest elf of the 3 max elfs
                    int smallestMaxElfIndex = -1, smallestMaxElfCalories = int.MaxValue;
                    for (int m = 0; m < maxElfIndexes.Length; m++) {
                        if (maxElfCalories[m] < smallestMaxElfCalories) {
                            smallestMaxElfCalories = maxElfCalories[m];
                            smallestMaxElfIndex = m;
                        }
                    }

                    // Updating the smallest max elf if the current max health has more calories
                    if (curElfCalories > smallestMaxElfCalories) {
                        maxElfIndexes[smallestMaxElfIndex] = curElfIndex;
                        maxElfCalories[smallestMaxElfIndex] = curElfCalories;
                    }

                    curElfIndex++;
                    curElfCalories = 0;
                }
            }

            return new Tuple<int, int>[] {
                Tuple.Create(maxElfIndexes[0], maxElfCalories[0]),
                Tuple.Create(maxElfIndexes[1], maxElfCalories[1]),
                Tuple.Create(maxElfIndexes[2], maxElfCalories[2])
            };
        }
    }
}
