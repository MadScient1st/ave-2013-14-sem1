using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Dumper
{

    public static void DispatchInterfaceAlerter(string path)
    {
        Assembly a = Assembly.LoadFrom(path);
        foreach (Type t in a.GetTypes())
        {
            // if (!t.IsAbstract && t.IsSubclassOf(typeof(IAlerter)))
            if (!t.IsAbstract && typeof(IAlerter).IsAssignableFrom(t))
            {
                // !!!!!! NUNCA FAZER ISTO qdo sabemos o tipo do t
                /*
                Object target = Activator.CreateInstance(t);
                MethodInfo m = t.GetMethod("Alert", new Type[] { });
                m.Invoke(target, null);
                */

                IAlerter target = (IAlerter) Activator.CreateInstance(t);
                target.Alert();
            }
        }
    }

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
    public static IEnumerable<Pair> GetValues(Object obj) {
        Type t = obj.GetType();
        foreach (FieldInfo f in t.GetFields())
        {
            yield return new Pair(f.Name, f.GetValue(obj));
        }
        foreach (PropertyInfo f in t.GetProperties())
        {
            if(f.GetIndexParameters().Length == 0 && f.CanRead)
                yield return new Pair(f.Name, f.GetValue(obj));
        }
    }
}

public class Pair{
    public readonly string name;
    public readonly object value;
    public Pair (string name, object value)
	{
        this.name = name;
        this.value = value;
	}
    public override string ToString(){
        return String.Format("( {0} = {1})", this.name, this.value);
    }
}

class Student
{
    public readonly int id;
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Student(int id, string name, DateTime dt) 
    {
        this.id = id;
        this.Name = name;
        this.BirthDate = dt;
    }
}

public interface IAlerter {
    void Alert();
}


static class App{

    static void Dispatch(this IEnumerable<Action<string>> handlers, string arg) {
        foreach (Action<string> h in handlers)
        {
            h(arg);
        }
    }

    static void ForEach<T>(this IEnumerable<T> elems, Action<T> a)
    {
        foreach (T e in elems)
        {
            a(e);
        }
    }

    static void Main()
    {
        /*
        IEnumerable<Action<string>> handlers = Dumper.DispatchMethods("Handlers.dll").ToList();
        handlers.Dispatch("Ola");
        handlers.Dispatch("123");
        handlers.Dispatch("isel");
        */

        Student s = new Student(13432, "Jose Baptista", new DateTime(1956, 11, 26));
        Dumper.GetValues(s).ForEach(pair => Console.WriteLine(pair));

        Dumper.DispatchInterfaceAlerter("Callbacks.dll");
    }
}