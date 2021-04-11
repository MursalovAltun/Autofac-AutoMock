using FluentValidation;
using WebAPI.DTO.Todo;

namespace WebAPI.Validators
{
    public class TodoCreateRequestValidator : AbstractValidator<TodoCreateRequest>
    {
        public TodoCreateRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty();
        }
    }
}