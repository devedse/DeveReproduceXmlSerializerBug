using DeveReproduceXmlSerializerBug.DeveConsoleMenu;
using DeveReproduceXmlSerializerBug.Streams;
using System;
using System.IO;

namespace DeveReproduceXmlSerializerBug.ConsoleApp
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TestConsoleOut();
            TestConsoleMenu();
        }

        private static void TestConsoleOut()
        {
            var mov = new MovingMemoryStream(400);

            var writer = new StreamWriter(mov)
            {
                AutoFlush = true
            };
            var reader = new StreamReader(mov);

            var longstring = new string('a', 1500);
            writer.Write("B");
            writer.WriteLine("L");
            writer.WriteLine(longstring);
            writer.WriteLine("Hello");
            writer.WriteLine("Devedse");
            writer.WriteLine("Test");

            int i = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine($"{i}: {line}");
                i++;
            }

            Console.WriteLine();
        }

        private static void TestConsoleMenu()
        {
            var running = true;
            var menu = new ConsoleMenu(ConsoleMenuType.StringInput, 1, 1, 3);

            menu.MenuOptions.Add(new ConsoleMenuOption("Do something", () => Console.WriteLine("This is the first option")));
            menu.MenuOptions.Add(new ConsoleMenuOption("Do something else", () => Console.WriteLine("This is the second option")));
            menu.MenuOptions.Add(new ConsoleMenuOption("Do something 3", () => Console.WriteLine("This is the third option")));
            menu.MenuOptions.Add(new ConsoleMenuOption("Exit", () => running = false));

            while (running)
            {
                menu.RenderMenu();
                menu.WaitForResult();
                Console.WriteLine();
                Console.WriteLine();

                //if (menu.ConsoleMenuType == ConsoleMenuType.KeyPress)
                //{
                //    menu.ConsoleMenuType = ConsoleMenuType.StringInput;
                //}
                //else
                //{
                //    menu.ConsoleMenuType = ConsoleMenuType.KeyPress;
                //}
            }
        }
    }
}
