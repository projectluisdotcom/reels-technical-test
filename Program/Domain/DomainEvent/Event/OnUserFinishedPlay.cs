using System;
using System.Collections.Generic;

namespace ReelWords.Domain
{
    public class OnUserFinishedPlay : IDomainEvent
    {
        public readonly IEnumerable<string> Matches;
        public readonly double TotalScore;
        public readonly double LastPlayScore;
        public readonly bool IsCorrect;

        public OnUserFinishedPlay(IEnumerable<string> matches, double totalScore, double lastPlayScore, bool isCorrect)
        {
            Matches = matches;
            TotalScore = totalScore;
            LastPlayScore = lastPlayScore;
            IsCorrect = isCorrect;
        }

        public override bool Equals(object obj)
        {
            return obj is OnUserFinishedPlay play &&
                   EqualityComparer<IEnumerable<string>>.Default.Equals(Matches, play.Matches) &&
                   TotalScore == play.TotalScore &&
                   LastPlayScore == play.LastPlayScore &&
                   IsCorrect == play.IsCorrect;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Matches, TotalScore, LastPlayScore, IsCorrect);
        }
    }
}
