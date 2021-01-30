using System.Collections.Generic;

namespace ReelWords.Domain
{
    public class NoPenalizationTableScore : ITableScore
    {
        private double _current;

        private readonly Dictionary<char, double> _scoreConfig;

        public NoPenalizationTableScore(Dictionary<char, double> scoreConfig, double score = 0)
        {
            _scoreConfig = scoreConfig;
            _current = score;
        }

        public double Get()
        {
            return _current;
        }

        public double CorrectPlay(IEnumerable<string> matches)
        {
            var playScore = 0d;
            foreach (var match in matches)
            {
                foreach (var c in match)
                {
                    if (_scoreConfig.TryGetValue(c, out var score))
                    {
                        playScore += 0;
                    }

                    playScore += score;
                }
            }

            _current += playScore;
            return playScore;
        }

        public double WrongPlay()
        {
            // We do nothing when the user fails in this implementation
            const double noPenalization = 0d;
            return noPenalization;
        }
    }
}
