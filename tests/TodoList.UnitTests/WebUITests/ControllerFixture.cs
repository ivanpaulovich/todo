namespace TodoList.UnitTests.WebUITests
{
    using System;
    using Moq;
    using TodoList.Core.Boundaries.List;
    using TodoList.Core.Boundaries;
    using TodoList.WebApi.Controllers;

    public sealed class ControllerFixture
    {
        public TodoItemsController Controller { get; }
        public ListPresenter ListPresenter { get; }
        public TodoPresenter TodoPresenter { get; }
        public Item Item1 = new Item(new Guid("af15e64c-94b0-4220-b49c-231824f1711c"), "Title1", false);
        public Item Item2 = new Item(new Guid("cf1f2395-43ba-461a-9ff0-943b3fd16e56"), "Title2", true);
        public Guid ItemId1 = new Guid("af15e64c-94b0-4220-b49c-231824f1711c");

        public ControllerFixture()
        {
            var todo = new Mock<IUseCase<Core.Boundaries.Todo.Request>>();
            var remove = new Mock<Core.Boundaries.Remove.IUseCase>();
            var list = new Mock<Core.Boundaries.List.IUseCase>();
            var rename = new Mock<IUseCase<Core.Boundaries.Rename.Request>>();
            var doUC = new Mock<Core.Boundaries.Do.IUseCase>();
            var undo = new Mock<Core.Boundaries.Undo.IUseCase>();

            ListPresenter = new ListPresenter();
            TodoPresenter = new TodoPresenter();
            Controller = new TodoItemsController(
                todo.Object,
                remove.Object,
                list.Object,
                rename.Object,
                doUC.Object,
                undo.Object,
                TodoPresenter,
                ListPresenter);
        }
    }
}