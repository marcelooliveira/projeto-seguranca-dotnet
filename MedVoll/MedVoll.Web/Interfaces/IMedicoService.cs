using MedVoll.Web.Dados;
using MedVoll.Web.Models;

namespace MedVoll.Web.Interfaces
{
    public interface IMedicoService
    {
        Task CadastrarAsync(DadosCadastroMedico dados);
        Task<DadosCadastroMedico> CarregarPorIdAsync(long id);
        Task ExcluirAsync(long id);
        Task<IEnumerable<DadosListagemMedico>> ListarAsync();
        Task<IEnumerable<DadosListagemMedico>> ListarPorEspecialidadeAsync(Especialidade especialidade);
    }
}