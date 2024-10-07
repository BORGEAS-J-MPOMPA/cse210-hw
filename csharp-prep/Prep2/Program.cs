using System;
using System.Formats.Asn1;
using System.Reflection.Metadata;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string answer = Console.ReadLine();
        int percent = int.Parse(answer);

        string sign = "";
        string letter = "";

        // Determine the letter grade
        if (percent >= 90)
        {
            letter = "A";
        }
        else if (percent >= 80)
        {
            letter = "B";
        }
        else if (percent >= 70)
        {
            letter = "C";
        }
        else if (percent >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Get the last digit of the percentage for sign determination
        int numericValue = percent % 10;

        // Determine the sign based on the last digit (numericValue)
        if (numericValue >= 7)
        {
            sign = $"{letter}+";
        }
        else if (numericValue >= 3)
        {
            sign = $"{letter}";
        }
        else
        {
            sign = $"{letter}-";
        }

        // Special case handling for A and F
        string signLetter = sign;

        // Condition 1: Remove "+" if letter is "A" or "F" and numericValue >= 7
        if ((letter == "A" || letter == "F") && numericValue >= 7)
        {
            signLetter = sign.Replace("+", "");
        }

        // Condition 2: Remove "-" if letter is "F" and numericValue < 3
        if (letter == "F" && numericValue < 3)
        {
            signLetter = signLetter.Replace("-", "");
        }

        // Output the results
        Console.WriteLine($"The last digit is: {numericValue}");
        Console.WriteLine($"Your grade is {signLetter}.");

        // Check if the student passed or failed
        if (percent >= 70)
        {
            Console.WriteLine("You passed the course!");
        }
        else
        {
            Console.WriteLine("You failed, please come back next term...!");
        }

    }
       
}