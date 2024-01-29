using System.IO;
using System.Reflection;
using System.Collections.Generic;
using PetLib;

namespace PetProject
{
    partial class Program
    {
        /// <summary>
        /// Метод выводяший цветную строку в консоль.
        /// </summary>
        /// <param name="color">Цвет строки.</param>
        /// <param name="str">Строка.</param>
        public static void ColorPrint(ConsoleColor color, string str)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        static void Main()
        {
            MainMenu mainMenu = new MainMenu("Main menu: ", 0, new List<Menu>());   // Главное меню.

            // Чтение данных из файла.
            while (Deserialize(out mainMenu)) { }
            Menu menu = new Menu();     // Переменная хранящая в себе нынешнее меню.
            Screen screen = mainMenu;   // Переменная отвечающая за вывод данных на экран.
            Screen previous = screen;   // Переменная хранящая данные предыдущего экрана.
            bool flag = true;           // Переменная отвечающая за повтор цикла.
            do
            {
                // Вывод данных на экран консоли.
                screen.Print();

                // Выбор действий в соотвествии с типом экрана.
                if (screen is MainMenu)
                {
                    flag = screen.Panel(ref screen, ref screen);
                }
                else if (screen is Menu)
                {
                    menu = (Menu)screen;
                    previous = mainMenu;
                    screen.Panel(ref screen, ref previous);
                }
                else
                {
                    previous = menu;
                    screen.Panel(ref screen, ref previous);
                }

            } while (flag);

            // Запись данных в файл.
            while (SerializeInFile(mainMenu)) { }

            // Завершение работы.
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 9, Console.WindowHeight / 2 - 3);
            Console.WriteLine("Goodbye!");
            Console.ForegroundColor = ConsoleColor.Blue;
        }
    }
}