using System;

namespace DeveReproduceXmlSerializerBug.DeveConsoleMenu
{
    public class ConsoleMenuOption : IConsoleMenuOption
    {
        public string Text { get; }
        public Action ActionToExecute { get; }

        public ConsoleMenuOption(string text, Action actionToExecute)
        {
            Text = text;
            ActionToExecute = actionToExecute;
        }
    }
}
