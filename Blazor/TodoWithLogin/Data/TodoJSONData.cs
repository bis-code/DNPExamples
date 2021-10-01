using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TodoWithLogin.Models;


namespace TodoWithLogin.Data
{
    public class TodoJSONData : ITodosData
    {
        private string todoFile = "todos.json";
        private IList<Todo> todos;

        public TodoJSONData()
        {
            if (!File.Exists(todoFile))
            {
                WriteTodosToFile();
            }
            else
            {
                string content = File.ReadAllText(todoFile);
                todos = JsonSerializer.Deserialize<List<Todo>>(content);
            }
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
            todos.Add(todo);
            WriteTodosToFile();
            /*Adding a todos to the todos list. Saving the entire
             list to the file*/
        }

        public void RemoveTodo(int todoId)
        {
            Todo toRemove = todos.First(t => t.TodoId == todoId);
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
    }
}