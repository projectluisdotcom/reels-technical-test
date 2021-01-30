using System.Collections.Generic;

namespace ReelWords.Domain
{
    public class Node
    {
        public char Character { get; }
        public HashSet<Node> Childs { get; }

        private char _endChar;

        public Node(char character, char endChar)
        {
            _endChar = endChar;
            Character = character;
            Childs = new HashSet<Node>();
        }

        public void Insert(string text)
        {
            var next = text[1..];
            if (next == "")
            {
                return;
            }

            var first = next[0];
            var n = new Node(first, _endChar);
            if (Childs.TryGetValue(n, out var child))
            {
                child.Insert(next);
                return;
            }
            Childs.Add(n);
            n.Insert(next);
        }

        public bool Search(string text)
        {
            var first = text[0];
            if (IsLast(first))
            {
                return true;
            }

            var next = text[1..];
            var n = new Node(next[0], _endChar);
            if (Childs.TryGetValue(n, out var child))
            {
                if (child.Search(next))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Delete(string text)
        {
            var first = text[0];
            if (IsLast(first))
            {
                return true;
            }

            var next = text[1..];
            var n = new Node(next[0], _endChar);
            if (!Childs.TryGetValue(n, out var child))
            {
                return false;
            }
            
            if (child.Delete(next))
            {
                Childs.Remove(child);
            }

            return false;
        }

        private bool IsLast(char c)
        {
            return c == _endChar;
        }

        public override bool Equals(object obj)
        {
            return obj is Node node &&
                   Character == node.Character;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Character);
        }
    }
}