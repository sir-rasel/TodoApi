using Cassandra;
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
            //Sql server Query
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return new TodoItem { Id = -1 };
            }

            //Cassandra DB Query
            await _session.ExecuteAsync(new SimpleStatement("DELETE FROM todoitems WHERE id = ?", id));

            //Sql server Query
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAllTodoItems()
        {
            //Sql server Query
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<ActionResult<TodoItem>> GetSingleTodoItem(long id)
        {
            //Sql server Query
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return new TodoItem { Id = -1 };
            }

            return todoItem;
        }

        public async Task<ActionResult<string>> GetTodoItemsByDate(DateTime? dateTime)
        {
            //Cassandra DB Query
            var items = await _session.ExecuteAsync(new SimpleStatement("SELECT * FROM tododb.todoitems WHERE date = ? allow filtering", dateTime));
            return items == null ? "Empty" : items.ToString();
        }

        public async Task<ActionResult<TodoItem>> PatchSingleTodoItem(long id, DateTime dateTime)
        {
            if (TodoItemExists(id))
            {
                //Cassandra DB Query
                await _session.ExecuteAsync(new SimpleStatement("UPDATE todoitems SET date = ? WHERE id = ?", dateTime, id));

                //Sql server Query
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
                //Cassandra DB Query
                await _session.ExecuteAsync(new SimpleStatement("INSERT INTO todoitems (id, tasktittle, date) VALUES (?, ?, ?)",
                    todoItem.Id, todoItem.TaskTittle, todoItem.Date));

                //Sql server Query
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
                //Cassandra DB Query
                await _session.ExecuteAsync(new SimpleStatement("DELETE FROM todoitems WHERE id = ? ", id));

                //Sql server Query
                var item = await _context.TodoItems.FindAsync(id);
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            //Cassandra DB Query
            await _session.ExecuteAsync(new SimpleStatement("INSERT INTO todoitems (id, tasktittle, date) VALUES (?, ?, ?)", 
                    todoItem.Id, todoItem.TaskTittle, todoItem.Date));

            //Sql server Query
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
