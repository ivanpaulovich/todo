namespace TodoList.ConsoleApp
{
    using System;
    using System.Drawing;
    using System.Linq;
    using TodoList.Core.Boundaries;
    using TodoList.Core.UseCases;
    using Console = Colorful.Console;

    public sealed class Presenter:
        IResponseHandler<Core.Boundaries.AddTodoItem.Response>,
        IResponseHandler<Core.Boundaries.ListTodoItems.Response>
        {
            public void DisplayInstructions()
            {
                Console.WriteLine("Usage");
                Console.WriteLine("\tadd [title]");
                Console.WriteLine("\tupdate [id] [title]");
                Console.WriteLine("\tcomplete [id]");
                Console.WriteLine("\tincomplete [id]");
                Console.WriteLine("\tlist");
                Console.WriteLine("\tremove [id]");
                Console.WriteLine("\texit");
            }

            public void Handle(Core.Boundaries.AddTodoItem.Response response)
            {
                Console.WriteLine($"Added {response.Id}.");
            }

            public void Handle(Core.Boundaries.ListTodoItems.Response response)
            {
                Console.WriteLine($"id\t\t\t\t\t    title", Color.Gray);
                Console.WriteLine($"----------------------------------------------------------", Color.Gray);

                foreach (var item in response.Items.Where(e => !e.IsCompleted))
                {
                    Console.WriteLine($"{item.Id}\t[ ] {item.Title}", Color.White);
                }

                foreach (var item in response.Items.Where(e => e.IsCompleted))
                {
                    Console.WriteLine($"{item.Id}\t[X] {item.Title}", Color.Green);
                }
            }
        }
}