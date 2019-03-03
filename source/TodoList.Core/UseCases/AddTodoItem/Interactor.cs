namespace TodoList.Core.UseCases.AddTodoItem {
    using System;

    public sealed class Interactor : IUseCase<Input> {
        private IOutputHandler _outputHandler;

        public Interactor (IOutputHandler outputHandler) {
            _outputHandler = outputHandler;
        }

        public void Execute (Input input) {
            if (input == null)
                throw new Exception ("Input is null");

            if (string.IsNullOrWhiteSpace (input.Title))
                throw new Exception ("Title is null");

            Output output = new Output (Guid.NewGuid ());
            _outputHandler.Handle (output);
        }
    }
}