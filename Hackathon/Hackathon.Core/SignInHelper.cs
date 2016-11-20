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
            //check database for existing username
            return true;
        }

        public bool isValidPassword()
        {
            //if username exists, match the password
            return true;
        }

        public SignInHelper(string user, string pass)
        {
            username = user;
            password = pass;
        }
    }
}
