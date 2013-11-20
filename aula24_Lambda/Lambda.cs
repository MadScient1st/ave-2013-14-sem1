using System;
using System.Windows.Forms;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Counter c = new Counter(3);
        c.obs += n => Console.WriteLine("ConsoleHandler = " + n);
        c.obs += n => MessageBox.Show("Item = " + n);
        c.obs += n =>
        {
            for (int i = 0; i < n; i++)
            {
                // Console.Beep();
                Console.Write("Hello ");
            }
            Console.WriteLine();
        };
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
