namespace TodoList.UnitTests.ConsoleUITests
{
    using System;
    using System.Threading.Tasks;
    using TodoList.Core.Boundaries.Todo;
    using Xunit;

    public sealed class TodoPresenterTests : IClassFixture<ControllerFixture>
    {
        private ControllerFixture _controllerFixture;

        public TodoPresenterTests(ControllerFixture controllerFixture)
        {
            _controllerFixture = controllerFixture;
        }

        [Fact]
        public async Task PrintsError_WhenNull()
        {
            Response response = null;
            _controllerFixture.TodoPresenter.Handle(response);
            string output = await _controllerFixture.ConsoleWriter.GetOutput();
            Assert.Contains("error", output);
        }

        [Fact]
        public async Task PrintsWarning_WhenInvalid()
        {
            Response response = new Response(Guid.Empty);
            _controllerFixture.TodoPresenter.Handle(response);
            string output = await _controllerFixture.ConsoleWriter.GetOutput();
            Assert.Contains("not added", output);
        }

        [Fact]
        public async Task PrintsData()
        {
            Response response = new Response(_controllerFixture.ItemId1);
            _controllerFixture.TodoPresenter.Handle(response);
            string output = await _controllerFixture.ConsoleWriter.GetOutput();
            Assert.Contains("af15e64c", output);
        }
    }
}