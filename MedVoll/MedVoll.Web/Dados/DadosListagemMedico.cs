using MedVoll.Web.Models;

namespace MedVoll.Web.Dados
{
    public record DadosListagemMedico(long Id, string Nome, string Email, string Crm, Especialidade Especialidade)
    {
        public DadosListagemMedico(Medico medico)
            : this(medico.Id, medico.Nome, medico.Email, medico.Crm, medico.Especialidade)
        {
        }
    }
}


