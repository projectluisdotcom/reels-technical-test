using System.Collections.Generic;

namespace ReelWords.Domain
{
    public class TrieWordValidator : IWordValidator
    {
        private readonly ITrie _validWords;

        public TrieWordValidator(IEnumerable<string> wordsArray, ITrieFactory trieFactory)
        {
            var wordsList = new List<string>(wordsArray);
            var trie = trieFactory.Create();
            wordsList.ForEach(x => trie.Insert(x));
            _validWords = trie;
        }

        public bool Validate(string text)
        {
            return _validWords.Search(text);
        }
    }
}