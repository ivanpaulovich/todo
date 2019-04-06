namespace TodoList.UnitTests.ConsoleUITests
{
    using Moq;
    using TodoList.ConsoleApp.Commands;
    using Xunit;

    public sealed class ControllerTests : IClassFixture<ControllerFixture>
    {
        private ControllerFixture _controllerFixture;

        public ControllerTests(ControllerFixture controllerFixture)
        {
            _controllerFixture = controllerFixture;
        }

        [Fact]
        public void CallsTodoWhenTodoCommand()
        {
            TodoCommand todoCommand = new TodoCommand(null);
            _controllerFixture.Controller.Execute(todoCommand);
            _controllerFixture.Todo.Verify(x => x.Execute(It.IsAny<Core.Boundaries.Todo.Request>()), Times.Once);
        }

        [Fact]
        public void CallsRemoveWhenRemoveCommand()
        {
            RemoveCommand removeCommand = new RemoveCommand(null);
            _controllerFixture.Controller.Execute(removeCommand);
            _controllerFixture.Remove.Verify(x => x.Execute(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CallsListWhenListCommand()
        {
            _controllerFixture.Controller.List();
            _controllerFixture.List.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void CallsRenameWhenRenameCommand()
        {
            RenameCommand renameCommand = new RenameCommand(null, null);
            _controllerFixture.Controller.Execute(renameCommand);
            _controllerFixture.Rename.Verify(x => x.Execute(It.IsAny<Core.Boundaries.Rename.Request>()), Times.Once);
        }

        [Fact]
        public void CallsDoWhenDoCommand()
        {
            DoCommand doCommand = new DoCommand(null);
            _controllerFixture.Controller.Execute(doCommand);
            _controllerFixture.Do.Verify(x => x.Execute(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void CallsUndoWhenUndoCommand()
        {
            UndoCommand undoCommand = new UndoCommand(null);
            _controllerFixture.Controller.Execute(undoCommand);
            _controllerFixture.Undo.Verify(x => x.Execute(It.IsAny<string>()), Times.Once);
        }
    }
}