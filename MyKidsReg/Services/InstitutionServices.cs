using MyKidsReg.Models;
using MyKidsReg.Repositories;

namespace MyKidsReg.Services
{
    public interface IinstitutionServices
    {
        Task<List<Institution>> GetAll();
        Task<Institution> GetById(int id);
        Task CreateInstitution(Institution institution);
        Task UpdateInstitution(int id, Institution institution);
        Task DeleteInstitution(int id);
    }
    public class InstitutionServices : IinstitutionServices
    {
        private readonly IinstitutionRepository _rep;

        public InstitutionServices(IinstitutionRepository rep)
        {
            _rep = rep;
        }
        public async Task CreateInstitution(Institution instutution)
        {
            await _rep.CreateInstitution(instutution);
        }

        public async Task DeleteInstitution(int id)
        {
            await _rep.DeleteInstitution(id);
        }

        public async Task<List<Institution>> GetAll()
        {
            return await _rep.GetAll();
        }

        public Task<Institution> GetById(int id)
        {
            return _rep.GetById(id);
        }

        public async Task UpdateInstitution(int id, Institution update)
        {
            var existingInstitution = await _rep.GetById(id);
            if (existingInstitution != null)
            {
                throw new InvalidOperationException($"Instutution findes allerede med dette {id}!!!");
            }
            existingInstitution.Name = update.Name;
            existingInstitution.Address = update.Address;
            existingInstitution.Zip_Code = update.Zip_Code;

            await _rep.UpdateInstitution(id, existingInstitution);
        }
    }
}

