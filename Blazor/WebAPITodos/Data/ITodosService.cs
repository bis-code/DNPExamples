using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoClient.Models;

namespace TodoClient.Data
{
    public interface ITodosService
    {
        Task<IList<Todo>> GetTodosAsync();
        Task<Todo> GetTodoAsync(int todoId);
        Task<Todo> AddTodoAsync(Todo todo);
        Task RemoveTodoAsync(int todoId);
        Task<Todo> UpdateAsync(Todo todo);
    }
}