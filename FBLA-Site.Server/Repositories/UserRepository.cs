namespace FBLA_Site
{
    public class UserRepository
    {
        private readonly JsonUtils<List<User>> userUtils = new JsonUtils<List<User>>("users");


        public void AddUser(User user)
        {
            List<User> users = userUtils.GetData() ?? new List<User>();
            users.Add(user);
            userUtils.SetData(users);
        }

        public User? GetUserByUsername(string username)
        {
            User? user = this.userUtils.GetData()?.FirstOrDefault(p => p?.Email == username);

            return user;
        }
    }
}
