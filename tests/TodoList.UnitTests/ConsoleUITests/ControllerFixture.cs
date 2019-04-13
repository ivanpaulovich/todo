namespace TodoList.UnitTests.ConsoleUITests
{
    using System;
    using Moq;
    using TodoList.ConsoleApp.Controllers;
    using TodoList.Core.Boundaries;

    public sealed class ControllerFixture
    {
        public Mock<IUseCase<Core.Boundaries.Todo.Request>> Todo { get; }
        public Mock<Core.Boundaries.Remove.IUseCase> Remove { get; }
        public Mock<Core.Boundaries.List.IUseCase> List { get; }
        public Mock<IUseCase<Core.Boundaries.Rename.Request>> Rename { get; }
        public Mock<Core.Boundaries.Do.IUseCase> Do { get; }
        public Mock<Core.Boundaries.Undo.IUseCase> Undo { get; }
        public TodoItemsController Controller { get; }
        public ListPresenter ListPresenter { get; }
        public TodoPresenter TodoPresenter { get; }
        public Core.Boundaries.List.Item Item1 { get; }
        public Core.Boundaries.List.Item Item2 { get; }
        public Guid ItemId1 { get; }
        public ConsoleWriter ConsoleWriter { get; }

        public ControllerFixture()
        {
            ConsoleWriter = ConsoleWriter.SharedConsoleWriter;

            Todo = new Mock<IUseCase<Core.Boundaries.Todo.Request>>();
            Remove = new Mock<Core.Boundaries.Remove.IUseCase>();
            List = new Mock<Core.Boundaries.List.IUseCase>();
            Rename = new Mock<IUseCase<Core.Boundaries.Rename.Request>>();
            Do = new Mock<Core.Boundaries.Do.IUseCase>();
            Undo = new Mock<Core.Boundaries.Undo.IUseCase>();

            ListPresenter = new ListPresenter();
            TodoPresenter = new TodoPresenter();
            Controller = new TodoItemsController(
                Todo.Object,
                Remove.Object,
                List.Object,
                Rename.Object,
                Do.Object,
                Undo.Object);

            Item1 = new Core.Boundaries.List.Item(
                new Guid("af15e64c-94b0-4220-b49c-231824f1711c"),
                "Title1",
                false);

            Item2 = new Core.Boundaries.List.Item(
                new Guid("cf1f2395-43ba-461a-9ff0-943b3fd16e56"),
                "Title2",
                true);

            ItemId1 = new Guid("af15e64c-94b0-4220-b49c-231824f1711c");
        }
    }
}