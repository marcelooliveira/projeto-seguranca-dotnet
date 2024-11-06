using MedVoll.Web.Dados;
using MedVoll.Web.Models;

namespace MedVoll.Web.Interfaces
{
    public interface IMedicoService
    {
        Task CadastrarAsync(DadosCadastroMedico dados);
        Task<DadosCadastroMedico> CarregarPorIdAsync(long id);
        Task ExcluirAsync(long id);
        Task<IQueryable<DadosListagemMedico>> ListarAsync();
        Task<List<DadosListagemMedico>> ListarPorEspecialidadeAsync(Especialidade especialidade);
    }
}