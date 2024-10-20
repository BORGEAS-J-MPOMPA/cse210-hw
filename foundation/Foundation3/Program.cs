using System;
using System.Collections.Generic;

public abstract class Activity
{
    protected DateTime date;
    protected int durationMinutes; // Length of activity in minutes

    public Activity(DateTime date, int durationMinutes)
    {
        this.date = date;
        this.durationMinutes = durationMinutes;
    }

    public abstract double GetDistance(); // Distance in miles or km
    public abstract double GetSpeed();    // Speed in mph or kph
    public abstract double GetPace();     // Pace in min per mile or km

    public virtual string GetSummary()
    {
        return $"{date.ToString("dd MMM yyyy")} ({durationMinutes} min)";
    }
}

public class Running : Activity
{
    private double distanceMiles; // Distance in miles

    public Running(DateTime date, int durationMinutes, double distanceMiles)
        : base(date, durationMinutes)
    {
        this.distanceMiles = distanceMiles;
    }

    public override double GetDistance()
    {
        return distanceMiles;
    }

    public override double GetSpeed()
    {
        return (distanceMiles / durationMinutes) * 60; // Speed in miles per hour
    }

    public override double GetPace()
    {
        return durationMinutes / distanceMiles; // Pace in min per mile
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Running: Distance {distanceMiles} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

public class Cycling : Activity
{
    private double speedMph; // Speed in miles per hour

    public Cycling(DateTime date, int durationMinutes, double speedMph)
        : base(date, durationMinutes)
    {
        this.speedMph = speedMph;
    }

    public override double GetDistance()
    {
        return (speedMph * durationMinutes) / 60; // Distance in miles
    }

    public override double GetSpeed()
    {
        return speedMph; // Speed in miles per hour
    }

    public override double GetPace()
    {
        return 60 / speedMph; // Pace in min per mile
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Cycling: Distance {GetDistance():F1} miles, Speed {speedMph} mph, Pace: {GetPace():F1} min per mile";
    }
}

public class Swimming : Activity
{
    private int laps; // Number of laps (1 lap = 50 meters)

    public Swimming(DateTime date, int durationMinutes, int laps)
        : base(date, durationMinutes)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        double distanceKm = (laps * 50) / 1000.0; // Convert meters to kilometers
        return distanceKm * 0.62; // Convert km to miles
    }

    public override double GetSpeed()
    {
        return (GetDistance() / durationMinutes) * 60; // Speed in miles per hour
    }

    public override double GetPace()
    {
        return durationMinutes / GetDistance(); // Pace in min per mile
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Swimming: Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list of activities
        List<Activity> activities = new List<Activity>();

        // Create instances of each activity type
        activities.Add(new Running(new DateTime(2022, 11, 3), 30, 3.0));
        activities.Add(new Cycling(new DateTime(2022, 11, 4), 40, 12.0));
        activities.Add(new Swimming(new DateTime(2022, 11, 5), 25, 20));

        // Iterate through the list and display the summary for each activity
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}