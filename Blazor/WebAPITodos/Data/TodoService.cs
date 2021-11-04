using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TodoClient.Models;

namespace TodoClient.Data
{
    public class TodoService : ITodosService
    {
        private string todoFile = "todos.json";
        private IList<Todo> todos;

        public TodoService()
        {
            if (!File.Exists(todoFile))
            {
                Seed();
                WriteTodosToFile();
            }
            else
            {
                string content = File.ReadAllText(todoFile);
                todos = JsonSerializer.Deserialize<List<Todo>>(content);
            }
        }

        private void Seed()
        {
            Todo[] ts =
            {
                new Todo
                {
                    UserId = 1,
                    TodoId = 1,
                    Title = "Do wishes",
                    IsCompleted = false
                },
                new Todo
                {
                    UserId = 1,
                    TodoId = 2,
                    Title = "Walk the dog",
                    IsCompleted = false
                },
                new Todo
                {
                    UserId = 2,
                    TodoId = 3,
                    Title = "Do DNP homework",
                    IsCompleted = true
                },
                new Todo
                {
                    UserId = 3,
                    TodoId = 4,
                    Title = "Eat breakfast",
                    IsCompleted = false
                },
                new Todo
                {
                    UserId = 4,
                    TodoId = 5,
                    Title = "Mow lawn",
                    IsCompleted = true
                },
                new Todo
                {
                    UserId = 4, 
                    TodoId = 6,
                    Title = "Do games",
                    IsCompleted = false
                },
                new Todo
                {
                    UserId = 1,
                    TodoId = 7,
                    Title = "Play with cat",
                    IsCompleted = true
                },
                new Todo
                {
                    UserId = 4,
                    TodoId = 8,
                    Title = "Love iubita",
                    IsCompleted = true
                },
            };
            todos = ts.ToList();
        }

        public async Task<IList<Todo>> GetTodosAsync()
        {
            List<Todo> tmp = new List<Todo>(todos);
            return tmp;
            /* Returning a copy of the todos list, so we don't modify
             it accidentally */
        }

        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            /* when you create another todos, todoId will get an unique ID */
            int max = todos.Max(todo => todo.TodoId);
            todo.TodoId = (++max);
            todos.Add(todo);
            WriteTodosToFile();
            /*Adding a todos to the todos list. Saving the entire
             list to the file*/
            return todo;
        }

        public async Task RemoveTodoAsync(int todoId)
        {
            Todo toRemove = todos.First(t => t.TodoId == todoId);
            todos.Remove(toRemove);
            WriteTodosToFile();
        }

        public async Task<Todo> UpdateAsync(Todo todo)
        {
            Todo toUpdate = todos.First(t => t.TodoId == todo.TodoId);
            if (toUpdate == null) throw new Exception($"Did not find todo with id: {todo.TodoId}");
            toUpdate.IsCompleted = todo.IsCompleted;
            toUpdate.Title = todo.Title;
            WriteTodosToFile();
            return toUpdate;
        }

        public async Task<Todo> GetTodoAsync(int todoId)
        {
            return todos.First(t => t.TodoId == t.TodoId);
        }
        private void WriteTodosToFile()
        {
            string todoAsJson = JsonSerializer.Serialize(todos);
            File.WriteAllText(todoFile, todoAsJson);
        }
    }
}