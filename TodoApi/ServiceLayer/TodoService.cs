using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.DataAccessLayer.Repositories;
using TodoApi.Models;

namespace TodoApi.ServiceLayer
{
    public class TodoService : ITodoService
    {
        private readonly ITodoDataAccess _dataAccessor;

        public TodoService(ITodoDataAccess dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        public async Task<ActionResult<TodoItem>> DeleteSingleTodoItem(long id)
        {
            return await _dataAccessor.DeleteSingleTodoItem(id);
        }

        public Task<ActionResult<IEnumerable<TodoItem>>> GetAllTodoItems()
        {
            return _dataAccessor.GetAllTodoItems();
        }

        public Task<ActionResult<TodoItem>> GetSingleTodoItem(long id)
        {
            return _dataAccessor.GetSingleTodoItem(id);
        }

        public Task<ActionResult<IEnumerable<dynamic>>> GetTodoItemsByDate(DateTime? dateTime)
        {
            return _dataAccessor.GetTodoItemsByDate(dateTime);
        }

        public Task<ActionResult<TodoItem>> PatchSingleTodoItem(long id, DateTime dateTime)
        {
            return _dataAccessor.PatchSingleTodoItem(id, dateTime);
        }

        public Task<ActionResult<TodoItem>> PostSingleTodoItem(TodoItem todoItem)
        {
            return _dataAccessor.PostSingleTodoItem(todoItem);
        }

        public Task<ActionResult<TodoItem>> PutSingleTodoItem(long id, TodoItem todoItem)
        {
            todoItem.Id = id;
            return _dataAccessor.PutSingleTodoItem(id, todoItem);
        }
    }
}
