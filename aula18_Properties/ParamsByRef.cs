using System;

public static class Program{
	public static void Main(){
		int x, y;
		GetVal(out x); // O x não precisa de ser inicializado
		Console.WriteLine(x);
		
		// y = 5;
		AddVal(ref x); // o y tem que ser obrigatoriamente inicializado
		Console.WriteLine(x);
		
		// AddVal(ref y); // error CS0165: Use of unassigned local variable 'y'
	}
	private static void GetVal(out int v){
		v = 10; // Este método tem que inicializar v.
	}
	// private static void GetVal(ref int v){ ERROR: cannot define overloaded methods  that differ only on ref and out
	private static void AddVal(ref int v){
		v += 10; // Este método pode usar o valor de inicialização de v.
	}
}