namespace TodoList.ConsoleApp.Controllers
{
    using System.Drawing;
    using System.Linq;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Boundaries.List;
    using Console = Colorful.Console;

    public sealed class ListPresenter : IResponseHandler<Response>
    {
        public void Handle(Response response)
        {
            if (response == null)
            {
                Console.WriteLine($"The infrastructure returned an unexpected value. Check for infrastructure errors.", Color.Red);
                return;
            }

            if (!response.Items.Any())
            {
                Console.WriteLine($"The list is empty. Try adding something todo.", Color.Yellow);
                return;
            }

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