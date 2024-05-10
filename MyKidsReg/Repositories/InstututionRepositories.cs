using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;

namespace MyKidsReg.Repositories
{
    public interface IinstututionRepository
    {
        Task<List<Instutution>> GetAll();
        Task<Instutution> GetById(int id);
        Task CreateInstutution(Instutution instutution);
        Task UpdateInstutution(int id, Instutution instutution);
        Task DeleteInstutution(int id);
    }
    public class InstututionRepositories : IinstututionRepository
    {
        private readonly MyKidsRegContext _context;
        public InstututionRepositories(MyKidsRegContext context)
        {
            _context = context;
        }

        public async Task<List<Instutution>> GetAll()
        {
            return await _context.Instututions.ToListAsync();
        }

        public async Task<Instutution> GetById(int id)
        {

            return await _context.Instututions.FindAsync(id);
        }

        public async Task CreateInstutution(Instutution instutution)
        {
            _context.Instututions.Add(instutution);

            _context.SaveChanges();
        }
        public async Task DeleteInstutution(int id)
        {
            var item = await _context.Instututions.FirstOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                _context.Instututions.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateInstutution(int id, Instutution instutution)
        {
            _context.Instututions.Update(instutution);
            await _context.SaveChangesAsync();
        }
    }
}