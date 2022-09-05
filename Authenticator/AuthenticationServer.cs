using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.util;

namespace Authenticator
{

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class AuthenticationServer : AuthenticatorInterface
    {
        public int Login(string name, string password)
        {
            throw new NotImplementedException();
        }

        public string Register(string name, string password)
        {
            
            string reglocation = Directory.GetCurrentDirectory() + @"\logs\register.txt";
            StreamReader reader = new StreamReader(reglocation);
            
            //read the text file line by line until its null
            while ((reader.ReadLine()) != null)
            {
                string lines = reader.ReadLine();
                if(lines.Contains(name + " " + password))
                {
                    return "The username already exists in the system";
                }
                
            }

            reader.Close();

            try
            {
                using (StreamWriter sw = File.AppendText(reglocation))
                {
                    sw.WriteLine(name + " " + password);
                    sw.Close();
                }

                return "Succesfully registered";
            }
            catch (IOException e)
            {
                return "Registration failure";
            }

        }

        public string Validate(int token)
        {
            throw new NotImplementedException();
        }
    }
}
