using MedVoll.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace MedVoll.Web.Dados
{
    public record DadosAgendamentoConsulta(
        long? Id,
        long IdMedico,
        [Required] string Paciente,
        [Required, DataType(DataType.DateTime)] DateTime Data,
        Especialidade Especialidade
    );
}


