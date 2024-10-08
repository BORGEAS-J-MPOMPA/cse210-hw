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
        // Define multiple scripture references and texts
        List<Scripture> scriptures = new List<Scripture>()
        {
            new Scripture(new ScriptureReference("John", 3, 16), "For God so loved the world that he gave his one and only Son that whoever believes in him shall not perish but have eternal life."),
            new Scripture(new ScriptureReference("Proverbs", 3, 5, 6), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."),
            new Scripture(new ScriptureReference("Proverbs", 27, 1, 3), "Do not boast about tomorrow, for you do not know what a day may bring. Let someone else praise you, and not your own mouth; an outsider, and not your own lips. Stone is heavy and sand a burden, but a fool's provocation is heavier than both."),
            new Scripture(new ScriptureReference("Hebrews", 13, 8), "Jesus Christ is the same yesterday and today and forever.")
        };

        int currentScriptureIndex = 0;

        while (true)
        {
            Console.Clear();
            
            // Display the current scripture passage
            Scripture currentScripture = scriptures[currentScriptureIndex];
            currentScripture.Display();

            if (currentScripture.AreAllWordsHidden())
            {
                currentScriptureIndex++;
                if (currentScriptureIndex >= scriptures.Count)
                {
                    // All passages have been hidden
                    Console.WriteLine("All scriptures are hidden. Goodbye!");
                    break;
                }
                else
                {
                    continue; // Move to the next passage
                }
            }

            Console.WriteLine("\nPress Enter to hide a word or type 'quit' to exit:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            if (!currentScripture.HideRandomWord())
            {
                // This should never happen because we check AreAllWordsHidden first,
                // but just in case, we handle it here.
                Console.WriteLine("All words are hidden for this scripture. Moving to the next one.");
                currentScriptureIndex++;
                if (currentScriptureIndex >= scriptures.Count)
                {
                    Console.WriteLine("All scriptures are hidden. Goodbye!");
                    break;
                }
            }
        }
    }
}
