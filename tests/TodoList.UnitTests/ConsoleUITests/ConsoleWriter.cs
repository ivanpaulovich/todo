namespace TodoList.UnitTests.ConsoleUITests
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class ConsoleWriter : TextWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }

        private StringBuilder output;

        public async Task<string> GetOutput()
        {
            string outputText = output.ToString();
            return outputText;
        }

        public ConsoleWriter()
        {
            output = new StringBuilder();
        }

        public override void Write(string value)
        {
            output.Append(value);
        }

        public override void WriteLine(string value)
        {
            output.AppendLine(value);
        }

        public static ConsoleWriter sharedConsoleWriter;

        public static ConsoleWriter SharedConsoleWriter
        {
            get
            {
                if (sharedConsoleWriter == null)
                    sharedConsoleWriter = new ConsoleWriter();

                return sharedConsoleWriter;
            }
        }
    }
}