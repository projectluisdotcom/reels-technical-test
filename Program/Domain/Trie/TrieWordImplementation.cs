namespace ReelWords.Domain
{
    public class WordTrie : ITrie
    {
        public Node Root;

        public char EndChar;
        public char StartChar;

        public WordTrie(
            char startChar = '^',
            char endChar = '='
        )
        {
            StartChar = startChar;
            EndChar = endChar;

            Root = new Node(StartChar, EndChar);
        }

        public bool Search(string s)
        {
            var processed = ProcessInput(s);
            if (processed == null)
            {
                return false;
            }
            return Root.Search(processed);
        }

        public void Insert(string s)
        {
            var processed = ProcessInput(s);
            if (processed == null)
            {
                return;
            }
            Root.Insert(processed);
        }

        public void Delete(string s)
        {
            var processed = ProcessInput(s);
            if (processed == null)
            {
                return;
            }
            if (!Search(s))
            {
                return;
            }

            Root.Delete(processed);
        }

        private string ProcessInput(string s)
        {
            var trimmed = s.ToLower().Trim();
            if (trimmed.Contains(StartChar))
            {
                throw new System.Exception($"Reserved key {StartChar} used");
            }
            if (trimmed.Contains(EndChar))
            {
                throw new System.Exception($"Reserved key {EndChar} used");
            }
            return StartChar + trimmed + EndChar;
        }
    }
}