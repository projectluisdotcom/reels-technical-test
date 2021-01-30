using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using ReelWords.Data;
using ReelWords.Domain;

namespace ReelWords.App
{
    // TODO: Tema de init async
    // TODO: Separar Program.cs en installer
    // TODO: Tema de encapsular el input y sacarlo fuera de Program

    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .Build();

            Run(configuration);
        }

        private static async void Run(IConfigurationRoot configuration)
        {
            var reelsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration.GetSection("reelsPath").Value);
            var scoresPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration.GetSection("scoresPath").Value);
            var wordsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration.GetSection("wordsPath").Value);

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