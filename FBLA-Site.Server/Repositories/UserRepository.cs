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

        public List<int> GetAllUserIds()
        {
            List<User>? users = this.userUtils.GetData();
            if (users == null)
                return new List<int>();
            return users.Select(u => u.Id).ToList();
        }

    }
}
