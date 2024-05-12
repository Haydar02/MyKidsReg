
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
        

        public async Task CreateDepartment(Department newdepartment)
        {
            try
            {
                if(await DepartmentExists(newdepartment.Name, newdepartment.Institution_Id))
                {
                    throw new Exception("En adeling med det angivne navn findes allerede.");

                }
                _context.Departments.Add(newdepartment);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
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
        public async Task<bool> DepartmentExists(string Name, int Id)
        {
            try
            {
                if (Name != null)
                {
                    return await _context.Departments.AnyAsync(u => u.Name == Name);
                }
                else if (Id != 0)
                {
                    return await _context.Departments.AnyAsync(d => d.Institution_Id == Id);
                }
               
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Fejl under kontrol af brugerens eksistens: {ex.Message}");
                throw;
            }
        }
    }
}
