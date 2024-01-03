using harjoitustyo.Models;
using harjoitustyo.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace harjoitustyo.Services
{
    public interface IUserAuthenticationService
    {
        Task<User>Authenticate(string username, string password);
        User CreateUserCredentials(User user);
        Task<bool>isMyMessage(string username, long messageId);
    }
}
