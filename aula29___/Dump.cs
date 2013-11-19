using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

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
}

class Person {
    public int Nr { get; set; }
    public string Name { get; set; }

    public override String ToString()
    {
        return String.Format("Person( {0}, {1})", Nr, Name);
    }   
}

static class App {

    static void Print<T>(this IEnumerable<T> elems)
    {
        foreach (T e in elems)
        {
            Console.WriteLine(e);
        }
    }

    public static void Main()
    {
        // Dumper.PrintMembers("Qry.exe");

        IEnumerable<NameValue> props = Dumper
            .Members(new { Nr = 761253, Name = "Joao Xavier Baptista" });

        props.Print();
            
        Person p = Dumper.BindTo<Person>(props);
        Console.WriteLine(p);
    }
}