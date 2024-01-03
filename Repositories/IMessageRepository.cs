using harjoitustyo.Models;

namespace harjoitustyo.Repositories
{
    public interface IMessageRepository
    {

        Task<IEnumerable<Message>> GetMessageAsync();
        Task<IEnumerable<Message>> GetSentMessageAsync(User user);
        Task<IEnumerable<Message>> GetRecievedMessageAsync(User user);
        Task<Message?> GetMessageAsync(long id);
        Task<Message?> NewMessageAsync(Message message);

        Task<bool> UpdateMessageAsync(Message message);
        Task<bool> DeleteMessageAsync(Message message);


    }
}
