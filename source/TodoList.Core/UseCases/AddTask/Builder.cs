namespace TodoList.Core.UseCases.AddTask
{
    public sealed class Builder
    {
        private Input _input;

        public Builder()
        {
            _input = new Input();
        }

        public Builder WithTitle(string title)
        {
            _input.Title = title;
            return this;
        }

        public Input Build()
        {
            return _input;
        }
    }
}