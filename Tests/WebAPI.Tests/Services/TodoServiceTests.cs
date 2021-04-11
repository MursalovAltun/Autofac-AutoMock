using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using FluentAssertions;
using UnitTests.Components.Helpers;
using WebAPI;
using WebAPI.DTO.Todo;
using WebAPI.Entities;
using WebAPI.Services;
using WebAPI.Services.Abstraction;
using Xunit;

namespace UnitTests.Components.Services
{
    public class TodoServiceTests
    {
        private readonly AutoMock _mock;
        private readonly ITodoService _service;
        private readonly ApplicationContext _context;

        public TodoServiceTests()
        {
            var fixture = new TestFixture();

            _mock = AutoMock.GetLoose(fixture.BeforeBuild);

            _service = _mock.Create<TodoService>();

            _context = fixture.Context;
        }

        [Fact]
        public async Task ShouldCreateTodo()
        {
            const string name = "name";

            await _service.AddAsync(new TodoCreateRequest {Name = name});

            var expected = new Todo
            {
                Name = name,
            };

            _context.GetDatabaseValues<Todo>().Single()
                .Should()
                .BeEquivalentTo(expected, config =>
                    config.Excluding(todo => todo.Id));
        }

        [Fact]
        public async Task ShouldCompleteTodo()
        {
            const string name = "name";
            var id = Guid.NewGuid();
            var now = DateTime.Now;

            _context.Todos.Add(new Todo
            {
                Id = id,
                Name = name,
            });
            _context.SaveChanges();
            
            _mock.Mock<IUtcNowProvider>()
                .Setup(provider => provider.UtcNow())
                .Returns(now);

            await _service.CompleteAsync(new TodoCompleteRequest {Id = id});

            var expected = new Todo
            {
                Id = id,
                Name = name,
                CompletedAt = now
            };

            _context.GetDatabaseValues<Todo>().Single()
                .Should()
                .BeEquivalentTo(expected);
        }
    }
}