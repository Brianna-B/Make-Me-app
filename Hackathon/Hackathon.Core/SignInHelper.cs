using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Core
{
    public class SignInHelper
    {
        public string username;
        private string password;
        List<UserInfo> Users;

        public bool isValidEntry()
        {
            if (string.IsNullOrEmpty(username)||string.IsNullOrEmpty(password))
            {
                return false;
            }

            return true;
        }

        public bool isValidateUsername()
        {
            if (Users.Find(x => x.userName == username) == null)
            {
                return false;
            }
                return true;
        }

        public bool isValidPassword()
        {
            UserInfo getPass = Users.Find(x => x.userName == username);
            
            if (getPass == null)
            {
                return false;
            }

            else
            {
                if (getPass.password != password)
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }
        }

        public SignInHelper(string user, string pass, List<UserInfo> users)
        {
            username = user;
            password = pass;
            Users = users;
        }
    }
}
