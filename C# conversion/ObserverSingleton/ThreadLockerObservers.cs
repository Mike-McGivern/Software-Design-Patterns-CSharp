using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;


public interface IObserver
{
    void Update();
}

public abstract class Subject
{
    protected readonly List<IObserver> observers = new List<IObserver>();

    public void Subscribe(IObserver observer)
    {
        lock (observers)
        {
            observers.Add(observer);
        }
    }

    public void Unsubscribe(IObserver observer)
    {
        lock (observers)
        {
            observers.Remove(observer);
        }
    }

    protected void NotifyObservers()
    {
        //Equivalent to spawing threads for each observer in Java
        lock(observers)
        {
            foreach(var observer in observers)
            {
                new Thread(observer.Update).Start();
            }
        }
    }
}

public class FileManager : Subject
{
    private static readonly object threadLock = new object();
    private static FileManager? instance = null;
    private const string FILE_NAME = "grades.txt";

    private FileManager()
    {
        if (!File.Exists(FILE_NAME))
        {
            File.Create(FILE_NAME).Dispose();
        }
    }

    public static FileManager GetInstance()
    {
        if (instance == null)
        {
            lock (threadLock)
            {
                if (instance == null)
                {
                    instance = new FileManager();
                }
            }
        }
        return instance;
    }

    public void AddGrade(int grade)
    {
        lock (threadLock)
        {
            using (StreamWriter sw = new StreamWriter(FILE_NAME, append: true))
            {
                sw.WriteLine(grade);
            }
        }
        NotifyObservers();
    }

    public int? GetFirstGrade()
    {
        lock (threadLock)
        {
            using (StreamReader sr = new StreamReader(FILE_NAME))
            {
                string? line = sr.ReadLine();
                if (line != null && int.TryParse(line.TrimEnd(), out int value))
                {
                    return value;   
                }
            }
        }
        return null;
    }

    public List<int> GetAllGrades()
    {
        List<int> grades = new List<int>();
        lock (threadLock)
        {
            foreach (var line in File.ReadAllLines(FILE_NAME))
            {
                if(int.TryParse(line.TrimEnd(), out int value))
                {
                    grades.Add(value);  
                }
            }
        }
        return grades;
    }

    public void DeleteAll()
    {
        lock(threadLock) {
            File.WriteAllText(FILE_NAME, string.Empty);
        }
        NotifyObservers();
    }
}

public class AllGradesDisplayUI : IObserver
{
    public void Update()
    {
        List<int> grades = FileManager.GetInstance().GetAllGrades();
        Console.WriteLine("Grades: ");
        foreach(int grade in grades)
        {
            Console.Write($"{grade}");
        }
        Console.WriteLine();
    }
}

public class AverageDisplayUI : IObserver
{
    public void Update()
    {
        List<int> grades = FileManager.GetInstance().GetAllGrades();

        if (grades.Count == 0)
        {
            Console.WriteLine("Average Grade: N/A");
            return;
        }

        double avg = grades.Average();
        Console.WriteLine($"Average: {avg:F2}");
    }
}

public class Program
{
    public static void Main()
    {
        FileManager fm = FileManager.GetInstance();

        fm.Subscribe(new AllGradesDisplayUI());
        fm.Subscribe(new AverageDisplayUI());

        fm.DeleteAll();

        Console.WriteLine("Adding grades....\n");
        fm.AddGrade(90);
        fm.AddGrade(80);
        fm.AddGrade(70);

        // Let observer threads print
        Thread.Sleep(500);

        Console.WriteLine("\nFirst grade: " + (fm.GetFirstGrade() ?? -1));

        Console.WriteLine("\nDeleting all grades...");
        fm.DeleteAll();

        Thread.Sleep(500);
        Console.WriteLine("\nProgram complete.");

    }

}
