namespace TodoList.UnitTests.ConsoleUITests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TodoList.Core.Boundaries.List;
    using Xunit;

    public sealed class ListPresenterTests : IClassFixture<ControllerFixture>
    {
        private readonly ControllerFixture _controllerFixture;

        public ListPresenterTests(ControllerFixture controllerFixture)
        {
            _controllerFixture = controllerFixture;
        }

        [Fact]
        public void PrintsError_WhenNull()
        {
            Response response = null;
            _controllerFixture.ListPresenter.Handle(response);
            string output = _controllerFixture.ConsoleWriter.GetOutput();
            Assert.Contains("error", output);
        }

        [Fact]
        public void PrintsWarning_WhenEmpty()
        {
            List<Item> items = new List<Item>();
            Response response = new Response(items);
            _controllerFixture.ListPresenter.Handle(response);
            string output = _controllerFixture.ConsoleWriter.GetOutput();
            Assert.Contains("list is empty", output);
        }

        [Fact]
        public void PrintsData()
        {
            List<Item> items = new List<Item>();
            items.Add(_controllerFixture.Item1);
            items.Add(_controllerFixture.Item2);
            Response response = new Response(items);
            _controllerFixture.ListPresenter.Handle(response);
            string output = _controllerFixture.ConsoleWriter.GetOutput();
            Assert.Contains(_controllerFixture.Item1.Title, output);
            Assert.Contains(_controllerFixture.Item2.Title, output);
        }
    }
}