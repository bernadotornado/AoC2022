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
        const char answerDraw = 'Y';
        const char answerLose = 'X';
        const char answerWin = 'Z';
        const int winReward = 6;
        const int drawReward = 3;
        const int loseReward = 0;
        const int rockReward = 1;
        const int paperReward = 2;
        const int scissorsReward = 3;
        static int[] score = new int[2] { 0, 0 };

        static int GetScore(char answer, int onRock, int onPaper, int onScissors)
         => answer switch
            {
                answerRock => onRock + rockReward,
                answerPaper => onPaper + paperReward,
                answerScissors => onScissors + scissorsReward
            };
        static int GetMatchingAnswer(char prompt, int answer) => prompt switch
        {
            promptRock => answer switch
            {
                answerWin => paperReward + winReward,
                answerLose => scissorsReward + loseReward,
                answerDraw => rockReward + drawReward,
            },
            promptPaper => answer switch
            {
                answerWin => scissorsReward + winReward,
                answerLose => rockReward + loseReward,
                answerDraw => paperReward + drawReward,
            },
            promptScissors => answer switch
            {
                answerWin => rockReward + winReward,
                answerLose => paperReward + loseReward,
                answerDraw => scissorsReward + drawReward,
            }
        };
        
        
        static void Main(string[] args)
        {
            var lines = Common.ParseFile(@"input.txt");
            foreach (var item in lines)
            {
                char prompt = item.ToCharArray()[0];
                char answer = item.ToCharArray()[2];
                score[0] += prompt switch
                {
                    promptRock => GetScore(answer, drawReward, winReward, loseReward),
                    promptPaper => GetScore(answer, loseReward, drawReward, winReward),
                    promptScissors => GetScore(answer, winReward, loseReward, drawReward)
                };
                score[1] += GetMatchingAnswer(prompt, answer);
            }
            Console.WriteLine($"score: {score[0]}\n" +
                $"score: {score[1]}\n");
        }
    }
}
