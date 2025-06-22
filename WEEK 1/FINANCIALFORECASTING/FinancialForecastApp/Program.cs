using System;

class FinancialForecast
{
    // Recursive method to calculate future value
    static double Forecast(double principal, double rate, int years)
    {
        if (years == 0)
            return principal;
        return Forecast(principal, rate, years - 1) * (1 + rate);
    }

    static void Main(string[] args)
    {
        Console.Write("Enter initial amount (Principal): ");
        double principal = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter annual growth rate (e.g., 0.05 for 5%): ");
        double rate = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter maximum number of years: ");
        int maxYears = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("\nYear\tFuture Value");
        Console.WriteLine("------------------------");

        for (int year = 1; year <= maxYears; year++)
        {
            double futureValue = Forecast(principal, rate, year);
            Console.WriteLine($"{year}\t{futureValue:F2}");
        }
    }
}
