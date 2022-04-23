using System.Collections.Generic;

namespace Design_Add_and_Search_Words_Data_Structure
{
  class Program
  {
    static void Main(string[] args)
    {
      WordDictionary dictionary = new WordDictionary();
      dictionary.AddWord("abcd");
      dictionary.Search(".bcd");
      dictionary.Search("a..d");
    }
  }

  public class WordDictionary
  {
    WordDictionary[] children;
    public bool isWord;
    public WordDictionary()
    {
      // we have 26 small letter alphabets, so size will be 26 only
      children = new WordDictionary[26];
    }

    public void AddWord(string word)
    {
      WordDictionary root = this;
      foreach (char c in word)
      {
        // here each character is equvalant to array index
        // eg - for char 'b', when we do  b - 'a' = 1 index, so if we have a value at 1 index which means its a chac b only
        int index = c - 'a';
        if(root.children[index] == null)
        {
          root.children[index] = new WordDictionary();
        }
        // update the root to the current element which is added
        // because the next char would be added as a children of current char.
        root = root.children[index];
      }
      // When we have processed the entire word our root would be at the last char, mark the last node as isWord.
      // When a node is marked as isWord = true, which help us to find a word.
      root.isWord = true;
    }

    public bool Search(string word)
    {
      // get the root
      WordDictionary root = this;
      for (int i = 0; i < word.Length; i++)
      {
        char c = word[i];
        // if the current char in word is "."
        if (c == '.')
        {
          // we have to skip the dot and match substring of word after "."
          string subStr = word.Substring(i + 1);
          // we will be searching within all children of root
          foreach(WordDictionary ch in root.children)
          {
            // if the current children is not null, start a search where root would be current index , how ? ch.Search(subStr) - we are calling search again on ch(which is current node)
            if (ch != null && ch.Search(subStr)) return true;
          }
          return false;
        }
        // if we have found the char in current node children
        var node = root.children[c - 'a'];
        if (node == null) return false;
        // update the root to found node.
        root = node;
      }

      return root.isWord;
    }
  }


  public class WordDictionary_Using_Dictionary
  {
    Dictionary<char, WordDictionary_Using_Dictionary> children = null;
    public bool isWord;
    public WordDictionary_Using_Dictionary()
    {
      children = new Dictionary<char, WordDictionary_Using_Dictionary>();
    }

    public void AddWord(string word)
    {
      WordDictionary_Using_Dictionary root = this;
      foreach (char c in word)
      {
        if (!root.children.ContainsKey(c))
        {
          root.children.Add(c, new WordDictionary_Using_Dictionary());
        }
        root = root.children[c];
      }
      root.isWord = true;
    }

    public bool Search(string word)
    {
      WordDictionary_Using_Dictionary root = this;
      for (int i = 0; i < word.Length; i++)
      {
        char c = word[i];
        if (c == '.')
        {
          string subStr = word.Substring(i + 1);
          foreach (WordDictionary_Using_Dictionary ch in root.children.Values)
          {
            if (ch != null && ch.Search(subStr)) return true;
          }
          return false;
        }
        if (!root.children.ContainsKey(c)) return false;
        var node = root.children[c];
        if (node == null) return false;
        root = node;
      }
      return root.isWord;
    }
  }

  /**
   * Your WordDictionary object will be instantiated and called as such:
   * WordDictionary obj = new WordDictionary();
   * obj.AddWord(word);
   * bool param_2 = obj.Search(word);
   */
}
