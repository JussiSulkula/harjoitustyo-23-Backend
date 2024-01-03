using harjoitustyo.Models;
namespace harjoitustyo.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDTO>> GetMessagesAsync();
        Task<IEnumerable<MessageDTO>> GetSentMessagesAsync(string username);
        Task<IEnumerable<MessageDTO>> GetRecievedMessagesAsync(string username);
        Task<MessageDTO> GetMessageAsync(long id);
        Task<MessageDTO> NewMessageAsync(MessageDTO message);
        Task<bool> UpdateMessageAsync(MessageDTO message);
        Task<bool> DeleteMessageAsync(long id);
    }
}
