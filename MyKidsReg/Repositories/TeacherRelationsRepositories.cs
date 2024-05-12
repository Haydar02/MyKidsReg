using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;

namespace MyKidsReg.Repositories
{
    public class TeacherRelationsRepositories
    {
        public interface ITeacherRelationsRepositories
        {
            Task<List<TeacherRelation>> GetAll();
            Task<TeacherRelation> GetById(int id);
            Task CreateTeacherRelations(TeacherRelation teacherRelation);
            Task UpdateTeacherRelations(int id, TeacherRelation teacherRelation);
            Task DeleteTeacherRelations(int id);
            Task<bool> TeacherRelationExist(int User_id, int Department_id);
        }
        public class TeacherRelationRepository : ITeacherRelationsRepositories
        {
            private readonly MyKidsRegContext _context;
            public TeacherRelationRepository(MyKidsRegContext context)
            {
                _context = context;
            }

            public async Task<List<TeacherRelation>> GetAll()
            {
                return await _context.TeacherRelations.ToListAsync();
            }

            public async Task<TeacherRelation> GetById(int id)
            {

                return await _context.TeacherRelations.FindAsync(id);
            }

            public async Task CreateTeacherRelations(TeacherRelation newTeacherRelation)
            {
                try
                {
                    if (await TeacherRelationExist(newTeacherRelation.User_id, newTeacherRelation.Department_id))
                    {
                        throw new Exception("En bruger med det angivne User_Id eller Department_id findes allerede.");
                    }
                    _context.TeacherRelations.Add(newTeacherRelation);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Her kan du logge fejlen eller give en brugervenlig besked
                    throw new Exception("Der opstod en fejl under oprettelse af Teacher Relationen. Kontakt venligst systemadministratoren.");
                }
            }
            public async Task DeleteTeacherRelations(int id)
            {
                var item = await _context.TeacherRelations.FirstOrDefaultAsync(i => i.Id == id);
                if (item != null)
                {
                    _context.TeacherRelations.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
            public async Task UpdateTeacherRelations(int id, TeacherRelation teacherRelation)
            {
                _context.TeacherRelations.Update(teacherRelation);
                await _context.SaveChangesAsync();
            }

            public async Task<bool> TeacherRelationExist(int User_id, int Department_id)
            {
                try
                {
                    if (User_id != null)
                    {
                        return await _context.TeacherRelations.AnyAsync(u => u.User_id == User_id);
                    }
                    else if (Department_id != null)
                    {
                        return await _context.TeacherRelations.AnyAsync(u => u.Department_id == Department_id);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl under kontrol af TeacherRelation: {ex.Message}");
                    throw;
                }
            }
        }
    }
}