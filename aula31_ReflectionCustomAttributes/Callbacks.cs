using System;
using System.Windows.Forms;

class FH1:IAlerter
{
    
    public void Alert()
    {
        Console.WriteLine("Hello!!!!!!!!!!!!");
    }
}

class FH2:IAlerter
{
    public void Alert()
    {
        MessageBox.Show("Ola");
    }
}

class FH3:IAlerter
{
    public void Alert()
    {
        Console.Beep();
        Console.Write("Hello ");
        Console.WriteLine();
    }
}