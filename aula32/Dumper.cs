using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Dumper
{
    /// <summary>
    /// Procura no assembly recebido por parametro todos os tipos compatíveis com a 
    /// interface IAlerter.
    /// Os tipos compatíveis e não abstractos, são instanciados e invocado o respectivo 
    /// método Alert() da interface IAlerter.
    /// </summary>
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

                IAlerter target = (IAlerter)Activator.CreateInstance(t);
                target.Alert();
            }
        }
    }

    /// <summary>
    /// Procura no assembly recebido por parametro todos os métodos que sejam compativeis com o 
    /// prototipo void nomeMetodo(string arg).
    /// Os metodos compativeis são retornados como handlers na forma de instancias de Action<string>.
    /// 
    /// 1ª versão: Retorna um IEnumerable de handlers representados por instancias de Action<string>
    /// 2ª versão: Retorna um "chaining delegate" de Action<string> que apontam para os handlers.
    /// 3ª versão: Retorna uma instancia de Action<string> que mantém os handlers num Dictionary<Action<string>...>
    /// </summary>
    public static Action<string> DispatchMethods(string path)
    {
        /*
         * Este dicionário handlers será capturado pelo contexto da Action<>
         * retornada no final desta função.
         */
        Dictionary<Action<string>, DumperHandlerAttribute> handlers
            = new Dictionary<Action<string>, DumperHandlerAttribute>();
        Assembly a = Assembly.LoadFrom(path);
        foreach (Type t in a.GetTypes())
        {
            if (!t.IsAbstract)
            {
                Object target = null;
                foreach (MethodInfo mi in t.GetMethods())
                {
                    /*
                    if (mi.IsDefined(typeof(DumperHandlerAttribute)) == false) 
                        continue;
                    */

                    DumperHandlerAttribute attr = (DumperHandlerAttribute)Attribute.GetCustomAttribute(mi, typeof(DumperHandlerAttribute));

                    if (attr == null) continue;

                    ParameterInfo[] paramTypes = mi.GetParameters();

                    if (mi.ReturnType == typeof(void) && paramTypes.Length == 1 && paramTypes[0].ParameterType == typeof(string))
                    {
                        if (!mi.IsStatic && target == null)
                        {
                            target = Activator.CreateInstance(t);
                        }
                        handlers.Add(
                            arg => mi.Invoke(target, new String[] { arg }),
                            attr);
                    }
                }
            }
        }
        return (str) =>
        {
            foreach (KeyValuePair<Action<string>, DumperHandlerAttribute> h in handlers)
            {
                if (h.Value.MaxCalls != 0)
                {
                    h.Key(str);
                    h.Value.Dec();
                }
            }
        };
    }

    /// <summary>
    /// Retorna um IEnumerable<Pair<String, Object>> com os pares nome, valor, 
    /// dos campos e propriedades do objecto obj recebido por parâmetro. 
    /// </summary>
    public static IEnumerable<Pair> GetValues(Object obj)
    {
        Type t = obj.GetType();
        foreach (MemberInfo m in t.GetMembers())
        {
            DumperValueAttribute attr = (DumperValueAttribute)
                Attribute.GetCustomAttribute(m, typeof(DumperValueAttribute));
            if (attr == null) continue;
            Object value = null;
            PropertyInfo p = m as PropertyInfo;
            if (p != null && p.GetIndexParameters().Length == 0 && p.CanRead)
                value = p.GetValue(obj);
            else if (m.MemberType == MemberTypes.Field)
                value = ((FieldInfo)m).GetValue(obj);
            else
                continue;
            Type k = attr.Formatter;
            IFormatter frmt = k != null ? (IFormatter)Activator.CreateInstance(k) : null;
            yield return new Pair(m.Name, frmt != null ? frmt.Format(value) : value);
        }
    }
}

/// <summary>
/// Classe auxiliar da função GetValues.
/// </summary>
public class Pair
{
    public readonly string name;
    public readonly object value;
    public Pair(string name, object value)
    {
        this.name = name;
        this.value = value;
    }
    public override string ToString()
    {
        return String.Format("( {0} = {1})", this.name, this.value);
    }
}

/// <summary>
/// Auxiliar à classe Dumper. Faz parte da API pública de Dumper.
/// </summary>
public interface IAlerter
{
    void Alert();
}

/// <summary>
/// Auxiliar à classe Dumper. Faz parte da API pública de Dumper.
/// </summary>
interface IFormatter
{
    String Format(Object value);
}

/// <summary>
/// Auxiliar à classe Dumper. Faz parte da API pública de Dumper.
/// </summary>
public class DumperHandlerAttribute : Attribute
{
    public DumperHandlerAttribute()
    {
        MaxCalls = int.MaxValue;
    }
    public int MaxCalls { get; set; }
    public void Dec()
    {
        if (MaxCalls != int.MaxValue)
            MaxCalls = MaxCalls - 1;
    }
}

/// <summary>
/// Auxiliar à classe Dumper. Faz parte da API pública de Dumper.
/// </summary>
public class DumperValueAttribute : Attribute
{
    private Type _formatter;
    public Type Formatter
    {
        get
        {
            return _formatter;
        }
        set
        {
            if (value.IsAbstract || !typeof(IFormatter).IsAssignableFrom(value))
                throw new ArgumentOutOfRangeException();
            _formatter = value;
        }
    }
}

/// <summary>
/// Classe do domínio da aplicação. Ex: aplicação para gestao de uma escola.
/// </summary>
class Student
{
    [DumperValue]
    public readonly int _id;
    public string Name { get; set; }
    [DumperValue(Formatter = typeof(DateFormatter))]
    public DateTime BirthDate { get; set; }
    public Student(int id, string name, DateTime dt)
    {
        this._id = id;
        this.Name = name;
        this.BirthDate = dt;
    }

    class DateFormatter : IFormatter
    {
        public String Format(Object value)
        {
            DateTime dt = (DateTime)value;
            return String.Format(
                "The birth date and time: {0:MM/dd/yy H:mm:ss zzz}",
                dt);
        }
    }
}

static class App
{

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
        Action<string> handlers = Dumper.DispatchMethods("Handlers.dll");
        handlers("Ola");
        handlers("123");
        handlers("isel");
        
        Dumper.DispatchInterfaceAlerter("Callbacks.dll");
        */

        Student s = new Student(13432, "Jose Baptista", new DateTime(1956, 11, 26));
        Dumper.GetValues(s).ForEach(pair => Console.WriteLine(pair));


    }
}