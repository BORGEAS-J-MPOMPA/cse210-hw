using System;
using System.Collections.Generic;
using System.Threading;

public class MindfulnessActivity
{
    protected int duration;

    public virtual void StartActivity()
    {
        Console.WriteLine("Enter the duration of the activity in seconds:");
        duration = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"Get ready to start! The activity will last for {duration} seconds.");
        Pause(3); // Common pause before starting
    }

    protected void Pause(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000); // Pauses for 1 second
        }
        Console.WriteLine();
    }

    protected void EndActivity(string activityName)
    {
        Console.WriteLine($"Good job! You have completed the {activityName} for {duration} seconds.");
        Pause(3); // Common pause after completing the activity
    }
}

public class BreathingActivity : MindfulnessActivity
{
    public override void StartActivity()
    {
        base.StartActivity();
        Console.WriteLine("This activity will help you relax by guiding you through breathing in and out slowly. Clear your mind and focus on your breathing.");

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            Pause(4); // Breath in duration
            Console.WriteLine("Breathe out...");
            Pause(4); // Breath out duration
        }

        EndActivity("Breathing Activity");
    }
}

public class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public override void StartActivity()
    {
        base.StartActivity();
        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");

        Random rand = new Random();
        string selectedPrompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(selectedPrompt);

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            string selectedQuestion = questions[rand.Next(questions.Count)];
            Console.WriteLine(selectedQuestion);
            Pause(6); // Pause before next question
        }

        EndActivity("Reflection Activity");
    }
}

public class ListingActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public override void StartActivity()
    {
        base.StartActivity();
        Console.WriteLine("This activity will help you reflect on the good things in your life by listing as many things as you can.");

        Random rand = new Random();
        string selectedPrompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(selectedPrompt);

        Pause(5); // Time to think about the prompt

        Console.WriteLine("Start listing your items. Press Enter after each item. Type 'done' when finished.");
        int count = 0;
        string input;
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        
        while (DateTime.Now < endTime)
        {
            input = Console.ReadLine();
            if (input.ToLower() == "done") break;
            count++;
        }

        Console.WriteLine($"You listed {count} items.");
        EndActivity("Listing Activity");
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome to the Mindfulness Program!");
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            string choice = Console.ReadLine();
            MindfulnessActivity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    continue;
            }

            activity.StartActivity();
        }
    }
}
