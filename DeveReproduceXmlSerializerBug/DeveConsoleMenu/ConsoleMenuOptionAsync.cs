using System;
using System.Threading.Tasks;

namespace DeveReproduceXmlSerializerBug.DeveConsoleMenu
{
    public class ConsoleMenuOptionAsync : IConsoleMenuOption
    {
        public string Text { get; }
        public Func<Task> ActionToExecute { get; }

        public ConsoleMenuOptionAsync(string text, Func<Task> actionToExecute)
        {
            Text = text;
            ActionToExecute = actionToExecute;
        }
    }
}
