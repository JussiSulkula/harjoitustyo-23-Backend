using harjoitustyo.Models;
using Microsoft.EntityFrameworkCore;

namespace harjoitustyo.Repositories
{
    public class UserRepository : IUserRepository
    {
        MessageServiceContext _context;

        public UserRepository(MessageServiceContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            if (user == null) 
            {
                return false;
            }
            else
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<User?> GetUserAsync(long id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<User?> GetUserAsync(string username)
        {
            return await _context.User.Where(x=>x.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUserAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> NewUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
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
    }
}

