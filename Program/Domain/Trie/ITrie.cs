namespace ReelWords.Domain
{
    public interface ITrie
    {
        public bool Search(string text);
        public void Insert(string text);
        public void Delete(string text);
    }
}