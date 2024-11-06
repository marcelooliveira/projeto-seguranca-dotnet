using MedVoll.Web.Dados;

namespace MedVoll.Web.Interfaces
{
    public interface IConsultaService
    {
        Task CadastrarAsync(DadosAgendamentoConsulta dados);
        Task<DadosAgendamentoConsulta> CarregarPorIdAsync(long id);
        Task ExcluirAsync(long id);
        Task<IQueryable<DadosListagemConsulta>> ListarAsync();
    }
}