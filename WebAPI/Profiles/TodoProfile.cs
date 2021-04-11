using AutoMapper;
using WebAPI.DTO.Todo;
using WebAPI.Entities;

namespace WebAPI.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<Todo, TodoDTO>();
        }
    }
}