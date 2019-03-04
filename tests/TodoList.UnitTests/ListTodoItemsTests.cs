namespace TodoList.UnitTests
{
    using System;
    using Moq;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.UseCases;
    using TodoList.Core.UseCases.ListTodoItems;
    using Xunit;

    public sealed class ListTodoItemsTests
    {
        [Fact]
        public void GivenNullContext_ThrowsException () {
            var context = new DBContext ();
            var gateway = new TodoItemGateway (context);
            var outputHandler = new Mock<IUseCaseOutputHandler<Output>> ();

            IUseCase list = new Interactor (outputHandler.Object, gateway);

            list.Execute ();
        }
    }
}