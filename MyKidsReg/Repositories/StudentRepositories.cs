using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;
using System;

namespace MyKidsReg.Repositories
{
    public interface IStudentRepositores
    {
        Task <List<Student>> GetAll();
        Task<Student>GetByID(int id);
        Task<int> CreateAsync(string name, string lastName, DateOnly birthdate, int Department_id, bool saveChanges = true);
        Task UpdateStudent(int id,Student student);
        Task DeleteStudent(int id);
    }
    public class StudentRepositories : IStudentRepositores
    {
        private readonly MyKidsRegContext _context;

        public StudentRepositories(MyKidsRegContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string name, string lastName, DateOnly birthdate,int Department_id, bool saveChanges = true)
        {
            var newStudent = new Student
            {
                Name = name,
                Last_name = lastName,
                Birthday = birthdate,
                Department_id= Department_id
            };

            _context.Students.Add(newStudent);

            if (saveChanges)
            {
                await _context.SaveChangesAsync();
            }

            return newStudent.Id; // Returner den nye persons id
        }

        public async Task DeleteStudent(int id)
        {
            var item = await _context.Students.FirstOrDefaultAsync(i =>i.Id == id);
            if (item != null)
            {
                _context.Students.Remove(item);
                await _context.SaveChangesAsync();
            }
           
        }

        public async Task<List<Student>> GetAll()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByID(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task UpdateStudent(int id, Student student)
        {
           _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
    }
}
