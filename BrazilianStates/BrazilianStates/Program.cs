using System;
using System.Collections.Generic;
using System.Linq;

namespace BrazilianStates
{
    class Program
    {
        static void Main(string[] args)
        {
            var country = new Country();
            var top = country.Top10StatesByArea();
            if (top.Length == 10)
                Console.WriteLine("Should_Return_10_Itens_When_Get_Top_10_States");

            List<State> states = country.GetStates();
            if (states.Count == 27)
                Console.WriteLine("Should_Return_10_Itens_When_Get_Top_10_States");

            bool hasMatch = states.Any(x => top.Any(y => y == x));
            if (!hasMatch)
                Console.WriteLine("Should_Return_10_Itens_When_Get_Top_10_States");

            
            State lastStates = country.GetStates().OrderByDescending(x => x.Extensive).Take(1).FirstOrDefault();
            if (top.FirstOrDefault().Extensive == lastStates.Extensive)
                Console.WriteLine("Should_Return_Top_1_States");
            
        }
    }
}
