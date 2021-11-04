using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TodoClientWithREST.Models;

namespace TodoClientWithREST.Data
{
    public class UserService : IUsersService
    {

        private string uri = "https://localhost:5001";
        private readonly HttpClient client;

        public UserService()
        {
            client = new HttpClient();
        }
        
        public async Task<IList<User>> GetAllUsersAsync()
        {
            Task<string> stringAsync = client.GetStringAsync($"{uri}/Users");
            string message = await stringAsync;
            List<User> result = JsonSerializer.Deserialize<List<User>>(message);
            return result;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            Task<string> stringAsync = client.GetStringAsync($"{uri}/Users/{id}");
            string message = await stringAsync;
            User result = JsonSerializer.Deserialize<User>(message);
            return result;
        }
        
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            Task<string> stringAsync = client.GetStringAsync($"{uri}/Users/{username}");
            string message = await stringAsync;
            User result = JsonSerializer.Deserialize<User>(message);
            return result;
        }

        public async Task AddUserAsync(User user)
        {
            string userAsJson = JsonSerializer.Serialize(user);
            HttpContent content = new StringContent(userAsJson,
                Encoding.UTF8, "application/json");
            await client.PostAsync($"{uri}/Users", content);
        }

        public async Task RemoveUserAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{uri}/User/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            string userAsJson = JsonSerializer.Serialize(user);
            HttpContent content = new StringContent(userAsJson,
                Encoding.UTF8, "application/json");
            await client.PatchAsync($"{uri}/Users/{user.UserID}", content);
        }

        public async Task<User> ValidateUser(string username, string password)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{uri}/users?username={username}&password={password}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string userAsJson = await response.Content.ReadAsStringAsync();
                User resultUser = JsonSerializer.Deserialize<User>(userAsJson);
                return resultUser;
            } 
            throw new Exception("User not found");
        }
    }
}