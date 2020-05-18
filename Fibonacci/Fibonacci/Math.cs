using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fibonacci
{
    class Math
    {
        public List<int> Fibonacci()
        {
            List<int> numbers = new List<int>() { 0, 1 };

            while (true)
            {
                int total = numbers[numbers.Count()  - 2] + numbers[numbers.Count() - 1];

                if (total > 350)
                    break;

                numbers.Add(total);
            }

            return numbers;
        }

        public bool IsFibonacci(int numberToTest)
        {
            return Fibonacci().Any(x => x == numberToTest);
        }
    }
}
