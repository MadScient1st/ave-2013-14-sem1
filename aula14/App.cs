using System;


class Employee
{
    public void Print()
    {
        Console.WriteLine(this.ToString());
    }

    public override bool Equals(Object o){
        if (o == null) return false;
        if (this == o) return true;
        Employee e = o as Employee;
        return e != null;
    }
}

class Manager : Employee {
    
    public override String ToString()
    {
        return String.Format("I am a Manager");
    }

}

class Operator : Employee
{

    public override String ToString()
    {
        return String.Format("I am a Operator");
    }

}


class Program
{
   static void Main()
   {
       Employee e = new Manager();
       Employee o = new Operator();
       e.Print(); // inspect call in disassembly
       e.Equals(o); // inspect call in disassembly
   }

}