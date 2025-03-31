using Events.Application.DTO.Request;
using FluentValidation;

namespace Events.Application.Validation
{
    public class RegisterRequestDTOValidator : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterRequestDTOValidator()
        {
            RuleFor(x => x.UserModel).SetValidator(new UserRequestDTOValidator());
            RuleFor(x => x.ParticipantModel).SetValidator(new ParticipantRequestDTOValidator());
        }
    }
}
