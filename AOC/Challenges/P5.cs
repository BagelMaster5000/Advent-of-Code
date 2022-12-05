using System;
using System.Collections.Generic;

namespace AOC.Challenges {
    class P5 {
        List<Stack<char>> AllStacks = new List<Stack<char>>{
        new Stack<char>(new [] {'S', 'Z', 'P', 'D', 'L', 'B', 'F', 'C'}),
        new Stack<char>(new [] {'N', 'V', 'G', 'P', 'H', 'W', 'B'}),
        new Stack<char>(new [] {'F', 'W', 'B', 'J', 'G'}),
        new Stack<char>(new [] {'G', 'J', 'N', 'F', 'L', 'W', 'C', 'S'}),
        new Stack<char>(new [] {'W', 'J', 'L', 'T', 'P', 'M', 'S', 'H'}),
        new Stack<char>(new [] {'B', 'C', 'W', 'G', 'F', 'S'}),
        new Stack<char>(new [] {'H', 'T', 'P', 'M', 'Q', 'B', 'W'}),
        new Stack<char>(new [] {'F', 'S', 'W', 'T'}),
        new Stack<char>(new [] {'N', 'C', 'R'}),
    };

        public static void Run() {
            Console.WriteLine("this is P5");
        }
    }
}