using System;
using System.Windows.Forms;

class FH1
{
    // Exemplo de um método estático como Handler
    public static void ConsoleHandler(String value)
    {
        Console.WriteLine("ConsoleHandler = " + value);
    }
    public static void HelloWorld(String s)
    {
        Console.WriteLine("Hello!!!!!!!!!!!!");
    }
}
class FH2
{
    // Exemplo de um método de instância como Handler
    public void MboxHandler(String value)
    {
        MessageBox.Show("Item = " + value);
    }

    // Exemplo de um método estático como Handler
    public static void BeepHandler(String value)
    {
        Console.Beep();
        Console.Write("Hello ");
        Console.WriteLine();
    }
}