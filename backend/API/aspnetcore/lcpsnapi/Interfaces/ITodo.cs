using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface ITodo
    {
        public List<Todo>? GetTodo();
        public Todo GetTodoDetails(int id);
        public void AddTodo(Todo item);
        public void UpdateTodo(Todo item);
        public Todo DeleteTodo(int id);
        public bool CheckTodoItems(int id);
    }
}
