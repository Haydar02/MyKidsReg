using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;

namespace MyKidsReg.Repositories
{
    public interface IinstitutionRepository
    {
        Task<List<Institution>> GetAll();
        Task<Institution> GetById(int id);
        Task CreateInstitution(Institution newInstitution);
        Task UpdateInstitution(int id, Institution institution);
        Task DeleteInstitution(int id);
    }
    public class InstitutionRepositories : IinstitutionRepository
    {
        private readonly MyKidsRegContext _context;
        public InstitutionRepositories(MyKidsRegContext context)
        {
            _context = context;
        }

        public async Task<List<Institution>> GetAll()
        {
            return await _context.Institutions.ToListAsync();
        }

        public async Task<Institution> GetById(int id)
        {

            return await _context.Institutions.FindAsync(id);
        }

        public async Task CreateInstitution(Institution newInstitution)
        {
            try
            {
                if(await InsitutionExists(newInstitution.Name))
                {
                    throw new Exception("En ny institution med det angivne navn findes allerede.");
                }
               _context.Institutions.Add(newInstitution);
               _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("forsøg igen med et andet navn");
            }
           
        }
        public async Task DeleteInstitution(int id)
        {
            var item = await _context.Institutions.FirstOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                _context.Institutions.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateInstitution(int id, Institution instutution)
        {
            _context.Institutions.Update(instutution);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> InsitutionExists(string Name)
        {
            try
            {
                if (Name != null)
                {
                    return await _context.Institutions.AnyAsync(i => i.Name ==Name );
                }
               
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Fejl under kontrol af institution eksistens: {ex.Message}");
                throw;
            }
        }
    }
}