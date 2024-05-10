using MyKidsReg.Models;
using MyKidsReg.Repositories;

namespace MyKidsReg.Services
{
    public interface IDepartmentServices
    {
        Task<List<Department>> GetAll();
        Task<Department> GetById(int id);

        Task CreateDepartment(Department department);
        Task UpdateDepartment(int id, Department department);
        Task DeleteDepartment(int id);
    }
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepositories _rep;

        public DepartmentServices (IDepartmentRepositories rep)
        {
            _rep = rep; 
        }
        public async Task CreateDepartment(Department department)
        {
             await _rep.CreateDepartment(department);
        }

        public async Task DeleteDepartment(int id)
        {
            await _rep.DeleteDepartment(id);
        }

        public async Task<List<Department>> GetAll()
        {
            return await _rep.GetAll();
        }

        public Task<Department> GetById(int id)
        {
            return _rep.GetById(id);
        }

        public async Task UpdateDepartment(int id, Department update)
        {
        var existingDepartment = await _rep.GetById(id);
            if (existingDepartment != null)
            {
                throw new InvalidOperationException($"Departmentet findes allerede med dette {id}!!!");
            }
            existingDepartment.Name = update.Name;
            existingDepartment.Institution_Id = update.Institution_Id;

            await _rep.UpdateDepartment(id, existingDepartment);
        }
    }
}

