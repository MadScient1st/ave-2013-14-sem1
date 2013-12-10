using System;
using System.IO;


class MyWriter:StreamWriter{
	public MyWriter(Stream stream):base(stream){}
	public MyWriter(String filename):base(new FileStream(filename, FileMode.Create)){}
	~MyWriter(){
		Flush();
	}
}
static class Program27{
	public static void Main(){
        // A ordem de instaciação dos objectos determina a ordem pela qual 
        // estes constarão na Finalization List e pela qual serão finalizados.

		// MyWriter writer = new MyWriter(new FileStream("temp.txt", FileMode.Create)); // Corre bem no Flush
		MyWriter writer = new MyWriter("temp.txt"); // Vai dar erro no Flush
		writer.WriteLine("Ola Mundo");
		writer.WriteLine("Ola Isel");
		Console.ReadLine();
	}
}