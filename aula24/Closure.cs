using System;
using System.Windows.Forms;
using System.Collections.Generic;

class Program
{
    static Func<int> dummy(int max){
        Console.WriteLine(max);
        Func<int> h = () => ++max;
        max++;
        h();
        Console.WriteLine(max);
        return h;
    }

    static void Main()
    {
        dummy(7);
    }
}


