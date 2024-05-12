using MyKidsReg.Models;
using MyKidsReg.Repositories;
using MyKidsReg.Services;
using static MyKidsReg.Repositories.TeacherRelationsRepositories;

namespace MyKidsReg.Services
{

        public interface ITeacherRelationServices
        {
            Task<List<TeacherRelation>> GetAll();
            Task<TeacherRelation> GetById(int id);
            Task CreateTeacherRelations(TeacherRelation teacherRelation);
            Task UpdateTeacherRelations(int id, TeacherRelation teacherRelation);
            Task DeleteTeacherRelations(int id);
        }
    }

    public class TeacherRelationServices : ITeacherRelationServices
{
        private readonly ITeacherRelationsRepositories _rep;

        public TeacherRelationServices(ITeacherRelationsRepositories rep)
        {
            _rep = rep;
        }

        public async Task CreateTeacherRelations(TeacherRelation teacherRelation)
        {
            await _rep.CreateTeacherRelations(teacherRelation);
        }

        public async Task DeleteTeacherRelations(int id)
        {
        var foundRelation = await _rep.GetById(id);
        if (foundRelation == null)
        {
            throw new Exception("Relationen findes ikke ");
        }
            await _rep.DeleteTeacherRelations(id);
        }

        public async Task<List<TeacherRelation>> GetAll()
        {
            return await _rep.GetAll();
        }

        public Task<TeacherRelation> GetById(int id)
        {
            return _rep.GetById(id);
        }

        public async Task UpdateTeacherRelations(int id, TeacherRelation update)
        {
            var existingTeacherRelation = await _rep.GetById(id);
            if (existingTeacherRelation == null)
            {
                throw new InvalidOperationException($"Parent relationen findes ikke med dette {id}!!!");
            }
            existingTeacherRelation.User_id = update.User_id;
            existingTeacherRelation.Department_id = update.Department_id;

            await _rep.UpdateTeacherRelations(id, existingTeacherRelation);
        }
    }

