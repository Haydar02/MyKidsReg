using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;

namespace MyKidsReg.Repositories
{
    public interface IAdminRelationsRepositories
    {
        Task<List<AdminRelation>> GetAll();
        Task<AdminRelation> GetById(int id);
        Task CreateAdminRelations(AdminRelation adminRelation);
        Task UpdateAdminRelations(int id, AdminRelation adminRelation);
        Task DeleteAdminRelations(int id);
        Task<bool> AdminRelationExist(int Institution_Id, int User_Id);

    }
    public class AdminRelationsRepository : IAdminRelationsRepositories
    {
        private readonly MyKidsRegContext _context;
        public AdminRelationsRepository(MyKidsRegContext context)
        {
            _context = context;
        }

        public async Task<List<AdminRelation>> GetAll()
        {
            return await _context.AdminRelations.ToListAsync();
        }

        public async Task<AdminRelation> GetById(int id)
        {

            return await _context.AdminRelations.FindAsync(id);
        }

        public async Task CreateAdminRelations(AdminRelation newAdminRelation)
        {
            try
            {
                if (await AdminRelationExist(newAdminRelation.User_Id, newAdminRelation.Institution_Id))
                {
                    throw new Exception("En bruger med det angivne User_Id eller Institution_Id findes allerede.");
                }
                _context.AdminRelations.Add(newAdminRelation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Her kan du logge fejlen eller give en brugervenlig besked
                throw new Exception("Der opstod en fejl under oprettelse af Admin Relationen. Kontakt venligst systemadministratoren.");
            }
        }

        public async Task DeleteAdminRelations(int id)
        {
            var item = await _context.AdminRelations.FirstOrDefaultAsync(i => i.User_Id == id);
            if (item != null)
            {
                _context.AdminRelations.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAdminRelations(int id, AdminRelation adminRelation)
        {
            _context.AdminRelations.Update(adminRelation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AdminRelationExist(int Institution_Id, int User_Id)
        {
            try
            {
                if (Institution_Id != null)
                {
                    return await _context.AdminRelations.AnyAsync(u => u.Institution_Id == Institution_Id);
                }
                else if (User_Id != null)
                {
                    return await _context.AdminRelations.AnyAsync(u => u.User_Id == User_Id);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Fejl under kontrol af AdminRelation: {ex.Message}");
                throw;
            }
        }
    }
}