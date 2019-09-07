using System;
using System.Collections.Generic;

namespace Flyweight
{
    public class Sentence
    {
        private string[] words;
        private Dictionary<int, WordToken> tokens = new Dictionary<int, WordToken>();
        public Sentence(string plainText)
        {
            words = plainText.Split(' ');
        }

        public WordToken this[int index]
        {
            get
            {
                WordToken wt = new WordToken();
                tokens.Add(index, wt);
                return tokens[index];
            }
        }

        public override string ToString()
        {
            List<string> ws = new List<string>();

            for (int i = 0; i < words.Length; i++)
            {
                var w = words[i];
                if (tokens.ContainsKey(i) && tokens[i].Capitalize)
                    w = words[i].ToUpperInvariant();
                ws.Add(w);
            }

            return string.Join(" ", ws);
        }

        public class WordToken
        {
            public bool Capitalize;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var sentence = new Sentence("hello world");
            sentence[1].Capitalize = true;
            Console.WriteLine(sentence);
            
        }
    }
}
