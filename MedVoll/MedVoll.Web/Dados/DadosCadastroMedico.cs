using MedVoll.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MedVoll.Web.Dados
{
    public class DadosCadastroMedico
    {
        public DadosCadastroMedico()
        {
        }

        public DadosCadastroMedico(Medico medico)
        {
            Id = medico.Id;
            Nome = medico.Nome;
            Email = medico.Email;
            Telefone = medico.Telefone;
            Crm = medico.Crm;
            Especialidade = medico.Especialidade;
        }

        public long? Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório"), MinLength(1)]
        public string Nome { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(6, MinimumLength = 4,
            ErrorMessage = "CRM deve ter de 4 a 6 digitos numéricos")]
        public string Crm { get; set; }
        [Required, RegularExpression(@"^(?:\d{8}|\d{9}|\d{4}-\d{4}|\d{5}-\d{4}|\(\d{2}\)\s*\d{4}-\d{4}|\(\d{2}\)\s*\d{5}-\d{4}|\(\d{2}\)\s*\d{9})$",
            ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; }
        [Required]
        public Especialidade Especialidade { get; set; }
    }
}


