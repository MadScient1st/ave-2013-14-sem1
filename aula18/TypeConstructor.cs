using System;

// BeforeFieldInit
class C{
  public static int x = InitX();
  public static int InitX(){
    Console.WriteLine("Initializing C.x....");
    return 5;
  }
}
// Precise
class D{
  public static int x;
  static D(){
    Console.WriteLine("Initializing D.x....");
    x = 9;
  }
}
public static class App{
	public static void Main(){
    Console.WriteLine("Main init....");
    int n = 7;
    n = n + C.x;
    Console.WriteLine("n + C.x = " + n);
    n = n + D.x;
    Console.WriteLine("n + D.x = " + n);
	}
}
