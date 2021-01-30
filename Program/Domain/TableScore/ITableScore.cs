using System.Collections.Generic;

namespace ReelWords.Domain
{
    public interface ITableScore
    {
        public double Get();
        public double CorrectPlay(IEnumerable<string> matches);
        public double WrongPlay();
    }
}