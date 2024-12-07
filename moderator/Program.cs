using System;
using System.IO;
using System.Text.Json;

namespace moderator
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
            Console.WriteLine("Введите путь к файлу с текстом:");
            string textFilePath = Console.ReadLine();
            Console.WriteLine("Введите путь к файлу со словами для модерирования:");
            string moderation = Console.ReadLine();
            ModerateTextFile(textFilePath, moderation);
            Console.WriteLine("Модерация завершена. Запрещённые слова заменены.");
        }

        static void ModerateTextFile(string textFilePath, string moderation)
        {
            TextFile textfile = SerializeFile(textFilePath);
            Moderate(textfile, moderation);
            SaveContentToFile(textFilePath, textfile);
        }

        static TextFile SerializeFile(string filePath)
        {
            string text = File.ReadAllText(filePath);
            TextFile textfile = new TextFile(text);
            string jsonFile = JsonSerializer.Serialize(textfile);
            File.WriteAllText("serialized_text.json", jsonFile);
            return textfile;
        }

        static void Moderate(TextFile textfile, string moderation)
        {
            string[] replacedWords = File.ReadAllLines(moderation);

            foreach (string word in replacedWords)
            {
                string replacement = new string('*', word.Length);
                textfile.Text = textfile.Text.Replace(word, replacement);
            }
        }

        static void SaveContentToFile(string filePath, TextFile textfile)
        {
            File.WriteAllText(filePath, textfile.Text);
        }
    }
}

