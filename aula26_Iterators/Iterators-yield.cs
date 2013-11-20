using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

struct Student
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
        return nr + " " + name;
    }

    public static Student Parse(String line)
    {
        int nr = int.Parse(line.Substring(0, 5));
        String name = line.Substring(6);
        return new Student(nr, name);
    }
}

static class App
{

    private static readonly String STUDENTS_FILE = "i41n.txt";

    static IEnumerable<string> LoadLines()
    {
        string line;
        StreamReader file = new StreamReader(STUDENTS_FILE, Encoding.UTF8);
        while ((line = file.ReadLine()) != null)
        {
            yield return line;
        }
        file.Dispose();
    }
    static IEnumerable<TRes> Select<TSrc, TRes>(this IEnumerable<TSrc> elems, Func<TSrc, TRes> transform)
    {
        foreach (TSrc e in elems)
        {
            yield return transform(e);
        }
    }

    static IEnumerable<T> Filter<T>(this IEnumerable<T> elems, Func<T, bool> predicate)
    {
        foreach (T e in elems)
        {
            if (predicate(e))
                yield return e;
        }
    }

    static void Do<T>(this IEnumerable<T> elems, Action<T> a)
    {
        foreach (T e in elems)
        {
            a(e);
        }
    }

    static void Separator()
    {
        Console.WriteLine("----------------------------------------------");
    }

    static void Main()
    {
       LoadLines()
         .Select(line => Student.Parse(line))
         .Filter(s => s.nr > 36000)
         .Select(s => s.name.Substring(0, s.name.IndexOf(' '))) //1º nome
         // .Distinct()
         .Do(s => Console.WriteLine(s));

    }
}