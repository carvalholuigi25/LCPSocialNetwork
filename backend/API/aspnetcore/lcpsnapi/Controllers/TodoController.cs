using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lcpsnapi.Classes;
using lcpsnapi.Interfaces;
using lcpsnapi.Extensions;

namespace lcpsnapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthMyRoles]
    public class TodoController : ControllerBase
    {
        private readonly ITodo _ITodo;

        public TodoController(ITodo ITodo)
        {
            _ITodo = ITodo;
        }

        /// <summary>
        /// Get all todo items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>?>> GetTodo()
        {
            return await Task.FromResult(_ITodo.GetTodo());
        }

        /// <summary>
        /// Get all todo items by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await Task.FromResult(_ITodo.GetTodoDetails(id));
            if (todo == null)
            {
                return NotFound();
            }
            return todo;
        }

        /// <summary>
        /// Insert the current todo item
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            _ITodo.AddTodo(todo);
            return await Task.FromResult(todo);
        }

        /// <summary>
        /// Update the current todo item by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Todo>> PutTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
            try
            {
                _ITodo.UpdateTodo(todo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(todo);
        }

        /// <summary>
        /// Delete the current todo item by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> DeleteTodo(int id)
        {
            var user = _ITodo.DeleteTodo(id);
            return await Task.FromResult(user);
        }

        /// <summary>
        /// Check if todo item exists by id
        /// </summary>
        private bool TodoExists(int id)
        {
            return _ITodo.CheckTodoItems(id);
        }
    }
}
