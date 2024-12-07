using System;
using System.IO;
using System.Text.Json;

namespace replaceword
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
            Console.WriteLine("Введите путь к файлу текста:");
            string textFilePath = Console.ReadLine();
            Console.WriteLine("Введите слово для поиска:");
            string searchWord = Console.ReadLine();
            Console.WriteLine("Введите слово для замены:");
            string replaceWord = Console.ReadLine();
            int replaced = ReplaceWords(textFilePath, searchWord, replaceWord);
            Console.WriteLine($"Количество замененных слов: {replaced}");
        }
        static int ReplaceWords(string filePath, string searchWord, string replaceWord)
        {
            TextFile textfile = SerializeFile(filePath);
            int count = CountAndReplace(textfile, searchWord, replaceWord);
            SaveContentToFile(filePath, textfile);
            return count;
        }
        static TextFile SerializeFile(string filePath)
        {
            string text = File.ReadAllText(filePath);
            TextFile textfile = new TextFile(text);
            string jsonFile = JsonSerializer.Serialize(textfile);
            File.WriteAllText("serialized_text.json", jsonFile);
            return textfile;
        }
        static int CountAndReplace(TextFile textfile, string searchWord, string replaceWord)
        {
            int result = textfile.Text.Split(new string[] { searchWord }, StringSplitOptions.None).Length - 1;
            textfile.Text = textfile.Text.Replace(searchWord, replaceWord);
            return result;
        }
        static void SaveContentToFile(string filePath, TextFile fileContent)
        {
            File.WriteAllText(filePath, fileContent.Text);
        }
    }
}

