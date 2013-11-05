using System;
using System.Collections.Generic;

delegate bool Predicate<T>(T elem);

class Utils { 

    public static bool Contains<T>(IEnumerable<T> elems, Predicate<T> p){
        foreach (T e in elems)
	    {
            // if (p.Invoke(e))
		    if(p(e))
                return true;
	    }
        return false;
    }
}

class Student {
    public int Grade { get; set; }
}

class Account {
    public int Balance { get; set; }
}

class Program
{
    private static bool GradePredicate (Student s){
        return s.Grade > 18;
    }

    private bool AccountPredicate(Account a)
    {
        return a.Balance < 0;
    }

    static void Main() {
        IEnumerable<Student> stds = null;
        IEnumerable<Account> accs = null;

        Utils.Contains(stds, new Predicate<Student>(GradePredicate));
        Utils.Contains(stds, GradePredicate);
        Program p = new Program();
        Utils.Contains(accs, p.AccountPredicate);
    }

}