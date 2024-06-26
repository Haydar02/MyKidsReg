﻿using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;

namespace MyKidsReg.Repositories
{
    public interface IParentRelationsRepositories
    {
        Task<List<ParentsRelation>> GetAll();
        Task<ParentsRelation> GetById(int id);
        Task<IEnumerable<ParentsRelation>> GetByUserId(int userId);

        Task CreateParentRelations(ParentsRelation parentsRelation);
        Task UpdateParentRelations(int id, ParentsRelation parentsRelation);
        Task DeleteParentRelations(int id);
        Task<bool> ParentRelationExist(int User_id, int Student_id);
    }
    public class ParentRelationRepository : IParentRelationsRepositories
    {
        private readonly MyKidsRegContext _context;
        public ParentRelationRepository(MyKidsRegContext context)
        {
            _context = context;
        }

        public async Task<List<ParentsRelation>> GetAll()
        {
            return await _context.ParentsRelations.ToListAsync();
        }

        public async Task<ParentsRelation> GetById(int id)
        {

            return await _context.ParentsRelations.FindAsync(id);
        }

        public async Task<IEnumerable<ParentsRelation>> GetByUserId(int userId)
        {
            return await _context.ParentsRelations.Where(pr => pr.User_id == userId).ToListAsync();
        }

        public async Task CreateParentRelations(ParentsRelation newParentRelation)
        {
            try
            {
                if (await ParentRelationExist(newParentRelation.User_id, newParentRelation.Student_id))
                {
                    throw new Exception("En bruger med det angivne User_Id eller Student_id findes allerede.");
                }
                _context.ParentsRelations.Add(newParentRelation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Her kan du logge fejlen eller give en brugervenlig besked
                throw new Exception("Der opstod en fejl under oprettelse af Parent Relationen. Kontakt venligst systemadministratoren.");
            }
        }
        public async Task DeleteParentRelations(int id)
        {
            var item = await _context.ParentsRelations.FirstOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                _context.ParentsRelations.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateParentRelations(int id, ParentsRelation parentsRelation)
        {
            _context.ParentsRelations.Update(parentsRelation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ParentRelationExist(int User_id, int Student_id)
        {
            try
            {
                if (User_id != null && Student_id != null)
                {
                    // Check if there is an existing parent relation with the same user_id and student_id
                    return await _context.ParentsRelations.AnyAsync(u => u.User_id == User_id && u.Student_id == Student_id);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl under kontrol af ParentRelation: {ex.Message}");
                throw;
            }
        }

    }
}