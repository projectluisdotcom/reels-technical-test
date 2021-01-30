using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReelWords.Domain
{
    public interface IGameRepository
    {
        Task<IEnumerable<string>> FetchAllWords();
        Task<IEnumerable<string>> FetchAllReels();
        Task<Dictionary<char, double>> FetchAllTableScores();
    }
}
