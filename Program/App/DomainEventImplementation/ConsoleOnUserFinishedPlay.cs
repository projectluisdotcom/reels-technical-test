using System;
using ReelWords.Domain;

namespace ReelWords.App
{
    public class ConsoleOnUserFinishedPlay : IDomainEventHandler<OnUserFinishedPlay>
    {
        public void Execute(OnUserFinishedPlay @event)
        {
            Console.WriteLine("\n");
            if (@event.IsCorrect)
            {
                Console.WriteLine($"Correct input!");
            } else
            {
                Console.WriteLine($"Incorrect input!");
            }

            foreach (var match in @event.Matches)
            {
                Console.WriteLine($"You matched -> {match}");
            }

            Console.WriteLine($"Your last play score is {@event.LastPlayScore}");
            Console.WriteLine($"Your total score is {@event.TotalScore}");
        }
    }
}

