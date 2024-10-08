using System;
using System.Collections.Generic;

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

        public void HideRandomWords(int numberOfWordsToHide)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfWordsToHide; i++)
            {
                int index = random.Next(words.Count);
                words[index].Hide();
            }
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

            while (!scripture.AreAllWordsHidden())
            {
                Console.Clear();
                scripture.Display();

                Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit:");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    break;
                }

                scripture.HideRandomWords(3); // Hiding 3 random words at a time
            }

            Console.WriteLine("All words are hidden. Goodbye!");
        }
}