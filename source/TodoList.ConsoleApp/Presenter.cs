namespace TodoList.ConsoleApp
{
    using System.Drawing;
    using System.Linq;
    using System;
    using TodoList.Core.Boundaries;
    using TodoList.Core.UseCases;
    using Console = Colorful.Console;

    public sealed class Presenter : IResponseHandler<Core.Boundaries.Todo.Response>, IResponseHandler<Core.Boundaries.List.Response>
    {
        public void DisplayInstructions()
        {
            Console.WriteLine("Usage");
            Console.WriteLine("\ttodo [title]");
            Console.WriteLine("\tren [id] [title]");
            Console.WriteLine("\tdo [id]");
            Console.WriteLine("\tundo [id]");
            Console.WriteLine("\tls");
            Console.WriteLine("\trm [id]");
            Console.WriteLine("\texit");
        }

        public void Handle(Core.Boundaries.Todo.Response response)
        {
            Console.WriteLine($"Added {response.ItemId}.");
        }

        public void Handle(Core.Boundaries.List.Response response)
        {
            Console.WriteLine($"id\t     title", Color.Gray);
            Console.WriteLine($"----------------------------------------------------------", Color.Gray);

            foreach (var item in response.Items.Where(e => !e.Done))
            {
                Console.WriteLine($"{item.ItemId.ToString().Substring(0, 8)} [ ] {item.Title}", Color.White);
            }

            foreach (var item in response.Items.Where(e => e.Done))
            {
                Console.WriteLine($"{item.ItemId.ToString().Substring(0, 8)} [X] {item.Title}", Color.Green);
            }
        }
    }
}