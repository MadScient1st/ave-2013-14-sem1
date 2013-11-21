using System;
using System.Windows.Forms;

public class FH1
{
    // Exemplo de um m�todo est�tico como Handler
    public static void ConsoleHandler(String value)
    {
        Console.WriteLine("ConsoleHandler = " + value);
    }
    public static void HelloWorld(String s)
    {
        Console.WriteLine("Hello!!!!!!!!!!!!");
    }
}
public class FH2
{

    // Exemplo de um m�todo de inst�ncia como Handler
    public void MboxHandler(String value)
    {
        MessageBox.Show("Item = " + value);
    }

    // Exemplo de um m�todo est�tico como Handler
    public static void BeepHandler(String value)
    {
        Console.Beep();
        Console.Write("Hello ");
        Console.WriteLine();
    }
}