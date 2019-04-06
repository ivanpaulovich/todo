namespace TodoList.ConsoleApp.Commands
{
    using System;

    public sealed class CommandParser
    {
        public ICommand ParseCommand(string text)
        {
            ICommand command;
            command = ParseTodo(text);
            if (command != null)
                return command;

            command = ParseRemove(text);
            if (command != null)
                return command;

            command = ParseDo(text);
            if (command != null)
                return command;

            command = ParseUndo(text);
            if (command != null)
                return command;

            command = ParseList(text);
            if (command != null)
                return command;

            command = ParseRename(text);
            if (command != null)
                return command;

            command = ParseInteractive(text);
            if (command != null)
                return command;

            return new HelpCommand();
        }

        private TodoCommand ParseTodo(string text)
        {
            string[] args = text.Split(' ');
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

        private RemoveCommand ParseRemove(string text)
        {
            string[] args = text.Split(' ');
            if (args.Length == 0)
                return null;

            if (string.Compare(args[0], "rm", StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            if (args.Length != 2)
                return null;

            return new RemoveCommand(args[1]);
        }

        private DoCommand ParseDo(string text)
        {
            string[] args = text.Split(' ');
            if (args.Length == 0)
                return null;

            if (string.Compare(args[0], "do", StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            if (args.Length != 2)
                return null;

            return new DoCommand(args[1]);
        }

        private UndoCommand ParseUndo(string text)
        {
            string[] args = text.Split(' ');
            if (args.Length == 0)
                return null;

            if (string.Compare(args[0], "undo", StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            if (args.Length != 2)
                return null;

            return new UndoCommand(args[1]);
        }

        private ListCommand ParseList(string text)
        {
            string[] args = text.Split(' ');
            if (args.Length == 0)
                return null;

            if (string.Compare(args[0], "ls", StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            return new ListCommand();
        }

        private InteractiveCommand ParseInteractive(string text)
        {
            string[] args = text.Split(' ');
            if (args.Length == 0)
                return null;

            if (string.Compare(args[0], "-i", StringComparison.CurrentCultureIgnoreCase) != 0)
                return null;

            return new InteractiveCommand();
        }

        private RenameCommand ParseRename(string text)
        {
            string[] args = text.Split(' ');
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