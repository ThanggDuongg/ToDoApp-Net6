using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.Enums;

namespace ToDoApp.Models.Entities
{
    public record TodoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
