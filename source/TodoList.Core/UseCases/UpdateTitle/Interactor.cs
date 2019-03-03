namespace TodoList.Core.UseCases.UpdateTitle
{
    using System;

    public sealed class Interactor : IUseCase<Input>
    {
        public void Execute(Input input)
        {
            if (input == null)
                throw new Exception("Input is null");
                
            if (string.IsNullOrWhiteSpace(input.Title))
                throw new Exception("Title is null");
        }
    }
}