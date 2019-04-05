namespace TodoList.ConsoleApp.Controllers
{
    using System.Drawing;
    using System.Linq;
    using System;
    using TodoList.Core.Boundaries;
    using TodoList.Core.UseCases;
    using Console = Colorful.Console;

    public sealed class ListPresenter : IResponseHandler<Core.Boundaries.List.Response>
    {
        public void DisplayInstructions()
        {
            Console.WriteLine("The usage");
            Console.WriteLine("\ttodo [title]");
            Console.WriteLine("\tren [id] [title]");
            Console.WriteLine("\tdo [id]");
            Console.WriteLine("\tundo [id]");
            Console.WriteLine("\tls");
            Console.WriteLine("\trm [id]");
            Console.WriteLine("\texit");
        }

        public void Handle(Core.Boundaries.List.Response response)
        {
            if (response == null)
            {
                Console.WriteLine($"The infrastructure returned an unexpected value. Check for infrastructure errors.", Color.Red);
                return;
            }

            if (response.Items.Count() == 0)
            {
                Console.WriteLine($"The list is empty. Try adding something todo.", Color.Yellow);
                return;
            }

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