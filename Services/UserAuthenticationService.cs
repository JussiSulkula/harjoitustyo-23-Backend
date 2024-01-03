using harjoitustyo.Models;
using harjoitustyo.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace harjoitustyo.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {

        private readonly IUserRepository _repository;
        private readonly IMessageRepository _messageRepository;

        public UserAuthenticationService(IUserRepository repository, IMessageRepository messageRepository)
        {
            _repository = repository;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            User? user;

            user = await _repository.GetUserAsync(username);
            if (user == null)
            {
                return null;
            }

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: user.Salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 258 / 8));

            if (hashedPassword != user.Password)
            {
                return null;
            }
            return user;
        }

        public User CreateUserCredentials(User user)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 258 / 8));

            if (hashedPassword != user.Password)
            {
                return null;
            }
            return user;

            User newUser = new User
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Salt = salt,
                Password = hashedPassword,
                JoinTime = user.JoinTime != null ? user.JoinTime : DateTime.Now,
                LastJoinTime = DateTime.Now
            };
            return newUser;
        }

        public async Task<bool> isMyMessage(string username, long messageId)
        {
            User? user = await _repository.GetUserAsync(username);
            Message msg = await _messageRepository.GetMessageAsync(messageId);

            if (msg == null)
            {
                return false;
            }

            if (msg.Sender == user)
            {
                return true;
            }

            return false;
        }
    }
}

