using System;


interface Printer
{

    void Print();
}

class A1 : Printer
{

    public virtual void Print()
    {
        Console.WriteLine("I am an A1");
    }
}

class A2 : A1
{

    public override void Print()
    {
        Console.WriteLine("I am an A2");
    }
}

class B1 : Printer
{
    void Printer.Print()
    {
        Console.WriteLine("I am an B1");
    }
}

class B2 : B1, Printer
{
    public void Print()
    {
        Console.WriteLine("I am an B2");
    }
}


class App
{
    public static void Main()
    {
        
        B1 b = new B2();
        // b.Print(); // Erro de compilação;
        ((Printer)b).Print();
    }

}