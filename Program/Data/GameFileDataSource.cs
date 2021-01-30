using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ReelWords.Domain;

namespace ReelWords.Data
{
    public class FileGameDataSource : IGameRepository
    {
        private readonly string _reelsPath;
        private readonly string _tableScoresPath;
        private readonly string _wordsPath;

        public FileGameDataSource(string reelsPath, string tableScoresPath, string wordsPath)
        {
            _reelsPath = reelsPath;
            _tableScoresPath = tableScoresPath;
            _wordsPath = wordsPath;
        }

        public Task<IEnumerable<string>> FetchAllReels()
        {
            var content = File.ReadAllLines(_reelsPath);
            var list = new List<string>(content);
            return Task.FromResult(list.Select(x => string.Join("", x.Split(" "))));
        }

        public Task<Dictionary<char, double>> FetchAllTableScores()
        {
            var content = File.ReadAllLines(_tableScoresPath);
            var list = new List<string>(content);
            return Task.FromResult(list.ToDictionary(x => x.Split(" ")[0][0], x => double.Parse(x.Split(" ")[1])));
        }

        public Task<IEnumerable<string>> FetchAllWords()
        {
            IEnumerable<string> words = File.ReadAllLines(_wordsPath);
            return Task.FromResult(words);
        }
    }
}
