namespace TodoList.UnitTests.ConsoleUITests
{
    using System;
    using System.Collections.Generic;
    using Colorful;
    using TodoList.ConsoleApp;
    using TodoList.Core.Boundaries.List;
    using Xunit;

    public sealed class ListPresenterTests
    {
        private readonly Item Item1 = new Item(new Guid("af15e64c-94b0-4220-b49c-231824f1711c"), "Title1", false);
        private readonly Item Item2 = new Item(new Guid("cf1f2395-43ba-461a-9ff0-943b3fd16e56"), "Title2", true);

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
        public void PrintsWarning_WhenEmpty()
        {
            using (var consoleWriter = new ConsoleWriter())
            {
                Colorful.Console.SetOut(consoleWriter);
                Presenter sut = new Presenter();
                List<Item> items = new List<Item>();
                Response response = new Response(items);
                sut.Handle(response);
                string output = consoleWriter.GetOutput();
                Assert.Contains("list is empty", output);
            }
        }

        [Fact]
        public void PrintsData()
        {
            using (var consoleWriter = new ConsoleWriter())
            {
                Colorful.Console.SetOut(consoleWriter);
                Presenter sut = new Presenter();
                List<Item> items = new List<Item>();
                items.Add(Item1);
                items.Add(Item2);
                Response response = new Response(items);
                sut.Handle(response);
                string output = consoleWriter.GetOutput();
                Assert.Contains(Item1.Title.ToString(), output);
                Assert.Contains(Item2.Title.ToString(), output);
            }
        }
    }
}