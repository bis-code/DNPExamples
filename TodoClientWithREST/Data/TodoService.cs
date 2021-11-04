using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TodoClientWithREST.Models;

namespace TodoClientWithREST.Data
{
    public class TodoService : ITodosService
    {
        private string uri = "https://localhost:5001";
        private readonly HttpClient client;

        public TodoService()
        {
            client = new HttpClient();
        }
        
        public async Task<IList<Todo>> GetAllTodosAsync()
        {
            Task<string> stringAsync = client.GetStringAsync($"{uri}/Todos");
            string message = await stringAsync;
            List<Todo> result = JsonSerializer.Deserialize<List<Todo>>(message);
            return result;
        }

        public async Task<Todo> GetTodoAsync(int id)
        {
            Task<string> stringAsync = client.GetStringAsync($"{uri}/Todos/{id}");
            string message = await stringAsync;
            Todo result = JsonSerializer.Deserialize<Todo>(message);
            return result;
        }

        public async Task AddTodoAsync(Todo todo)
        {
            string todoAsJson = JsonSerializer.Serialize(todo);
            HttpContent content = new StringContent(todoAsJson,
                Encoding.UTF8, "application/json");
            await client.PostAsync($"{uri}/Todos", content);
        }

        public async Task RemoveTodoAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{uri}/Todos/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task UpdateTodoAsync(Todo todo)
        {
            string todoAsJson = JsonSerializer.Serialize(todo);
            HttpContent content = new StringContent(todoAsJson,
                Encoding.UTF8, "application/json");
            await client.PatchAsync($"{uri}/Todos/{todo.TodoId}", content);
        }
    }
}