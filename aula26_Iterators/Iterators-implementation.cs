using System;
using System.Collections;
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


class FileLinesEnumerable : IEnumerable<String>
{
    string path;
    public FileLinesEnumerable(string path) { this.path = path; }
    public IEnumerator<string> GetEnumerator() { return new FileLinesEnumerator(path); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    class FileLinesEnumerator : IEnumerator<String>
    {
        StreamReader file;
        string line;
        public FileLinesEnumerator(string path) { file = new StreamReader(path, Encoding.UTF8); }
        public bool MoveNext() { line = file.ReadLine(); return line != null; }
        public void Reset() { throw new Exception("Not Supported."); }
        void IDisposable.Dispose() { if (file != null) file.Dispose(); file = null; }
        public string Current { get { return line; } }
        object IEnumerator.Current { get { return Current; } }
    }
}

class SelectEnumerable<TSrc, TRes> : IEnumerable<TRes>
{
    IEnumerable<TSrc> src;
    Func<TSrc, TRes> transf;
    public SelectEnumerable(IEnumerable<TSrc> src, Func<TSrc, TRes> transf) { this.src = src; this.transf = transf; }
    public IEnumerator<TRes> GetEnumerator() { return new SelectEnumerator(src.GetEnumerator(), transf); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    class SelectEnumerator : IEnumerator<TRes>
    {
        IEnumerator<TSrc> src;
        Func<TSrc, TRes> transf;
        public SelectEnumerator(IEnumerator<TSrc> src, Func<TSrc, TRes> transf) { this.src = src; this.transf = transf; }
        public bool MoveNext() { return src.MoveNext(); }
        public void Reset() { src.Reset(); }
        void IDisposable.Dispose() { src.Dispose(); }
        public TRes Current { get { return transf(src.Current); } }
        object IEnumerator.Current { get { return Current; } }
    }
}

class FilterEnumerable<T> : IEnumerable<T>
{
    IEnumerable<T> src;
    Func<T, bool> predicate;
    public FilterEnumerable(IEnumerable<T> src, Func<T, bool> predicate) { this.src = src; this.predicate = predicate; }
    public IEnumerator<T> GetEnumerator() { return new FilterEnumerator(src.GetEnumerator(), predicate); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    class FilterEnumerator : IEnumerator<T>
    {
        IEnumerator<T> src;
        Func<T, bool> predicate;
        public FilterEnumerator(IEnumerator<T> src, Func<T, bool> predicate) { this.src = src; this.predicate = predicate; }
        public bool MoveNext() { while (src.MoveNext()) { if(predicate(src.Current)) return true; } return false; }
        public void Reset() { src.Reset(); }
        void IDisposable.Dispose() { src.Dispose(); }
        public T Current { get { return src.Current; } }
        object IEnumerator.Current { get { return Current; } }
    }
}

static class App
{

    private static readonly String STUDENTS_FILE = "i41n.txt";

    static IEnumerable<string> LoadLines()
    {
        return new FileLinesEnumerable(STUDENTS_FILE);
    }

    static IEnumerable<TRes> Select<TSrc, TRes>(this IEnumerable<TSrc> elems, Func<TSrc, TRes> transform)
    {
        return new SelectEnumerable<TSrc, TRes>(elems, transform);
    }

    static IEnumerable<T> Filter<T>(this IEnumerable<T> elems, Func<T, bool> predicate)
    {
        return new FilterEnumerable<T>(elems, predicate);
    }

    static void Do<T>(this IEnumerable<T> elems, Action<T> a)
    {
        foreach (T e in elems)
        {
            a(e);
        }
    }

    static void Separator() {
        Console.WriteLine("----------------------------------------------");
    }

    static void Main() {
        LoadLines()
            .Select(line => Student.Parse(line))
            .Filter(s => s.nr > 36000)
            .Select(s => s.name.Substring(0, s.name.IndexOf(' '))) //1º nome
            // .Distinct()
            .Do(s => Console.WriteLine(s));
    }
}