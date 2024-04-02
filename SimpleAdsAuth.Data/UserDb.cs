using System.Data.SqlClient;

namespace SimpleAdsAuth.Data
{
    public class UserDb
    {
        private readonly string _connectionString;

        public UserDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user, string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using var ctx = new SimpleAdsDataContext(_connectionString);
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (isValidPassword)
            {
                return user; //success!!
            }

            return null;
        }

        public User GetByEmail(string email)
        {
            using var ctx = new SimpleAdsDataContext(_connectionString);
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool EmailAvailable(string email)
        {
            using var ctx = new SimpleAdsDataContext(_connectionString);
            return !ctx.Users.Any(u => u.Email == email);
        }
    }
}