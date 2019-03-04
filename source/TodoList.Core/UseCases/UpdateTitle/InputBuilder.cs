namespace TodoList.Core.UseCases.UpdateTitle {
    using System;

    public sealed class InputBuilder {

        private Guid _todoItemId;
        private string _title;

        public InputBuilder WithTodoItemId (Guid todoItemId) {
            _todoItemId = todoItemId;
            return this;
        }

        public InputBuilder WithTitle (string title) {
            _title = title;
            return this;
        }

        public Input Build () {
            Input _input = new Input(_todoItemId, _title);
            return _input;
        }
    }
}