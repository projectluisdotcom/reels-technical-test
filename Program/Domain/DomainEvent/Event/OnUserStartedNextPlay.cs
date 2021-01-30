namespace ReelWords.Domain
{
    public class OnUserStartedNextPlay : IDomainEvent
    {
        public readonly string Letters;

        public OnUserStartedNextPlay(string letters)
        {
            Letters = letters;
        }
    }

}

