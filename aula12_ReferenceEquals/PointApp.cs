using System;


interface Printer
{
    void Print();
}

struct  Point : Printer
{
    public int x, y;
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void Print()
    {
        Console.WriteLine(this.ToString());
    }

    public override String ToString() {
        return String.Format("( {0}, {1})", x, y);
    }
}

class PointRef : Printer
{
    public int x, y;
    public PointRef(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void Print()
    {
        Console.WriteLine(this.ToString());
    }

    public override String ToString()
    {
        return String.Format("( {0}, {1})", x, y);
    }

    public override bool Equals(Object o) {
        if (o == null) return false;
        if (o.GetType() != this.GetType()) return false;
        PointRef r = (PointRef)o;
        return x == r.x && y == r.y;
    }
}

class Point3 : PointRef {
    int z;

    public Point3(int x, int y, int z):base(x, y){
        this.z = z;
    }

    public override bool Equals(Object o)
    {
        if (o == null) return false;
        if (o.GetType() != this.GetType()) return false;
        Point3 r = (Point3)o;
        if (this.z != r.z) return false;
        return base.Equals(o);
    }

    public override String ToString()
    {
        return String.Format("( {0}, {1}, {2})", x, y, z);
    }

}

class Program
{
    static void exemplo1() {
        Point p1 = new Point(5, 7);
        Point p2 = new Point(5, 7);
        Point p3 = new Point(9, 11);

        /*
         * Estamos a testar o Equals herdado de ValueType (cmp Igualdade entre campos).
         * !!!!!! Esta implementação de Euals recorre a Reflexão !!!!
         * Porque o Point não tem uma implementação de Equals.
         */
        Console.WriteLine("{0}.Equals({1}) = {2}", p1, p2, p1.Equals(p2));
        Console.WriteLine("{0}.Equals({1}) = {2}", p1, p3, p1.Equals(p3));


        PointRef r1 = new PointRef(5, 7);
        PointRef r2 = new PointRef(5, 7);
        PointRef r3 = new PointRef(9, 11);

        /*
         * Estamos a testar o Equals redefinido em PointRef.
         */
        Console.WriteLine("{0}.Equals({1}) = {2}", r1, r2, r1.Equals(r2));
        Console.WriteLine("{0}.Equals({1}) = {2}", r1, r3, r1.Equals(r3));

        Console.WriteLine("{0} == {1} = {2}", r1, r2, r1 == r2);
        Console.WriteLine("{0} == {1} = {2}", r1, r3, r1 == r3);

        Console.WriteLine("{0} == {1} = {2}", r1, r2, Object.ReferenceEquals(r1, r2));
        Console.WriteLine("{0} == {1} = {2}", r1, r3, Object.ReferenceEquals(r1, r3));

    }

    static void exemplo2() {
        Point3 p1 = new Point3(5, 7, 11);
        Point3 p2 = new Point3(5, 7, 11);
        Console.WriteLine("{0}.Equals({1}) = {2}", p1, p2, p1.Equals(p2));
        
    }
    static void Main()
    {
        // exemplo1();

        exemplo2();

    }

}