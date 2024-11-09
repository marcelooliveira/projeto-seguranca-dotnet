using MedVoll.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace MedVoll.Web.Dados
{
    public class DadosAgendamentoConsulta
    {
        public DadosAgendamentoConsulta()
        {
        }

        public DadosAgendamentoConsulta(
            long? Id,
            long IdMedico,
            string Paciente,
            
            DateTime Data,
            Especialidade Especialidade
        )
        {
            this.Id = Id;
            this.IdMedico = IdMedico;
            this.Paciente = Paciente;
            this.Data = Data;
            this.Especialidade = Especialidade;
        }

        public long? Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public long IdMedico { get; set; }
        [Required(ErrorMessage = "Campo obrigatório"), StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 digitos")]
        public string Paciente { get; set; }
        [Required(ErrorMessage = "Campo obrigatório"), DataType(DataType.DateTime)]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public Especialidade Especialidade { get; set; }
    }
}


