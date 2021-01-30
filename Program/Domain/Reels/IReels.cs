using System;
using System.Collections.Generic;

namespace ReelWords.Domain
{
    public interface IReels
    {
        public string Get();
        public PlayResult Play(string text);
        public void Suffle(Random random);

        public struct PlayResult
        {
            public readonly bool IsCorrect;
            public readonly IReadOnlyList<string> Matches;

            public PlayResult(bool isCorrect, IReadOnlyList<string> matches)
            {
                IsCorrect = isCorrect;
                Matches = matches;
            }
        }
    }
}