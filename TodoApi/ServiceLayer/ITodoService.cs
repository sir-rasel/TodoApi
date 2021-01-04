﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.ServiceLayer
{
    public interface ITodoService
    {
        public Task<ActionResult<IEnumerable<TodoItem>>> GetAllTodoItems();
        public Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItemsByDate(DateTime? dateTime);
        public Task<ActionResult<TodoItem>> GetSingleTodoItem(long id);
        public Task<ActionResult<TodoItem>> PutSingleTodoItem(long id, TodoItem todoItem);
        public Task<ActionResult<TodoItem>> PatchSingleTodoItem(long id, DateTime dateTime);
        public Task<ActionResult<TodoItem>> PostSingleTodoItem(TodoItem todoItem);
        public Task<ActionResult<TodoItem>> DeleteSingleTodoItem(long id);
    }
}
