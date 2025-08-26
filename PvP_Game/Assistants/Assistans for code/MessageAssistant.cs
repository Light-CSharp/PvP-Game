namespace PvP_Game
{
    public static class MessageAssistant
    {
        /// <summary>
        /// Выводит сообщение зелёного цвета, нужен для обозначения успешности действия.
        /// </summary>
        /// <param name="message"></param>
        public static void GreenMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Выводит сообщение красного цвета, нужен для обозначения неудачности действия.
        /// </summary>
        /// <param name="message"></param>
        public static void RedMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Выводит сообщение синего цвета, нужен для обозначения сведений.
        /// </summary>
        /// <param name="message"></param>
        public static void BlueMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Выравнивание введённого текста, в зависимости от максимальной длины текста
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        public static void WriteCentered(string text, int width)
        {
            if (text.Length >= width)
            {
                Console.WriteLine(text);
                return;
            }

            int leftPadding = (width - text.Length) / 2;
            Console.WriteLine(new string(' ', leftPadding) + text);
        }
    }
}