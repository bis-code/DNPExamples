using System.Collections;
using System.Collections.Generic;
using AdvancedTools.Models;

namespace AdvancedTools.Data
{
    public interface ITodosData
    {
        IList<Todo> GetTodos();
        void AddTodo(Todo todo);
    }
}