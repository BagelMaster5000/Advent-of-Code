using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Challenges {
    public class D2 {
        public static void Run() {
            int score = Part1GetScore();
            Console.WriteLine("Pt 1 Score: " + score);
            Console.WriteLine();

            score = Part2GetScore();
            Console.WriteLine("Pt 2 Score: " + score);
            Console.WriteLine();
        }

        private static int Part1GetScore() {
            int score = 0;
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D2\\D2_Input.txt");
            foreach (string input in inputs) {
                char opponentAction = input.ElementAt(0);
                char selfAction = input.ElementAt(2);

                switch (selfAction) {
                    case 'X': score += 1; break;
                    case 'Y': score += 2; break;
                    case 'Z': score += 3; break;
                }

                bool roundWon = (opponentAction == 'A' && selfAction == 'Y') ||
                    (opponentAction == 'B' && selfAction == 'Z') ||
                    (opponentAction == 'C' && selfAction == 'X');
                if (roundWon) {
                    score += 6;
                }
                else {
                    bool roundTied = (opponentAction == 'A' && selfAction == 'X') ||
                        (opponentAction == 'B' && selfAction == 'Y') ||
                        (opponentAction == 'C' && selfAction == 'Z');
                    if (roundTied) {
                        score += 3;
                    }
                }
            }

            return score;
        }

        private static int Part2GetScore() {
            int score = 0;
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D2\\D2_Input.txt");
            foreach (string input in inputs) {
                char opponentAction = input.ElementAt(0);
                char roundResult = input.ElementAt(2);

                char selfAction = ' ';
                switch (roundResult) {
                    case 'X':
                        switch (opponentAction) {
                            case 'A': selfAction = 'Z'; break;
                            case 'B': selfAction = 'X'; break;
                            case 'C': selfAction = 'Y'; break;
                        }
                        break;
                    case 'Y':
                        switch (opponentAction) {
                            case 'A': selfAction = 'X'; break;
                            case 'B': selfAction = 'Y'; break;
                            case 'C': selfAction = 'Z'; break;
                        }
                        break;
                    case 'Z':
                        switch (opponentAction) {
                            case 'A': selfAction = 'Y'; break;
                            case 'B': selfAction = 'Z'; break;
                            case 'C': selfAction = 'X'; break;
                        }
                        break;
                }


                switch (selfAction) {
                    case 'X': score += 1; break;
                    case 'Y': score += 2; break;
                    case 'Z': score += 3; break;
                }

                switch (roundResult) {
                    case 'X': break;
                    case 'Y': score += 3; break;
                    case 'Z': score += 6; break;
                }
            }

            return score;
        }
    }
}
