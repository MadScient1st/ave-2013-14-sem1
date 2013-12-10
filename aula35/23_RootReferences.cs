using System;

sealed class Node
{
    public Node next;
    public Node(Node p)
    {
        this.next = p;
    }
}
static class Program23
{
    public static void Main()
    {
        Node n1 = new Node(null);
        Node n2 = null;
        Console.WriteLine("Geracao de n1 = {0}", GC.GetGeneration(n1)); // geracao 0
        GC.Collect();
        Console.WriteLine("Geracao de n1 = {0}", GC.GetGeneration(n1)); // geracao 1
        Console.WriteLine(GC.GetTotalMemory(false));
        for (int i = 0; i < 1 * 1024 * 1024; i++)
            n2 = new Node(n2);
        Console.WriteLine("-------------------------------");
        Console.WriteLine("Geracao de n1 = {0}", GC.GetGeneration(n1)); // geracao 2 por causa da instanciacao do bloco de memoria anterior
        Console.WriteLine("Geracao de n2 = {0}", GC.GetGeneration(n2)); 
        Node last = null; // vai procurar o ultimo no da lista = ao mais no' mais velho
        for (last = n2; last.next != null; last = last.next) ;
        Console.WriteLine("Geracao de ultimo No de n2 = {0}", GC.GetGeneration(last));
        Console.WriteLine(GC.GetTotalMemory(true)); // n2 é root reference
        Console.WriteLine(GC.GetTotalMemory(true)); // n2 é root reference       
        n2.GetHashCode();
        Console.WriteLine(GC.GetTotalMemory(true)); // n2 NÃO é root reference
    }
}