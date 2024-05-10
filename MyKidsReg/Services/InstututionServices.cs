using MyKidsReg.Models;
using MyKidsReg.Repositories;

namespace MyKidsReg.Services
{
    public interface IinstututionServices
    {
        Task<List<Instutution>> GetAll();
        Task<Instutution> GetById(int id);
        Task CreateInstutution(Instutution instutution);
        Task UpdateInstutution(int id, Instutution instutution);
        Task DeleteInstutution(int id);
    }
    public class InstututionServices : IinstututionServices
    {
        private readonly IinstututionRepository _rep;

        public InstututionServices(IinstututionRepository rep)
        {
            _rep = rep;
        }
        public async Task CreateInstutution(Instutution instutution)
        {
            await _rep.CreateInstutution(instutution);
        }

        public async Task DeleteInstutution(int id)
        {
            await _rep.DeleteInstutution(id);
        }

        public async Task<List<Instutution>> GetAll()
        {
            return await _rep.GetAll();
        }

        public Task<Instutution> GetById(int id)
        {
            return _rep.GetById(id);
        }

        public async Task UpdateInstutution(int id, Instutution update)
        {
            var existingInstutution = await _rep.GetById(id);
            if (existingInstutution != null)
            {
                throw new InvalidOperationException($"Instutution findes allerede med dette {id}!!!");
            }
            existingInstutution.Name = update.Name;
            existingInstutution.Address = update.Address;
            existingInstutution.Zip_Code = update.Zip_Code;

            await _rep.UpdateInstutution(id, existingInstutution);
        }
    }
}

