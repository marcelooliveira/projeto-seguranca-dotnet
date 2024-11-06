using MedVoll.Web.Interfaces;
using MedVoll.Web.Models;

namespace MedVoll.Web.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsultaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Consulta>> GetAllOrderedByDataAsync()
        {
            return _context.Consultas.OrderBy(c => c.Data).AsQueryable();
        }

        public async Task SaveAsync(Consulta consulta)
        {
            _context.Add(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task<Consulta> FindByIdAsync(long id)
        {
            return await _context.Consultas.FindAsync(id);
        }

        public async Task DeleteByIdAsync(long id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
            }
        }
    }
}


