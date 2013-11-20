using System;

public static class Program{
	public static void Swap(ref Object a, ref Object b){
		Object t = b;
		b = a;
		a = t;
	}
	public static void Main(){
		String s1 = "Jeff";
		String s2 = "Richter";
		
		// Swap(ref s1, ref s2); //  error CS1503: cannot convert from 'ref string' to 'ref object'
		
		// Variáveis passadas por referência 
		// têm que coincidir com o tipo esperado no método.
		Object o1 = s1, o2 = s2;
		Swap(ref o1, ref o2);
		
		s1 = (String ) o1;
		s2 = (String ) o2;
		
		Console.WriteLine(o1); //Displays "Richter"
		Console.WriteLine(o2); //Displays "Jeff"
	}
}

//Richter: "thankfully won't compile, shows how type safety could be compromised"
sealed class SomeType{
	public int _val;
	private static void GetAnObject(out Object o){
		o = new String('X', 100);
	}	
	private static void InitSomeType(){
		SomeType st;
		
		// GetAnObject(out st);
		
		// Console.WriteLine(st._val);
	}
}

