using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatick.Protocol.Users
{
    public class User
    {
        String name;
        public String username
        {
            get { return name; }
            set { name = value; }
        }

        public User(String name)
        {
            this.name = name;
        }
    }
}
