using System;
using System.Collections.Generic;
using NSubstitute;
using ReelWords.Domain;
using Xunit;

namespace ReelWordsTests
{
    public class DefaultReelsTest
    {
        [Fact]
        public void ReturnsMatches()
        {
            var validator = Substitute.For<IWordValidator>();
            validator.Validate(Arg.Any<string>()).Returns(true);

            var initialReels = new List<string>
            {
                "abc",
                "def",
                "ghi",
            };

            var reels = new SingleMatchReels(initialReels, validator);
            var matches = reels.Play("ab");

            Assert.True(matches.IsCorrect);
            Assert.Collection(matches.Matches,
                item => Assert.Equal("ab", item)
            );
        }

        [Fact]
        public void IncorrectPlayReturnsEmptyMatches()
        {
            var validator = Substitute.For<IWordValidator>();
            validator.Validate(Arg.Any<string>()).Returns(true);

            var initialReels = new List<string>
            {
                "abc",
                "def",
                "ghi",
            };

            var reels = new SingleMatchReels(initialReels, validator);
            var matches = reels.Play("zzz");

            Assert.False(matches.IsCorrect);
            Assert.Empty(matches.Matches);
        }

        [Fact]
        public void GetCurrentReelsTest()
        {
            var validator = Substitute.For<IWordValidator>();
            validator.Validate(Arg.Any<string>()).Returns(true);

            var initialReels = new List<string>
            {
                "abc",
                "def",
                "ghi",
            };

            var reels = new SingleMatchReels(initialReels, validator);
            Assert.Equal("abc", reels.Get());
        }

        [Fact]
        public void SpinWhenIsCorrect()
        {
            var validator = Substitute.For<IWordValidator>();
            validator.Validate(Arg.Any<string>()).Returns(true);

            var initialReels = new List<string>
            {
                "abc",
                "def",
                "ghi",
            };

            var reels = new SingleMatchReels(initialReels, validator);

            reels.Play("a");
            Assert.Equal("dbc", reels.Get());

            reels.Play("dbc");
            Assert.Equal("gef", reels.Get());
        }

        [Fact]
        public void DontSpinWhenIncorrectPlayMade()
        {
            var validator = Substitute.For<IWordValidator>();
            validator.Validate(Arg.Any<string>()).Returns(true);

            var initialReels = new List<string>
            {
                "abc",
                "def",
                "ghi",
            };

            var reels = new SingleMatchReels(initialReels, validator);

            reels.Play("ccc");
            Assert.Equal("abc", reels.Get());
        }

        [Fact]
        public void DontSpinWhenIsNotValidWord()
        {
            var validator = Substitute.For<IWordValidator>();
            validator.Validate(Arg.Any<string>()).Returns(false);

            var initialReels = new List<string>
            {
                "abc",
                "def",
                "ghi",
            };

            var reels = new SingleMatchReels(initialReels, validator);

            reels.Play("abc");
            Assert.Equal("abc", reels.Get());
        }

        [Fact]
        public void SpinMultipleTimes()
        {
            var validator = Substitute.For<IWordValidator>();
            validator.Validate(Arg.Any<string>()).Returns(true);

            var initialReels = new List<string>
            {
                "abc",
                "def",
                "ghi",
            };

            var reels = new SingleMatchReels(initialReels, validator);

            reels.Play("a");
            Assert.Equal("dbc", reels.Get());

            reels.Play("dbc");
            Assert.Equal("gef", reels.Get());
        }


        [Fact]
        public void DoesNotSpinSameLetter()
        {
            var validator = Substitute.For<IWordValidator>();
            validator.Validate(Arg.Any<string>()).Returns(true);

            var initialReels = new List<string>
            {
                "aaa",
                "bbb",
                "ccc",
            };

            var reels = new SingleMatchReels(initialReels, validator);

            reels.Play("aa");
            Assert.Equal("bba", reels.Get());

            reels.Play("b");
            Assert.Equal("cba", reels.Get());
        }

        [Fact]
        public void CanDoAFullCicle()
        {
            var validator = Substitute.For<IWordValidator>();
            validator.Validate(Arg.Any<string>()).Returns(true);

            var initialReels = new List<string>
            {
                "aaa",
                "bbb",
                "ccc",
            };

            var reels = new SingleMatchReels(initialReels, validator);

            reels.Play("a");
            Assert.Equal("baa", reels.Get());

            reels.Play("b");
            Assert.Equal("caa", reels.Get());

            reels.Play("c");
            Assert.Equal("aaa", reels.Get());

            reels.Play("a");
            Assert.Equal("baa", reels.Get());
        }

        [Fact]
        public void Suffle()
        {
            var validator = Substitute.For<IWordValidator>();
            validator.Validate(Arg.Any<string>()).Returns(true);

            var initialReels = new List<string>
            {
                "aaa",
                "bbb",
                "ccc",
            };

            var reels = new SingleMatchReels(initialReels, validator);

            var random = Substitute.For<Random>();
            random.Next(Arg.Any<int>()).Returns(1, 2, 0);
            reels.Suffle(random);

            Assert.Equal("bca", reels.Get());
        }
    }
}