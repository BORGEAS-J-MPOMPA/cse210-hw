using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep1 World!");

        Console.Write("What's your first name? ");

        string first = Console.ReadLine();

        Console.Write("What's your last name? ");

        string last = Console.ReadLine();


        Console.WriteLine($"Your name's {last}, {first} {last}. Nice to meet you {first}");
    }
}