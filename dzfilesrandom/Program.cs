using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using System.IO; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.Runtime.Serialization; 
using System.Xml.Serialization; 
using System.Runtime.Serialization.Json; 
namespace dzfilesrandom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            List<int> numbers = GenerateNumbers(1, 1000, 100);
            List<int> Primenumbers = GetPrimeNumbers(numbers);
            List<int> Fibonaccinumbers = GetFibonacciNumbers(numbers);
            SaveToFile("Primenumbers.json", Primenumbers);
            SaveToFile("Fibonaccinumbers.json", Fibonaccinumbers);
            Console.WriteLine($"Количество простых чисел: {Primenumbers.Count}");
            Console.WriteLine($"Количество чисел фибоначчи: {Fibonaccinumbers.Count}");
        }
        static List<int> GenerateNumbers(int first, int last, int count)
        {
            List<int> numbers = new List<int>();
            int start_index = first;
            while (numbers.Count < count && start_index <= last)
            {
                numbers.Add(start_index);
                start_index++;
                if (start_index > last)
                {
                    start_index = first;
                }
            }
            return numbers;
        }
        static List<int> GetPrimeNumbers(List<int> numbers)
        {
            List<int> primes = new List<int>();
            foreach (int number in numbers)
            {
                if (IsPrime(number))
                {
                    primes.Add(number);
                }
            }
            return primes;
        }
        static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        static List<int> GetFibonacciNumbers(List<int> numbers)
        {
            List<int> fibonacciNumbers = new List<int>();
            foreach (int number in numbers)
            {
                if (IsFibonacci(number))
                {
                    fibonacciNumbers.Add(number);
                }
            }
            return fibonacciNumbers;
        }
        static bool IsFibonacci(int number)
        {
            bool IsPerfectSquare(int x)
            {
                int s = (int)Math.Sqrt(x);
                return s * s == x;
            }
            return IsPerfectSquare(5 * number * number + 4) || IsPerfectSquare(5 * number * number - 4);
        }
        static void SaveToFile<T>(string fileName, List<T> data)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<T>));
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                serializer.WriteObject(stream, data);
            }
        }
    }
}
