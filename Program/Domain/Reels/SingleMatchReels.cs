using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReelWords.Domain
{
    public class SingleMatchReels : IReels
    {
        private readonly IEnumerable<Queue<char>> _reels;
        private readonly IWordValidator _validator;

        public SingleMatchReels(IEnumerable<IEnumerable<char>> initialReels, IWordValidator validator)
        {
            _validator = validator;
            var reelsList = new List<IEnumerable<char>>(initialReels);
            var reelsQueue = new List<Queue<char>>();

            reelsList.ForEach(chars =>
            {
                var i = 0;
                foreach (var c in chars)
                {
                    if (reelsQueue.ElementAtOrDefault(i) == null)
                    {
                        reelsQueue.Add(new Queue<char>());
                    }

                    reelsQueue[i].Enqueue(c);
                    i++;
                }
            });

            _reels = reelsQueue;
        }

        public string Get()
        {
            var sb = new StringBuilder();
            foreach (var r in _reels)
            {
                sb.Append(r.Peek().ToString());
            }

            return sb.ToString();
        }

        public IReels.PlayResult Play(string text)
        {
            if (!CanPlay(text))
            {
                var noMatches = new string[0];
                return new IReels.PlayResult(false, noMatches);
            }

            var matches = new string[] { text };
            Spin(text);
            return new IReels.PlayResult(true, matches);
        }

        public void Suffle(Random random)
        {
            const int maxSuffle = 100;
            foreach (var reel in _reels)
            {
                var suffle = random.Next(maxSuffle);
                for (var i = 0; i < suffle; i++)
                {
                    var @char = reel.Dequeue();
                    reel.Enqueue(@char);
                }
            }
        }

        private string Spin(string text)
        {
            foreach (var @char in text)
            {
                foreach (var reel in _reels)
                {
                    if (@char == reel.Peek())
                    {
                        var actual = reel.Dequeue();
                        reel.Enqueue(actual);
                        break;
                    }
                }
            }
            return Get();
        }

        private bool CanPlay(string text)
        {
            var currentLetters = Get();
            foreach (var @char in text)
            {
                var index = currentLetters.IndexOf(@char);
                if (index == -1)
                {
                    return false;
                }

                currentLetters = currentLetters.Remove(index, 1);
            }

            return _validator.Validate(text);
        }
    }
}
