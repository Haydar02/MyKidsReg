
using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;

namespace MyKidsReg.Repositories
{
    public interface IDepartmentRepositories
    {
        Task<List<Department>> GetAll();
        Task<Department> GetById(int id);
        Task CreateDepartment(Department department);
        Task UpdateDepartment(int id, Department department);
        Task DeleteDepartment(int id);
    }
    public class DepartmentRepositories : IDepartmentRepositories
    {
        private readonly MyKidsRegContext _context;
        public DepartmentRepositories(MyKidsRegContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetById(int id)
        {

            return await _context.Departments.FindAsync(id);
        }

        //public async Task<int> CreateAsync(string name, int Institution_id, bool saveChanges = true)
        //{
        //    var newDepartment = new Department
        //    {
        //        Name = name,
        //        Institution_Id = Institution_id
        //    };
        //    _context.Departments.Add(newDepartment);

        //    if (saveChanges)
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    return newDepartment.Id;
        //}

        public async Task CreateDepartment(Department department)
        {
            _context.Departments.Add(department);

            _context.SaveChanges();
        }
        public async Task DeleteDepartment(int id)
        {
            var item = await _context.Departments.FirstOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                _context.Departments.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateDepartment(int id, Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }
    }
}
