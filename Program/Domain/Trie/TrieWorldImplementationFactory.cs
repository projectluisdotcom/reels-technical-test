namespace ReelWords.Domain
{
    public class TrieWorldImplementationFactory : ITrieFactory
    {
        public ITrie Create()
        {
            return new WordTrie();
        }
    }
}