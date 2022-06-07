using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
using ToDoApp.Enums;
using ToDoApp.Models.Entities;
using ToDoApp.Models.Payloads.Requests;
using ToDoApp.Models.Payloads.Responses;
using ToDoApp.Pagination;
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
            //var actionContext = this._actionContextAccessor.ActionContext;
            try
            {
                /*if (!actionContext.ModelState.IsValid)
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
                {*/
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
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Response<bool>> deleteAsync(string id)
        {
            try
            {
                if (String.IsNullOrEmpty(id))
                {
                    return new Response<bool>()
                    {
                        Success = false,
                        Message = "Id is invalid",
                    };
                }
                else
                {
                    var result_getById = await this._todosRepository.getByIdAsync(Guid.Parse(id));
                    if (result_getById != null)
                    {
                        await this._todosRepository.deleteAsync(result_getById);
                        return new Response<bool>()
                        {
                            Success = true,
                            Message = "Delete todo id: " + id + " successfully!",
                        };
                    }
                    else
                    {
                        return new Response<bool>()
                        {
                            Success = false,
                            Message = "todo id: " + id + " is not existed",
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<Response<PaginatedList<TodoResponse>>> getAllAsync(TodoPaginationRequest todoPaginationRequest)
        {
            try
            {
                var todoEntities = await this._todosRepository.getAllAsync();
                var result = this._mapper.Map<IEnumerable<TodoResponse>>(todoEntities);

                if (!String.IsNullOrEmpty(todoPaginationRequest.searching))
                {
                    result = result.Where(t => t.Name.Contains(todoPaginationRequest.searching)
                                           || t.Description.Contains(todoPaginationRequest.searching));
                }

                todoPaginationRequest.filter_complete = String.IsNullOrEmpty(todoPaginationRequest.filter_complete) ? "none" : todoPaginationRequest.filter_complete;
                switch (todoPaginationRequest.filter_complete)
                {
                    case "complete":
                        result = result.Where(t => t.IsCompleted == IsCompletedTodoEnum.COMPLETE);
                        break;
                    case "uncomplete":
                        result = result.Where(t => t.IsCompleted == IsCompletedTodoEnum.UNCOMPLETED);
                        break;
                    default:
                        //result = result.Where(t => t.IsCompleted == IsCompletedTodoEnum.COMPLETE);
                        break;
                }

                todoPaginationRequest.filter_status = String.IsNullOrEmpty(todoPaginationRequest.filter_status) ? "none" : todoPaginationRequest.filter_status;
                switch (todoPaginationRequest.filter_status)
                {
                    case "complete_late":
                        result = result.Where(t => t.Status == StatusTodoEnum.COMPLETE_LATE);
                        break;
                    case "complete_early":
                        result = result.Where(t => t.Status == StatusTodoEnum.COMPLETE_EARLY);
                        break;
                    default:
                        break;
                }

                todoPaginationRequest.sortOrder = String.IsNullOrEmpty(todoPaginationRequest.sortOrder) ? "expected_asc" : todoPaginationRequest.sortOrder;
                switch (todoPaginationRequest.sortOrder)
                {
                    case "created_desc":
                        result = result.OrderByDescending(t => t.Created);
                        break;
                    case "created_asc":
                        result = result.OrderBy(t => t.Created);
                        break;
                    case "expected_desc":
                        result = result.OrderByDescending(t => t.ExpectedCompletionTime);
                        break;
                    case "expected_asc":
                        result = result.OrderBy(t => t.Created);
                        break;
                    default:
                        result = result.OrderBy(t => t.Created);
                        break;
                }

                return new Response<PaginatedList<TodoResponse>>()
                {
                    Success = true,
                    Message = "Get all todos successfully!",
                    Data = PaginatedList<TodoResponse>.ToPagedList(result.AsQueryable(), todoPaginationRequest.pageNumber, todoPaginationRequest.maxPageSize),
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

        public async Task<Response<bool>> updateAsync(String id, TodoUpdateRequest todo)
        {
            //var actionContext = this._actionContextAccessor.ActionContext;
            try
            {
                /*if (!actionContext.ModelState.IsValid)
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
                }*/
                if (String.IsNullOrEmpty(id))
                {
                    return new Response<bool>()
                    {
                        Success = false,
                        Message = "Id is invalid",
                    };
                }
                else
                {
                    //var result_getById = await this._todosRepository.getByIdAsync(Guid.Parse(id));
                    
                    if (1 == 1)
                    {
                        // TodoEntity todoEntity = this._mapper.Map<TodoEntity>(todo);
                        TodoEntity todoEntity = new TodoEntity()
                        {
                            Id = Guid.Parse(id),
                            Name = todo.Name,
                            Description = todo.Description,
                            IsCompleted = todo.IsCompleted,
                            ExpectedCompletionTime = todo.ExpectedCompletionTime,
                            Created = todo.Created,
                            Updated = todo.Updated,
                            Status = todo.Status,
                        };
                        await this._todosRepository.update(todoEntity);

                        return new Response<bool>()
                        {
                            Success = true,
                            Message = "Update todo id: " + id + " successfully!",
                        };
                    }
                    else
                    {
                        return new Response<bool>()
                        {
                            Success = false,
                            Message = "Can't find todo with id: " + id,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
