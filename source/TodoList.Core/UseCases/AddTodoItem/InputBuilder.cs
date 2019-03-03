namespace TodoList.Core.UseCases.AddTodoItem {
    public sealed class InputBuilder {
        private Input _input;

        public InputBuilder () {
            _input = new Input ();
        }

        public InputBuilder WithTitle (string title) {
            _input.Title = title;
            return this;
        }

        public Input Build () {
            return _input;
        }
    }
}