using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NameValue
{
    public readonly string name;
    public readonly Object value;

    public NameValue(string name, Object v)
    {
        this.name = name;
        this.value = v;
    }

    public override String ToString() {
        return String.Format("Name = {0}, Value = {1}", name, value);
    }
}


public class Dumper
{
    public static void PrintMembers(String assemblyPath)
    {
        Assembly a = Assembly.LoadFrom(assemblyPath);
        Type[] types = a.GetTypes();
        foreach (Type t in types)
        {
            Console.WriteLine("----------------- {0} -------------", t);
            foreach (MemberInfo mi in t.GetMembers())
            {
                Console.WriteLine("{0}:  {1} ",
                  mi.MemberType,
                  mi.Name);
            }
        }
    }

    public static IEnumerable<NameValue> Members(Object target)
    {
            foreach (MemberInfo mi in target.GetType().GetMembers())
            {
                if (mi.MemberType == MemberTypes.Property) {
                    PropertyInfo prop = (PropertyInfo)mi;
                    yield return new NameValue(mi.Name, prop.GetMethod.Invoke(target, null));
                }
            }
    }
    public static T BindTo<T>(IEnumerable<NameValue> props) {
        Type klass = typeof(T);
        T target = (T) Activator.CreateInstance(klass);
        foreach (NameValue pair in props)
        {
            PropertyInfo p = klass.GetProperty(pair.name);
            p.SetValue(target, pair.value);
        }
        return target;
    }

    public static IEnumerable<Action<String>> Dispatcher(string path)
    {
        Assembly a = Assembly.LoadFrom(path);
        foreach (Type t in a.GetTypes())
        {
            if (!t.IsAbstract)
            {
                Object target = null;
                foreach (MethodInfo mi in t.GetMethods())
                {
                    if (!mi.IsStatic && target == null)
                    {
                        target = Activator.CreateInstance(t);
                    }
                    // Object[] args = new Object[mi.GetParameters().Length];
                    // yield return () => mi.Invoke(target, args);
                    if (mi.GetParameters().Length == 1 && mi.GetParameters()[0].ParameterType == typeof(String))
                    {
                        Object refToThis = mi.IsStatic ? null : target;
                        yield return (Action<String>)Delegate.CreateDelegate(typeof(Action<String>), refToThis, mi);
                    }
                }
            }
        }
    }
}

class App {

    public static void Main()
    {
        // Dumper.PrintMembers("Qry.exe");
        /*
        IEnumerable<NameValue> props = Dumper
            .Members(new { Nr = 761253, Name = "Joao Xavier Baptista" });

        props.Print();
            
        Person p = Dumper.BindTo<Person>(props);
        Console.WriteLine(p);
        */

        Dumper.Dispatcher("Handlers.dll").ToList().ForEach( a => a("Ola"));

        // MethodInfo mi = typeof(App).GetMethod("m", new Type[]{typeof(String)});
        // Action<String> a = (Action<String>)Delegate.CreateDelegate(typeof(Action<String>), null, mi);
    }

    public void m(String s) { 
    }

    class Person
    {
        public int Nr { get; set; }
        public string Name { get; set; }

        public override String ToString()
        {
            return String.Format("Person( {0}, {1})", Nr, Name);
        }
    }
}