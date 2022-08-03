using System.ComponentModel.DataAnnotations;
using ToDoApp.Enums;

namespace ToDoApp.Models.Payloads.Requests
{
    public class TodoUpdateRequest
    {
        //[Required(ErrorMessage = "Id is invalid")]
        //public string Id { get; init; }

        [Required(ErrorMessage = "Todo Name is invalid")]
        [StringLength(25, ErrorMessage = "Todo Name can contain only 25 characters")]
        public string Name { get; init; } = String.Empty;

        public string Description { get; init; } = String.Empty;

        [Required(ErrorMessage = "IsCompletedTodo is invalid")]
        public IsCompletedTodoEnum IsCompleted { get; init; }

        [Required(ErrorMessage = "StatusTodo is invalid")]
        public StatusTodoEnum Status { get; init; }

        [Required(ErrorMessage = "Created is invalid")]
        public DateTime Created { get; init; }

        [Required(ErrorMessage = "Updated is invalid")]
        public DateTime Updated { get; init; }

        public DateTime? ExpectedCompletionTime { get; init; }
    }
}
