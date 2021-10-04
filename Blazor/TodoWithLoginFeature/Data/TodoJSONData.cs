using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TodoWithLoginFeature.Models;

namespace TodoWithLoginFeature.Data
{
    public class TodoJSONData : ITodosData
    {
        private string todoFile = "todos.json";
        private IList<Todo> todos;
        private UsersJSONData UsersData;

        private static TodoJSONData instance;

        public TodoJSONData()
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

        public IList<Todo> GetTodos()
        {
            List<Todo> tmp = new List<Todo>(todos);
            return tmp;
            /* Returning a copy of the todos list, so we don't modify
             it accidentally */
        }

        public void AddTodo(Todo todo)
        {
            /* when you create another todos, todoId will get an unique ID */
            int max = todos.Max(todo => todo.TodoId);
            todo.TodoId = (++max);
            UsersJSONData.Instance().AddTodoToUser(todo);
            LogsJSONData.Instance().AddLog(new Log() {LogMessage = "Todo added: " + todo.Title});
            todos.Add(todo);
            
            WriteTodosToFile();
            /*Adding a todos to the todos list. Saving the entire
             list to the file*/
        }

        public void RemoveTodo(int todoId)
        {
            Todo toRemove = todos.First(t => t.TodoId == todoId);
            LogsJSONData.Instance().AddLog(new Log() {LogMessage = $"Todo ID:{toRemove.TodoId}, with {toRemove.Title} removed"});
            todos.Remove(toRemove);
            WriteTodosToFile();
            
        }

        public void Update(Todo todo)
        {
            Todo toUpdate = todos.First(t => t.TodoId == todo.TodoId);
            toUpdate.IsCompleted = todo.IsCompleted;
            toUpdate.Title = todo.Title;
            WriteTodosToFile();
        }

        public Todo Get(int id)
        {
            return todos.FirstOrDefault(t => t.TodoId == id);
        }

        private void WriteTodosToFile()
        {
            string todoAsJson = JsonSerializer.Serialize(todos);
            File.WriteAllText(todoFile, todoAsJson);
        }

        public static TodoJSONData Instance()
        {
            if (instance == null)
            {
                instance = new TodoJSONData();
            }

            return instance;
        }
    }
}