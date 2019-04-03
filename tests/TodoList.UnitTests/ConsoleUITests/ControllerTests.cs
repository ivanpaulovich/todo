namespace TodoList.UnitTests.ConsoleUITests
{
    using Moq;
    using TodoList.ConsoleApp.Commands;
    using TodoList.ConsoleApp.Controllers;
    using TodoList.Core.Boundaries;
    using Xunit;

    public sealed class ControllerTests
    {
        [Fact]
        public void TodoCommand_CallsTodo()
        {
            var todo = new Mock<IUseCase<Core.Boundaries.Todo.Request>>();
            var remove = new Mock<Core.Boundaries.Remove.IUseCase>();
            var list = new Mock<Core.Boundaries.List.IUseCase>();
            var rename = new Mock<IUseCase<Core.Boundaries.Rename.Request>>();
            var doUC = new Mock<Core.Boundaries.Do.IUseCase>();
            var undo = new Mock<Core.Boundaries.Undo.IUseCase>();

            TodoItemsController controller = new TodoItemsController(
                todo.Object,
                remove.Object,
                list.Object,
                rename.Object,
                doUC.Object,
                undo.Object,
                null);
            
            TodoCommand todoCommand = new TodoCommand(null);
            controller.Execute(todoCommand);

            RemoveCommand removeCommand = new RemoveCommand(null);
            controller.Execute(removeCommand);

            ListCommand listCommand = new ListCommand();
            controller.Execute(listCommand);

            RenameCommand renameCommand = new RenameCommand(null, null);
            controller.Execute(renameCommand);

            DoCommand doCommand = new DoCommand(null);
            controller.Execute(doCommand);

            UndoCommand undoCommand = new UndoCommand(null);
            controller.Execute(undoCommand);

            todo.Verify(x => x.Execute(It.IsAny<Core.Boundaries.Todo.Request>()), Times.Once);
            remove.Verify(x => x.Execute(It.IsAny<string>()), Times.Once);
            list.Verify(x => x.Execute(), Times.Once);
            rename.Verify(x => x.Execute(It.IsAny<Core.Boundaries.Rename.Request>()), Times.Once);
            doUC.Verify(x => x.Execute(It.IsAny<string>()), Times.Once);
            undo.Verify(x => x.Execute(It.IsAny<string>()), Times.Once);
        }
    }
}