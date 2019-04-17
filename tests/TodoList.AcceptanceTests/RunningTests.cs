namespace TodoList.AcceptanceTests
{
    using Xunit;
    using System.Diagnostics;
    using System;

    public sealed class RunningTests
    {
        [Fact]
        public void RunningConsole()
        {
            var processStartInfo = new ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = "todo.dll help",
                WorkingDirectory = "../../../../../source/TodoList.ConsoleApp/bin/Debug/netcoreapp2.2/",
                RedirectStandardOutput = true
            };

            var process = Process.Start(processStartInfo);
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();
            Assert.Contains("The usage", output);
        }
    }
}