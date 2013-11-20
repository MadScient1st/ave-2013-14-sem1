using System;
using System.Collections.Generic;

class Program{
	public static void Main(){
		Nullable<int> a = null;
		Nullable<int> b = 3;
		
		int? c = null;
		int? d = 5;
		
		b += d;    // b <- 8                
		d = a + b; // d <- null
		
		int e = (int)b; // e <- 3
		int f = (int)c; // excepção porque c é null
		
		int g = c ?? -1; // g <- -1
		int h = a ?? c ?? 0; // h <- 0
	}
}