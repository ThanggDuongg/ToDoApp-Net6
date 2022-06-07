using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToDoApp.Models.Payloads.Responses
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<ModelError> Errors { get; set; }
        public T Data { get; set; }

        public Response() { }

        public Response(T data, string? message = null)
        {
            this.Success = true;
            this.Message = message;
            this.Data = data;
        }

        public Response(string message)
        {
            this.Success = false;
            this.Message = message;
        }
    }
}
