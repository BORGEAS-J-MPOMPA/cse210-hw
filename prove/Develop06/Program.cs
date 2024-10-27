using System;
using System.Collections.Generic;
public abstract class Goal
{
    protected string name;
    protected string description;
    protected int points;
    protected bool isComplete;

    public Goal(string name, string description, int points)
    {
        this.name = name;
        this.description = description;
        this.points = points;
        this.isComplete = false;
    }

    public abstract void RecordEvent(); // To record progress on the goal
    public abstract string GetStatus(); // To display progress status


    public string GetName()
    {
        return name;
    }
    public string GetDescription()
    {
        return description;
    }

    public int GetPoints()
    {
        return points;
    }

    public bool IsComplete()
    {
        return isComplete;
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base( name, description, points) { }

    public override void RecordEvent()
    {
        isComplete = true;
    }

    public override string GetStatus()
    {
        return isComplete ? "[X] " +name +": "+ description : "[ ] " +name +": "+ description;
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(String name, string description, int points) : base(name, description, points) { }

    public override void RecordEvent()
    {
        // Since this goal is eternal, it never gets "completed"
        // Just add points for each instance
    }

    public override string GetStatus()
    {
        return "[∞] " + name +": "+ description;
    }
}

public class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;
    private int bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints) 
        : base(name, description, points)
    {
        this.targetCount = targetCount;
        this.currentCount = 0;
        this.bonusPoints = bonusPoints;
    }

    public override void RecordEvent()
    {
        currentCount++;
        if (currentCount >= targetCount)
        {
            isComplete = true;
        }
    }

    public override string GetStatus()
    {
        return isComplete 
            ? $"[X] {name}: {description} - Completed {currentCount}/{targetCount} times" 
            : $"[ ] {name}: {description} - Completed {currentCount}/{targetCount} times";
    }

    public int GetBonusPoints()
    {
        return isComplete ? bonusPoints : 0;
    }
}

class EternalQuestProgram
{
    private List<Goal> goals = new List<Goal>();
    private int totalPoints = 0;

    public void Run()
    {
        int choice = -1;
        while (choice != 0)
        {
            Console.WriteLine("\nEternal Quest Program");
            Console.WriteLine("1. Create a New Goal");
            Console.WriteLine("2. Record an Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Display Total Score");
            Console.WriteLine("0. Quit");
            Console.Write("Enter your choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateGoal();
                    break;
                case 2:
                    RecordEvent();
                    break;
                case 3:
                    DisplayGoals();
                    break;
                case 4:
                    DisplayTotalScore();
                    break;
            }
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter choice: ");
        int goalType = int.Parse(Console.ReadLine());

        // Name the goal
        Console.Write("Enter the name of your goal: ");
        string name = Console.ReadLine();

        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();

        Console.Write("Enter points for completing the goal: ");
        int points = int.Parse(Console.ReadLine());

        if (goalType == 1)
        {
            goals.Add(new SimpleGoal(name, description, points));
        }
        else if (goalType == 2)
        {
            goals.Add(new EternalGoal(name, description, points));
        }
        else if (goalType == 3)
        {
            Console.Write("Enter the number of times the goal must be completed: ");
            int targetCount = int.Parse(Console.ReadLine());

            Console.Write("Enter bonus points for completing the checklist: ");
            int bonusPoints = int.Parse(Console.ReadLine());

            goals.Add(new ChecklistGoal(name, description, points, targetCount, bonusPoints));
        }
    }

    private void RecordEvent()
    {
        DisplayGoals();

        Console.Write("Select a goal to record an event for (number): ");
        int goalIndex = int.Parse(Console.ReadLine());

        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            Goal selectedGoal = goals[goalIndex];
            selectedGoal.RecordEvent();
            totalPoints += selectedGoal.GetPoints();

            // Add bonus points for checklist goal
            if (selectedGoal is ChecklistGoal checklistGoal && checklistGoal.IsComplete())
            {
                totalPoints += checklistGoal.GetBonusPoints();
            }

            Console.WriteLine("Event recorded!");
        }
    }

    private void DisplayGoals()
    {
        Console.WriteLine("\nGoals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i}. {goals[i].GetStatus()}");
        }
    }

    private void DisplayTotalScore()
    {
        Console.WriteLine($"Total Score: {totalPoints} points");
    }
}

class Program
{
    static void Main(string[] args)
    {
        EternalQuestProgram questProgram = new EternalQuestProgram();
        questProgram.Run();
    }
}