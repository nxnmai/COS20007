using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class CommandProcessor
    {
        private List<Command> _commands;

        public CommandProcessor()
        {
            _commands = new List<Command>();
        }

        public void AddCommand(Command command)
        {
            _commands.Add(command);
        }

        public string Execute(Player player, string[] text)
        {
            if (text == null || text.Length == 0)
            {
                return "No command provided.";
            }

            string commandWord = text[0].ToLower();
            foreach (Command command in _commands)
            {
                if (command.AreYou(commandWord))
                {
                    return command.Execute(player, text);
                }
            }

            return $"I don't understand the command '{commandWord}'.";
        }
    }
}
