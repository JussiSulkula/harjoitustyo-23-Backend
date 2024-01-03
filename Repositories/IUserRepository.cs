using harjoitustyo.Models;

namespace harjoitustyo.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUserAsync();
        Task<User?>GetUserAsync(long id);
        Task<User?> GetUserAsync(string username);
        Task<User?> NewUserAsync(User user);
        Task<bool>UpdateUserAsync(User user);
        Task<bool>DeleteUserAsync(User user);
    }
}
