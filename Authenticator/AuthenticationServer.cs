using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.util;

namespace Authenticator
{

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class AuthenticationServer : AuthenticatorInterface
    {
        private int minutes = 0;

        public void setMinutes(int mins)
        {
            this.minutes = mins;
        }

        public int Login(string name, string password)
        {
            string reglocation = Directory.GetCurrentDirectory() + @"\logs\register.txt";
            StreamReader reader = new StreamReader(reglocation);

            while ((reader.ReadLine()) != null)
            {
                string lines = reader.ReadLine();
                if (lines.Contains(name + " " + password))
                {


                    Random random = new Random();
                    int token = random.Next(0, 99999);

                    string tokenlocation = Directory.GetCurrentDirectory() + @"\logs\tokens.txt";

                    using (StreamWriter sw = File.AppendText(tokenlocation))
                    {
                        sw.WriteLine(token);
                        sw.Close();
                    }

                    return token;

                }
            }
            return -99;
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
            string tokenlocation = Directory.GetCurrentDirectory() + @"\logs\tokens.txt";
            StreamReader reader = new StreamReader(tokenlocation);
            
            
            while ((reader.ReadLine()) != null)
            {
                string lines = reader.ReadLine();
                if (lines.Contains(""+token))
                {
                    return "Validated";
                }

            }
            
                return "Not Validated";
            
        }

        public void clearTokens()
        {
            while (true)
            {
                Thread.Sleep(minutes * 60000);

                string tokenlocation = Directory.GetCurrentDirectory() + @"\logs\tokens.txt";
                using (StreamWriter sw = new StreamWriter(tokenlocation))
                {
                    sw.WriteLine("");
                    sw.Close();
                }

                Console.WriteLine("Tokens Cleared");
            }
        }
    }
}
