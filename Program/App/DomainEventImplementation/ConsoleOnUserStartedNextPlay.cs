using System;
using ReelWords.Domain;

namespace ReelWords.App
{
    public class ConsoleOnUserStartedNextPlay : IDomainEventHandler<OnUserStartedNextPlay>
    {
        public void Execute(OnUserStartedNextPlay @event)
        {
            Console.WriteLine("Showing letters to play with");
            var text = string.Join('-', @event.Letters.ToCharArray());
            Console.WriteLine($"-> {text} <-");
            Console.WriteLine("Introduce your guess, and press enter:");
        }
    }
}
