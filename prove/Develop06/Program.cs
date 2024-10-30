using System;
using System;
using System.Collections.Generic;
using System.IO;

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

    // Defining abstract methods
    public abstract void RecordEvent();
    public abstract string GetStatus();

    public string GetName() => name;
    public string GetDescription() => description;
    public int GetPoints() => points;
    public bool IsComplete() => isComplete;

    // Saving and Loading Methods
    public abstract string SaveData();
    public abstract void LoadData(string[] data);
}
    // Defining the Simple Goal
public class SimpleGoal: Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points) { }

    public override void RecordEvent() => isComplete = true;

    public override string GetStatus() => isComplete ? $"[X] {name}: {description}" : $"[ ] {name}: {description}";
    public override string SaveData() => $"Simple, {name}, {description}, {points}, {isComplete}";
    public override void LoadData(string[] data)
    {
        name = data[1];
        description = data[2];
        points = int.Parse(data[3]);
        isComplete = bool.Parse(data[4]);
    }

}

    // Defining The Eternal Goal
public class EternalGoal: Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }
    public override void RecordEvent() { } // This will never be completed
    public override string GetStatus() => $"[∞] {name}: {description}";

    public override string SaveData() => $"Eternal, {name}, {description}, {points}";

    public override void LoadData(string[] data)
    {
        name = data[1];
        description = data[2];
        points = int.Parse(data[3]);
    }

}

// Defining The Checklist Goals
public class CheckListGoal: Goal
{
    private int targetCount;
    private int currentCount;
    private int bonusPoints;

    public CheckListGoal(string name, string description, int points, int targetCount, int bonusPoints): base(name, description, points)
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

        public override string GetStatus() =>
        isComplete
        ? $"[X] {name}: {description} - Completed {currentCount} / {targetCount} times"
        : $"[ ] {name}: {description} - Completed {currentCount} / {targetCount} times";

    public int GetBonusPoints() => isComplete ? bonusPoints : 0;
    public override string SaveData() => $"CheckList, {name}, {description}, {points}, {isComplete}, {targetCount}, {currentCount}, {bonusPoints}";
    public override void LoadData(string[] data)
    {
        name = data[1];
        description = data[2];
        points = int.Parse(data[3]);
        isComplete = bool.Parse(data[4]);
        targetCount = int.Parse(data[5]);
        currentCount = int.Parse(data[6]);
        bonusPoints = int.Parse(data[7]);
    }
}

class Quest
{
    private List<Goal> goals = new List<Goal>();
    private int totalPoints = 0;
    private const string FileName = "goals.txt";

    public void Run()
    {
        int choice = -1;
        while (choice != 0)
        {
            Console.WriteLine("\nWelcome to the Eternal Quest Program!");
            Console.WriteLine("1. Set a new Goal");
            Console.WriteLine("2. Record an Event");
            Console.WriteLine("3. Display Goal(s)");
            Console.WriteLine("4. Display Total Score");
            Console.WriteLine("5. Save Goal(s)");
            Console.WriteLine("6. Load Goal(s)");
            Console.WriteLine("0. Quit the Program");
            Console.Write("Enter Your Choice: ");
            choice = int.Parse(Console.ReadLine());

            switch(choice)
            {
                case 1: CreateGoal(); break;
                case 2: RecordEvent(); break;
                case 3: DisplayGoals(); break;
                case 4: DisplayTotalScore(); break;
                case 5: SaveGoals(); break;
                case 6: LoadGoals(); break;
            }
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("Select Goal Type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. CheckList Goal");
        Console.Write("Enter Your Choice: ");
        int goalType = int.Parse(Console.ReadLine());

        Console.Write("Enter the Name of Your Goal: ");
        string name = Console.ReadLine();
        Console.Write("Enter the Goal Description: ");
        string description = Console.ReadLine();
        Console.Write("Enter Points for Completing the Goal: ");
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
            Console.Write("Enter the Number of Times the Goal Should be Completed: ");
            int targetCount = int.Parse(Console.ReadLine());
            Console.Write("Enter Bonus Points for Completing the CheckList Goal: ");
            int bonusPoints = int.Parse(Console.ReadLine());
            goals.Add(new CheckListGoal(name, description, points, targetCount, bonusPoints));
        }
        else if (goalType > 3)
        {
            Console.Write("Please Enter a Number listed in the Menu!: ");
            int rightAnswer = int.Parse(Console.ReadLine());
            while (rightAnswer >= 4 )
            {
                Console.Write("Please Enter a Number listed in the Menu!: ");
                rightAnswer = int.Parse(Console.ReadLine());


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
                Console.Write("Enter the Number of Times the Goal Should be Completed: ");
                int targetCount = int.Parse(Console.ReadLine());
                Console.Write("Enter Bonus Points for Completing the CheckList Goal: ");
                int bonusPoints = int.Parse(Console.ReadLine());
                goals.Add(new CheckListGoal(name, description, points, targetCount, bonusPoints));
                }
            }
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

    private void RecordEvent()
    {
        DisplayGoals();
        Console.Write("Select a Goal to Record an Event for (number): ");
        int goalIndex = int.Parse(Console.ReadLine());

        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            Goal selectedGoal = goals[goalIndex];
            selectedGoal.RecordEvent();
            totalPoints += selectedGoal.GetPoints();

            if (selectedGoal is CheckListGoal checkListGoal && checkListGoal.IsComplete())
            {
                totalPoints += checkListGoal.GetBonusPoints();
            }

            Console.WriteLine("Event Recorded!");
        }
    }

    private void DisplayTotalScore()
    {
        Console.WriteLine($"Total Score: {totalPoints} Points.");
    }

    private void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter(path:"cse210-hw"))
        {
            foreach (var goal in goals)
            {
                writer.WriteLine(goal.SaveData());
            }
        }
        Console.WriteLine("Goal(s) Saved Successfully.");
    }

    private void LoadGoals()
    {
        if (!File.Exists(path:"cse210-hw"))
        {
            Console.WriteLine("No Saved Goals Found!");
            return;
        }
        goals.Clear();
        using (StreamReader reader = new StreamReader(path:"cse210-hw"))
        {
            string Line;
            while ((Line  = reader.ReadLine()) != null)
            {
                string[] data = Line.Split(',');
                Goal goal = data[0] switch
                {
                    "Simple" => new SimpleGoal("", "", 0),
                    "Eternal" => new EternalGoal("", "", 0),
                    "CheckList" => new CheckListGoal("", "", 0, 0, 0),
                    _ => null
                };

                goal?.LoadData(data);
                if (goal != null)
                {
                    goals.Add(goal);
                }
            }
        }
        Console.WriteLine("Goal(s) Loaded Successfully.");
    }

}

class Program
{
    static void Main(string[] args)
    {
        Quest EternalQuestProgram = new Quest();
        EternalQuestProgram.Run();
    }
}