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
        private IUseCase<Core.Boundaries.Todo.Request> _todoUseCase;
        private Core.Boundaries.Remove.IUseCase _removeUseCase;
        private Core.Boundaries.List.IUseCase _listUseCase;
        private IUseCase<Core.Boundaries.Rename.Request> _renameUseCase;
        private Presenter _presenter;

        public TodoItemsController(
            IUseCase<Core.Boundaries.Todo.Request> todoUseCase,
            Core.Boundaries.Remove.IUseCase removeUseCase,
            Core.Boundaries.List.IUseCase listUseCase,
            IUseCase<Core.Boundaries.Rename.Request> renameUseCase,
            Presenter presenter)
        {
            _todoUseCase = todoUseCase;
            _removeUseCase = removeUseCase;
            _listUseCase = listUseCase;
            _renameUseCase = renameUseCase;
            _presenter = presenter;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<TodoItemViewModel> Get()
        {
            _listUseCase.Execute();
            return _presenter.ListItems;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<TodoItemViewModel> Post([FromBody] string value)
        {
            var request = new Core.Boundaries.Todo.Request(value);
            _todoUseCase.Execute(request);
            return CreatedAtAction(nameof(Post), new { id = _presenter.CreatedItem.Id }, _presenter.CreatedItem);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] string value)
        {
            var request = new Core.Boundaries.Rename.Request(id, value);
            _renameUseCase.Execute(request);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _removeUseCase.Execute(id);
        }
    }
}