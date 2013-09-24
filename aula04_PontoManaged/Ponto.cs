using System;

public class Ponto{

    private double module;
    public double x, y;

	public Ponto(int x, int y){
		this.x = x;
		this.y = y;
        this.module = Math.Sqrt(x * x + y * y);
	}

	public double GetModule() {
        return module;
        // return Math.Sqrt(x * x + y * y);
	}

	public void Print(){
		Console.WriteLine("Point: ({0}, {1})", x, y);
	}
}
