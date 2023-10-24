using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppForTest
{
    class Program
    {

        /// <summary>
        /// Главная функция приложения, предоставляющая пользователю выбор из трех действий: 
        /// 1 - создание текстовых файлов,  2 - чтение и обработка файлов, 3 - завершение программы.
        /// </summary>
        /// <param name="args">Аргументы командной строки, переданные при запуске программы.</param>

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите действие (1, 2, 3):");
                Console.WriteLine("1. Создать текстовые файлы");
                Console.WriteLine("2. Прочитать и обработать файлы");
                Console.WriteLine("3. Выйти из программы");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateTextFiles();
                        break;
                    case "2":
                        ProcessTextFiles();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nНекорректный выбор. Пожалуйста, выберите 1, 2 или 3.");
                        break;
                }
            }
        }

        /// <summary>
        /// Создает текстовые файлы со случайными числами в указанном пользователем каталоге.
        /// </summary>
        static void CreateTextFiles()
        {
            Console.WriteLine("\nВведите путь к каталогу, в котором вы хотите создать файлы:");
            string? directoryPath = Console.ReadLine();

            if (Directory.Exists(directoryPath))
            {
                Console.WriteLine("\nВведите количество файлов для создания:");
                if (int.TryParse(Console.ReadLine(), out int numberOfFiles) && numberOfFiles > 0)
                {
                    Random random = new Random();
                    Console.WriteLine("");
                    for (int i = 1; i <= numberOfFiles; i++)
                    {
                        string fileName = $"file_{i}.txt";
                        string filePath = Path.Combine(directoryPath, fileName);

                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            int numberOfLines = random.Next(100, 1001);
                            for (int j = 0; j < numberOfLines; j++)
                            {
                                int randomNumber = random.Next(100, 1001);
                                writer.WriteLine(randomNumber);
                            }
                        }
                        Console.WriteLine($"Файл {fileName} создан");
                    }
                }
                else
                {
                    Console.WriteLine("\nНекорректное количество файлов.");
                }
            }
            else
            {
                Console.WriteLine("\nУказанный каталог не существует.");
            }
        }

        /// <summary>
        /// Обрабатывает текстовые файлы в указанном каталоге, извлекая уникальные числа, 
        /// которые при делении на 4 дают остаток 3, и сохраняет их в файле "result.txt".
        /// </summary>
        static void ProcessTextFiles()
        {
            Console.WriteLine("\nВведите путь к каталогу с текстовыми файлами:");
            string? directoryPath = Console.ReadLine();

            if (Directory.Exists(directoryPath))
            {
                List<int> uniqueNumbers = new List<int>();
                string[] filePaths = Directory.GetFiles(directoryPath, "*.txt");
                Console.WriteLine("");
                foreach (string filePath in filePaths)
                {
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    Console.WriteLine($"Файл {fileName} прочитан");
                    string[] lines = File.ReadAllLines(filePath);

                    foreach (string line in lines)
                    {
                        if (int.TryParse(line, out int number))
                        {
                            if (!uniqueNumbers.Contains(number) && number % 4 == 3)
                            {
                                uniqueNumbers.Add(number);
                            }
                        }
                    }
                }
                uniqueNumbers.Sort((a, b) => b.CompareTo(a));

                string resultFilePath = Path.Combine(directoryPath, "result.txt");
                File.WriteAllLines(resultFilePath, uniqueNumbers.Select(n => n.ToString()));

                Console.WriteLine("\nРезультат сохранен в файле result.txt");
            }
            else
            {
                Console.WriteLine("\nУказанный каталог не существует.");
            }
        }
    }
}
