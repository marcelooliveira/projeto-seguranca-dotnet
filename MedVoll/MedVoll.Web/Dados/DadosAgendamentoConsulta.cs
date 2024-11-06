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
            [Required] string Paciente,
            [Required, DataType(DataType.DateTime)] DateTime Data,
            Especialidade Especialidade
        )
        {
            this.Id = Id;
            this.IdMedico = IdMedico;
            this.Paciente = Paciente;
            this.Data = Data;
            this.Especialidade = Especialidade;
        }

        public long? Id { get; }
        public long IdMedico { get; }
        public string Paciente { get; }
        public DateTime Data { get; }
        public Especialidade Especialidade { get; }
    }
}


