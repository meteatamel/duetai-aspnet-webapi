using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

public class MockedTodoContext : TodoContext
{
    public MockedTodoContext()
        : base(new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase("TodoApi")
            .Options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TodoApi");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>().ToTable("TodoItems");
    }
}
