namespace TodoList.UnitTests.ConsoleUITests
{
    using System.IO;
    using System.Text;

    public class ConsoleWriter : TextWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }

        private StringBuilder output;

        public string GetOutput()
        {
            return output.ToString();
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
    }
}