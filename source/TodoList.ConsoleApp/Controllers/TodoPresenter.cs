namespace TodoList.ConsoleApp.Controllers
{
    using System.Drawing;
    using System.Linq;
    using System;
    using TodoList.Core.Boundaries;
    using TodoList.Core.UseCases;
    using Console = Colorful.Console;

    public sealed class TodoPresenter : IResponseHandler<Core.Boundaries.Todo.Response>
    {
        public void Handle(Core.Boundaries.Todo.Response response)
        {
            if (response == null)
            {
                Console.WriteLine($"The infrastructure returned an unexpected value. Check for infrastructure errors.", Color.Red);
                return;
            }

            if (response.ItemId == Guid.Empty)
            {
                Console.WriteLine($"Item not added. Please verify the input values.", Color.Yellow);
                return;
            }

            Console.WriteLine($"Added {response.ItemId}.");
        }
    }
}