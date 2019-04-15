namespace TodoList.AcceptanceTests
{
    using System.Diagnostics;
    using Xunit;
    
    public sealed class ConsoleTests
    {
        [Fact]
        public void RunInMemory()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "source/TodoList.ConsoleApp/bin/Debug/netcoreapp2.2/todo.dll -- dm"
                }
            };
            process.Start();
        }
    }
}