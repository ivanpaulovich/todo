namespace TodoList.UnitTests.WebUITests
{
    using System;
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
        public void ReturnsNotFound_WhenNull()
        {
            Response response = null;
            _controllerFixture.TodoPresenter.Handle(response);

            var actual = _controllerFixture
                .TodoPresenter
                .BuildResponse(_controllerFixture.Controller);

            Assert.NotNull(actual);
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(actual.Result);
        }

        [Fact]
        public void ReturnsBadRequest_WhenInvalid()
        {
            Response response = new Response(Guid.Empty);
            _controllerFixture.TodoPresenter.Handle(response);

            var actual = _controllerFixture
                .TodoPresenter
                .BuildResponse(_controllerFixture.Controller);

            Assert.NotNull(actual);
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(actual.Result);
        }

        [Fact]
        public void ReturnsCreatedAtActionResult_WhenValid()
        {
            Response response = new Response(_controllerFixture.ItemId1);
            _controllerFixture.TodoPresenter.Handle(response);

            var actual = _controllerFixture
                .TodoPresenter
                .BuildResponse(_controllerFixture.Controller);

            Assert.NotNull(actual);
            Assert.IsType<Microsoft.AspNetCore.Mvc.CreatedAtActionResult>(actual.Result);
        }
    }
}