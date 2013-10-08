using System;

class Program{

    void conv1(Object r) { 
        /*
         *  casting explicito => geração da instrução IL isinst
         */
        String str = (String) r;
        Object r2 = str;
    }

    void conv2(Object r)
    {
        /*
         * gera o operador isinst
         */
        String str = r as String;

    }

    void conv3(Object r)
    {
        String str = null;
        /*
         * gera o operador isinst
         */
        if (r is String) {
            str = (String) r;
        }

    }

    /*
     * Exemplo de coercao
     */
    static void conv4(long n1) {
        checked
        {
            byte n2 = (byte)n1;
            Console.WriteLine(n2);
        }
    }


    static void conv5()
    {
        String str = "ola";
        int n1 = 745;
        Type strClass = str.GetType();
        Type n1Class = n1.GetType();
        Console.WriteLine(strClass);
        Console.WriteLine(n1Class);
    }

	static void Main(){
        conv4(37);

        /* 
         * gera excepção pq temos uma conversão checked 
         * => operador il com overflow 
         * (excepcao em caso de perca de precisao)
         */
        conv4(1037); 
        
	}

}