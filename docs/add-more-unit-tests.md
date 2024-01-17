# Add more unit tests to test new functionality with Duet AI

Let's see if Duet AI can generate us some unit tests for the new functionality.

**Prompt**
> Can you add a new unit test in `TodoItemsControllerTests.cs` to test the `HeadTodoItem` method in `TodoItemsController.cs`?

**Answer**

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TodoApi.Controllers;
using TodoApi.Models;
using Xunit;

namespace TodoApi.Tests;

public class TodoItemsControllerTests
{
    [Fact]
    public async Task HeadTodoItem_ReturnsNoContent_WhenItemExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoItems")
            .Options;
        var context = new TodoContext(options);
        var controller = new TodoItemsController(context);
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
        var id = 1;

        // Act
        var result = await controller.HeadTodoItem(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
```

The good news is that both tests compile fine! The first test actually fails but
the second test works. The reason why the first test fails is that the in-memory
database does not have any items really, so I asked Duet AI to refine the test.

**Prompt**
> `HeadTodoItem_ReturnsNoContent_WhenItemExists` method does not work because the item is not in the list. Can you change the code to make sure the item actually exists?

**Answer**
> Sure. Here is the updated code for the HeadTodoItem_ReturnsNoContent_WhenItemExists method:

```csharp
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
```

That’s perfect! This actually works!

The only thing you need to change is to make sure the second test refers to a
non-existent item. You can check the final tests
[TodoItemsControllerTests.cs](../after/TodoApiTest/TodoItemsControllerTests.cs):

**Lessons learned**:

* The generated tests are good starting points. They won’t be perfect but will
  save a lot of time. You then need to nudge Duet AI to generate the right tests for
  what you want to exactly test.
