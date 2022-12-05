using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Challenges {
    class P5 {
        static List<List<char>> allStacks = new List<List<char>>{
        new List<char>(new [] {'S', 'Z', 'P', 'D', 'L', 'B', 'F', 'C'}),
        new List<char>(new [] {'N', 'V', 'G', 'P', 'H', 'W', 'B'}),
        new List<char>(new [] {'F', 'W', 'B', 'J', 'G'}),
        new List<char>(new [] {'G', 'J', 'N', 'F', 'L', 'W', 'C', 'S'}),
        new List<char>(new [] {'W', 'J', 'L', 'T', 'P', 'M', 'S', 'H'}),
        new List<char>(new [] {'B', 'C', 'W', 'G', 'F', 'S'}),
        new List<char>(new [] {'H', 'T', 'P', 'M', 'Q', 'B', 'W'}),
        new List<char>(new [] {'F', 'S', 'W', 'T'}),
        new List<char>(new [] {'N', 'C', 'R'}),
    };

        public static void Run() {
            DisplayAllStacks();

            IEnumerable<String> inputs = System.IO.File.ReadLines("Challenges\\P5_Input.txt");
            foreach (String input in inputs) {
                string[] moveInfo = input.Split(' ');
                int moveAmt = int.Parse(moveInfo[1]);
                int fromStackIndex = int.Parse(moveInfo[3]) - 1;
                int toStackIndex = int.Parse(moveInfo[5]) - 1;

                List<char> fromStack = allStacks[fromStackIndex];
                List<char> toStack = allStacks[toStackIndex];
                for (int m = 0; m < moveAmt && fromStack.Count > 0; m++) {
                    toStack.Add(fromStack.ElementAt(fromStack.Count - 1));
                    fromStack.RemoveAt(fromStack.Count - 1);
                }
            }

            DisplayAllStacks();
            DisplayTopsOfStacks();
        }

        private static void DisplayAllStacks() {
            foreach (List<char> s in allStacks) {
                Console.Write("[ ");
                foreach (char c in s) {
                    Console.Write(c + " ");
                }
                Console.WriteLine("]");
            }

            Console.WriteLine();
        }

        private static void DisplayTopsOfStacks() {
            foreach (List<char> s in allStacks) {
                if (s.Count > 0) {
                    Console.Write(s.ElementAt(s.Count - 1));
                }
            }

            Console.WriteLine();
        }
    }
}