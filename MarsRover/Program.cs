using System;
using MarsRover.Commands;

namespace MarsRover
{
    class Program
    {
        private static readonly Rover _rover = new Rover(new SurfaceArea());
        private static readonly CommandParser _commandParser = new CommandParser();
        private static FlashMessage _flashMessage;

        private static void Main(string[] args)
        {
            MainMenu();

            var option = Console.ReadKey();

            while(option.Key != ConsoleKey.Q)
            {
                switch (option.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        EnterCommand();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        ExecuteCommands();
                        break;
                }

                MainMenu();

                option = Console.ReadKey();
            }

            Console.Clear();
        }

        private static void EnterCommand()
        {
            Console.WriteLine();
            Console.Write("Please enter command, followed by enter: ");

            var input = Console.ReadLine();

            var command = _commandParser.Parse(input);

            if (command != null)
            {
                try
                {
                    _rover.AddCommand(command);

                    _flashMessage = FlashMessage.Success("Command added successfully");
                }
                catch (Exception ex)
                {
                    _flashMessage = FlashMessage.Error(ex.Message);
                }
            }
            else
            {
                _flashMessage = FlashMessage.Warning("Invalid command");
            }
        }

        private static void ExecuteCommands()
        {
            try
            {
                var result = _rover.ExecuteCommands();

                _flashMessage = FlashMessage.Success($"Rover position: {result}");
            }
            catch (Exception ex)
            {
                _flashMessage = FlashMessage.Error(ex.Message);
            }
        }

        private static void MainMenu()
        {
            Console.Clear();

            Console.WriteLine("Mars Rover Simulation");
            Console.WriteLine();

            if (_flashMessage != null)
            {
                _flashMessage.Display();
                _flashMessage = null;

                Console.WriteLine();
            }

            Console.WriteLine("Please select an option:");
            Console.WriteLine();
            Console.WriteLine("1. Enter command");
            Console.WriteLine("2. Execute commands");
            Console.WriteLine("q. Quit");
        }
    }
}
