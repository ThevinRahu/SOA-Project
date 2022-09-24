using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class User
    {
        // To create only one instance of the User
        private User() { }
        private static User instance = null;
        public static User Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new User();
                }
                return instance;
            }
        }
        // Top save the name and token of user to used by the service page and for authentication
        private string name;
        private int token;

        public void setName(string name)
        {
            this.name = name;
        }
        public string getName()
        {
            return this.name;
        }
        public void setToken(int token)
        {
            this.token = token;
        }
        public int getToken()
        {
            return this.token;
        }
    }
}
