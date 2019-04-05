namespace TodoList.UnitTests.WebUITests
{
    using System.Collections.Generic;
    using TodoList.Core.Boundaries.List;
    using Xunit;

    public sealed class ListPresenterTests : IClassFixture<ControllerFixture>
    {
        private ControllerFixture _controllerFixture;

        public ListPresenterTests(ControllerFixture controllerFixture)
        {
            _controllerFixture = controllerFixture;
        }

        [Fact]
        public void ReturnsBadRequest_WhenNull()
        {
            Response response = null;
            _controllerFixture.ListPresenter.Handle(response);

            var actual = _controllerFixture
                .ListPresenter
                .BuildResponse(_controllerFixture.Controller);

            Assert.NotNull(actual);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(actual.Result);
        }

        [Fact]
        public void ReturnsEmpty_WhenEmpty()
        {
            List<Item> items = new List<Item>();
            Response response = new Response(items);
            _controllerFixture.ListPresenter.Handle(response);

            var actual = _controllerFixture
                .ListPresenter
                .BuildResponse(_controllerFixture.Controller);

            Assert.NotNull(actual);
            Assert.Empty(actual.Value);
        }

        [Fact]
        public void PrintsData()
        {
            List<Item> items = new List<Item>();
            items.Add(_controllerFixture.Item1);
            items.Add(_controllerFixture.Item2);
            Response response = new Response(items);
            _controllerFixture.ListPresenter.Handle(response);

            var actual = _controllerFixture
                .ListPresenter
                .BuildResponse(_controllerFixture.Controller);

            Assert.NotNull(actual);
            Assert.NotEmpty(actual.Value);
        }
    }
}