using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
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

