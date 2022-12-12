using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Challenges {
    public class D8 {
        private static int columns, rows;
        private static int[,] trees;

        public static void Run() {
            GenerateTreesGrid();

            int numVisibleTrees = Part1GetNumVisibleTreesInGrid();
            Console.WriteLine("Pt 1 # visible trees: " + numVisibleTrees);
            Console.WriteLine();

            int highestScenicScore = Part2GetHighestScenicScore();
            Console.WriteLine("Pt 2 highest scenic score: " + highestScenicScore);
            Console.WriteLine();
        }

        private static int Part1GetNumVisibleTreesInGrid() {
            int numVisibleTrees = 0;
            for (int y = 0; y < rows; y++) {
                for (int x = 0; x < columns; x++) {
                    bool visible = false;

                    if (IsTreeVisibleFromLeftEdge(y, x)) {
                        visible = true;
                    }
                    else if (IsTreeVisibleFromTopEdge(y, x)) {
                        visible = true;
                    }
                    else if (IsTreeVisibleFromRightEdge(y, x)) {
                        visible = true;
                    }
                    else if (IsTreeVisibleFromBottomEdge(y, x)) {
                        visible = true;
                    }

                    if (visible) {
                        numVisibleTrees++;
                    }
                }
            }

            return numVisibleTrees;
        }

        #region Pt. 1 Helpers
        private static bool IsTreeVisibleFromLeftEdge(int y, int x) {
            int curTreeHeight = trees[y, x];

            for (int l = 0; l < x; l++) {
                int leftTreeHeight = trees[y, l];
                if (leftTreeHeight >= curTreeHeight) {
                    return false;
                }
            }
            return true;
        }
        private static bool IsTreeVisibleFromTopEdge(int y, int x) {
            int curTreeHeight = trees[y, x];

            for (int t = 0; t < y; t++) {
                int topTreeHeight = trees[t, x];
                if (topTreeHeight >= curTreeHeight) {
                    return false;
                }
            }
            return true;
        }
        private static bool IsTreeVisibleFromRightEdge(int y, int x) {
            int curTreeHeight = trees[y, x];

            for (int r = columns - 1; r > x; r--) {
                int rightTreeHeight = trees[y, r];
                if (rightTreeHeight >= curTreeHeight) {
                    return false;
                }
            }
            return true;
        }
        private static bool IsTreeVisibleFromBottomEdge(int y, int x) {
            int curTreeHeight = trees[y, x];

            for (int b = rows - 1; b > y; b--) {
                int bottomTreeHeight = trees[b, x];
                if (bottomTreeHeight >= curTreeHeight) {
                    return false;
                }
            }
            return true;
        }
        #endregion

        private static int Part2GetHighestScenicScore() {
            int highestScenicScore = -1;
            for (int y = 0; y < rows; y++) {
                for (int x = 0; x < columns; x++) {
                    int curScenicScore = 1;
                    curScenicScore *= GetViewDistanceLeft(y, x);
                    curScenicScore *= GetViewDistanceTop(y, x);
                    curScenicScore *= GetViewDistanceRight(y, x);
                    curScenicScore *= GetViewDistanceBottom(y, x);

                    if (curScenicScore > highestScenicScore) {
                        highestScenicScore = curScenicScore;
                    }
                }
            }

            return highestScenicScore;
        }

        #region Pt. 2 Helpers
        private static int GetViewDistanceLeft(int y, int x) {
            int curTreeHeight = trees[y, x];

            int viewDistance = 0;
            for (int l = x - 1; l >= 0; l--) {
                viewDistance++;

                int leftTreeHeight = trees[y, l];
                if (leftTreeHeight >= curTreeHeight) {
                    break;
                }
            }

            return viewDistance;
        }
        private static int GetViewDistanceTop(int y, int x) {
            int curTreeHeight = trees[y, x];

            int viewDistance = 0;
            for (int t = y - 1; t >= 0; t--) {
                viewDistance++;

                int topTreeHeight = trees[t, x];
                if (topTreeHeight >= curTreeHeight) {
                    break;
                }
            }

            return viewDistance;
        }
        private static int GetViewDistanceRight(int y, int x) {
            int curTreeHeight = trees[y, x];

            int viewDistance = 0;
            for (int r = x + 1; r < columns; r++) {
                viewDistance++;

                int rightTreeHeight = trees[y, r];
                if (rightTreeHeight >= curTreeHeight) {
                    break;
                }
            }

            return viewDistance;
        }
        private static int GetViewDistanceBottom(int y, int x) {
            int curTreeHeight = trees[y, x];

            int viewDistance = 0;
            for (int b = y + 1; b < rows; b++) {
                viewDistance++;

                int bottomTreeHeight = trees[b, x];
                if (bottomTreeHeight >= curTreeHeight) {
                    break;
                }
            }

            return viewDistance;
        }
        #endregion

        private static void GenerateTreesGrid() {
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D8\\D8_Input.txt");
            rows = inputs.Count();
            columns = inputs.ElementAt(0).Length;

            trees = new int[rows, columns];
            for (int y = 0; y < rows; y++) {
                for (int x = 0; x < columns; x++) {
                    char digit = inputs.ElementAt(y).ElementAt(x);
                    trees[y, x] = digit - '0';
                }
            }
        }
    }
}
