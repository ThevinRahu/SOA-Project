using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authenticator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //Start the server
            Console.WriteLine("Welcome to Authentication Server");
            //This represents a tcp / ip binding in the Windows network stack
            var tcp = new NetTcpBinding();
            //This is the actual host service system
            //Bind server to the implementation of DataServer
            var host = new ServiceHost(typeof(AuthenticationServer));
            //Present the publicly accessible interface to the client. 0.0.0.0 tells.net to accept on any interface. :8100 means this will use port 8100. DataService is a name for the actual service, this can be any string.
            host.AddServiceEndpoint(typeof(AuthenticatorInterface), tcp, "net.tcp://0.0.0.0:8100/AuthenticationService");
            //And open the host for business!
            host.Open();
            Console.WriteLine("System is online");

            AuthenticationServer authenticator = new AuthenticationServer();
            Thread clock = new Thread(authenticator.clearTokens);

            Console.WriteLine("Enter number of minutes to clear the Tokens: ");
            string min = Console.ReadLine();
            int mins = Convert.ToInt32(min);
            
            authenticator.setMinutes(mins);
            Console.WriteLine("Tokens clearing set to "+mins+" minutes");

            clock.IsBackground = true; 
            clock.Start(); 

            Console.ReadLine();
            //Close the host
            host.Close();
        }
    }
}
