using System;

class TParams {
   static void showArgs(params object[] args) {
      foreach(object arg in args)
         Console.Write(arg.ToString() + " ");
      Console.WriteLine("FIM");
   }
   // Para s utilizações mais frequentes
	static void showArgs(object arg1) {
      Console.Write(arg1.ToString() + " ");
      Console.WriteLine("FIM");
   }	
   static void showArgs(object arg1, object arg2) {
      Console.Write(arg1.ToString() + " ");
	  Console.Write(arg2.ToString() + " ");
      Console.WriteLine("FIM");
   }	

   static void Main() {
	  showArgs("ola", "admiravel");
      showArgs("ola", "admiravel", "mundo", "novo!");
      showArgs(); //compila
   }
}
