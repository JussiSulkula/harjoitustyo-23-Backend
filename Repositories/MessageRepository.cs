using harjoitustyo.Models;
using Microsoft.EntityFrameworkCore;
namespace harjoitustyo.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        MessageServiceContext _context;

        public MessageRepository(MessageServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetMessageAsync()
        {
            return await _context.Message.Where(x => x.Recipient == null).TakeLast(10).ToListAsync();
        }

        public async Task<Message?> GetMessageAsync(long id)
        {
            return await _context.Message.FindAsync(id);
        }

        public async Task<Message?> NewMessageAsync(Message message)
        {
            _context.Message.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<bool> DeleteMessageAsync(Message message)
        {
            if(message == null)
            {
                return false;
            }
            else
            {
                _context.Message.Remove(message);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdateMessageAsync(Message message)
        {
            _context.Entry(message).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<Message>> GetSentMessageAsync(User user)
        {
            return await _context.Message.Where(x => x.Sender == user).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetRecievedMessageAsync(User user)
        {
            return await _context.Message.Where(x => x.Recipient == user).ToListAsync();
        }
    }
}
