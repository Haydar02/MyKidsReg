using MyKidsReg.Models;
using MyKidsReg.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyKidsReg.Services
{
    public interface IParentRelationServices
    {
        Task<List<ParentsRelation>> GetAll();
        Task<ParentsRelation> GetById(int id);
        Task CreateParentRelations(ParentsRelation parentsRelation);
        Task UpdateParentRelations(int id, ParentsRelation parentsRelation);
        Task DeleteParentRelations(int id);
    }

    public class ParentRelationServices : IParentRelationServices
    {
        private readonly IParentRelationsRepositories _rep;

        public ParentRelationServices(IParentRelationsRepositories rep)
        {
            _rep = rep;
        }

        public async Task CreateParentRelations(ParentsRelation parentsRelation)
        {
            await _rep.CreateParentRelations(parentsRelation);
        }

        public async Task DeleteParentRelations(int id)
        {
            await _rep.DeleteParentRelations(id);
        }

        public async Task<List<ParentsRelation>> GetAll()
        {
            return await _rep.GetAll();
        }

        public Task<ParentsRelation> GetById(int id)
        {
            return _rep.GetById(id);
        }

        public async Task UpdateParentRelations(int id, ParentsRelation update)
        {
            var existingParentRelation = await _rep.GetById(id);
            if (existingParentRelation != null)
            {
                throw new InvalidOperationException($"Parent relationen findes ikke med dette {id}!!!");
            }
            existingParentRelation.User_id = update.User_id;
            existingParentRelation.Student_id = update.Student_id;

            await _rep.UpdateParentRelations(id, existingParentRelation);
        }
    }
}
