using System;
using System.Collections.Generic;

namespace ReelWords.Domain
{
    public class EventDomainDispatcher : IDomainEventDispatcher
    {
        private readonly Dictionary<Type, List<object>> _events;

        public EventDomainDispatcher()
        {
            _events = new Dictionary<Type, List<object>>();
        }

        public void Send<T>(T @event)
            where T : IDomainEvent
        {
            var type = typeof(T);
            if (!_events.TryGetValue(type, out var commands))
            {
                throw new Exception($"No implementations registered for DomainEvent {type.Name}");
            }
            foreach (var command in commands)
            {
                var casted = (IDomainEventCommand<T>)command;
                casted.Execute(@event);
            }
        }

        public void Add<TDomainEvent, TDomainEventCommand>(TDomainEventCommand command)
            where TDomainEvent : IDomainEvent
            where TDomainEventCommand : IDomainEventCommand<TDomainEvent>
        {
            var type = typeof(TDomainEvent);
            if (!_events.ContainsKey(type))
            {
                _events.Add(type, new List<object>());
            }

            _events.TryGetValue(type, out var list);
            list.Add(command);
        }
    }
}