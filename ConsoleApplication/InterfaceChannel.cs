using Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    internal class InterfaceChannel
    {
        //Connecting the .net remoting server
         
        private AuthenticatorInterface interfaceChannel;
        public AuthenticatorInterface generateChannel()
        {
            ChannelFactory<AuthenticatorInterface> channelFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:8100/AuthenticationService";
            channelFactory = new ChannelFactory<AuthenticatorInterface>(tcp, URL);
            interfaceChannel = channelFactory.CreateChannel();
            return interfaceChannel;
        }

    }
}
