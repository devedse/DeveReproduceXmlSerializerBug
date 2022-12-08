using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeveReproduceXmlSerializerBug.DeveConsoleMenu
{
    public class ConsoleMenu
    {
        public ConsoleMenuType ConsoleMenuType { get; set; }
        public int StartNumber { get; set; }
        public int? AutoSelectOption { get; set; }
        public int AutoSelectWaitTimerInSeconds { get; set; }
        public List<IConsoleMenuOption> MenuOptions { get; set; } = new List<IConsoleMenuOption>();


        public ConsoleMenu(ConsoleMenuType consoleMenuType, int startNumber = 1, int? autoSelectOption = null, int autoSelectWaitTimerInSeconds = 3)
        {
            if (autoSelectWaitTimerInSeconds > int.MaxValue / 1000)
            {
                throw new ArgumentException($"autoSelectWaitTimerInSeconds should be below {int.MaxValue / 1000}, it was {autoSelectWaitTimerInSeconds} instead.", nameof(autoSelectWaitTimerInSeconds));
            }

            ConsoleMenuType = consoleMenuType;
            StartNumber = startNumber;
            AutoSelectOption = autoSelectOption;
            AutoSelectWaitTimerInSeconds = autoSelectWaitTimerInSeconds;
        }

        public void RenderMenu()
        {
            var autoSelectString = AutoSelectOption != null ? $" Option {AutoSelectOption.Value} will be auto selected in {AutoSelectWaitTimerInSeconds} seconds" : "";
            Console.WriteLine($"Choose any option:{autoSelectString}");

            int maxLength = MenuOptions.Count.ToString().Length;

            for (int i = 0; i < MenuOptions.Count; i++)
            {
                var option = MenuOptions[i];
                Console.WriteLine($"  {$"{i + StartNumber}:".PadRight(maxLength, ' ')}  {option.Text}");
            }
        }

        public void WaitForResult()
        {
            WaitForResultAsync().Wait();
        }

        public async Task WaitForResultAsync()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            if (AutoSelectOption != null)
            {
                _ = Task.Run(async () =>
                {
                    await Task.Delay(AutoSelectWaitTimerInSeconds * 1000);
                    cancellationTokenSource.Cancel();
                });
            }

            while (true)
            {
                Console.WriteLine();
                Console.Write("Choose an option: ");

                string input = "";
                try
                {
                    if (ConsoleMenuType == ConsoleMenuType.KeyPress)
                    {
                        input = CancellableReadKey(cancellationTokenSource.Token);
                        Console.WriteLine();
                    }
                    else
                    {
                        input = CancellableReadLine(cancellationTokenSource.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    input = AutoSelectOption?.ToString() ?? "";
                    Console.WriteLine(input);
                }

                if (int.TryParse(input, out int result))
                {
                    int actualChoice = result - StartNumber;
                    if (actualChoice >= 0 && actualChoice < MenuOptions.Count)
                    {
                        await ExecuteOption(actualChoice);
                        return;
                    }
                }

                Console.WriteLine("Input is not valid, please choose any of the provided options");
            }
        }

        private Task ExecuteOption(int id)
        {
            var option = MenuOptions[id];

            switch (option)
            {
                case ConsoleMenuOption c:
                    c.ActionToExecute();
                    return Task.CompletedTask;
                case ConsoleMenuOptionAsync c:
                    return c.ActionToExecute();                    
                default:
                    throw new ArgumentException($"IConsoleMenuOption implementation not supported. Please use '{nameof(ConsoleMenuOption)}' or '{nameof(ConsoleMenuOptionAsync)}'.");
            }
        }


        public static string CancellableReadKey(CancellationToken cancellationToken)
        {
            while (!Console.KeyAvailable)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Thread.Sleep(50);
            }
            var keyInfo = Console.ReadKey();
            return keyInfo.KeyChar.ToString();
        }


        public static string CancellableReadLine(CancellationToken cancellationToken)
        {
            StringBuilder stringBuilder = new StringBuilder();

            ConsoleKeyInfo keyInfo;
            var startingLeft = Console.CursorLeft;
            var startingTop = Console.CursorTop;
            var currentIndex = 0;
            do
            {
                var previousLeft = Console.CursorLeft;
                var previousTop = Console.CursorTop;
                while (!Console.KeyAvailable)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(50);
                }
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.B:
                    case ConsoleKey.C:
                    case ConsoleKey.D:
                    case ConsoleKey.E:
                    case ConsoleKey.F:
                    case ConsoleKey.G:
                    case ConsoleKey.H:
                    case ConsoleKey.I:
                    case ConsoleKey.J:
                    case ConsoleKey.K:
                    case ConsoleKey.L:
                    case ConsoleKey.M:
                    case ConsoleKey.N:
                    case ConsoleKey.O:
                    case ConsoleKey.P:
                    case ConsoleKey.Q:
                    case ConsoleKey.R:
                    case ConsoleKey.S:
                    case ConsoleKey.T:
                    case ConsoleKey.U:
                    case ConsoleKey.V:
                    case ConsoleKey.W:
                    case ConsoleKey.X:
                    case ConsoleKey.Y:
                    case ConsoleKey.Z:
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Decimal:
                    case ConsoleKey.Add:
                    case ConsoleKey.Subtract:
                    case ConsoleKey.Multiply:
                    case ConsoleKey.Divide:
                    case ConsoleKey.D0:
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.NumPad5:
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.NumPad7:
                    case ConsoleKey.NumPad8:
                    case ConsoleKey.NumPad9:
                    case ConsoleKey.Oem1:
                    case ConsoleKey.Oem102:
                    case ConsoleKey.Oem2:
                    case ConsoleKey.Oem3:
                    case ConsoleKey.Oem4:
                    case ConsoleKey.Oem5:
                    case ConsoleKey.Oem6:
                    case ConsoleKey.Oem7:
                    case ConsoleKey.Oem8:
                    case ConsoleKey.OemComma:
                    case ConsoleKey.OemMinus:
                    case ConsoleKey.OemPeriod:
                    case ConsoleKey.OemPlus:
                        stringBuilder.Insert(currentIndex, keyInfo.KeyChar);
                        currentIndex++;
                        if (currentIndex < stringBuilder.Length)
                        {
                            var left = Console.CursorLeft;
                            var top = Console.CursorTop;
                            Console.Write(stringBuilder.ToString().Substring(currentIndex));
                            Console.SetCursorPosition(left, top);
                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (currentIndex > 0)
                        {
                            currentIndex--;
                            stringBuilder.Remove(currentIndex, 1);
                            var left = Console.CursorLeft;
                            var top = Console.CursorTop;
                            if (left == previousLeft)
                            {
                                left = Console.BufferWidth - 1;
                                top--;
                                Console.SetCursorPosition(left, top);
                            }
                            Console.Write(stringBuilder.ToString().Substring(currentIndex) + " ");
                            Console.SetCursorPosition(left, top);
                        }
                        else
                        {
                            Console.SetCursorPosition(startingLeft, startingTop);
                        }
                        break;
                    case ConsoleKey.Delete:
                        if (stringBuilder.Length > currentIndex)
                        {
                            stringBuilder.Remove(currentIndex, 1);
                            Console.SetCursorPosition(previousLeft, previousTop);
                            Console.Write(stringBuilder.ToString().Substring(currentIndex) + " ");
                            Console.SetCursorPosition(previousLeft, previousTop);
                        }
                        else
                            Console.SetCursorPosition(previousLeft, previousTop);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (currentIndex > 0)
                        {
                            currentIndex--;
                            var left = Console.CursorLeft - 2;
                            var top = Console.CursorTop;
                            if (left < 0)
                            {
                                left = Console.BufferWidth + left;
                                top--;
                            }
                            Console.SetCursorPosition(left, top);
                            if (currentIndex < stringBuilder.Length - 1)
                            {
                                Console.Write(stringBuilder[currentIndex].ToString() + stringBuilder[currentIndex + 1]);
                                Console.SetCursorPosition(left, top);
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(startingLeft, startingTop);
                            if (stringBuilder.Length > 0)
                                Console.Write(stringBuilder[0]);
                            Console.SetCursorPosition(startingLeft, startingTop);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentIndex < stringBuilder.Length)
                        {
                            Console.SetCursorPosition(previousLeft, previousTop);
                            Console.Write(stringBuilder[currentIndex]);
                            currentIndex++;
                        }
                        else
                        {
                            Console.SetCursorPosition(previousLeft, previousTop);
                        }
                        break;
                    case ConsoleKey.Home:
                        if (stringBuilder.Length > 0 && currentIndex != stringBuilder.Length)
                        {
                            Console.SetCursorPosition(previousLeft, previousTop);
                            Console.Write(stringBuilder[currentIndex]);
                        }
                        Console.SetCursorPosition(startingLeft, startingTop);
                        currentIndex = 0;
                        break;
                    case ConsoleKey.End:
                        if (currentIndex < stringBuilder.Length)
                        {
                            Console.SetCursorPosition(previousLeft, previousTop);
                            Console.Write(stringBuilder[currentIndex]);
                            var left = previousLeft + stringBuilder.Length - currentIndex;
                            var top = previousTop;
                            while (left > Console.BufferWidth)
                            {
                                left -= Console.BufferWidth;
                                top++;
                            }
                            currentIndex = stringBuilder.Length;
                            Console.SetCursorPosition(left, top);
                        }
                        else
                            Console.SetCursorPosition(previousLeft, previousTop);
                        break;
                    default:
                        Console.SetCursorPosition(previousLeft, previousTop);
                        break;
                }
            } while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine();

            return stringBuilder.ToString();
        }
    }
}
