using MedVoll.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace MedVoll.Web.Dados
{
    public class DadosCadastroMedico
    {
        public DadosCadastroMedico()
        {
                
        }

        public DadosCadastroMedico(
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
            Especialidade Especialidade)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Email = Email;
            this.Telefone = Telefone;
            this.Crm = Crm;
            this.Especialidade = Especialidade;
        }

        public long? Id { get; }
        public string Nome { get; }
        public string Email { get; }
        public string Telefone { get; }
        public string Crm { get; }
        public Especialidade Especialidade { get; }
    }
}


