using System.Threading.Tasks;
using WebAPI.DTO.Todo;
using WebAPI.Entities;

namespace WebAPI.Services.Abstraction
{
    public interface ITodoService
    {
        Task<Todo> AddAsync(TodoCreateRequest request);
        Task<Todo> CompleteAsync(TodoCompleteRequest request);
    }
}