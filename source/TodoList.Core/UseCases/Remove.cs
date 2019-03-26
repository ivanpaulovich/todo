namespace TodoList.Core.UseCases
{
    using System;
    using TodoList.Core.Boundaries.Remove;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class Remove : IUseCase
    {
        private IItemGateway _itemGateway;

        public Remove(IItemGateway itemGateway)
        {
            _itemGateway = itemGateway;
        }

        public void Execute(string itemId)
        {
            _itemGateway.Delete(itemId);
        }
    }
}