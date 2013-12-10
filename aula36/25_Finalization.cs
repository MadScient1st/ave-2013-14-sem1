using System;
using System.IO;


class MyFileStream : FileStream { 
    	
        public MyFileStream (string path, FileMode mode) : base(path, mode)
		{
		}

        ~MyFileStream()
        {
            Console.WriteLine("Finalize runing!!!!");
            Console.WriteLine("Wait for user to hit <Enter> to run GC");
            Console.ReadLine();
            Console.WriteLine("Finalize finished!!!!");
        }
}

static class Program25{
	public static void Main(){
		FileStream fs = new MyFileStream("out.txt", FileMode.Create);

        Console.WriteLine("[The native handle locks the out.txt file in exclusive mode]");
        Console.WriteLine("Wait for user to hit <Enter> to run GC");
		Console.ReadLine();
		
		// Force a garbage collection to occur for this demo
        // Does not guarantee the execution of the Finalize method of fs object.
        // 
		GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("After fs's object been collected, you can write to out.txt.");
        Console.WriteLine("Wait for user to hit <Enter>");
        Console.ReadLine();
	}
}
