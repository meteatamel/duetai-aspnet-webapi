using TodoApi.Controllers;
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
}
