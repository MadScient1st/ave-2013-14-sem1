using System;

/*
 * Employee <- Operator 
 * Employee <- Manager <- Boss <- Owner
 */
class Employee
{
}

class Operator : Employee
{

    public override String ToString()
    {
        return String.Format("I am a Operator");
    }

}

class Manager : Employee
{

    public new virtual String ToString()
    {
        return String.Format("I am a Manager");
    }

}

class Boss: Manager
{

    public override String ToString()
    {
        return String.Format("I am a Boss");
    }

}

class Owner : Boss
{

    public new String ToString()
    {
        return String.Format("I am a Owner");
    }

}

/*
 * Employee <- Operator 
 * Employee <- Manager <- Boss <- Owner
 */
class Program
{
   static void Main()
   {
       Employee e1 = new Owner();
       Manager m1 = (Manager)e1;
       Boss b1 = (Boss)e1;
       Owner o1 = (Owner)e1;

       Employee e2 = new Operator();
       Operator opr2 = (Operator)e2;

       Console.WriteLine(e1.ToString()); // Owner
       Console.WriteLine(m1.ToString()); // I am a Boss
       Console.WriteLine(b1.ToString()); // I am a Boss
       Console.WriteLine(o1.ToString()); // I am a Owner 

       Console.WriteLine(e2.ToString()); // I am a Operator
       Console.WriteLine(opr2.ToString()); // I am a Operator
   }
}