using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public struct Student
{
    public readonly int nr;
    public readonly String name;

    public Student(int nr, String name)
    {
        this.nr = nr;
        this.name = name;
    }

    public override String ToString()
    {
        return String.Format("{0} {1}", nr, name);
    }
    public void Print()
    {
        Console.WriteLine(this);
    }
}

static class App
{

    private static readonly String STUDENTS_FILE = "i41n.txt";

    
    static IEnumerable<String> WithLines(string path)
    {
        string line;
        using (StreamReader file = new StreamReader(path, Encoding.UTF8))
        {
            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }

        }
        yield break;
    }

    static Student ToStudent(String line)
    {
        int nr = int.Parse(line.Substring(0, 5));
        String name = line.Substring(6);
        return new Student(nr, name);
    }

    static void Print<T>(this IEnumerable<T> elems) { 
        foreach(T e in elems){
            Console.WriteLine(e);
        }
    }

    static void Separator()
    {
        Console.WriteLine("------------------------------------------------");
    }

    static void Main()
    {
        WithLines(STUDENTS_FILE).Select(ToStudent).First().Print();
        Separator();
        WithLines(STUDENTS_FILE).Select(ToStudent).Last().Print();
        Separator();
        WithLines(STUDENTS_FILE).Select(ToStudent).Where(s => s.nr > 36000).Print();
        Separator();
        WithLines(STUDENTS_FILE).Select(ToStudent).Where(s => s.name.Contains("Miguel")).Print();
    }
}