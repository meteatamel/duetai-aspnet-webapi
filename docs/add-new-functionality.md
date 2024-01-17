# Add new functionality with Duet AI

Now that we have basics in place, let's use Duet AI to add a new endpoint to
check in an item exists or not.

**Prompt**
> Can you modify `TodoItemsController.cs` to add a method to check if a ToDoItem exists? The method should handle the HTTP HEAD method and it should use the id of ToDoItem to check for existence

**Answer**
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
...

        // HEAD: api/TodoItems/5
        [HttpHead("{id}")]
        public async Task<IActionResult> HeadTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
```

The answer is very good and compiles out of the box!

You can test by adding an item:

```sh
curl -X POST \
    -H "Content-Type: application/json" \
    -d '{
        "name": "Learn ASP.NET Core",
        "isComplete": false
    }' \
    http://localhost:5112/api/TodoItems

{"id":1,"name":"Learn ASP.NET Core","isComplete":false}%
```

Verify that the new endpoint works:

```sh
curl -I -X HEAD http://localhost:5112/api/ToDoItems/1
HTTP/1.1 204 No Content
Date: Mon, 15 Jan 2024 13:42:24 GMT
Server: Kestrel
```
