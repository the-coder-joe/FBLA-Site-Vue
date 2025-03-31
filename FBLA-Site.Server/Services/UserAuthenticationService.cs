using FBLA_Site.Server.Models;
using FBLA_Site.Server.Utils;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Security.Cryptography;
using System.Text;

namespace FBLA_Site.Server.Services
{
    public class UserAuthenticationService
    {
        private readonly JsonUtils<List<User>> userUtils = new JsonUtils<List<User>>("users");
        private readonly int hashIters = 100000;
        public void CreateUser(string username, string partiallyHashedPassword, string role)
        {
            string hashedPassword = HashPassword(partiallyHashedPassword);

            User user = new User
            {
                Email = username,
                Hash = hashedPassword,
                Role = role
            };

            List<User> users = userUtils.GetData() ?? new List<User>();
            users.Add(user);
            userUtils.SetData(users);
        }
        public bool Authenticate(string username, string partiallyHashedPassword)
        {
            User? user = this.userUtils.GetData()?.FirstOrDefault(p => p?.Email == username);
            if (user == null)
                return false;

            return user.Hash == HashPassword(partiallyHashedPassword);
        }

        private string HashPassword(string partiallyHashedPassword)
        {

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hash = Encoding.UTF8.GetBytes(partiallyHashedPassword);


                for (int i = 0; i < hashIters; i++)
                    hash = sha512.ComputeHash(hash);


                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

    }

}