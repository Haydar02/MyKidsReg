using MyKidsReg.Models;
using MyKidsReg.Repositories;

namespace MyKidsReg.Services
{
    public interface IMessageServices
    {
        Task<List<Message>> GetAll();
        Task<Message> GetByID(int id);
        Task CreateMessage(Message newMessage);

        Task DeleteMessage(int id);
    }
    public class MessageServices : IMessageServices
    {
        private readonly IMessageRepositories _rep;

        public MessageServices(IMessageRepositories rep)
        {
            _rep = rep;
        }

        public async Task CreateMessage(Message newMessage)
        {
            try
            {
                newMessage.DescriptionValidate();
                newMessage.CreatedAt = DateTime.UtcNow;
                newMessage.Date = DateOnly.FromDateTime(DateTime.UtcNow);
                newMessage.Time = TimeOnly.FromDateTime(DateTime.UtcNow);
                

                await _rep.CreateMessage(newMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteMessage(int id)
        {
            await _rep.DeleteMessage(id);
        }

        public async Task<List<Message>> GetAll()
        {
            return await _rep.GetAll();
        }

        public async Task<Message> GetByID(int id)
        {
            return await _rep.GetByID(id);
        }

        public async Task DeleteOldMessages()
        {
            var messages = await _rep.GetAll();
            var currentTime = DateTime.UtcNow;


            foreach (var message in messages)
            {

                if (currentTime.Subtract(message.CreatedAt).TotalDays >= 365)
                {
                    await _rep.DeleteMessage(message.Message_id);
                }
            }
        }
    }
}
