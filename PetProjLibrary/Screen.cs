using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetLib
{
    /// <summary>
    /// Класс отвечающий за вывод объектов.
    /// </summary>
    public abstract class Screen
    {
        public string MainMessage { get; set; }     // Основной текст.
        public int Number { get; set; }     = 0;    // Номер объекта.

        /// <summary>
        /// Метод отвечающий за вывод объекта.
        /// </summary>
        public virtual void Print()
        {
            Console.Clear();
            ColorPrint("Команды:\n" +
                       "off  - Завершение работы программы.\n" +
                       "exit - Возврат в прошлое меню.\n" +
                       "edit - Редактирование основного сообщения.", ConsoleColor.Yellow);
            Console.WriteLine(MainMessage);
        }

        /// <summary>
        /// Метод отвечающий за взаимодействие с объектом.
        /// </summary>
        /// <param name="screen">Основной экран.</param>
        /// <param name="previous">Предыдущий экран.</param>
        /// <returns>Повторять ли цикл.</returns>
        public abstract bool Panel(ref Screen screen, ref Screen previous);

        /// <summary>
        /// Метод выводяший цветную строку в консоль.
        /// </summary>
        /// <param name="color">Цвет строки.</param>
        /// <param name="str">Строка.</param>
        public static void ColorPrint(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }

        /// <summary>
        /// Метод конвертации в строку.
        /// </summary>
        /// <returns>Строка данных.</returns>
        public override string ToString()
        {
            return MainMessage;
        }
    }
}
