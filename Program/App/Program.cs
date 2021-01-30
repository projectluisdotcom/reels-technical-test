using System;
using System.IO;
using ReelWords.Data;
using ReelWords.Domain;

namespace ReelWords.App
{
    // TODO: Tema de init async
    // TODO: Separar Program.cs en installer
    // TODO: Tema de test de la logica de WordGame
    // TODO: Añadir Docker y GitHubActions CI/CD
    // TODO: Tema de encapsular el input y sacarlo fuera de Program

    class Program
    {
        static void Main(string[] args) => Run();

        private static async void Run()
        {
            var reelsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/reels.txt");
            var scoresPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/scores.txt");
            var wordsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/american-english-large.txt");

            var eventDomainDispatcher = new EventDomainDispatcher();

            eventDomainDispatcher.Add<OnUserStartedGame, ConsoleOnUserStartedGame>(new ConsoleOnUserStartedGame());
            eventDomainDispatcher.Add<OnUserFinishedPlay, ConsoleOnUserFinishedPlay>(new ConsoleOnUserFinishedPlay());
            eventDomainDispatcher.Add<OnUserStartedNextPlay, ConsoleOnUserStartedNextPlay>(new ConsoleOnUserStartedNextPlay());

            var trieFactory = new TrieWorldImplementationFactory();
            var gameRepository = new FileGameDataSource(reelsPath, scoresPath, wordsPath);
            var game = await new DefaultGameFactory(gameRepository, trieFactory, eventDomainDispatcher).Create();
                
            game.Init();

            while (game.IsPlaying)
            {
                var input = Console.ReadLine();
                game.Play(input);
            }
        }
    }
}