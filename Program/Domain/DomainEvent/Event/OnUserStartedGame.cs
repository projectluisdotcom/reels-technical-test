namespace ReelWords.Domain
{
    public class OnUserStartedGame : IDomainEvent
    {
        public OnUserStartedGame()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is OnUserStartedGame play;
        }
    }
}
