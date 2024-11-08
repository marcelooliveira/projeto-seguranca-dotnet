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

        public long? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Crm { get; set; }
        public Especialidade Especialidade { get; set; }
    }
}


