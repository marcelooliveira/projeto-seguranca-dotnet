using MedVoll.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace MedVoll.Web.Dados
{
    public record DadosCadastroMedico(
        long? Id,

        [Required, MinLength(1)]
        string Nome,

        [Required, EmailAddress]
        string Email,

        [Required, MinLength(1)]
        string Telefone,

        [Required, RegularExpression(@"^\d{4,6}$", ErrorMessage = "CRM deve ter de 4 a 6 digitos numéricos")]
        string Crm,

        [Required]
        Especialidade Especialidade
    );
}


