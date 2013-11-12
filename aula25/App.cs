using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class App
{

    private static readonly String STUDENTS_FILE = "i41n.txt";


    static void Main()
    {
        string line;
        StreamReader file = new StreamReader(STUDENTS_FILE, Encoding.UTF8);

        while ((line = file.ReadLine()) != null)
        {
            if(int.Parse(line.Substring(0, 6)) > 37000)
            // if(line.Contains("Filipe"))
                Console.WriteLine(line);
        }

        file.Dispose();

    }
}