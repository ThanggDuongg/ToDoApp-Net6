using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Context;
using ToDoApp.Models.Entities;
using ToDoApp.Models.Payloads.Responses;
using ToDoApp.Repositories.Interfaces;

namespace ToDoApp.Repositories
{
    public class TodosRepository : ITodosRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        // private readonly IMapper _mapper;

        public TodosRepository(ApplicationDBContext applicationDBContext)
        {
            this._applicationDBContext = applicationDBContext ?? throw new ArgumentNullException(nameof(applicationDBContext)); 
            // this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task create(TodoEntity entity)
        {
            await this._applicationDBContext.todos.AddAsync(entity);
            await this._applicationDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoEntity>> getAllAsync()
        {
            var result = await this._applicationDBContext.todos.OrderByDescending(t => t.Id).ToListAsync().ConfigureAwait(false);
            // return this._mapper.Map<IEnumerable<TodoResponse>>(result);
            return result;
        }

        public async Task<TodoEntity> getByIdAsync(Guid id)
        {
            var result = await this._applicationDBContext.todos.FirstOrDefaultAsync(t => t.Id == id);
            // return this._mapper.Map<TodoResponse>(result);
            return result;
        }

        public async Task<TodoEntity> getByNameAsync(string name)
        {
            var result = await this._applicationDBContext.todos.FirstOrDefaultAsync(t => t.Name.Equals(name));
            return result;
        }
    }
}
