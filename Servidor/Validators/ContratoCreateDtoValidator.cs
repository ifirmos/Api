//ContratoCreateDtoValidator.cs
//Essa é a validação da criação de novos contratos.
//Ela é feita no servidor, e é utilizada para validar
//os dados antes de serem enviados para o banco de dados.
//A idéia aqui foi evitar cargas desnecessárias no banco de dados.
using Servidor.DTOs;
using FluentValidation;

public class ContratoCreateDtoValidator : AbstractValidator<ContratoCreateDto>
{
    public ContratoCreateDtoValidator()
    {
        RuleFor(x => x.Cliente)
            .NotEmpty().WithMessage("O nome do cliente não pode ser vazio.")
            .Length(3, 100).WithMessage("O nome do cliente deve ter entre 3 e 100 caracteres.");

        RuleFor(x => x.NumeroContrato)
            .NotEmpty().WithMessage("O número do contrato não pode ser vazio.")
            .Length(9).WithMessage("O número do contrato deve ter 9 caracteres.")
            .Matches("^[0-9]*$").WithMessage("O número do contrato deve conter apenas números.");

        RuleFor(x => x.Valor)
            .NotEmpty().WithMessage("O valor do contrato não pode ser vazio.")
            .GreaterThan(0).WithMessage("O valor do contrato deve ser maior que zero.");

        RuleFor(x => x.DataAssinatura)
            .NotEmpty().WithMessage("A data de assinatura não pode ser vazia.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de assinatura não pode ser uma data futura.");
    }
}
