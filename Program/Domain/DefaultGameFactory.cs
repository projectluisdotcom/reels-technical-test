using System.Threading.Tasks;

namespace ReelWords.Domain
{
    public class DefaultGameFactory : IGameFactory
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IGameRepository _gameRepository;
        private readonly ITrieFactory _trieFactory;

        public DefaultGameFactory(IGameRepository gameRepository, ITrieFactory trieFactory, IDomainEventDispatcher domainEventDispatcher)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _gameRepository = gameRepository;
            _trieFactory = trieFactory;
        }

        public async Task<WordGame> Create()
        {
            var tableScoreData = await _gameRepository.FetchAllTableScores();
            var tableScore = new NoPenalizationTableScore(tableScoreData);
            var validator = new TrieWordValidator(await _gameRepository.FetchAllWords(), _trieFactory);
            var reels = new SingleMatchReels(await _gameRepository.FetchAllReels(), validator);
            return new WordGame(tableScore, reels, _domainEventDispatcher);
        }
    }
}
