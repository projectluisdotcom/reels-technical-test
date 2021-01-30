using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReelWords.Domain
{
    public interface IGameRepository
    {
        Task<IReadOnlyList<string>> FetchAllWords();
        Task<IReadOnlyList<string>> FetchAllReels();
        Task<Dictionary<char, double>> FetchAllTableScores();
    }
}
