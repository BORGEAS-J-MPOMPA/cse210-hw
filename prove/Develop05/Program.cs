   using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = new Scripture(new Reference("Proverbs", 3, 5, 6), 
            "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.");
        
        // The program loop
        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetRenderedScripture());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;
            
            scripture.HideRandomWords(3); // Hides 3 words at a time
        }

        Console.WriteLine("All words are hidden! Well done!");
    }

    public class Scripture
    {
        private Reference reference;
        private List<Word> words;

        public Scripture(Reference reference, string text)
        {
            this.reference = reference;
            this.words = new List<Word>();
            foreach (string word in text.Split(' '))
            {
                words.Add(new Word(word));
            }
        }

        // Hide random words in the scripture
        public void HideRandomWords(int count)
        {
            Random random = new Random();
            int hiddenCount = 0;
            while (hiddenCount < count)
            {
                int index = random.Next(words.Count);
                if (!words[index].IsHidden())
                {
                    words[index].Hide();
                    hiddenCount++;
                }
            }
        }

        // Check if all words are hidden
        public bool AllWordsHidden()
        {
            foreach (Word word in words)
            {
                if (!word.IsHidden())
                    return false;
            }
            return true;
        }

        // Get the current state of the scripture text with hidden words
        public string GetRenderedScripture()
        {
            string renderedText = reference.ToString() + ": ";
            foreach (Word word in words)
            {
                renderedText += word.GetRenderedWord() + " ";
            }
            return renderedText.Trim();
        }
    }


    public class Reference
    {
        private string book;
        private int startChapter;
        private int startVerse;
        private int endChapter;
        private int endVerse;

        // Constructor for a single verse
        public Reference(string book, int chapter, int verse)
        {
            this.book = book;
            this.startChapter = chapter;
            this.startVerse = verse;
            this.endChapter = chapter;
            this.endVerse = verse;
        }

        // Constructor for a verse range
        public Reference(string book, int startChapter, int startVerse, int endVerse)
        {
            this.book = book;
            this.startChapter = startChapter;
            this.startVerse = startVerse;
            this.endChapter = startChapter;
            this.endVerse = endVerse;
        }

        public override string ToString()
        {
            if (startChapter == endChapter && startVerse == endVerse)
                return $"{book} {startChapter}:{startVerse}";
            else
                return $"{book} {startChapter}:{startVerse}-{endVerse}";
        }
    }



    public class Word
    {
        private string text;
        private bool hidden;

        public Word(string text)
        {
            this.text = text;
            this.hidden = false;
        }

        public void Hide()
        {
            hidden = true;
        }

        public bool IsHidden()
        {
            return hidden;
        }

        public string GetRenderedWord()
        {
            return hidden ? "_____" : text;
        }
    }

}
