using System;
using AoC2022;

namespace Day2
{
    class Program
    {
        const char promptRock = 'A';
        const char promptPaper = 'B';
        const char promptScissors = 'C';
        const char answerRock = 'X';
        const char answerPaper = 'Y';
        const char answerScissors = 'Z';
        static int score = 0;
        const int winReward = 6;
        const int drawReward = 3;
        const int lossReward = 0;
        const int rockReward = 1;
        const int paperReward = 2;
        const int scissorsReward = 3;

        static int GetScore(char answer, int onRock, int onPaper, int onScissors)
         => answer switch
            {
                answerRock => onRock + rockReward,
                answerPaper => onPaper + paperReward,
                answerScissors => onScissors + scissorsReward
            };
        
        static void Main(string[] args)
        {
            var lines = Common.ParseFile(@"input.txt");
            foreach (var item in lines)
            {
                char prompt = item.ToCharArray()[0];
                char answer = item.ToCharArray()[2];
                score += prompt switch
                {
                    promptRock => GetScore(answer, drawReward, winReward, lossReward),
                    promptPaper => GetScore(answer, lossReward, drawReward, winReward),
                    promptScissors => GetScore(answer, winReward, lossReward, drawReward)
                };
            }
            Console.WriteLine($"score: {score}");
        }
    }
}
