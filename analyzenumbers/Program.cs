using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace analyzenumbers
{
    [Serializable]
    public class TextFile
    {
        public string Text { get; set; }
        public TextFile(string text)
        {
            Text = text;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу с числами:");
            string filePath = Console.ReadLine();
            AnalyzeFile(filePath);
        }

        static void AnalyzeFile(string filePath)
        {
            TextFile textfile = SerializeFile(filePath);
            List<int> numbers = ParseNumbers(textfile);
            Dictionary<string, List<int>> analysis = AnalyzeNumbers(numbers);
            SaveAnalysisToFile(analysis);
            Console.WriteLine($"Положительных чисел: {analysis["positives"].Count}");
            Console.WriteLine($"Отрицательных чисел: {analysis["negatives"].Count}");
            Console.WriteLine($"Двузначных чисел: {analysis["twoDigits"].Count}");
            Console.WriteLine($"Пятизначных чисел: {analysis["fiveDigits"].Count}");
        }

        static TextFile SerializeFile(string filePath)
        {
            string text = File.ReadAllText(filePath);
            TextFile textfile = new TextFile(text);
            string jsonFile = JsonSerializer.Serialize(textfile);
            File.WriteAllText("serialized_numbers.json", jsonFile);
            return textfile;
        }

        static List<int> ParseNumbers(TextFile textfile)
        {
            string[] numberStrings = textfile.Text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> numbers = new List<int>();
            foreach (string numberString in numberStrings)
            {
                if (int.TryParse(numberString, out int number))
                {
                    numbers.Add(number);
                }
            }
            return numbers;
        }
        static Dictionary<string, List<int>> AnalyzeNumbers(List<int> numbers)
        {
            Dictionary<string, List<int>> analysis = new Dictionary<string, List<int>>
            {
                { "positives", new List<int>() },
                { "negatives", new List<int>() },
                { "twoDigits", new List<int>() },
                { "fiveDigits", new List<int>() }
            };

            foreach (int number in numbers)
            {
                if (number > 0)
                {
                    analysis["positives"].Add(number);
                }
                if (number < 0)
                {
                    analysis["negatives"].Add(number);
                }
                if (Math.Abs(number) >= 10 && Math.Abs(number) < 100)
                {
                    analysis["twoDigits"].Add(number);
                }
                if (Math.Abs(number) >= 10000 && Math.Abs(number) < 100000)
                {
                    analysis["fiveDigits"].Add(number);
                }
            }
            return analysis;
        }

        static void SaveAnalysisToFile(Dictionary<string, List<int>> analysis)
        {
            File.WriteAllLines("positives.txt", analysis["positives"].ConvertAll(n => n.ToString()));
            File.WriteAllLines("negatives.txt", analysis["negatives"].ConvertAll(n => n.ToString()));
            File.WriteAllLines("two_digits.txt", analysis["twoDigits"].ConvertAll(n => n.ToString()));
            File.WriteAllLines("five_digits.txt", analysis["fiveDigits"].ConvertAll(n => n.ToString()));
        }
    }
}

