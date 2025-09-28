// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Threading;

public class ReadThread
{
    private static readonly object threadLocker = new object();
    private readonly string param;
    private Thread thread;

    public ReadThread(string str)
    {
        this.param = str;
        thread = new Thread(Run);
    }

    private void Run()
    {
        SearchCount(param);
    }

    public void SearchCount(string searchString)
    {
        try
        {
            using (StreamReader reader = new StreamReader("bible.txt"))
            {
                string line;
                int count = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');
                    foreach (string word in words)
                    {
                        if (word.ToLower().Contains(searchString.ToLower()))
                        {
                            count++;
                            break;
                        }
                    }
                }
                Console.WriteLine($"Found {count} occurrences of the word {searchString}");
            }
        }
        catch (FileNotFoundException)
        {
            Console.Error.WriteLine("File not found");
        }
        catch (IOException)
        {
            Console.Error.WriteLine("IO error");
        }
    }

    public void SyncStart()
    {
        lock (threadLocker)
        {
            thread.Start();
            try
            {
                thread.Join();
            }
            catch (ThreadInterruptedException)
            {
                Console.Error.WriteLine("Thread interrupted");
            }
        }
    }

    public void Start()
    {
        thread.Start();
    }

    public void Join()
    {
        thread.Join();
    }
}
