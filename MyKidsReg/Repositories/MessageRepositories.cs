using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;

namespace MyKidsReg.Repositories
{ 
    public interface IMessageRepositories
    {
        Task<List<Message>> GetAll();
        Task<Message> GetByID(int id);
        Task CreateMessage(Message newMessage);
       
        Task DeleteMessage(int id);
    }
    public class MessageRepositories : IMessageRepositories
    {
        private readonly MyKidsRegContext _context;

        public MessageRepositories(MyKidsRegContext context)
        {
            _context = context;
        }

        public async Task CreateMessage(Message newMessage)
        {
            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMessage(int id)
        {
            var foundMessage = await _context.Messages.FindAsync(id);
            if(foundMessage == null)
            {
                _context.Messages.Remove(foundMessage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Message>> GetAll()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message> GetByID(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

    }
}
