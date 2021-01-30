using System;

namespace ReelWords.Domain
{
    public class OnUserStartedNextPlay : IDomainEvent
    {
        public readonly string Letters;

        public OnUserStartedNextPlay(string letters)
        {
            Letters = letters;
        }

        public override bool Equals(object obj)
        {
            return obj is OnUserStartedNextPlay play &&
                   Letters == play.Letters;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Letters);
        }
    }
}

