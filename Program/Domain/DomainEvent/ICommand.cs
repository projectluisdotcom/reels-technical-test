namespace ReelWords.Domain
{
    public interface IDomainEventHandler<T>
        where T : IDomainEvent
    {
        public void Execute(T @event); 
    }
}