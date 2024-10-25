using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain.UserAgg
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PasswordSalt { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public DateTime LastLogin { get; private set; }
        public User(string name, string email, string password, string passwordSalt)
        {
            Name = name;
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
            RegisterDate = DateTime.Now;
            LastLogin = DateTime.Now;
            
        }
        public void ChangeUserName(string name)
        {
            Name = name;

        }
        public void ChangeEmail(string email)
        {
            Email = email;
        }
        public void ChangePassword(string password)
        { 
            Password = password; 
        }
        public void UpdateLastLogin()
        {
            LastLogin = DateTime.Now;
        }

    }

}
