using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class TodoRepository : ITodo
    {
        private readonly MyDBContext _dbContext;

        public TodoRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Todo>? GetTodo()
        {
            try
            {
                return _dbContext.Todo?.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Todo GetTodoDetails(int id)
        {
            try
            {
                Todo? item = _dbContext.Todo?.Find(id);
                if (item != null)
                {
                    return item;
                }
                else
                {
                    throw new ArgumentException("Cannot get the todo items");
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddTodo(Todo item)
        {
            try
            {
                _dbContext.Todo?.Add(item);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateTodo(Todo item)
        {
            try
            {
                _dbContext.Entry(item).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Todo DeleteTodo(int id)
        {
            try
            {
                Todo? item = _dbContext.Todo?.Find(id);

                if (item != null)
                {
                    _dbContext.Todo?.Remove(item);
                    _dbContext.SaveChanges();
                    return item;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the todo item");
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckTodoItems(int id)
        {
            if (_dbContext.Todo == null)
            {
                throw new ArgumentException("The todo item cannot be checked!");
            }

            return _dbContext.Todo.Any(e => e.Id == id);
        }
    }

}
