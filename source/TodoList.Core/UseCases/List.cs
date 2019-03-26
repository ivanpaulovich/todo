namespace TodoList.Core.UseCases
{
    using System.Collections.Generic;
    using TodoList.Core.Boundaries.List;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Gateways;
    using TodoList.Core.Entities;

    public sealed class List : IUseCase
    {
        private IResponseHandler<Response> _outputHandler;
        private IItemGateway _itemGateway;

        public List(
            IResponseHandler<Response> outputHandler,
            IItemGateway itemGateway)
        {
            _outputHandler = outputHandler;
            _itemGateway = itemGateway;
        }

        public void Execute()
        {
            var items = _itemGateway.List();
            Response output = BuildOutput(items);
            _outputHandler.Handle(output);
        }

        private Response BuildOutput(IList<IItem> items)
        {
            ResponseBuilder builder = new ResponseBuilder();
            foreach (var item in items)
            {
                if (item.Done)
                    builder.WithDone(item.Id, item.Title);
                else
                    builder.WithUndone(item.Id, item.Title);
            }

            return builder.Build();
        }
    }
}