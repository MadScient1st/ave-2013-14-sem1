using System;

  public sealed class Employee {
    private int _age;
    public String _name;

    public int Age {
      get {
        return _age;
      }
      set { //parametro implicito value, que recebe o novo valor
        _age = value;
      }
    }
  }
  class Program {
    static void Main(string[] args) {
      Employee e = new Employee();
      e._name = "Zé Manel";
      e.Age = 23;
      e.Age = -5;
      Console.WriteLine(e.Age);
    }
	}