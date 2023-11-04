using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task6_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "numbers.txt";
            string numbersText = File.ReadAllText(filePath);
            int[] numbers = numbersText.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(int.Parse)
                                      .ToArray();
            //int[] numbers = { 1, 2, 8, -1, 4, 2, 7, 9, 15, 5 };

            var longestSequenceLength = numbers
                .AsParallel()
                .Select((number, index) =>
                    Enumerable.Count(
                        Enumerable.SkipWhile(numbers, (_, i) => i <= index)
                            .TakeWhile((n, i) => n > (i == 0 ? int.MinValue : numbers[i + index]))))
                .Max();

            Console.WriteLine("Довжина найбільшої зростаючої послідовності: " + longestSequenceLength);
            Console.ReadKey();           

            var positiveSequences = numbers
                .AsParallel ()
                .Select((n, i) =>
                {
                    var sequence = Enumerable.SkipWhile(numbers, (_, index) => index <= i)
                                             .TakeWhile((c, index) => c > 0 || (index > i && numbers[i + index] > 0));
                    return new { Sequence = sequence, Count = sequence.Count() };
                })
                .OrderByDescending(item => item.Count)
                .First();

            Console.WriteLine($"Довжина найбільшої додатної послідовності: {positiveSequences.Count}");
            Console.WriteLine($"Послідовність: {string.Join(" ", positiveSequences.Sequence)}");
            Console.ReadKey();
        }
    }
}
