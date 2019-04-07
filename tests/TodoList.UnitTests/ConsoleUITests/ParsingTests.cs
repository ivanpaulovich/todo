namespace TodoList.UnitTests.ConsoleUITests
{
    using TodoList.ConsoleApp.Commands;
    using Xunit;

    public sealed class ParsingTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("-h")]
        [InlineData("/h")]
        [InlineData("--help")]
        [InlineData("h")]
        [InlineData("help")]
        [InlineData(" help ")]
        [InlineData("-u", "33333", "aaaa")]
        [InlineData("--interactive", "ls")]
        [InlineData("--do", "333333", "a")]
        [InlineData("/l", "test")]
        public void HelpCommand(params string[] args)
        {
            CommandParser sut = new CommandParser();
            ICommand actual = sut.ParseCommand(args);
            Assert.IsType<HelpCommand>(actual);
        }

        [Theory]
        [InlineData("-i")]
        [InlineData("/i")]
        [InlineData("--interactive")]
        [InlineData("i")]
        [InlineData("interactive")]
        [InlineData(" interactive ")]
        public void InteractiveCommand(params string[] args)
        {
            CommandParser sut = new CommandParser();
            ICommand actual = sut.ParseCommand(args);
            Assert.IsType<InteractiveCommand>(actual);
        }

        [Theory]
        [InlineData("-rm", "33333")]
        [InlineData("/rm", "33333")]
        [InlineData("--remove", "333333")]
        [InlineData("rm", "3333333")]
        [InlineData("remove", "333333")]
        [InlineData(" remove ", "33333333")]
        public void RemoveCommand(params string[] args)
        {
            CommandParser sut = new CommandParser();
            ICommand actual = sut.ParseCommand(args);
            Assert.IsType<RemoveCommand>(actual);
        }

        [Theory]
        [InlineData("-d", "33333")]
        [InlineData("/d", "33333")]
        [InlineData("--do", "333333")]
        [InlineData("do", "3333333")]
        [InlineData(" do ", "33333333")]
        public void DoCommand(params string[] args)
        {
            CommandParser sut = new CommandParser();
            ICommand actual = sut.ParseCommand(args);
            Assert.IsType<DoCommand>(actual);
        }

        [Theory]
        [InlineData("-u", "33333")]
        [InlineData("/u", "33333")]
        [InlineData("--undo", "333333")]
        [InlineData("undo", "3333333")]
        [InlineData(" undo ", "33333333")]
        public void UndoCommand(params string[] args)
        {
            CommandParser sut = new CommandParser();
            ICommand actual = sut.ParseCommand(args);
            Assert.IsType<UndoCommand>(actual);
        }

        [Theory]
        [InlineData("-l")]
        [InlineData("/l")]
        [InlineData("-ls")]
        [InlineData("/ls")]
        [InlineData("--list")]
        [InlineData("list")]
        [InlineData(" list ")]
        public void ListCommand(params string[] args)
        {
            CommandParser sut = new CommandParser();
            ICommand actual = sut.ParseCommand(args);
            Assert.IsType<ListCommand>(actual);
        }

        [Theory]
        [InlineData("-r", "33333", "New Title")]
        [InlineData("-ren", "33333", "New Title")]
        [InlineData("/ren", "33333", "New Title")]
        [InlineData("--rename", "333333", "New Title")]
        [InlineData("ren", "3333333", "New Title")]
        [InlineData(" rename ", "33333333", "New Title")]
        public void RenameCommand(params string[] args)
        {
            CommandParser sut = new CommandParser();
            ICommand actual = sut.ParseCommand(args);
            Assert.IsType<RenameCommand>(actual);
        }

        [Theory]
        [InlineData("My Task")]
        [InlineData("/Task")]
        [InlineData("Test")]
        [InlineData("My task help")]
        public void TodoCommand(params string[] args)
        {
            CommandParser sut = new CommandParser();
            ICommand actual = sut.ParseCommand(args);
            Assert.IsType<TodoCommand>(actual);
        }

        [Theory]
        [InlineData("ls")]
        public void CommandLineArgs(string line)
        {
            string[] args = CommandArgsParser.TokenizeCommandLineToStringArray(line);
            Assert.NotEmpty(args);
            Assert.Equal("ls", args[0]);
        }
    }
}