using System.Collections.Generic;
using NSubstitute;
using ReelWords.Domain;
using Xunit;

namespace UnitTests
{
    public class WordGameTests
    {
        [Fact]
        public void PlayCorrectPlay()
        {
            var tableScore = Substitute.For<ITableScore>();
            var reels = Substitute.For<IReels>();
            var dispatcher = Substitute.For<IDomainEventDispatcher>();

            reels.Play("word").Returns(new IReels.PlayResult(true, new List<string>{ "word" }));
            reels.Get().Returns("other");
            tableScore.CorrectPlay(Arg.Any<string[]>()).Returns(5d);
            tableScore.Get().Returns(5d);

            var game = new WordGame(tableScore, reels, dispatcher);

            Assert.False(game.IsPlaying);

            game.Init();

            Assert.True(game.IsPlaying);

            game.Play("word");

            tableScore.Received(1).CorrectPlay(Arg.Any<IEnumerable<string>>());
            tableScore.DidNotReceive().WrongPlay();

            dispatcher.Received(1).Send(Arg.Is(new OnUserStartedGame()));
            dispatcher.Received(2).Send(Arg.Any<OnUserStartedNextPlay>());
            dispatcher.Received(1).Send(Arg.Any<OnUserFinishedPlay>());
        }

        [Fact]
        public void PlayIncorrectPlay()
        {
            var tableScore = Substitute.For<ITableScore>();
            var reels = Substitute.For<IReels>();
            var dispatcher = Substitute.For<IDomainEventDispatcher>();

            reels.Play("word").Returns(new IReels.PlayResult(false, new List<string> { "word" }));
            reels.Get().Returns("other");
            tableScore.CorrectPlay(Arg.Any<string[]>()).Returns(5d);
            tableScore.Get().Returns(5d);

            var game = new WordGame(tableScore, reels, dispatcher);

            Assert.False(game.IsPlaying);

            game.Init();

            Assert.True(game.IsPlaying);

            game.Play("word");

            tableScore.Received(1).WrongPlay();
            tableScore.DidNotReceive().CorrectPlay(Arg.Any<IEnumerable<string>>());

            dispatcher.Received(1).Send(Arg.Is(new OnUserStartedGame()));
            dispatcher.Received(2).Send(Arg.Any<OnUserStartedNextPlay>());
            dispatcher.Received(1).Send(Arg.Any<OnUserFinishedPlay>());
        }
    }
}
