using System;


interface Printer {
    void Print();
    void SetX(int n);
    void SetY(int n);
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

    void Printer.SetX(int n) {
        this.x = n;
    }
    void Printer.SetY(int n)
    {
        this.y = n;
    }
}

class Program{

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

       pr.Print();

       /*
        * O CLR não permite alterar o estado de uma instância (de tipo valor)
        * que esteja boxed.
        */
       // ((Point) pr).x = 78; // Erro de compilação
       pr.SetX(78);

       pr.Print();


       /*
        *  !!!!! CUIDADO 
        *  
        */
       p1.Print();
       p1.x = 8;
       p1.Print();
       ((Printer) p1).SetX(9); // Erro pq o metodo SetX so é acessivel com uma referncia do tipo Printer
       p1.Print();

   }
	static void Main(){
        Point p1 = new Point(5, 7);
        conv6();
	}

}