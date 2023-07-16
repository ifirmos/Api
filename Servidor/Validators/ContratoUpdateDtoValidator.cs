using FluentValidation;
using Servidor.DTOs;

namespace Servidor.Validators
{
    public class ContratoUpdateDtoValidator : AbstractValidator<ContratoUpdateDto>
    {
        public ContratoUpdateDtoValidator()
        {
            RuleFor(x => x.Cliente)
                .NotEmpty().WithMessage("O nome do cliente não pode ser vazio.")
                .Length(3, 100).WithMessage("O nome do cliente deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Valor)
                .NotEmpty().WithMessage("O valor do contrato não pode ser vazio.")
                .GreaterThan(0).WithMessage("O valor do contrato deve ser maior que zero.");

            RuleFor(x => x.DataAssinatura)
                .NotEmpty().WithMessage("A data de assinatura não pode ser vazia.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de assinatura não pode ser uma data futura.");
        }
    }
}
