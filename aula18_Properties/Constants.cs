using System;

public sealed class SomeTypeLibrary
{
    // O resultado da express�o s� � determin�vel em tempo de execu��o
    //  public const int Capacity = CalcNr(120); // ERRO de Compila��o
    public const int Capacity = 130 * 2 + 5;

    public static readonly int Increment = CalcNr(130); // Aceite

    static int CalcNr(int n) { return n * 2 + 5; }
}

static class Program
{
    public static void Main()
    {
        ShowCapacity();
        ShowIncrement();
    }

    public static void ShowCapacity()
    {
        Console.WriteLine("Max entries in List: " + SomeTypeLibrary.Capacity); // O compilador substitui Capacity pelo valor da constante
    }
    public static void ShowIncrement()
    {
        Console.WriteLine("Capacity increment: " + SomeTypeLibrary.Increment);
    }

}