using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.DAL.Repositories
{
    public interface IPasswordHasher
    {
        Task<string> HashPassword(string password);
        Task<bool> VerifyPassword(string password, string hashPassword);
    }

    public class PasswordHasher : IPasswordHasher
    {
        public async Task<string> HashPassword(string password)
        {
            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password) + "cannot be null or empty");


            using (var sha = SHA256.Create())
            {
                var hashPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashPassword);
            }
        }

        public async Task<bool> VerifyPassword(string password, string hashPassword)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password) + "cannot be null or empty");

            if (string.IsNullOrWhiteSpace(hashPassword))
                throw new ArgumentNullException(nameof(hashPassword) + "cannot be null or empty");

            var hashInputPassword = await HashPassword(password);
            return hashPassword == hashInputPassword;
        }
    }
}
