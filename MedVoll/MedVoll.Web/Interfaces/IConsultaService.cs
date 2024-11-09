using MedVoll.Web.Dtos;

namespace MedVoll.Web.Interfaces
{
    public interface IConsultaService
    {
        //Task CadastrarAsync(DadosAgendamentoConsulta dados);
        Task CadastrarAsync(ConsultaDto dados);
        //Task<DadosAgendamentoConsulta> CarregarPorIdAsync(long id);
        Task<ConsultaDto> CarregarPorIdAsync(long id);
        Task ExcluirAsync(long id);
        //Task<IEnumerable<DadosListagemConsulta>> ListarAsync();
        Task<IEnumerable<ConsultaDto>> ListarAsync();
    }
}