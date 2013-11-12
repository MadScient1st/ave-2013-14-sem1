using System;

class Program
{
    static void m(int i)
    {
        Func<int> f = () => { ++i; Console.WriteLine(i); return i; };
        f();
        Console.WriteLine(i);
        f();
        Console.WriteLine(i);
    }

    static void Main()
    {
        m(5);
    }
}
