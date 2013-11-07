using System;
using System.Windows.Forms;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Counter c = new Counter();
        c.AddFeedbackHandler(FH1.ConsoleHandler);
        c.AddFeedbackHandler(new FH2().MboxHandler);
        c.AddFeedbackHandler(FH2.BeepHandler);
        // c.AddFeedbackHandler(FH1.HelloWorld); // Erro de compilação.
        c.DoIt(5, 7);
    }
}
class Counter
{
    private List<Feedback> obs = new List<Feedback>();

    public void AddFeedbackHandler(Feedback f)
    {
        obs.Add(f);
    }

    public void RemoveFeedbackHandler(Feedback f)
    {
        obs.Remove(f);
    }

    public void NotifyObservers(int n)
    {
        //if any callbacks are specified, call them
        foreach (Feedback h in obs)
        {
            h(n); // <=> h.Invoke(n);
        }
    }

    // Notifica o método de callback Feedback do objecto o,
    // por cada iteração de from a to.
    public void DoIt(int from, int to)
    {
        for (int i = from; i <= to; i++)
        {
            NotifyObservers(i);
        }
    }
}

/*
 * Definição do contracto Observer.
 * Especificação do protótipo da função Handler
 */
delegate void Feedback(int value);

class FH1
{
    // Exemplo de um método estático como Handler
    public static void ConsoleHandler(int value)
    {
        Console.WriteLine("ConsoleHandler = " + value); 
    }
    public static void HelloWorld(String s)
    {
        // Este método não obedece ao prototipo especificado por Feedback
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