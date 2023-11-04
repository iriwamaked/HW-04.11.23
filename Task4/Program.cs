using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Task4
{
    internal class Program
    {
        static void Main()
        {
            int number = 6789; 

            Parallel.Invoke(
                () => CalculateFactorial(number),
                () => DisplayDigitsInformation(number)
            );
            Console.ReadKey();
        }

        static void CalculateFactorial(int n)
        {
            BigInteger factorial = 1;
            for (int i = 1; i <= n; i++)
            {
                factorial *= i;
            }
            Console.WriteLine($"Факторіал числа {n}: {factorial}");
        }

        static void DisplayDigitsInformation(int number)
        {
            int digitCount = number.ToString().Length;
            int digitSum = 0;
            int num = number;
            while (num != 0)
            {
                digitSum += num % 10;
                num /= 10;
            }

            Console.WriteLine($"Кількість цифр у числі {number}: {digitCount}");
            Console.WriteLine($"Сума цифр у числі {number}: {digitSum}");
        }
    }
}
