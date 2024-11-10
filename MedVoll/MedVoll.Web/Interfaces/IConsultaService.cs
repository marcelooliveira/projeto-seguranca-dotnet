using MedVoll.Web.Dtos;

namespace MedVoll.Web.Interfaces
{
    public interface IConsultaService
    {
        Task CadastrarAsync(ConsultaDto dados);
        Task<ConsultaDto> CarregarPorIdAsync(long id);
        Task ExcluirAsync(long id);
        Task<IEnumerable<ConsultaDto>> ListarAsync();
    }
}