using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using AdvancedTools.Models;

namespace AdvancedTools.Data
{
    public class TodoJSONDAta : ITodosData
    {
        /* This now our File-based database */
        public string todoFile = "todos.json";
        private IList<Todo> todos;

        public TodoJSONDAta()
        {
            /*If the file does not exist, we want to create a new file
                  with some sample data. That data is created in the Seed()
                  method on the next slide.*/
            if (!File.Exists((todoFile)))
            {
                Seed();
                string todoAsJson = JsonSerializer.Serialize(todos);
                File.WriteAllText(todoFile, todoAsJson);
                /* If it exists, we load the todos into the todos-list*/
            }
            else
            {
                /* this constructor will check if the file containing
           todos exits*/
                string content = File.ReadAllText(todoFile);
                todos = JsonSerializer.Deserialize<List<Todo>>(content);
            }
        }

        private void Seed()
        {
            /* A method that adds new todos objects to the todos-list*/
            Todo[] ts =
            {
                new Todo
                {
                    UserId = 1,
                    TodoId = 1,
                    Title = "Do dishes",
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
                    IsCompleted = false
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
            };
            todos = ts.ToList();
        }

        /*Returning a copy of the todos list, so we don't modify it accidentally*/
        public IList<Todo> GetTodos()
        {
            List<Todo> tmp = new List<Todo>(todos);
            return tmp;
        }

        /* Adding a todos to the todos list. Saving the entire list to the file*/
        public void AddTodo(Todo todo)
        {
            todos.Add(todo);
            string todoAsJson = JsonSerializer.Serialize(todos);
            File.WriteAllText(todoFile, todoAsJson);
        }
    }
}