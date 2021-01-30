using System;
using System.Collections.Generic;

namespace ReelWords.Domain
{
    public interface IReels
    {
        public string Get();
        public PlayResult Play(string text);
        public void Suffle(Random random);

        public class PlayResult
        {
            public readonly bool IsCorrect;
            public readonly IEnumerable<string> Matches;

            public PlayResult(bool isCorrect, IEnumerable<string> matches)
            {
                IsCorrect = isCorrect;
                Matches = matches;
            }
        }
    }
}