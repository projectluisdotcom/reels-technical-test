using System.Collections.Generic;
using ReelWords.Domain;
using Xunit;

namespace ReelWordsTests
{
    public class NoPenalizationTableScoreTests
    {
        [Fact]
        public void CountsMatchesScores()
        {
            var tableScoreData = new Dictionary<char, double>
            {
                { 'a', 1d },
                { 'b', 2d },
                { 'c', 3d },
            };
            var matches = new string[]
            {
                "a",
                "ab",
                "abc",
            };

            var tableScore = new NoPenalizationTableScore(tableScoreData);

            var score = tableScore.CorrectPlay(matches);
            Assert.Equal(10, score);
            Assert.Equal(10, tableScore.Get());
        }

        [Fact]
        public void CountsTotalScoreOnMultiplePlays()
        {
            var tableScoreData = new Dictionary<char, double>
            {
                { 'a', 1d },
                { 'b', 2d },
                { 'c', 3d },
            };
            var matchesOne = new string[]
            {
                "a",
                "ab",
                "abc",
            };
            var matchesTwo = new string[]
            {
                "c",
            };

            var tableScore = new NoPenalizationTableScore(tableScoreData);

            var scoreOne = tableScore.CorrectPlay(matchesOne);
            Assert.Equal(10, scoreOne);
            Assert.Equal(10, tableScore.Get());

            var scoreTwo = tableScore.CorrectPlay(matchesTwo);
            Assert.Equal(3, scoreTwo);
            Assert.Equal(13, tableScore.Get());
        }

        [Fact]
        public void NoMatchesCountsAsZero()
        {
            var tableScoreData = new Dictionary<char, double>
            {
            };
            var matches = new string[]
            {
                "a",
                "ab",
                "abc",
            };

            var tableScore = new NoPenalizationTableScore(tableScoreData);

            var score = tableScore.CorrectPlay(matches);
            Assert.Equal(0, score);
            Assert.Equal(0, tableScore.Get());
        }
    }
}
