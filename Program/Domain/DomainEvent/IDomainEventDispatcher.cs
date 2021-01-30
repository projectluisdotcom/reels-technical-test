namespace ReelWords.Domain
{
    public interface IDomainEventDispatcher
    {
        public void Send<T>(T @event) where T : IDomainEvent;
    }
}