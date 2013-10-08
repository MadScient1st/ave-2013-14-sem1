using System;


interface Printer {
    void Print();
}

struct Point : Printer{
    public int x, y;
    public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public void Print() {
        Console.WriteLine("( {0}, {1})", x, y);
    }
}

class PointRef
{
    int x, y;
    public PointRef(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

class Program{

   static void conv5()
    {
        String str = "ola";
        int n1 = 745;
        Type strClass = str.GetType();
        Type n1Class = n1.GetType(); // tem um box implicito
        Console.WriteLine(strClass);
        Console.WriteLine(n1Class);
    }

   static void conv6()
   {
       Point p1 = new Point(5, 7);
       Object r1 = p1; // box;

       int n2 = 789;
       Object r2 = n2; // box;

       p1 = (Point)r1; // unbox
       // p1 = (Point)n1; // unbox => CastClassException

       Printer pr = p1; // Box
       p1 = (Point) pr; // unbox

       // ((Point) pr).x = 78;

       pr.Print();

       // TPC modificar o objecto referido por pr (sem criar novos objectos)
       // de maneira a que no proximo Print as coordenadas tenham outro valor.

       pr.Print();

   }
	static void Main(){
        Point p1 = new Point(5, 7);
        PointRef p2 = new PointRef(6, 8);
        conv6();
	}

}