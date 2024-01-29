using System;
using System.Text;
using System.Text.Json;
using PetLib;

namespace PetProject
{
    partial class Program
    {
        /// <summary>
        /// Метод отвечающий за сериализацию данных.
        /// </summary>
        /// <param name="mainMenu">Главное меню.</param>
        /// <returns>Успешна ли операция.</returns>
        static bool SerializeInFile(MainMenu mainMenu)
        {
            string menuInformation;
            menuInformation = JsonSerializer.Serialize(mainMenu);
            try
            {
                File.WriteAllText(@"data.json", menuInformation, Encoding.Unicode);
            }
            catch (System.IO.IOException)
            {
                ColorPrint(ConsoleColor.Red, "The file is in use by another program, please close the file and try again.");
                ColorPrint(ConsoleColor.Blue, "To repeat the program, press any key.");
                Console.ReadKey();
                Console.Clear();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод отвечащий за чтения содержимого файла.
        /// </summary>
        /// <returns>Содержимое файла в формате строки.</returns>
        static string ReadFile()
        {
            try
            {
                return File.ReadAllText(@"data.json");
            }
            catch (System.IO.FileNotFoundException)
            {
                ColorPrint(ConsoleColor.Red, "File not found, please try again.");
                ColorPrint(ConsoleColor.Blue, "To repeat the program, press any key.");
                Console.ReadKey();
                Console.Clear();
                return "!!!Exception!!!";
            }
            catch (System.IO.IOException)
            {
                ColorPrint(ConsoleColor.Red, "The file is in use by another program, please close the file and try again.");
                ColorPrint(ConsoleColor.Blue, "To repeat the program, press any key.");
                Console.ReadKey();
                Console.Clear();
                return "!!!Exception!!!";
            }
        }

        /// <summary>
        /// Метод конвертирующий строку данных в новый объект.
        /// </summary>
        /// <param name="data">Строка данных.</param>
        /// <param name="flag">Успешана ли операция.</param>
        /// <returns>Объект образованный с помощъю новых данных.</returns>
        static MainMenu ReadData(string data, ref bool flag)
        {
            try
            {
                return JsonSerializer.Deserialize<MainMenu>(data);
            }
            catch (System.Text.Json.JsonException)
            {
                ColorPrint(ConsoleColor.Red, "The file data is corrupted, please try again.");
                ColorPrint(ConsoleColor.Blue, "To repeat the program, press any key.");
                Console.ReadKey();
                Console.Clear();
                flag = true;
                return new MainMenu();
            }
        }

        /// <summary>
        /// Метод отвечающий за десериализацию данных.
        /// </summary>
        /// <param name="mainMenu">Объект образованный с помощъю новых данных.</param>
        /// <returns>Успешна ли операция.</returns>
        static bool Deserialize(out MainMenu mainMenu)
        {
            bool flag = false;
            string data = ReadFile();
            mainMenu = new MainMenu();
            if (data.Contains("!!!Exception!!!"))
                return true;
           mainMenu = ReadData(data, ref flag);
            if (flag)
                return true;
            return false;
        }
    }
}
