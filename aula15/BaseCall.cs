using System;

class Employee
{
    public virtual void Print() {
        Console.WriteLine("I am an Employee");
    }
}

class Operator : Employee
{
    public override void Print()
    {
        base.Print();
        Console.WriteLine("I am an Operator");
    }
}

class Program
{
   static void Main()
   {
       Employee e = new Operator();
       e.Print();
   }
}