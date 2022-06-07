using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using ToDoApp.Models.Payloads.Responses;

namespace ToDoApp.Filters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                /*List<string> Er = new List<string>();
                foreach (var item in context.ModelState.)
                {
                   Er.Add(item.ToString());
                }*/
                IEnumerable<ModelError> allErrors = context.ModelState.Values.SelectMany(v => v.Errors);
                context.Result = new UnprocessableEntityObjectResult(new Response<Exception>()
                {
                    Success = false,
                    Message = "Something wrong!",
                    Errors = allErrors,
                });
            }
        }
    }
}
