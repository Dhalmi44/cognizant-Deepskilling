using System;

class MyLogger
{
    private static MyLogger instance;
    private static readonly object lockObj = new object();

    private MyLogger()
    {
        Console.WriteLine("====== MyLogger instance created ======");
    }

    public static MyLogger GetInstance()
    {
        if (instance == null)
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = new MyLogger();
                }
            }
        }
        return instance;
    }

    public void Log(string message)
    {
        Console.WriteLine("LOG: " + message);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // First logger instance
        MyLogger logger1 = MyLogger.GetInstance();
        Console.WriteLine("Enter the first log message:");
        string firstMessage = Console.ReadLine();
        logger1.Log(firstMessage);

        // Second logger instance
        MyLogger logger2 = MyLogger.GetInstance();
        Console.WriteLine("Enter the second log message:");
        string secondMessage = Console.ReadLine();
        logger2.Log(secondMessage);

        // Check if they are the same instance
        if (ReferenceEquals(logger1, logger2))
        {
            Console.WriteLine("\n===== Same MyLogger instance used =====");
        }
        else
        {
            Console.WriteLine("\n===== Different MyLogger instances created! =====");
        }
    }
}
