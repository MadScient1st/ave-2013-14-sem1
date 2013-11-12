using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class App
{

    delegate void Handler();

    static void m() { 
    }

    void mInst() { 
    }

    static void Main()
    {
        Handler h1 = m;
        Handler h2 = m;
        // Console.WriteLine(h1 == h2); // <=> h1.Equals(h2)
        Console.WriteLine(h1.Equals(h2)); // São iguais as propriedades Method e Target forem iguais entre h1 e h2
        Console.WriteLine(Object.ReferenceEquals(h1, h2));

        App a = new App();
        Handler h3 = new App().mInst;
        Handler h4 = new App().mInst;
        Console.WriteLine(h3.Equals(h4)); // São iguais as propriedades Method e Target forem iguais entre h1 e h2
        Console.WriteLine(Object.ReferenceEquals(h3, h4));
    }
}






