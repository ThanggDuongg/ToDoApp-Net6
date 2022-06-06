using ToDoApp.Models.Payloads.Requests;
using ToDoApp.Models.Payloads.Responses;

namespace ToDoApp.Services.Interfaces
{
    public interface ITodosService
    {
        public Task<Response<IEnumerable<TodoResponse>>> getAllAsync();

        public Task<Response<TodoResponse>> getByIdAsync(String id);

        public Task<Response<TodoResponse>> getByNameAsync(String name);

        public Task<Response<bool>> createAsync(TodoCreateRequest todo);
    }
}
