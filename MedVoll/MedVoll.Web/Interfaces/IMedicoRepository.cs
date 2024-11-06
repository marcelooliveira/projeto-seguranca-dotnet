using MedVoll.Web.Models;

namespace MedVoll.Web.Interfaces
{
    public interface IMedicoRepository
    {
        Task<bool> IsJaCadastradoAsync(string email, string crm, long? id);
        Task<List<Medico>> FindByEspecialidadeAsync(Especialidade especialidade);
        Task SaveAsync(Medico medico);
        Task<Medico> FindByIdAsync(long id);
        Task DeleteByIdAsync(long id);
        Task<IQueryable<Medico>> GetAllAsync();
    }
}


