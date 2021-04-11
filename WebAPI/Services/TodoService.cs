using System.Threading.Tasks;
using Autofac.AttributeExtensions;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTO.Todo;
using WebAPI.Entities;
using WebAPI.Extensions;
using WebAPI.Services.Abstraction;

namespace WebAPI.Services
{
    [InstancePerLifetimeScope]
    public class TodoService : ITodoService
    {
        private readonly IApplicationContext _context;
        private readonly IUtcNowProvider _utcNowProvider;

        public TodoService(IApplicationContext context,
            IUtcNowProvider utcNowProvider)
        {
            _context = context;
            _utcNowProvider = utcNowProvider;
        }

        public async Task<Todo> AddAsync(TodoCreateRequest request)
        {
            var todo = _context.Todos.CreateProxy();

            return await MapThenUpdateAsync(request, todo);
        }

        public async Task<Todo> CompleteAsync(TodoCompleteRequest request)
        {
            var todo = await _context.Todos.FindAsync(request.Id);

            todo.CompletedAt = _utcNowProvider.UtcNow();

            _context.Update(todo);

            await _context.SaveChangesAsync();

            return todo;
        }

        // Only responsible to update the fields that user can modify
        // Use instead of reverse mapping
        private async Task<Todo> MapThenUpdateAsync(TodoCreateRequest src, Todo dst)
        {
            dst.Name = src.Name.SafeTrim();

            _context.Update(dst);

            await _context.SaveChangesAsync();

            return dst;
        }
    }
}