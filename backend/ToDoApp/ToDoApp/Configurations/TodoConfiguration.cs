using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Enums;
using ToDoApp.Models.Entities;

namespace ToDoApp.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<TodoEntity>
    {
        public void Configure(EntityTypeBuilder<TodoEntity> builder)
        {
            builder.ToTable("Todo");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired();
            builder.Property(t => t.Description).IsRequired(false);
            builder.Property(t => t.IsCompleted).IsRequired();
            builder.Property(t => t.IsCompleted).IsRequired().HasDefaultValue(IsCompletedTodoEnum.UNCOMPLETED);
            builder.Property(t => t.Status).IsRequired().HasDefaultValue(StatusTodoEnum.NO_INFOR);
            builder.Property(t => t.Created).IsRequired();
            builder.Property(t => t.ExpectedCompletionTime).IsRequired(false);
            builder.Property(t => t.Updated).IsRequired();
        }
    }
}
