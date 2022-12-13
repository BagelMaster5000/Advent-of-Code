using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Challenges {
    public class D10 {
        public class Process {
            public int addAmount, completionTime;
            public Process(int addAmount, int completionTime) {
                this.addAmount = addAmount;
                this.completionTime = completionTime;
            }
        }

        public static void Run() {
            int signalStrengthTotal = Pt1CalcTotalSignalStrength();
            Console.WriteLine("Pt 1 Total Signal Strength: " + signalStrengthTotal);
            Console.WriteLine();

            bool[,] crtScreen = Pt2CalcDrawnCRT();
            Console.WriteLine("Pt 2 CRT:");
            for (int r = 0; r < 6; r++) {
                for (int c = 0; c < 40; c++) {
                    if (crtScreen[r, c]) {
                        Console.Write("#");
                    }
                    else {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        #region Part 1
        private static int Pt1CalcTotalSignalStrength() {
            int X = 1, signalStrengthTotal = 0;
            int cycleNum = 1;
            Process curProcess = null;

            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D10\\D10_Input.txt");
            int i = 0;

            while (i < inputs.Count() || curProcess != null) {
                if (curProcess == null && i < inputs.Count()) { // Don't accept another process if one is already processing
                    string[] inputTokens = inputs.ElementAt(i).Split(' ');
                    if (inputTokens[0] == "addx") {
                        int addAmt = int.Parse(inputTokens[1]);
                        curProcess = new Process(addAmt, cycleNum + 2);
                    }

                    i++;
                }

                if ((cycleNum - 20) % 40 == 0) {
                    int curSignalStrength = X * cycleNum;
                    signalStrengthTotal += curSignalStrength;
                }

                cycleNum++;

                if (curProcess != null && curProcess.completionTime == cycleNum) {
                    X += curProcess.addAmount;
                    curProcess = null;
                }
            }

            return signalStrengthTotal;
        }
        #endregion

        #region Part 2
        private static bool[,] Pt2CalcDrawnCRT() {
            bool[,] crtScreen = new bool[6, 40];

            int X = 1;
            int cycleNum = 0;
            Process curProcess = null;

            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D10\\D10_Input.txt");
            int i = 0;

            while (i < inputs.Count() || curProcess != null) {
                if (curProcess == null && i < inputs.Count()) { // Don't accept another process if one is already processing
                    string[] inputTokens = inputs.ElementAt(i).Split(' ');
                    if (inputTokens[0] == "addx") {
                        int addAmt = int.Parse(inputTokens[1]);
                        curProcess = new Process(addAmt, cycleNum + 2);
                    }

                    i++;
                }

                if (Math.Abs((cycleNum % 40) - X) <= 1) {
                    crtScreen[cycleNum / 40, cycleNum % 40] = true;
                }

                cycleNum++;

                if (curProcess != null && curProcess.completionTime == cycleNum) {
                    X += curProcess.addAmount;
                    curProcess = null;
                }
            }

            return crtScreen;
        }
        #endregion
    }
}
