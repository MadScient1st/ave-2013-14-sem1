using System;
using System.Collections.Generic;

class Utils {

    public static void AddNew<T>(List<T> l) where T : new(){
        l.Add(new T());
    }

    public static void Init<T>(List<T> l) // where T : class
    {
        for (int i = 0; i < l.Count; i++)
        {
            l[i] = default(T);
        }
    }


}

class Point {
    int x, y;

    public Point(int n1, int n2) {
        x = n1;
        y = n2;
    }
}

class Program {

    static void Main() {
        List<int> l1 = new List<int>();
        Utils.AddNew(l1);

        List<Point> l2 = new List<Point>();
        // Utils.AddNew(l2); // Erro de compilação pq Point não tem um construtor sem parâmetros
    }

}