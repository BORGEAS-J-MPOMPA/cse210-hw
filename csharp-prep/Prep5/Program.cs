using System;

class Program
{
    static void Main(string[] args)
    {
        // Call DisplayWelcome function to show welcome message
        DisplayWelcome();

        // Call PromptUserName to get the user's name
        string userName = PromptUserName();

        // Call PromptUserNumber to get the user's favorite number
        int userNumber = PromptUserNumber();

        // Call SquareNumber to calculate the square of the number
        int squaredNumber = SquareNumber(userNumber);

        // Call DisplayResult to show the final result
        DisplayResult(userName, squaredNumber);
    }

    // Function to display a welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // Function to prompt the user for their name and return it
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    // Function to prompt the user for their favorite number and return it
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return int.Parse(Console.ReadLine());
    }

    // Function to calculate and return the square of a number
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // Function to display the result: user's name and the squared number
    static void DisplayResult(string userName, int squaredNumber)
    {
        Console.WriteLine($"{userName}, the square of your number is {squaredNumber}");
    }
}
