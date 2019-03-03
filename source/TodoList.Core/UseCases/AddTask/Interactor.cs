namespace TodoList.Core.UseCases.AddTask
{
    public sealed class Interactor : IUseCase
    {
        private IOutputHandler _outputHandler;

        public Interactor(IOutputHandler outputHandler)
        {
            _outputHandler = outputHandler;
        }

        public void Execute(Input input)
        {
            Output output = new Output();
            _outputHandler.Handle(output);
        }
    }
}