namespace TodoList.UnitTests.ConsoleUITests
{
    using System;
    using System.Threading.Tasks;
    using TodoList.Core.Boundaries.Todo;
    using Xunit;

    public sealed class TodoPresenterTests : IClassFixture<ControllerFixture>
    {
        private readonly ControllerFixture _controllerFixture;

        public TodoPresenterTests(ControllerFixture controllerFixture)
        {
            _controllerFixture = controllerFixture;
        }

        [Fact]
        public void PrintsError_WhenNull()
        {
            Response response = null;
            _controllerFixture.TodoPresenter.Handle(response);
            string output = _controllerFixture.ConsoleWriter.GetOutput();
            Assert.Contains("error", output);
        }

        [Fact]
        public void PrintsWarning_WhenInvalid()
        {
            Response response = new Response(Guid.Empty);
            _controllerFixture.TodoPresenter.Handle(response);
            string output = _controllerFixture.ConsoleWriter.GetOutput();
            Assert.Contains("not added", output);
        }

        [Fact]
        public void PrintsData()
        {
            Response response = new Response(_controllerFixture.ItemId1);
            _controllerFixture.TodoPresenter.Handle(response);
            string output = _controllerFixture.ConsoleWriter.GetOutput();
            Assert.Contains("af15e64c", output);
        }
    }
}