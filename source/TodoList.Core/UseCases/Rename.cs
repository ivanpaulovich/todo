namespace TodoList.Core.UseCases
{
    using System;
    using TodoList.Core.Boundaries.Rename;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;
    using TodoList.Core.Exceptions;

    public sealed class Rename : IUseCase<Request>
    {
        private IItemGateway _itemGateway;

        public Rename(IItemGateway itemGateway)
        {
            _itemGateway = itemGateway;
        }

        public void Execute(Request request)
        {
            if (request == null)
                throw new Exception("Input is null");

            if (string.IsNullOrWhiteSpace(request.Title))
                throw new Exception("Title is null");

            IItem item = _itemGateway.Get(request.ItemId);

            if (item == null)
                throw new BusinessException($"Item with id { request.ItemId } was not found.");
                
            item.Rename(request.Title);
            _itemGateway.Update(item);
        }
    }
}