using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public class ScriptureReference
    {
        private string book;
        private int chapter;
        private int startVerse;
        private int endVerse;

        // Constructor for single verse
        public ScriptureReference(string book, int chapter, int verse)
        {
            this.book = book;
            this.chapter = chapter;
            this.startVerse = verse;
            this.endVerse = verse;
        }

        // Constructor for verse range
        public ScriptureReference(string book, int chapter, int startVerse, int endVerse)
        {
            this.book = book;
            this.chapter = chapter;
            this.startVerse = startVerse;
            this.endVerse = endVerse;
        }

        public string GetReference()
        {
            if (startVerse == endVerse)
            {
                return $"{book} {chapter}:{startVerse}";
            }
            else
            {
                return $"{book} {chapter}:{startVerse}-{endVerse}";
            }
        }
    }

    public class Word
    {
        private string word;
        private bool isHidden;

        public Word(string word)
        {
            this.word = word;
            this.isHidden = false;
        }

        public void Hide()
        {
            isHidden = true;
        }

        public bool IsHidden()
        {
            return isHidden;
        }

        public string GetDisplayText()
        {
            return isHidden ? new string('_', word.Length) : word;
        }
    }

    public class Scripture
    {
        private ScriptureReference reference;
        private List<Word> words;

        public Scripture(ScriptureReference reference, string text)
        {
            this.reference = reference;
            words = new List<Word>();
            string[] wordArray = text.Split(' ');
            foreach (string word in wordArray)
            {
                words.Add(new Word(word));
            }
        }

        public void Display()
        {
            Console.WriteLine(reference.GetReference());
            foreach (Word word in words)
            {
                Console.Write(word.GetDisplayText() + " ");
            }
            Console.WriteLine();
        }

        public bool HideRandomWord()
        {
            Random random = new Random();
            List<Word> visibleWords = words.Where(w => !w.IsHidden()).ToList();
            
            if (visibleWords.Count > 0)
            {
                int index = random.Next(visibleWords.Count);
                visibleWords[index].Hide();
                return true;
            }

            return false; // No more words to hide
        }

        public bool AreAllWordsHidden()
        {
            return words.All(w => w.IsHidden());
        }
    }

    static void Main(string[] args)
    {
        // Example scripture with reference and text
        ScriptureReference reference = new ScriptureReference("John", 3, 16);
        Scripture scripture = new Scripture(reference, "For God so loved the world that he gave his one and only Son that whoever believes in him shall not perish but have eternal life.");

        while (true)
        {
            Console.Clear();
            scripture.Display();

            if (scripture.AreAllWordsHidden())
            {
                Console.WriteLine("All words are hidden. Goodbye!");
                break;
            }

            Console.WriteLine("\nPress Enter to hide a word or type 'quit' to exit:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            if (!scripture.HideRandomWord())
            {
                // This should never happen because we check AreAllWordsHidden first,
                // but just in case, we handle it here.
                Console.WriteLine("All words are hidden. Goodbye!");
                break;
            }
        }
    }
}
