namespace TodoList.UnitTests.ConsoleUITests
{
    using System;
    using System.Collections.Generic;
    using Colorful;
    using TodoList.ConsoleApp;
    using TodoList.Core.Boundaries.Todo;
    using Xunit;

    public sealed class TodoPresenterTests
    {
        private readonly Guid Item1 = new Guid("af15e64c-94b0-4220-b49c-231824f1711c");

        [Fact]
        public void PrintsError_WhenNull()
        {
            using (var consoleWriter = new ConsoleWriter())
            {
                Colorful.Console.SetOut(consoleWriter);
                Presenter sut = new Presenter();
                Response response = null;
                sut.Handle(response);
                string output = consoleWriter.GetOutput();
                Assert.Contains("error", output);
            }
        }

        [Fact]
        public void PrintsWarning_WhenInvalid()
        {
            using (var consoleWriter = new ConsoleWriter())
            {
                Colorful.Console.SetOut(consoleWriter);
                Presenter sut = new Presenter();
                Response response = new Response(Guid.Empty);
                sut.Handle(response);
                string output = consoleWriter.GetOutput();
                Assert.Contains("not added", output);
            }
        }

        [Fact]
        public void PrintsData()
        {
            using (var consoleWriter = new ConsoleWriter())
            {
                Colorful.Console.SetOut(consoleWriter);
                Presenter sut = new Presenter();
                Response response = new Response(Item1);
                sut.Handle(response);
                string output = consoleWriter.GetOutput();
                Assert.Contains("af15e64c", output);
            }
        }
    }
}