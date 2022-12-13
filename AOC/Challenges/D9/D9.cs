using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Challenges {
    public class D9 {
        public static int columns, rows;
        public static bool[,] visitedPositions;

        public class GridPosition {
            public int x, y;
            public GridPosition(int setX, int setY) {
                x = setX;
                y = setY;
            }

            public bool Touching(GridPosition other) {
                if (Math.Abs(x - other.x) > 1 || Math.Abs(y - other.y) > 1) {
                    return false;
                }

                return true;
            }

            public void CatchupTo(GridPosition to) {
                if (Touching(to)) { // Don't move if touching to
                    return;
                }

                if (x != to.x) { // Not on the same column
                    if (to.x > x) {
                        x++;
                    }
                    else if (to.x < x) {
                        x--;
                    }
                }
                if (y != to.y) { // Not on the same row
                    if (to.y > y) {
                        y++;
                    }
                    else if (to.y < y) {
                        y--;
                    }
                }
            }

            public void SetPosition(int setX, int setY) {
                x = setX;
                y = setY;
            }

            public GridPosition Clone() {
                return new GridPosition(x, y);
            }

            public override string ToString() { return "(" + x + ", " + y + ")"; }
        }
        public static GridPosition startPosition;
        public static GridPosition headPosition;
        public static List<GridPosition> tailPositions;




        public static void Run() {
            Part1GridSetup();
            Part1ProcessMovements();
            int pt1NumPositionsVisited = Part1GetVisitedPositions();
            Console.WriteLine("Pt 1 Positions Visited: " + pt1NumPositionsVisited);
            Console.WriteLine();

            int tailLength = 9;
            Part2GridSetup(tailLength);
            Part2ProcessMovements();
            int pt2NumPositionsVisited = Part2GetVisitedPositions();
            Console.WriteLine("Pt 2 Positions Visited: " + pt2NumPositionsVisited);
            Console.WriteLine();
        }

        #region Part 1
        private static void Part1GridSetup() {
            columns = 800;
            rows = 800;
            visitedPositions = new bool[columns, rows];

            // Starting position is bottom left corner
            startPosition = new GridPosition(columns / 2, rows / 2);
            headPosition = startPosition.Clone();
            tailPositions = new List<GridPosition>();
            tailPositions.Add(startPosition.Clone());
            visitedPositions[tailPositions[0].x, tailPositions[0].y] = true;
        }

        private static void Part1ProcessMovements() {
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D9\\D9_Input.txt");
            foreach (string input in inputs) {
                string[] inputTokens = input.Split(' ');
                char moveDirection = inputTokens[0].ElementAt(0);
                int moveAmount = int.Parse(inputTokens[1]);

                for (int m = 0; m < moveAmount; m++) {
                    switch (moveDirection) {
                        case 'U': headPosition.y--; break;
                        case 'R': headPosition.x++; break;
                        case 'D': headPosition.y++; break;
                        case 'L': headPosition.x--; break;
                    }

                    tailPositions[0].CatchupTo(headPosition);
                    visitedPositions[tailPositions[0].x, tailPositions[0].y] = true;
                }
            }
        }

        private static int Part1GetVisitedPositions() {
            int numVisited = 0;
            for (int c = 0; c < columns; c++) {
                for (int r = 0; r < rows; r++) {
                    if (visitedPositions[c, r]) {
                        numVisited++;
                    }
                }
            }

            return numVisited;
        }
        #endregion

        #region Part 2
        private static void Part2GridSetup(int tailLength) {
            columns = 800;
            rows = 800;
            visitedPositions = new bool[columns, rows];

            // Starting position is bottom left corner
            startPosition = new GridPosition(columns / 2, rows / 2);
            headPosition = startPosition.Clone();
            tailPositions = new List<GridPosition>();
            for (int t = 0; t < tailLength; t++) {
                tailPositions.Add(startPosition.Clone());
            }
            visitedPositions[tailPositions[0].x, tailPositions[0].y] = true;
        }

        private static void Part2ProcessMovements() {
            IEnumerable<string> inputs = System.IO.File.ReadLines("Challenges\\D9\\D9_Input.txt");
            foreach (string input in inputs) {
                string[] inputTokens = input.Split(' ');
                char moveDirection = inputTokens[0].ElementAt(0);
                int moveAmount = int.Parse(inputTokens[1]);

                for (int m = 0; m < moveAmount; m++) {
                    switch (moveDirection) {
                        case 'U': headPosition.y--; break;
                        case 'R': headPosition.x++; break;
                        case 'D': headPosition.y++; break;
                        case 'L': headPosition.x--; break;
                    }

                    for (int t = 0; t < tailPositions.Count; t++) {
                        GridPosition gotoPosition = (t == 0) ? headPosition : tailPositions[t - 1];
                        tailPositions[t].CatchupTo(gotoPosition);
                    }

                    visitedPositions[tailPositions[tailPositions.Count - 1].x, tailPositions[tailPositions.Count - 1].y] = true;
                }
            }
        }

        private static int Part2GetVisitedPositions() {
            int numVisited = 0;
            for (int c = 0; c < columns; c++) {
                for (int r = 0; r < rows; r++) {
                    if (visitedPositions[c, r]) {
                        numVisited++;
                    }
                }
            }

            return numVisited;
        }
        #endregion
    }
}
