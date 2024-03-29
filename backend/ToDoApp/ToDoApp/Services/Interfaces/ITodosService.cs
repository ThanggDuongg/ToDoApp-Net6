﻿using ToDoApp.Models.Payloads.Requests;
using ToDoApp.Models.Payloads.Responses;
using ToDoApp.Pagination;

namespace ToDoApp.Services.Interfaces
{
    public interface ITodosService
    {
        public Task<Response<PaginatedList<TodoResponse>>> getAllAsync(TodoPaginationRequest todoPaginationRequest);

        public Task<Response<TodoResponse>> getByIdAsync(String id);

        public Task<Response<TodoResponse>> getByNameAsync(String name);

        public Task<Response<bool>> createAsync(TodoCreateRequest todo);

        public Task<Response<bool>> updateAsync(String id, TodoUpdateRequest todo);

        public Task<Response<bool>> deleteAsync(String id);
    }
}
