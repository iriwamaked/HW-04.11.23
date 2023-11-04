using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Task5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\2. ШАГ Уроки\3. Пары\2. 2 семестр ООП\44. 04.11.23\HW_04.11.23\Task5\bin\Debug\numbers.txt";

            List<int> numbers = new List<int>();
            numbers.Add(10);
            numbers.Add(2);
            numbers.Add(3);
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (int.TryParse(line, out int number))
                    {
                        numbers.Add(number);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Ошибка чтения файла: " + e.Message);
            }

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            Parallel.ForEach(numbers, (number) =>
            {
                BigInteger factorial = CalculateFactorial(number);
                Console.WriteLine($"Факториал числа {number} равен {factorial}");
            });
            Console.ReadKey();
        }

        static BigInteger CalculateFactorial(int number)
        {
            if (number < 0)
            {
                return 0;
            }
            BigInteger factorial = 1;
            for (int i = 2; i <= number; i++)
            {
                factorial *= i;
            }
            return factorial;
        }
    }
}
