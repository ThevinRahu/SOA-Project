using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.util;
using Utilities;

namespace Authenticator
{
    //support multiple threading
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class AuthenticationServer : AuthenticatorInterface
    {
        private int minutes = 0;

        //set minutes for timer
        public void setMinutes(int mins)
        {
            this.minutes = mins;
        }

        //login function with name and password as parameters
        public int Login(string name, string password)
        {
            string reglocation = Directory.GetCurrentDirectory() + Paths.REGISTRY_FILE_PATH;
            StreamReader reader = new StreamReader(reglocation);

            //read the file
            while ((File.ReadAllText(reglocation)) != null)
            {
                string lines = reader.ReadLine();
                
                if (lines == null)
                {
                    reader.Close();
                    break;
                }
                else 
                {
                    //to split the string and remove the space in the string
                    var words = lines.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

                    if (words[0] == name && words[1] == password)
                    {
                        //generate a random number as a token
                        Random random = new Random();
                        int token = random.Next(0, 99999);

                        string tokenlocation = Directory.GetCurrentDirectory() + Paths.TOKEN_FILE_PATH;

                        //add the token to file
                        using (StreamWriter sw = File.AppendText(tokenlocation))
                        {
                            sw.WriteLine(token);
                            sw.Close();
                        }
                        reader.Close();
                        return token;
                    }
                }
            }
            reader.Close();
            return -99;
        }

        //function to register with name and password as parameters
        public string Register(string name, string password)
        {
            
            string reglocation = Directory.GetCurrentDirectory() + Paths.REGISTRY_FILE_PATH;
            StreamReader reader = new StreamReader(reglocation);

            //read the text file and check if it is null
            while (File.ReadAllText(reglocation) != null)
            {
                string lines = reader.ReadLine();
                //read the text file line by line until its null
                if (lines == null)
                {
                    break;
                }
                else 
                {
                    //to split the string and remove the space in the string
                    var words = lines.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

                    //if username and password is already registered
                    if (words[0] == name && words[1] == password)
                    {
                        reader.Close();
                        return "The username already exists in the system";
                    }
                }

            }

            reader.Close();

            //add new username and password to the system
            try
            {
                //using (FileStream fs = new FileStream(@"\logs\register.txt",FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter sw = new StreamWriter(reglocation, append: true))
                {
                    sw.WriteLine(name + " " + password);
                    sw.Flush();
                    sw.Close();
                }
                return "Succesfully registered";
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return "Registration failure";
            }

        }

        //validate token function
        public string Validate(int token)
        {
            string tokenlocation = Directory.GetCurrentDirectory() + Paths.TOKEN_FILE_PATH;
            StreamReader reader = new StreamReader(tokenlocation);
            
            //read the file
            while (File.ReadAllText(tokenlocation) != null)
            {
                string lines = reader.ReadLine();
                if (lines == null)
                {
                    reader.Close();
                    break;
                }
                else if (lines.Contains(""+token))
                {
                    // return "Validted" if token in the system
                    reader.Close();
                    return "Validated";
                }

            }
            reader.Close();
            return "Not Validated";
            
        }

        //function to clear tokens on minutes
        public void clearTokens()
        {
            while (true)
            {
                //thread handling
                Thread.Sleep(minutes * 60000);

                string tokenlocation = Directory.GetCurrentDirectory() + Paths.TOKEN_FILE_PATH;
                //clear tokens from file
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
