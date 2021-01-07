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
        public async Task<ActionResult<IEnumerable<dynamic>>> GetTodoItemsByDate(DateTime? dateTime)
        {
            if(dateTime == null)
            {
                _logger.LogInformation("Failed Get Data");
                return BadRequest("Query DateTime not specified");
            }
            _logger.LogInformation("Successfully Get Data");
            return await _TodoService.GetTodoItemsByDate(dateTime);
        }

        // GET: Todo/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var item =  await _TodoService.GetAllTodoItems();
            _logger.LogInformation("Successfully Get Data");
            return item;
        }

        // GET: Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _TodoService.GetSingleTodoItem(id);

            if (todoItem.Value.Id == -1)
            {
                _logger.LogInformation("Failed Get Data");
                return NotFound();
            }

            _logger.LogInformation("Successfully Get Data");
            return todoItem;
        }

        // PUT: Todo/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> PutTodoItem(long id,[FromBody] TodoItem todoItem)
        {
            var item = await _TodoService.PutSingleTodoItem(id, todoItem);

            _logger.LogInformation("Successfully Put Data");
            return item;
        }

        //PATCH: Todo/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<TodoItem>> PatchTodoItem(long id, [FromBody] DateTime dateTime)
        {
            var item = await _TodoService.PatchSingleTodoItem(id, dateTime);

            if(item.Value.Id == -1)
            {
                _logger.LogInformation("Failed To Patch");
                return NoContent();
            }

            _logger.LogInformation("Successfully Patch Data");
            return item;
        }

        // POST: Todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var item =  await _TodoService.PostSingleTodoItem(todoItem);
            if(item.Value.Id == -1)
            {
                _logger.LogInformation("Failed To Post");
                return BadRequest();
            }

            _logger.LogInformation("Successfully Post Data");
            return item;
        }

        // DELETE: Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var item = await _TodoService.DeleteSingleTodoItem(id);
            if(item.Value.Id == -1)
            {
                _logger.LogInformation("Failed To Delete");
                return NotFound();
            }

            _logger.LogInformation("Successfully Delete Data");
            return item;
        }
    }
}
