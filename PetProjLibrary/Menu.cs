using System;
using System.Net.Mail;

namespace PetLib
{
    /// <summary>
    /// Класс содержащий лист типа Info.
    /// </summary>
    public class Menu : Screen
    {
        public List<Info> Infos { get; set; }   // Лист типа Info.

        /// <summary>
        /// Конструктор без параметров.
        /// </summary>
        public Menu()
        {
            MainMessage = "";
            Number = 0;
            Infos = new List<Info>();
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="mainMessage">Основное сообщение.</param>
        /// <param name="number">Номер объекта.</param>
        /// <param name="infos">Лист типа Info.</param>
        public Menu(string mainMessage, int number, List<Info> infos)
        {
            MainMessage = mainMessage;
            Number = number;
            Infos = new List<Info>(infos);
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
                       "exit   - Возврат в прошлое меню.\n" +
                       "rename - Изменение главного сообщения.\n" +
                       "delete - Удаление объекта из массива. (exit - Отмена удаления.)", ConsoleColor.Yellow);
            ColorPrint(MainMessage, ConsoleColor.Blue);
            for (int i = 0; i < Infos.Count; ++i)
            {
                Console.WriteLine($"[{i + 1}] {Infos[i]}");
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

                // Условие возврата на предыдущий экран.
                if (command == "exit")
                {
                    screen = previous;
                    break;
                }
                // Условие изменения основного сообщения.
                else if (command == "rename")
                {
                    ColorPrint("Enter a new name:", ConsoleColor.Blue);
                    MainMessage = Console.ReadLine();
                    break;
                }
                // Условие удаления строки из массива.
                else if (command == "delete")
                {
                    ColorPrint("Enter the number of the line to be deleted:", ConsoleColor.Blue);

                    // Выбор строки которую нужно удалить.
                    command = Console.ReadLine();
                    while (!(int.TryParse(command, out number) && 0 < number && number <= Infos.Count) && command != "exit")
                    {
                        ColorPrint("This line does not exist, please try again or to exit the delete menu, type \"exit\" command:", ConsoleColor.Red);
                        command = Console.ReadLine();
                    }

                    // Условие выхода из меню удаления.
                    if (command == "exit")
                    {
                        continue;
                    }

                    // Перемещение нижних строк.
                    Infos.RemoveAt(--number);
                    while (number < Infos.Count)
                    {
                        --Infos[number].Number;
                        ++number;
                    }
                    break;
                }
                // Условие добавления новой строки.
                else if (command == "add")
                {
                    ColorPrint("Enter the contents of the line:", ConsoleColor.Blue);
                    Infos.Add(new Info(Console.ReadLine(), Infos.Count));
                    break;
                }
                // Условие перехода в меню строки.
                else if (int.TryParse(command, out number) && 0 < number && number <= Infos.Count)
                {
                    screen = Infos[number - 1];
                    break;
                }
                // Условие завершения программы.
                else if (command == "off")
                    return false;
                ColorPrint("This command does not exist, please try again.", ConsoleColor.Red);
            }
            return true;
        }

        /// <summary>
        /// Метод конвертации в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public override string ToString()
        {
            string str = MainMessage + '\n';
            for (int i = 0; i < Infos.Count; ++i)
            {
                str += $" {i + 1}) " + Infos[i].ToString() + '\n';
            }
            return str;
        }
    }
}