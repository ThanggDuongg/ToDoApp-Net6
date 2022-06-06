using Microsoft.EntityFrameworkCore;
using ToDoApp.Enums;
using ToDoApp.Models.Entities;

namespace ToDoApp.Context.Seeders
{
    public static class DatabaseSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoEntity>().HasData(
                new TodoEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Learn TypeScript",
                    Description = "None",
                    IsCompleted = IsCompletedTodoEnum.UNCOMPLETED,
                    Status = StatusTodoEnum.NO_INFOR,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                },
                new TodoEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Learn GraphQL",
                    IsCompleted = IsCompletedTodoEnum.UNCOMPLETED,
                    Status = StatusTodoEnum.NO_INFOR,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                }
            );
        }
    }
}
