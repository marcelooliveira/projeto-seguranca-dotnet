using MedVoll.Web.Dtos;
using MedVoll.Web.Models;

namespace MedVoll.Web.Interfaces
{
    public interface IMedicoService
    {
        Task CadastrarAsync(MedicoDto dados);
        Task<MedicoDto> CarregarPorIdAsync(long id);
        Task ExcluirAsync(long id);
        Task<IEnumerable<MedicoDto>> ListarAsync();
        Task<IEnumerable<MedicoDto>> ListarPorEspecialidadeAsync(Especialidade especialidade);

    }
}