using Autofac.Extras.Moq;
using FluentValidation.TestHelper;
using WebAPI.DTO.Todo;
using WebAPI.Validators;
using Xunit;

namespace UnitTests.Components.Validators
{
    public class TodoCreateRequestValidatorTests
    {
        private readonly TodoCreateRequestValidator _validator;
        
        public TodoCreateRequestValidatorTests()
        {
            var mock = AutoMock.GetLoose();
            _validator = mock.Create<TodoCreateRequestValidator>();
        }

        [Fact]
        public void ReturnsValidWhenModelIsValid()
        {
            _validator.ShouldNotHaveValidationErrorFor(r => r.Name, new TodoCreateRequest {Name = "name"});
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void ReturnsInvalidWhenNameIsNotValid(string name)
        {
            _validator.ShouldHaveValidationErrorFor(r => r.Name, new TodoCreateRequest {Name = name});
        }
    }
}