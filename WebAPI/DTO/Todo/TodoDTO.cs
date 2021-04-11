using System;

namespace WebAPI.DTO.Todo
{
    public class TodoDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
    }
}