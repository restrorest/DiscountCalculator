using System;
using System.Collections.Generic;

public class DiscountCalculator
{
    public decimal CalculateDiscount(int points)
    {
        if (points < 0)
            throw new ArgumentException("Points cannot be negative");

        if (points < 100)
            return 1;
        else if (points < 200)
            return 3;
        else if (points < 500)
            return 5;
        else
            return 10;
    }
}

public class TestRunner
{
    public static void AssertEqual(decimal expected, decimal actual, string message)
    {
        if (expected != actual)
        {
            throw new Exception($"Test failed: {message}. Expected: {expected}, Actual: {actual}");
        }
        Console.WriteLine($"Test passed: {message}");
    }

    public static void AssertThrows<TException>(Action action, string message) where TException : Exception
    {
        try
        {
            action();
            throw new Exception($"Test failed: {message}. Expected exception {typeof(TException).Name} was not thrown");
        }
        catch (TException)
        {
            Console.WriteLine($"Test passed: {message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Test failed: {message}. Expected {typeof(TException).Name} but got {ex.GetType().Name}");
        }
    }

    public static void RunTests()
    {
        var calculator = new DiscountCalculator();

        // Тесты для обычных случаев
        AssertEqual(1, calculator.CalculateDiscount(0), "0 points should give 1%");
        AssertEqual(1, calculator.CalculateDiscount(50), "50 points should give 1%");
        AssertEqual(1, calculator.CalculateDiscount(99), "99 points should give 1%");
        AssertEqual(3, calculator.CalculateDiscount(100), "100 points should give 3%");
        AssertEqual(3, calculator.CalculateDiscount(150), "150 points should give 3%");
        AssertEqual(3, calculator.CalculateDiscount(199), "199 points should give 3%");
        AssertEqual(5, calculator.CalculateDiscount(200), "200 points should give 5%");
        AssertEqual(5, calculator.CalculateDiscount(350), "350 points should give 5%");
        AssertEqual(5, calculator.CalculateDiscount(499), "499 points should give 5%");
        AssertEqual(10, calculator.CalculateDiscount(500), "500 points should give 10%");
        AssertEqual(10, calculator.CalculateDiscount(750), "750 points should give 10%");
        AssertEqual(10, calculator.CalculateDiscount(1000), "1000 points should give 10%");

        // Тест на отрицательное значение
        AssertThrows<ArgumentException>(() => calculator.CalculateDiscount(-1), "Negative points should throw exception");

        Console.WriteLine("All tests completed!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        TestRunner.RunTests();
    }
}