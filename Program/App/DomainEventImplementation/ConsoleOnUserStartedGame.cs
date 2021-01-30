using System;
using ReelWords.Domain;

namespace ReelWords.App
{
    public class ConsoleOnUserStartedGame : IDomainEventCommand<OnUserStartedGame>
    {
        public void Execute(OnUserStartedGame @event)
        {
            Console.WriteLine("Welcome to GAME_NAME!");
            Console.WriteLine("\n");
        }
    }
}
