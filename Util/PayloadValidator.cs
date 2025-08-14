using ApiResponseExamplesDemo.Models;
using FluentValidation;

namespace Authentication.Login.Util
{
    public class AccountPayloadValidator : AbstractValidator<MyPayload>
    {
        public AccountPayloadValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
                .Must(u => !string.IsNullOrWhiteSpace(u) && !u.Contains(" ")).WithMessage("O nome de usuário não pode conter espaços ou ser nulo/vazio.")
                .MinimumLength(6).WithMessage("O nome de usuário deve ter pelo menos 6 caracteres.")
                .MaximumLength(50).WithMessage("O nome de usuário deve ter no máximo 50 caracteres.");
        }
    }
}