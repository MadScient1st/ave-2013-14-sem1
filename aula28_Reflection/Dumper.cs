using System;
using System.Reflection;

public class Dumper
{
    public static void DispatchMethods(string path) 
    {
        Assembly a = Assembly.LoadFrom(path);
        foreach (Type t in a.GetTypes())
        {
            if(!t.IsAbstract)
            {
                Object target = null;
                foreach (MethodInfo mi in t.GetMethods())
                {
                    if (!mi.IsStatic && target == null) {
                        target = Activator.CreateInstance(t);
                    }
                    Object [] args = new Object [mi.GetParameters().Length];
                    mi.Invoke(target, args);
                }
            }
        }
    }
}

class App{
    static void Main()
    {
        Dumper.DispatchMethods("Handlers.dll");
    }
}