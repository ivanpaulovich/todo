using System;
using System.IO;

namespace TodoList.ConsoleApp.Commands
{
    public class CommandParser
    {
        public ICommand ParseCommand(string[] args)
        {
            ICommand command;
            string text = string.Join(' ', args);

            command = ParseTodo(args, text);
            if (command != null)
                return command;
            
            command = ParseRemove(args);
            if (command != null)
                return command;

            command = ParseDo(args);
            if (command != null)
                return command;

            command = ParseUndo(args);
            if (command != null)
                return command;

            command = ParseList(args);
            if (command != null)
                return command;

            command = ParseRename(args, text);
            if (command != null)
                return command;
           
            return new HelpCommand();
        }

        private ICommand ParseTodo(string[] args, string text)
        {
            if (args.Length == 0)
                return null;

            if (string.Compare(args[0], "todo", StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            int separatorIndex = text.IndexOf(' ');

            if (separatorIndex <= 0)
                return null;

            string title = text.Substring(separatorIndex + 1);

            return new TodoCommand(title);
        }

        private ICommand ParseRemove(string[] args)
        {
            if (args.Length == 0)
                return null;

            if (string.Compare(args[0], "rm", StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            if (args.Length != 2)
                return null;

            return new RemoveCommand(args[1]);
        }

        private ICommand ParseDo(string[] args)
        {
            if (args.Length == 0)
                return null;

            if (string.Compare(args[0], "do", StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            if (args.Length != 2)
                return null;

            return new DoCommand(args[1]);
        }

        private ICommand ParseUndo(string[] args)
        {
            if (args.Length == 0)
                return null;

            if (string.Compare(args[0], "undo", StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            if (args.Length != 2)
                return null;

            return new UndoCommand(args[1]);
        }

        private ICommand ParseList(string[] args)
        {
            if (args.Length == 0)
                return null;

            if (string.Compare(args[0], "ls", StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            return new ListCommand();
        }

        private ICommand ParseRename(string[] args, string text)
        {
            if (args.Length == 0)
                return null;

            if (string.Compare(args[0], "ren", StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            if (args.Length < 3)
                return null;

            string id = args[1];

            int firstSeparatorIndex = text.IndexOf(' ');
            if (firstSeparatorIndex <= 0)
                return null;

            int secondSeparatorIndex = text.IndexOf(' ', firstSeparatorIndex);
            if (secondSeparatorIndex <= 0)
                return null;

            string title = text.Substring(secondSeparatorIndex + 1);

            return new RenameCommand(id, title);
        }
    }
}