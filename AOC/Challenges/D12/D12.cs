using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Challenges {
    public class D12 {
        static int[,] heightMap;
        static int numRows, numCols;

        static int startRow, startColumn;
        static int goalRow, goalColumn;

        public static void Run() {
            GenerateHeightMapFromInput();

            int distanceFromStartToGoal = DijkstraCalcShortestPaths(startColumn, startRow);
            Console.WriteLine("Pt 1 Distance from start to goal: " + distanceFromStartToGoal);
            Console.WriteLine();

            int minDistanceFromLowestPointToGoal = Pt2CalcMinDistanceFromLowestPointToGoal();
            Console.WriteLine("Pt 2 Distance from any \'a\' to goal: " + minDistanceFromLowestPointToGoal);
            Console.WriteLine();
        }

        private static int DijkstraCalcShortestPaths(int startC, int startR) {
            int[,] distances = new int[numCols, numRows];
            bool[,] visited = new bool[numCols, numRows];
            for (int c = 0; c < numCols; c++) {
                for (int r = 0; r < numRows; r++) {
                    distances[c, r] = int.MaxValue;
                    visited[c, r] = false;
                }
            }
            distances[startC, startR] = 0;

            int curC = startC, curR = startR;
            while (true) {
                if (curC == goalColumn && curR == goalRow) { // Base case
                    int shortestPathToGoal = distances[goalColumn, goalRow];
                    return shortestPathToGoal;
                }

                visited[curC, curR] = true;

                int curHeight = heightMap[curC, curR];
                int curDistance = distances[curC, curR];
                int nextDistance = curDistance + 1;

                bool validAdjacentLeftNode = (curC > 0) && !visited[curC - 1, curR] && (heightMap[curC - 1, curR] <= curHeight + 1);
                if (validAdjacentLeftNode) {
                    if (nextDistance < distances[curC - 1, curR]) {
                        distances[curC - 1, curR] = nextDistance;
                    }
                }
                bool validAdjacentTopNode = (curR > 0) && !visited[curC, curR - 1] && (heightMap[curC, curR - 1] <= curHeight + 1);
                if (validAdjacentTopNode) {
                    if (nextDistance < distances[curC, curR - 1]) {
                        distances[curC, curR - 1] = nextDistance;
                    }
                }
                bool validAdjacentRightNode = (curC < numCols - 1) && !visited[curC + 1, curR] && (heightMap[curC + 1, curR] <= curHeight + 1);
                if (validAdjacentRightNode) {
                    if (nextDistance < distances[curC + 1, curR]) {
                        distances[curC + 1, curR] = nextDistance;
                    }
                }
                bool validAdjacentBottomNode = (curR < numRows - 1) && !visited[curC, curR + 1] && (heightMap[curC, curR + 1] <= curHeight + 1);
                if (validAdjacentBottomNode) {
                    if (nextDistance < distances[curC, curR + 1]) {
                        distances[curC, curR + 1] = nextDistance;
                    }
                }

                int minDistanceNotVisited = int.MaxValue;
                int minNodeNotVisitedC = -1, minNodeNotVisitedR = -1;
                for (int c = 0; c < numCols; c++) {
                    for (int r = 0; r < numRows; r++) {
                        if (!visited[c, r] && distances[c, r] < minDistanceNotVisited) {
                            minDistanceNotVisited = distances[c, r];
                            minNodeNotVisitedC = c;
                            minNodeNotVisitedR = r;
                        }
                    }
                }
                bool foundNextValidNode = minNodeNotVisitedC != -1;
                if (!foundNextValidNode) {
                    break;
                }

                curC = minNodeNotVisitedC;
                curR = minNodeNotVisitedR;
            }

            return -1;
        }

        private static int Pt2CalcMinDistanceFromLowestPointToGoal() {
            int minDistanceFromLowestPointToGoal = int.MaxValue;
            for (int c = 0; c < numCols; c++) {
                for (int r = 0; r < numRows; r++) {
                    if (heightMap[c, r] == 0) {
                        int distanceToGoal = DijkstraCalcShortestPaths(c, r);
                        if (distanceToGoal != -1 && distanceToGoal < minDistanceFromLowestPointToGoal) {
                            minDistanceFromLowestPointToGoal = distanceToGoal;
                        }
                    }
                }
            }

            return minDistanceFromLowestPointToGoal;
        }

        #region Helpers
        private static void GenerateHeightMapFromInput() {
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D12\\D12_Input.txt");
            heightMap = new int[numCols, numRows];
            numRows = inputs.Count();
            numCols = inputs.ElementAt(0).Length;
            for (int r = 0; r < numRows; r++) {
                for (int c = 0; c < numCols; c++) {
                    char curChar = inputs.ElementAt(r)[c];
                    if (curChar == 'S') {
                        curChar = 'a';

                        startRow = r;
                        startColumn = c;
                    }
                    else if (curChar == 'E') {
                        curChar = 'z';

                        goalRow = r;
                        goalColumn = c;
                    }

                    int curHeight = curChar - 'a';
                    heightMap[c, r] = curHeight;
                }
            }
        }
        #endregion
    }
}
