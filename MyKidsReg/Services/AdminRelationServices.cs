using MyKidsReg.Models;
using MyKidsReg.Repositories;

namespace MyKidsReg.Services
{
    public interface IAdminRelationServices
    {
        Task<List<AdminRelation>> GetAll();
        Task<AdminRelation> GetById(int id);
        Task CreateAdminRelations(AdminRelation adminRelation);
        Task UpdateAdminRelations(int id, AdminRelation adminRelation);
        Task DeleteAdminRelations(int id);
    }
    public class AdminRelationServices : IAdminRelationServices
    {
        private readonly IAdminRelationsRepositories _rep;

        public AdminRelationServices(IAdminRelationsRepositories rep)
        {
            _rep = rep;
        }
        public async Task CreateAdminRelations(AdminRelation adminRelation)
        {
            await _rep.CreateAdminRelations(adminRelation);
        }

        public async Task DeleteAdminRelations(int id)
        {
            await _rep.DeleteAdminRelations(id);
        }

        public async Task<List<AdminRelation>> GetAll()
        {
            return await _rep.GetAll();
        }

        public Task<AdminRelation> GetById(int id)
        {
            return _rep.GetById(id);
        }

        public async Task UpdateAdminRelations(int id, AdminRelation update)
        {
            var existingadminRelation = await _rep.GetById(id);
            if (existingadminRelation != null)
            {
                throw new InvalidOperationException($"AdminRelation findes allerede med dette {id}!!!");
            }
            existingadminRelation.Institution_Id = update.Institution_Id;

            await _rep.UpdateAdminRelations(id,existingadminRelation);
        }
    }
}

