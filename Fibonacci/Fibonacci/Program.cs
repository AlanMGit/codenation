using System;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Desafio Fibonacci");

            Math math = new Math();
            if(math.IsFibonacci(2))
                Console.WriteLine("Valor existe na lista");

            if (!math.IsFibonacci(400))
                Console.WriteLine("Não existe na lista");
        }
    }
}
