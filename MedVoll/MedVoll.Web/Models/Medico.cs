using MedVoll.Web.Dados;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedVoll.Web.Models
{
    [Table("medicos")]
    public class Medico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório"), EmailAddress]
        public string Email { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Telefone { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório"), RegularExpression(@"^\d{4,6}$", ErrorMessage = "CRM deve ter de 4 a 6 digitos numéricos")]
        public string Crm { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public Especialidade Especialidade { get; private set; }

        public virtual ICollection<Consulta>? Consultas { get; set; }

        public Medico() { }

        public Medico(DadosCadastroMedico dados)
        {
            AtualizarDados(dados);
        }

        public void AtualizarDados(DadosCadastroMedico dados)
        {
            Nome = dados.Nome;
            Email = dados.Email;
            Telefone = dados.Telefone;
            Crm = dados.Crm;
            Especialidade = dados.Especialidade;
        }
    }
}


