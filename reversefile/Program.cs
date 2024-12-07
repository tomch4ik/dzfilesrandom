using System;
using System.IO;
using System.Text.Json;

namespace reversefile
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
            Console.WriteLine("Введите путь к файлу:");
            string filePath = Console.ReadLine();
            string newFilePath = ReverseFileContent(filePath);
            Console.WriteLine($"Новый файл с реверснутым текстом: {newFilePath}");
        }
        static string ReverseFileContent(string filePath)
        {
            TextFile textfile = SerializeFile(filePath);
            ReverseText(textfile);
            string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), "reversed_" + Path.GetFileName(filePath));
            SaveContentToFile(newFilePath, textfile);
            return newFilePath;
        }
        static TextFile SerializeFile(string filePath)
        {
            string text = File.ReadAllText(filePath);
            TextFile textfile = new TextFile(text);
            string jsonFile = JsonSerializer.Serialize(textfile);
            File.WriteAllText("serialized_text.json", jsonFile);
            return textfile;
        }
        static void ReverseText(TextFile textfile)
        {
            char[] reversedArray = textfile.Text.ToCharArray();
            Array.Reverse(reversedArray);
            textfile.Text = new string(reversedArray);
        }
        static void SaveContentToFile(string filePath, TextFile textfile)
        {
            File.WriteAllText(filePath, textfile.Text);
        }
    }
}

