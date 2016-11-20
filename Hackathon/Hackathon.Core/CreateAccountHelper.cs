using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Core
{
    public class CreateAccountHelper
    {
        public string username;
        private string password;
        private string confirmPassword;
        public string email;
        List<UserInfo> Users;

        public CreateAccountHelper(string user, string pass, string cpass, string e, List<UserInfo> users)
        {
            username = user;
            password = pass;
            confirmPassword = cpass;
            email = e;
            Users = new List<UserInfo>(users);
        }

        public bool isAccountValid()
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(email))
            {
                return false;
            }

            return true;
        }

        public bool isValidateUsername()
        {
            if(Users.Find(x => x.userName == username) != null)
            {
                return false;
            }

            return true;
        }

        public bool isValidPassword()
        {
            if (password != confirmPassword)
            {
                return false;
            }

            return true;
        }

        public bool isValidateEmail()
        {
            if (Users.Find(x => x.email == email) != null)
            {
                return false;
            }

            return true;
        }

        public List<UserInfo> AddNewUser()
        {
            UserInfo user = new UserInfo(email, username, password);
            Users.Add(user);
            return Users;
        }

    }
}
