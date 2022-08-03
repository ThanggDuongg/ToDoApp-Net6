using AutoMapper;
using ToDoApp.Models.Entities;
using ToDoApp.Models.Payloads.Requests;
using ToDoApp.Models.Payloads.Responses;

namespace ToDoApp.Helper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            this.CreateMap<TodoEntity, TodoResponse>().ReverseMap();
            this.CreateMap<TodoEntity, TodoCreateRequest>().ReverseMap();
            this.CreateMap<TodoEntity, TodoUpdateRequest>().ReverseMap();
        }
    }
}
