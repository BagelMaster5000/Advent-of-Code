using System;
using System.Collections.Generic;

namespace AOC.Challenges {
    public class D6 {
        public static void Run() {
            int charactersProcessed = Part1Process4Characters();
            Console.WriteLine(charactersProcessed);
            Console.WriteLine();

            charactersProcessed = Part2Process14Characters();
            Console.WriteLine(charactersProcessed);
            Console.WriteLine();
        }

        private static int Part1Process4Characters() {
            string text = System.IO.File.ReadAllText("Challenges\\D6\\D6_Input.txt");

            int charactersProcessed = -1;
            for (int c = 0; c < text.Length - 3; c++) {
                char c1 = text[c];
                char c2 = text[c + 1];
                char c3 = text[c + 2];
                char c4 = text[c + 3];

                if (c1 != c2 && c1 != c3 && c1 != c4 && c2 != c3 && c2 != c4 && c3 != c4) {
                    charactersProcessed = c + 4;
                    break;
                }
            }

            return charactersProcessed;
        }

        private static int Part2Process14Characters() {
            string text = System.IO.File.ReadAllText("Challenges\\D6\\D6_Input.txt");

            int charactersProcessed = -1;
            for (int c = 0; c < text.Length - 13; c++) {
                List<char> chars = new List<char>();
                for (int n = 0; n < 14; n++) {
                    char curChar = text[c + n];
                    if (chars.Contains(curChar)) {
                        break;
                    }

                    chars.Add(curChar);
                }

                if (chars.Count == 14) {
                    charactersProcessed = c + 14;
                    break;
                }
            }

            return charactersProcessed;
        }
    }
}
