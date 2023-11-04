using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 4, 2, 5, 2, 8, 4, 6, 5, 9, 1, 4, 3, 9 };

        Task removeDuplicatesTask = Task.Run(() =>
        {
            Console.WriteLine("Початковий масив:");
            PrintArray(array);

            array = RemoveDuplicates(array);
            Console.WriteLine("\nМасив без повторень:");
            PrintArray(array);
        }).ContinueWith(task =>
        {
            Array.Sort(array);
            Console.WriteLine("\nВідсортований масив:");
            PrintArray(array);
        }).ContinueWith(task =>
        {
            int target = 5; // Значення для пошуку
            int index = Array.BinarySearch(array, target);

            if (index >= 0)
                Console.WriteLine($"\nЗначення {target} знайдено на позиції {index} відсортованого масиву.");
            else
                Console.WriteLine($"\nЗначення {target} не знайдено відсортованому масиві.");
        });

        Task.WaitAll();
        Console.ReadKey();
    }

    static int[] RemoveDuplicates(int[] arr)
    {
        return arr.Distinct().ToArray();
    }

    static void PrintArray(int[] arr)
    {
        foreach (var item in arr)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
    }
    
}
