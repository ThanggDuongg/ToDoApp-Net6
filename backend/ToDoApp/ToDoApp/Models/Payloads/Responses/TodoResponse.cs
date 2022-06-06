using ToDoApp.Enums;

namespace ToDoApp.Models.Payloads.Responses
{
    public record TodoResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = String.Empty;

        public string Description { get; init; } = String.Empty;

        public IsCompletedTodoEnum IsCompleted { get; init; }

        public StatusTodoEnum Status { get; init; }

        public DateTime Created { get; init; }

        public DateTime Updated { get; init; }

        public DateTime? ExpectedCompletionTime { get; init; }
    }
}
