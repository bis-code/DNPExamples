using System.Collections.Generic;
using System.Threading.Tasks;
using TodoClientWithREST.Models;

namespace TodoClientWithREST.Data
{
    public interface ITodosService
    {
        Task<IList<Todo>> GetAllTodosAsync();
        Task<Todo> GetTodoAsync(int id);
        Task AddTodoAsync(Todo todo);
        Task RemoveTodoAsync(int id);
        Task UpdateTodoAsync(Todo todo);
    }
}