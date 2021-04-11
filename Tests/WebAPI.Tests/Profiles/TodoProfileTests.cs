using System;
using AutoMapper;
using FluentAssertions;
using WebAPI.DTO.Todo;
using WebAPI.Entities;
using WebAPI.Profiles;
using Xunit;

namespace UnitTests.Components.Profiles
{
    public class TodoProfileTests
    {
        private readonly IMapper _mapper;

        public TodoProfileTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<TodoProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ShouldMapTodoToTodoDTO()
        {
            const string name = "name";
            var id = Guid.NewGuid();

            var todo = new Todo
            {
                Id = id,
                Name = name,
                CompletedAt = DateTime.Now
            };

            var expected = new TodoDTO
            {
                Id = id,
                Name = name,
                Completed = true
            };

            _mapper.Map<TodoDTO>(todo).Should().BeEquivalentTo(expected);
        }
    }
}