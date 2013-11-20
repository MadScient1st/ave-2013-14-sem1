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

    // Notifica o método de callback Observer do objecto o,
    // por cada iteração de from a to.
    public void Start()
    {
        for (int i = 1; i <= max; i++)
        {
            obs(i);
        }
    }
}

/*
 * Definição do contracto Observer.
 * Especificação do protótipo da função Handler
 */
delegate void Observer(int value);

class FH1
{
    // Exemplo de um método estático como Handler
    public static void ConsoleHandler(int value)
    {
        Console.WriteLine("ConsoleHandler = " + value); 
    }
    public static void HelloWorld(String s)
    {
        // Este método não obedece ao prototipo especificado por Observer
    }
}
class FH2
{
    // Exemplo de um método de instância como Handler
    public void MboxHandler(int value)
    {
        MessageBox.Show("Item = " + value);
    }

    // Exemplo de um método estático como Handler
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