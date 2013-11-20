using System;
using System.Windows.Forms;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Counter c = new Counter(3);
        c.obs += FH1.ConsoleHandler;
        c.obs += new FH2().MboxHandler;
        c.Start();
    }
}
class Counter
{
    private int max;
    public event Observer obs;

    public Counter(int n) {
        max = n;
    }

    // Notifica o m�todo de callback Observer do objecto o,
    // por cada itera��o de from a to.
    public void Start()
    {
        for (int i = 1; i <= max; i++)
        {
            obs(i);
        }
    }
}

/*
 * Defini��o do contracto Observer.
 * Especifica��o do prot�tipo da fun��o Handler
 */
delegate void Observer(int value);

class FH1
{
    // Exemplo de um m�todo est�tico como Handler
    public static void ConsoleHandler(int value)
    {
        Console.WriteLine("ConsoleHandler = " + value); 
    }
    public static void HelloWorld(String s)
    {
        // Este m�todo n�o obedece ao prototipo especificado por Observer
    }
}
class FH2
{
    // Exemplo de um m�todo de inst�ncia como Handler
    public void MboxHandler(int value)
    {
        MessageBox.Show("Item = " + value);
    }

    // Exemplo de um m�todo est�tico como Handler
    public static void BeepHandler(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Console.Beep();
            Console.Write("Hello ");
        }
        Console.WriteLine();
    }
}