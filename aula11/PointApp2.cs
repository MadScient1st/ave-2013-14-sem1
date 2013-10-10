using System;


interface Printer
{
    void Print();
}

struct Point : Printer
{
    public int x, y;
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void Print()
    {
        int _x = this.x;
        int _y = this.y;
        Console.WriteLine("( {0}, {1})", x, y);
    }
}

class Program
{

    static void Main()
    {
        Point p1 = new Point(5, 7);
        p1.Print();
 
        Printer pr = p1; // box;
        pr.Print();

        Point p2 = new Point(5, 7);
        Console.WriteLine(p1.Equals(p2));
    }

}