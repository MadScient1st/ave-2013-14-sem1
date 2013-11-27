using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Dumper
{
    public static IEnumerable<Action<string>> DispatchMethods(string path) 
    {
        Assembly a = Assembly.LoadFrom(path);
        foreach (Type t in a.GetTypes())
        {
            if(!t.IsAbstract)
            {
                Object target = null;
                foreach (MethodInfo mi in t.GetMethods())
                {
                    ParameterInfo [] paramTypes = mi.GetParameters();

                    if (mi.ReturnType == typeof(void) && paramTypes.Length == 1 && paramTypes[0].ParameterType == typeof(string)) { 

                        if (!mi.IsStatic && target == null) {
                            target = Activator.CreateInstance(t);
                        }
                        /*
                        Object localRef = mi.IsStatic ? null : target;
                        yield return (Action<string>) 
                            Delegate.CreateDelegate(typeof(Action<String>), localRef, mi);
                         */
                        yield return arg => mi.Invoke(target, new String[] { arg });
                    }
                }
            }
        }
    }
}

static class App{

    static void Dispatch(this IEnumerable<Action<string>> handlers, string arg) {
        foreach (Action<string> h in handlers)
        {
            h(arg);
        }
    }

    static void Main()
    {
        IEnumerable<Action<string>> handlers = Dumper.DispatchMethods("Handlers.dll").ToList();
        handlers.Dispatch("Ola");
        handlers.Dispatch("123");
        handlers.Dispatch("isel");
    }
}