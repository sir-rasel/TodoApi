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

        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAllTodoItems()
        {
            return await _dataAccessor.GetAllTodoItems();
        }

        public async Task<ActionResult<TodoItem>> GetSingleTodoItem(long id)
        {
            return await _dataAccessor.GetSingleTodoItem(id);
        }

        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItemsByDate(DateTime? dateTime)
        {
            return await _dataAccessor.GetTodoItemsByDate(dateTime);
        }

        public async Task<ActionResult<TodoItem>> PatchSingleTodoItem(long id, DateTime dateTime)
        {
            return await _dataAccessor.PatchSingleTodoItem(id, dateTime);
        }

        public async Task<ActionResult<TodoItem>> PostSingleTodoItem(TodoItem todoItem)
        {
            return await _dataAccessor.PostSingleTodoItem(todoItem);
        }

        public async Task<ActionResult<TodoItem>> PutSingleTodoItem(long id, TodoItem todoItem)
        {
            todoItem.Id = id;
            return await _dataAccessor.PutSingleTodoItem(id, todoItem);
        }
    }
}
