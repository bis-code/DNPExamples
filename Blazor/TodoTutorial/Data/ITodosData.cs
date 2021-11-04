using System.Collections;
using System.Collections.Generic;
using TodoClient.Models;

namespace TodoClient.Data
{
    public interface ITodosData
    {
        IList<Todo> GetTodos();
        void AddTodo(Todo todo);
        void RemoveTodo(int todoId);
        void Update(Todo todo);
        Todo Get(int id);
    }
}