using System;

public class Ponto{
	public int z, x, y;
	public Ponto(int x, int y){
		this.x = x;
		this.y = y;
	}
	public double GetModule() {	 
		return Math.Sqrt(x*x + y*y);
	}
	public void Print(){
		Console.WriteLine("SUPER ({0}, {1})", x, y);
	}
}
