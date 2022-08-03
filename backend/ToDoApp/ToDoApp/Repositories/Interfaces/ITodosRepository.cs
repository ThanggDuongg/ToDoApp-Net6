using System.Collections;
using ToDoApp.Models.Entities;
using ToDoApp.Models.Payloads.Responses;

namespace ToDoApp.Repositories.Interfaces
{
    public interface ITodosRepository
    {
        public Task<IEnumerable<TodoEntity>> getAllAsync();

        public Task<TodoEntity> getByIdAsync(Guid id);

        public Task<TodoEntity> getByNameAsync(string name);

        public Task create(TodoEntity entity);

        public Task update(TodoEntity entity);

        public Task deleteAsync(TodoEntity entity);
    }
}
