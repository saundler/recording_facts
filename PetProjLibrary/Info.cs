using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetLib
{
    /// <summary>
    /// Класс содержащий только одну строку для вывода.
    /// </summary>
    public class Info : Screen
    {
        /// <summary>
        /// Кострутор без параметров.
        /// </summary>
        public Info()
        {
            MainMessage = "";
            Number = 0;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="mainMessage">Основное сообщение.</param>
        /// <param name="number">Номер объекта.</param>
        public Info(string mainMessage, int number)
        {
            MainMessage = mainMessage;
            Number = number;
        }

        /// <summary>
        /// Метод отвечающий за взаимодействие с объектом.
        /// </summary>
        /// <param name="screen">Основной экран.</param>
        /// <param name="previous">Предыдущий экран.</param>
        /// <returns>Повторять ли цикл.</returns>
        public override bool Panel(ref Screen screen, ref Screen previous)
        {
            string command;    // Переменная для выбора команды.

            while (true)
            {
                ColorPrint("Enter command:", ConsoleColor.Blue);

                command = Console.ReadLine();

                // Условие возврата на предыдущий экран.
                if (command == "exit")
                {
                    screen = previous;
                    break;
                }
                // Условие редактирования основного сообщения.
                else if (command == "edit")
                {
                    ColorPrint("Enter new data:", ConsoleColor.Blue);
                    MainMessage = Console.ReadLine();
                    break;
                }
                // Условие завершения программы.
                else if (command == "off")
                    return false;

                ColorPrint("This command does not exist, please try again.", ConsoleColor.Red);
            }
            return true;
        }
    }
}
