using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Core;
using TodoList.Core.Boundaries;
using TodoList.Core.Entities;
using TodoList.Core.Gateways;
using TodoList.Core.Gateways.InMemory;
using TodoList.Core.UseCases;
using TodoList.WebApi.Models;

namespace TodoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private IUseCase<Core.Boundaries.AddTodoItem.Request> _addTodoItem;
        private Core.Boundaries.RemoveTodoItem.IUseCase _removeTodoItem;
        private Core.Boundaries.ListTodoItems.IUseCase _listTodoItems;
        private IUseCase<Core.Boundaries.UpdateTitle.Request> _updateTitle;
        private Presenter _presenter;

        public TodoItemsController(
            IUseCase<Core.Boundaries.AddTodoItem.Request> addTodoItem,
            Core.Boundaries.RemoveTodoItem.IUseCase removeTodoItem,
            Core.Boundaries.ListTodoItems.IUseCase listTodoItems,
            IUseCase<Core.Boundaries.UpdateTitle.Request> updateTitle,
            Presenter presenter)
        {
            _addTodoItem = addTodoItem;
            _removeTodoItem = removeTodoItem;
            _listTodoItems = listTodoItems;
            _updateTitle = updateTitle;
            _presenter = presenter;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<TodoItemViewModel> Get()
        {
            _listTodoItems.Execute();
            return _presenter.ListItems;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<TodoItemViewModel> Post([FromBody] string value)
        {
            var request = new Core.Boundaries.AddTodoItem.Request(value);
            _addTodoItem.Execute(request);
            return CreatedAtAction(nameof(Post), new { id = _presenter.CreatedItem.Id }, _presenter.CreatedItem);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] string value)
        {
            var request = new Core.Boundaries.UpdateTitle.Request(id, value);
            _updateTitle.Execute(request);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _removeTodoItem.Execute(id);
        }
    }
}