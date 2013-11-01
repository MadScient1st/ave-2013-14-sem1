using System;

delegate void XptoHandler();

class Program
{
   static void Main()
   {
       int[] array = { 1, 8, 19, 4 };

       // descending sort 
       Array.Sort(array, (a, b) => -1 * a.CompareTo(b));
   }
}