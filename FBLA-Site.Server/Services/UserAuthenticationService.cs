using FBLA_Site.Server.Models;
using FBLA_Site.Server.Utils;
using System.Security.Cryptography;
using System.Text;

namespace FBLA_Site.Server.Services
{
    public class UserAuthenticationService
    {
        private readonly JsonUtils<User> userUtils = new JsonUtils<User>("users");
        private readonly int hashIters = 100000;
        public void CreateUser()
        {


        }
        public bool Authenticate(string username, string partiallyHashedPassword)
        {

            User? user = userUtils.GetData();
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