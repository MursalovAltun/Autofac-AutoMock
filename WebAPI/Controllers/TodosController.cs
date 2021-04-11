using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO.Todo;
using WebAPI.Services.Abstraction;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _service;
        private readonly IMapper _mapper;

        public TodosController(ITodoService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<TodoDTO> Create([FromBody] TodoCreateRequest request)
        {
            return _mapper.Map<TodoDTO>(await _service.AddAsync(request));
        }

        [HttpPost]
        public async Task<TodoDTO> Complete([FromBody] TodoCompleteRequest request)
        {
            return _mapper.Map<TodoDTO>(await _service.CompleteAsync(request));
        }
    }
}