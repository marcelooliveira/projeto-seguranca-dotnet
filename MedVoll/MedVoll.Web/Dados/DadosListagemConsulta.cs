using MedVoll.Web.Models;

namespace MedVoll.Web.Dados
{
    public record DadosListagemConsulta(long Id, string Medico, string Paciente, DateTime Data, Especialidade Especialidade)
    {
        public DadosListagemConsulta(Consulta consulta)
            : this(consulta.Id, consulta.Medico.Nome, consulta.Paciente, consulta.Data, consulta.Medico.Especialidade)
        {
        }
    }
}


