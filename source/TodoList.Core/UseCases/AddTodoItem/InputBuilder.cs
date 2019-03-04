namespace TodoList.Core.UseCases.AddTodoItem {
    public sealed class InputBuilder {
        string _title;

        public InputBuilder WithTitle (string title) {
            _title = title;
            return this;
        }

        public Input Build () {
            return new Input (_title);
        }
    }
}