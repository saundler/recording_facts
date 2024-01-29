using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PetLib
{
    /// <summary>
    /// Класс содержащий лист типа Menu.
    /// </summary>
    public class MainMenu : Screen
    {
        public List<Menu> Menus { get; set; }   // Лист типа Menu.

        /// <summary>
        /// Констурктор без параметров.
        /// </summary>
        public MainMenu()
        {
            MainMessage = "";
            Number = 0;
            Menus = new List<Menu>();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="mainMessage">Основное сообщение.</param>
        /// <param name="number">Номер объекта.</param>
        /// <param name="menus">Лист типа Menu.</param>
        public MainMenu(string mainMessage, int number, List<Menu> menus)
        {
            MainMessage = mainMessage;
            Number = number;
            Menus = new List<Menu>(menus);
        }

        /// <summary>
        /// Метод отвечающий за вывод объекта.
        /// </summary>
        public override void Print()
        {
            Console.Clear();
            ColorPrint("Команды:\n" +
                       "n      - Переход в меню объекта. (\"[n] Объект\", n - Не буква, а номер объекта)\n" +
                       "off    - Завершение работы программы.\n" +
                       "add    - Добавление объекта в массив.\n" +
                       "delete - Удаление объекта из массива. (exit - Отмена удаления.)" , ConsoleColor.Yellow);
            ColorPrint(MainMessage, ConsoleColor.Blue);
            for (int i = 0; i < Menus.Count; ++i)
            {
                Console.WriteLine($"[{i + 1}] {Menus[i]}");
            }
        }

        /// <summary>
        /// Метод отвечающий за взаимодействие с объектом.
        /// </summary>
        /// <param name="screen">Основной экран.</param>
        /// <param name="previous">Предыдущий экран.</param>
        /// <returns>Повторять ли цикл.</returns>
        public override bool Panel(ref Screen screen, ref Screen previous)
        {
            int number = 0;    // Перменная для выбора строки.
            string command;    // Переменная для выбора команды.

            while (true)
            {
                ColorPrint("Enter command:", ConsoleColor.Blue);

                command = Console.ReadLine();
                
                // Условие завершения программы.
                if (command == "off")
                {
                    return false;
                }
                // Условие удаления объекта из массива.
                else if (command == "delete")
                {
                    ColorPrint("Enter the number of the line to be deleted:", ConsoleColor.Blue);

                    // Выбор объекта который необходимо удалить.
                    command = Console.ReadLine();
                    while (!(int.TryParse(command, out number) && 0 < number && number <= Menus.Count) && command != "exit")
                    {
                        ColorPrint("This line does not exist, please try again or to exit the delete menu, type \"exit\" command:", ConsoleColor.Red);
                        command = Console.ReadLine();
                    }

                    // Усливие выхода из меню удаления.
                    if (command == "exit")
                    {
                        continue;
                    }

                    // Перемещение нижних строк.
                    Menus.RemoveAt(--number);
                    while (number < Menus.Count)
                    {
                        --Menus[number].Number;
                        ++number;
                    }
                    break;
                }
                // Условие добавления нового объекта.
                else if (command == "add")
                {
                    ColorPrint("Enter the contents of the line:", ConsoleColor.Blue);
                    Menus.Add(new Menu(Console.ReadLine(), Menus.Count, new List<Info>()));
                    break;
                }
                // Условие перехода в меню строки.
                else if (int.TryParse(command, out number) && 0 < number && number <= Menus.Count)
                {
                    screen = Menus[number - 1];
                    break;
                }

                ColorPrint("This command does not exist, please try again.", ConsoleColor.Red);
            }
            return true;
        }
    }
}
