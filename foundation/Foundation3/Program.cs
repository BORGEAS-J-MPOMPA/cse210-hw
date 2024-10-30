using System;
using System.Collections.Generic;

public abstract class Activity
{
    protected DateTime date;
    protected int durationMinutes;

    public Activity(DateTime date, int durationMinutes)
    {
        this.date = date;
        this.durationMinutes = durationMinutes;
    }

    // Abstract Methods
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Summary of the Activity
    public virtual string GetSummary()
    {
        return $"{date:dd MMM yyyy} ({durationMinutes} min)";
    }
}

public class Running : Activity
{
    private double distanceMiles;

    public Running(DateTime date, int durationMinutes, double distanceMiles)
        : base(date, durationMinutes)
    {
        this.distanceMiles = distanceMiles;
    }

    public override double GetDistance() => distanceMiles;
    public override double GetSpeed() => (distanceMiles / durationMinutes) * 60;
    public override double GetPace() => durationMinutes / distanceMiles;

    // Override GetSummary Method
    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Running: Distance {distanceMiles} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

public class Cycling : Activity
{
    private double speedMph;

    public Cycling(DateTime date, int durationMinutes, double speedMph)
        : base(date, durationMinutes)
    {
        this.speedMph = speedMph;
    }

    public override double GetDistance() => (speedMph * durationMinutes) / 60;
    public override double GetSpeed() => speedMph;
    public override double GetPace() => 60 / speedMph;

    // Override GetSummary Method
    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Cycling: Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

public class Swimming : Activity
{
    private int laps;

    public Swimming(DateTime date, int durationMinutes, int laps)
        : base(date, durationMinutes)
    {
        this.laps = laps;
    }

    public override double GetDistance() => laps * 50 / 1000.0 * 0.62; // Converts laps to miles
    public override double GetSpeed() => (GetDistance() / durationMinutes) * 60;
    public override double GetPace() => durationMinutes / GetDistance();

    // Override GetSummary Method
    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Swimming: Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

public class Program
{
    private static readonly Random random = new Random();

    private static DateTime GetRandomDate()
    {
        int year = random.Next(2020, 2024); // Years from 2020 to 2023
        int month = random.Next(1, 13); // Months from January (1) to December (12)
        int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1); // Valid day for given month and year
        return new DateTime(year, month, day);
    }

    public static void Main()
    {
        // Create random activities
        var activities = new List<Activity>
        {
            new Running(GetRandomDate(), random.Next(20, 61), 3.0),   // 20-60 minutes, 3 miles
            new Cycling(GetRandomDate(), random.Next(20, 61), 15.0),  // 20-60 minutes, 15 mph
            new Swimming(GetRandomDate(), random.Next(20, 61), 20)    // 20-60 minutes, 20 laps
        };

        // Display summaries
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
