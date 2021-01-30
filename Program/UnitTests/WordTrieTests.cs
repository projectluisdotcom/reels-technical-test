
using ReelWords.Domain;
using Xunit;

namespace ReelWordsTests
{
    public class WordTrieTests
    {
        [Fact]
        public void NodeHashEquality()
        {
            var firstNode = new Node('a', '=');
            var secondNode = new Node('a', '=');

            Assert.Equal(firstNode, secondNode);
        }

        [Fact]
        public void ThrowsWhenUsingReservedCharacters()
        {
            Assert.Throws<System.Exception>(() => {
                var trie = new WordTrie();
                trie.Insert(trie.StartChar.ToString());
            });

            Assert.Throws<System.Exception>(() => {
                var trie = new WordTrie();
                trie.Insert(trie.EndChar.ToString());
            });
        }

        [Fact]
        public void TrieInsertTest()
        {
            var trie = new WordTrie();
            trie.Insert("ab");

            Assert.Equal(trie.StartChar, trie.Root.Character);

            var aNode = new Node('a', trie.EndChar);
            var bNode = new Node('b', trie.EndChar);
            var lastNode = new Node(trie.EndChar, trie.EndChar);

            Assert.True(trie.Root.Childs.TryGetValue(aNode, out var a));
            Assert.Equal('a', a.Character);

            Assert.True(a.Childs.TryGetValue(bNode, out var b));
            Assert.Equal('b', b.Character);

            Assert.True(b.Childs.TryGetValue(lastNode, out var last));
            Assert.Equal('=', last.Character);
        }

        [Fact]
        public void SearchTest()
        {
            var trie = new WordTrie();

            trie.Insert("ab");
            trie.Insert("abc");
            trie.Insert("abd");
            trie.Insert("abcd");

            Assert.True(trie.Search("ab"));
            Assert.True(trie.Search("abc"));
            Assert.True(trie.Search("abd"));
            Assert.True(trie.Search("abcd"));

            Assert.False(trie.Search("z"));
            Assert.False(trie.Search("zb"));
            Assert.False(trie.Search("bc"));
            Assert.False(trie.Search("acb"));

            Assert.False(trie.Search("?"));
        }


        [Fact]
        public void TrieDeleteTest()
        {
            var trie = new WordTrie();

            trie.Insert("ab");
            trie.Insert("abc");
            trie.Insert("abd");

            trie.Delete("ab");
            Assert.False(trie.Search("ab"));
            Assert.True(trie.Search("abc"));
            Assert.True(trie.Search("abd"));

            trie.Delete("abc");
            Assert.False(trie.Search("ab"));
            Assert.False(trie.Search("abc"));
            Assert.True(trie.Search("abd"));

            trie.Delete("abd");
            Assert.False(trie.Search("ab"));
            Assert.False(trie.Search("abc"));
            Assert.False(trie.Search("abd"));
        }

        [Fact]
        public void TrieTrimTest()
        {
            var trie = new WordTrie();

            trie.Insert("  ab ");

            Assert.True(trie.Search("ab"));
        }

        [Fact]
        public void TrieLowerTest()
        {
            var trie = new WordTrie();

            trie.Insert("Ab");

            Assert.True(trie.Search("ab"));
        }
    }
}