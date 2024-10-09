

using System;

class Program
{
    static void Main(string[] args)
    {
        // Creating fractions using different constructors
        Fraction fraction1 = new Fraction();           // 1/1
        Fraction fraction2 = new Fraction(5);          // 5/1
        Fraction fraction3 = new Fraction(3, 4);       // 3/4
        Fraction fraction4 = new Fraction(1, 3);       // 1/3

        // Displaying fraction strings and decimal values
        Console.WriteLine(fraction1.GetFractionString());   // Output: 1/1
        Console.WriteLine(fraction1.GetDecimalValue());     // Output: 1

        Console.WriteLine(fraction2.GetFractionString());   // Output: 5/1
        Console.WriteLine(fraction2.GetDecimalValue());     // Output: 5

        Console.WriteLine(fraction3.GetFractionString());   // Output: 3/4
        Console.WriteLine(fraction3.GetDecimalValue());     // Output: 0.75

        Console.WriteLine(fraction4.GetFractionString());   // Output: 1/3
        Console.WriteLine(fraction4.GetDecimalValue());     // Output: 0.3333333333333333

        // Testing getters and setters
        fraction3.SetNumerator(7);
        fraction3.SetDenominator(9);
        Console.WriteLine(fraction3.GetFractionString());   // Output: 7/9
        Console.WriteLine(fraction3.GetDecimalValue());     // Output: 0.7777777777777778
    }

    public class Fraction
    {
        private int _numerator;
        private int _denominator;

        // No-parameter constructor (1/1)
        public Fraction()
        {
            _numerator = 1;
            _denominator = 1;
        }

        // Constructor with one parameter for the numerator, denominator is 1
        public Fraction(int numerator)
        {
            _numerator = numerator;
            _denominator = 1;
        }

        // Constructor with two parameters
        public Fraction(int numerator, int denominator)
        {
            _numerator = numerator;
            _denominator = denominator;
        }

        // Getters and Setters
        public int GetNumerator()
        {
            return _numerator;
        }

        public void SetNumerator(int numerator)
        {
            _numerator = numerator;
        }

        public int GetDenominator()
        {
            return _denominator;
        }

        public void SetDenominator(int denominator)
        {
            _denominator = denominator;
        }

    // Method to return fraction string representation (e.g., "3/4")
        public string GetFractionString()
        {
            return $"{_numerator}/{_denominator}";
        }

    // Method to return decimal value of the fraction (e.g., 0.75)
        public double GetDecimalValue()
        {
            return (double)_numerator / _denominator;
        }
    }

}

