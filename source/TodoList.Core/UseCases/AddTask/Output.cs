namespace TodoList.Core.UseCases.AddTask
{
    using System;

    public sealed class Output
    {
        public Guid Id { get; }

        public Output(Guid id)
        {
            Id = id;
        }
    }
}