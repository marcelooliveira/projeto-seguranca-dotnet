using MedVoll.Web.Dtos;
using MedVoll.Web.Models;

namespace MedVoll.Web.Interfaces
{
    public interface IMedicoService
    {
        //Task CadastrarAsync(DadosCadastroMedico dados);
        //Task<DadosCadastroMedico> CarregarPorIdAsync(long id);
        //Task ExcluirAsync(long id);
        //Task<IEnumerable<DadosListagemMedico>> ListarAsync();
        //Task<IEnumerable<DadosListagemMedico>> ListarPorEspecialidadeAsync(Especialidade especialidade);

        Task CadastrarAsync(MedicoDto dados);
        Task<MedicoDto> CarregarPorIdAsync(long id);
        Task ExcluirAsync(long id);
        Task<IEnumerable<MedicoDto>> ListarAsync();
        Task<IEnumerable<MedicoDto>> ListarPorEspecialidadeAsync(Especialidade especialidade);

    }
}