using System;

class App
{
    public static void Main()
    {
        Point p = new Point(7, 3);
        p.Print();
        Console.WriteLine(p.ToString());
        Console.WriteLine(p.GetType());
        Console.WriteLine(p.GetHashCode());
    }

}