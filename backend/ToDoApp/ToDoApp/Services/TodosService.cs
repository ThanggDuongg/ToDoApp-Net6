using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
using ToDoApp.Models.Entities;
using ToDoApp.Models.Payloads.Requests;
using ToDoApp.Models.Payloads.Responses;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services
{
    public class TodosService : ITodosService
    {
        private readonly ITodosRepository _todosRepository;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IMapper _mapper;

        public TodosService(ITodosRepository todosRepository, IMapper mapper, IActionContextAccessor actionContextAccessor) {
            this._todosRepository = todosRepository ?? throw new ArgumentNullException(nameof(todosRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._actionContextAccessor = actionContextAccessor ?? throw new ArgumentNullException(nameof(actionContextAccessor));
        }
        
        public async Task<Response<bool>> createAsync(TodoCreateRequest todo)
        {
            var actionContext = this._actionContextAccessor.ActionContext;
            try
            {
                if (!actionContext.ModelState.IsValid)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in actionContext.ModelState.Values)
                    {
                        sb.Append(item);
                        sb.Append("& ");
                    }

                    return new Response<bool>()
                    {
                        Success = false,
                        Message = sb.ToString(),
                    };
                }
                else
                {
                    //TodoEntity todoEntity = this._mapper.Map<TodoEntity>(todo);

                    TodoEntity todoEntity = new TodoEntity()
                    {
                        Id = Guid.NewGuid(),
                        Name = todo.Name,
                        Description = todo.Description,
                        IsCompleted = todo.IsCompleted,
                        ExpectedCompletionTime = todo.ExpectedCompletionTime,
                        Created = todo.Created,
                        Updated = todo.Updated,
                        Status = todo.Status,
                    };
                    await this._todosRepository.create(todoEntity);

                    return new Response<bool>()
                    {
                        Success = true,
                        Message = "Create new todo successfully!",
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Response<IEnumerable<TodoResponse>>> getAllAsync()
        {
            try
            {
                var todoEntities = await this._todosRepository.getAllAsync();
                var result = this._mapper.Map<IEnumerable<TodoResponse>>(todoEntities);

                return new Response<IEnumerable<TodoResponse>>()
                {
                    Success = true,
                    Message = "Get all todos successfully!",
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Response<TodoResponse>> getByIdAsync(string id)
        {
            try
            {
                if (String.IsNullOrEmpty(id))
                {
                    return new Response<TodoResponse>()
                    {
                        Success = false,
                        Message = "Id is invalid",
                    };
                }

                Guid Id = Guid.Parse(id);
                var todoEntity = await this._todosRepository.getByIdAsync(Id);
                
                if (todoEntity != null)
                {
                    var result = this._mapper.Map<TodoResponse>(todoEntity);

                    return new Response<TodoResponse>()
                    {
                        Success = true,
                        Message = "Get todo by Id successfully!",
                        Data = result,
                    };
                }
                else
                {
                    return new Response<TodoResponse>()
                    {
                        Success = false,
                        Message = "Not existed Id: " + Id.ToString(),
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Response<TodoResponse>> getByNameAsync(string name)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                {
                    return new Response<TodoResponse>()
                    {
                        Success = false,
                        Message = "Name is invalid",
                    };
                }

                var todoEntity = await this._todosRepository.getByNameAsync(name);
                
                if (todoEntity != null)
                {
                    var result = this._mapper.Map<TodoResponse>(todoEntity);

                    return new Response<TodoResponse>()
                    {
                        Success = true,
                        Message = "Get todo by Name successfully!",
                        Data = result,
                    };
                }
                else
                {
                    return new Response<TodoResponse>()
                    {
                        Success = false,
                        Message = "Not existed Name: " + name,
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
