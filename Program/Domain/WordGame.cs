namespace ReelWords.Domain
{
    public class WordGame
    {
        public bool IsPlaying { get; private set; }

        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly ITableScore _tableScore;
        private readonly IReels _reels;

        public WordGame(ITableScore tableScore, IReels reels, IDomainEventDispatcher domainEventDispatcher)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _tableScore = tableScore;
            _reels = reels;

            IsPlaying = true;
        }

        public void Init()
        {
            _domainEventDispatcher.Send(new OnUserStartedGame());
            _reels.Suffle(new System.Random());
            _domainEventDispatcher.Send(new OnUserStartedNextPlay(_reels.Get()));
        }

        public void Play(string input)
        {
            var matches = _reels.Play(input);
            double playScore;
            if (matches.IsCorrect)
            {
                playScore = _tableScore.CorrectPlay(matches.Matches);
            }
            else
            {
                playScore = _tableScore.WrongPlay();
            }

            _domainEventDispatcher.Send(new OnUserFinishedPlay(matches.Matches, _tableScore.Get(), playScore, matches.IsCorrect));
            _domainEventDispatcher.Send(new OnUserStartedNextPlay(_reels.Get()));
        }
    }
}
