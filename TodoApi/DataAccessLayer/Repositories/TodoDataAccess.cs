using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.DataAccessLayer.Repositories
{
    public class TodoDataAccess : BaseRepository, ITodoDataAccess
    {
        public TodoDataAccess(TodoContext context) :
            base(context) { }

        public async Task<ActionResult<TodoItem>> DeleteSingleTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return new TodoItem { Id = -1 };
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAllTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<ActionResult<TodoItem>> GetSingleTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return new TodoItem { Id = -1 };
            }

            return todoItem;
        }

        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItemsByDate(DateTime? dateTime)
        {
            var items = await _context.TodoItems.ToListAsync();
            return items;
        }

        public async Task<ActionResult<TodoItem>> PatchSingleTodoItem(long id, DateTime dateTime)
        {
            if (TodoItemExists(id))
            {
                var item = await _context.TodoItems.FindAsync(id);
                item.Date = dateTime;
                await _context.SaveChangesAsync();

                return item;
            }

            else
            {
                return new TodoItem { Id = -1 };
            }
        }

        public async Task<ActionResult<TodoItem>> PostSingleTodoItem(TodoItem todoItem)
        {
            try
            {
                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();
            }
            catch
            {
                todoItem = new TodoItem { Id = -1 };
            }
            
            return todoItem;
        }

        public async Task<ActionResult<TodoItem>> PutSingleTodoItem(long id, TodoItem todoItem)
        {
            if (TodoItemExists(id))
            {
                var item = await _context.TodoItems.FindAsync(id);
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
