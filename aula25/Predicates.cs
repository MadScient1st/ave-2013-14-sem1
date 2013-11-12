using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

struct Student {
    public readonly int nr;
    public readonly String name;

    public Student(int nr, String name) 
    {
        this.nr = nr;
        this.name = name;
    }

    public override String ToString() { 
        return nr + " " + name;
    }

    public static Student Parse(String line) { 
        int nr = int.Parse(line.Substring(0, 5));
        String name = line.Substring(6);
        return new Student(nr, name);
    }
}


static class App
{

    private static readonly String STUDENTS_FILE = "i41n.txt";

    static List<Student> LoadStudents()
    {
        string line;
        List<Student> res = new List<Student>();
        StreamReader file = new StreamReader(STUDENTS_FILE, Encoding.UTF8);
        while ((line = file.ReadLine()) != null)
        {
            Student s = Student.Parse(line);
            res.Add(s);
        }
        file.Dispose();
        return res;
    }

    static List<Student> Filter(this List<Student> elems, Func<Student, bool> predicate)
    {
        List<Student> res = new List<Student>();
        foreach(Student s in elems)
        {
            if (predicate(s))
                res.Add(s);
        }
        return res;
    }

    static void Do(this List<Student> elems, Action<Student> a)
    {
        foreach (Student s in elems)
        {
            a(s);
        }
    }

    static void Separator() {
        Console.WriteLine("----------------------------------------------");
    }

    static void Main() {
        List<Student> stds = LoadStudents();
        List<Student> filipes = stds.Filter(s => s.name.Contains("Filipe"));

        // Do(filipes, s => Console.Write(s.nr + " "));
        int count = 0;
        filipes.Do(s => count++);
        Console.WriteLine("Existem {0} Filipes", count);
        Separator();
        
        //Do(Filter(filipes, s => s.nr > 37000), s => Console.WriteLine(s));

        filipes.Filter(s => s.nr > 37000).Do(s => Console.WriteLine(s));

    }
}