using System;

namespace MarsRover
{
    public class FlashMessage
    {
        public ConsoleColor Color { get; set; } = ConsoleColor.Gray;

        public string Text { get; set; }

        public void Display()
        {
            Console.ForegroundColor = Color;

            Console.WriteLine(Text);

            Console.ResetColor();
        }

        public static FlashMessage Info(string text)
        {
            return new FlashMessage { Color = ConsoleColor.Blue, Text = text };
        }

        public static FlashMessage Error(string text)
        {
            return new FlashMessage {Color = ConsoleColor.Red, Text = text};
        }

        public static FlashMessage Success(string text)
        {
            return new FlashMessage { Color = ConsoleColor.DarkGreen, Text = text };
        }

        public static FlashMessage Warning(string text)
        {
            return new FlashMessage { Color = ConsoleColor.Yellow, Text = text };
        }
    }
}
