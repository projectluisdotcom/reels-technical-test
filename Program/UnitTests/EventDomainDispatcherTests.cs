using System;
using NSubstitute;
using ReelWords.Domain;
using Xunit;

namespace ReelWordsTests
{
    public class EventDomainDispatcherTests
    {
        [Fact]
        public void CallsRegisteredEvent()
        {
            var eventDispatcher = new EventDomainDispatcher();

            var implementationOneEventOne = Substitute.For<ImplementationOneEventOne>();
            eventDispatcher.Add<EventOne, ImplementationOneEventOne>(implementationOneEventOne);

            eventDispatcher.Send(new EventOne());

            implementationOneEventOne.Received(1).Execute(Arg.Any<EventOne>());
        }

        [Fact]
        public void CallsSameInstanceRegistrations()
        {
            var eventDispatcher = new EventDomainDispatcher();

            var instanceOne = Substitute.For<ImplementationOneEventOne>();
            var instanceTwo = Substitute.For<ImplementationOneEventOne>();
            eventDispatcher.Add<EventOne, ImplementationOneEventOne>(instanceOne);
            eventDispatcher.Add<EventOne, ImplementationOneEventOne>(instanceTwo);

            eventDispatcher.Send(new EventOne());

            instanceOne.Received(1).Execute(Arg.Any<EventOne>());
            instanceTwo.Received(1).Execute(Arg.Any<EventOne>());
        }

        [Fact]
        public void CallsDiferentInstanceRegistrations()
        {
            var eventDispatcher = new EventDomainDispatcher();

            var implementationOneEventOne = Substitute.For<ImplementationOneEventOne>();
            var implementationTwoEventOne = Substitute.For<ImplementationTwoEventOne>();
            eventDispatcher.Add<EventOne, ImplementationOneEventOne>(implementationOneEventOne);
            eventDispatcher.Add<EventOne, ImplementationTwoEventOne>(implementationTwoEventOne);

            eventDispatcher.Send(new EventOne());

            implementationOneEventOne.Received(1).Execute(Arg.Any<EventOne>());
            implementationTwoEventOne.Received(1).Execute(Arg.Any<EventOne>());
        }

        [Fact]
        public void CallsDiferentDomainEvents()
        {
            var eventDispatcher = new EventDomainDispatcher();

            var implementationOneEventOne = Substitute.For<ImplementationOneEventOne>();
            var implementationOneEventTwo = Substitute.For<ImplementationOneEventTwo>();
            eventDispatcher.Add<EventOne, ImplementationOneEventOne>(implementationOneEventOne);
            eventDispatcher.Add<EventTwo, ImplementationOneEventTwo>(implementationOneEventTwo);

            eventDispatcher.Send(new EventOne());

            implementationOneEventOne.Received(1).Execute(Arg.Any<EventOne>());
            implementationOneEventTwo.Received(1).Execute(Arg.Any<EventTwo>());
        }

        [Fact]
        public void ThrowsWhenEventsHaveNoRegistrations()
        {
            var eventDispatcher = new EventDomainDispatcher();

            Assert.Throws<Exception>(() => eventDispatcher.Send(new EventOne()));
        }

        public class EventOne : IDomainEvent { }

        public class EventTwo : IDomainEvent { }

        public class ImplementationOneEventOne : IDomainEventHandler<EventOne>
        {
            public void Execute(EventOne @event)
            {
            }
        }
        public class ImplementationTwoEventOne : IDomainEventHandler<EventOne>
        {
            public void Execute(EventOne @event)
            {
            }
        }
        public class ImplementationOneEventTwo : IDomainEventHandler<EventTwo>
        {
            public void Execute(EventTwo @event)
            {
            }
        }
    }
}
