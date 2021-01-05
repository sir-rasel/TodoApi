using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApi.ServiceLayer;
using TodoApi.Models;
using System;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _TodoService;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoService TodoService, ILogger<TodoController> logger)
        {
            _logger = logger;
            _TodoService = TodoService;
        }

        // GET: Todo/?datetime = Datetime_Object
        [HttpGet]
        public async Task<ActionResult<string>> GetTodoItemsByDate(DateTime? dateTime)
        {
            if(dateTime == null)
            {
                return BadRequest("Query DateTime not specified");
            }
            return await _TodoService.GetTodoItemsByDate(dateTime);
        }

        // GET: Todo/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _TodoService.GetAllTodoItems();
        }

        // GET: Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _TodoService.GetSingleTodoItem(id);

            if (todoItem.Value.Id == -1)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: Todo/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> PutTodoItem(long id,[FromBody] TodoItem todoItem)
        {
            return await _TodoService.PutSingleTodoItem(id, todoItem);
        }

        //PATCH: Todo/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<TodoItem>> PatchTodoItem(long id, [FromBody] DateTime dateTime)
        {
            var item = await _TodoService.PatchSingleTodoItem(id, dateTime);

            if(item.Value.Id == -1)
                return NoContent();

            return item;
        }

        // POST: Todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var item =  await _TodoService.PostSingleTodoItem(todoItem);
            if(item.Value.Id == -1)
            {
                return BadRequest();
            }

            return item;
        }

        // DELETE: Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var item = await _TodoService.DeleteSingleTodoItem(id);
            if(item.Value.Id == -1)
            {
                return NotFound();
            }

            return item;
        }
    }
}
