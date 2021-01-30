namespace ReelWords.Domain
{
    public interface IDomainEventCommand<T>
        where T : IDomainEvent
    {
        public void Execute(T @event); 
    }
}