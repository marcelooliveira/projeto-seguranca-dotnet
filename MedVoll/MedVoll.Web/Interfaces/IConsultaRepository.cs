using MedVoll.Web.Models;

namespace MedVoll.Web.Interfaces
{
    public interface IConsultaRepository
    {
        Task<IQueryable<Consulta>> GetAllOrderedByDataAsync();
        Task SaveAsync(Consulta consulta);
        Task<Consulta> FindByIdAsync(long id);
        Task DeleteByIdAsync(long id);
        Task UpdateAsync(Consulta consulta);
    }
}


