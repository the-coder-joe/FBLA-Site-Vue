using Microsoft.AspNetCore.Mvc.Formatters;
using System.Security.Cryptography;
using System.Text;

namespace FBLA_Site
{
    public class UserAuthenticationService
    {
        private readonly int hashIters = 100000;
        private readonly UserRepository userRepository = new UserRepository();
        public void CreateUser(string username, string partiallyHashedPassword, string role)
        {
            string hashedPassword = HashPassword(partiallyHashedPassword);

            User user = new User
            {
                Email = username,
                Hash = hashedPassword,
                Role = role
            };

            userRepository.AddUser(user);
        }
        public User? Authenticate(string username, string partiallyHashedPassword)
        {
            User? user = userRepository.GetUserByUsername(username);

            if (user == null)
                return null;

            if (user.Hash == HashPassword(partiallyHashedPassword))
                return user;

            return null;
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