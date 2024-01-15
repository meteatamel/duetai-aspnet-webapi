using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Controllers;
using TodoApi.Models;
using Xunit;

namespace TodoApi.Tests;

public class TodoItemsControllerTests
{
    [Fact]
    public void GetTodoItems_ShouldNotReturnNull()
    {
        // Arrange
        var controller = new TodoItemsController(new MockedTodoContext());

        // Act
        var result = controller.GetTodoItems();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task HeadTodoItem_ReturnsNoContent_WhenItemExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoItems")
            .Options;
        var context = new TodoContext(options);
        var controller = new TodoItemsController(context);
        var todoItem = new TodoItem { Id = 1, Name = "Item 1", IsComplete = false };
        context.TodoItems.Add(todoItem);
        await context.SaveChangesAsync();
        var id = 1;

        // Act
        var result = await controller.HeadTodoItem(id);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task HeadTodoItem_ReturnsNotFound_WhenItemDoesNotExist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoItems")
            .Options;
        var context = new TodoContext(options);
        var controller = new TodoItemsController(context);
        var id = 2;

        // Act
        var result = await controller.HeadTodoItem(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
