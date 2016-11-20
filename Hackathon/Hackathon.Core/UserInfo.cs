using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Core
{
    class UserInfo
    {
        public string email;
        public string userName;
        public string password;

        public UserInfo(string email, string userName, string password)
        {
            this.email = email;
            this.userName = userName;
            this.password = password;
        }
    }
}
