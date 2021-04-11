using System;

namespace WebAPI.Entities
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? CompletedAt { get; set; }

        public bool Completed => CompletedAt.HasValue;
    }
}