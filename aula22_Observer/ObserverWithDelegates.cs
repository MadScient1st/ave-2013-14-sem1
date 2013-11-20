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
        // c.AddFeedbackHandler(FH1.HelloWorld); // Erro de compila��o.
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

    // Notifica o m�todo de callback Feedback do objecto o,
    // por cada itera��o de from a to.
    public void DoIt(int from, int to)
    {
        for (int i = from; i <= to; i++)
        {
            NotifyObservers(i);
        }
    }
}

/*
 * Defini��o do contracto Observer.
 * Especifica��o do prot�tipo da fun��o Handler
 */
delegate void Feedback(int value);

class FH1
{
    // Exemplo de um m�todo est�tico como Handler
    public static void ConsoleHandler(int value)
    {
        Console.WriteLine("ConsoleHandler = " + value); 
    }
    public static void HelloWorld(String s)
    {
        // Este m�todo n�o obedece ao prototipo especificado por Feedback
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