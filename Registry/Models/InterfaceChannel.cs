using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Authenticator;

namespace ServiceProvider.Models
{
    public class InterfaceChannel
    {
        private AuthenticatorInterface interfaceChannel;

        //function to connect authenticator to api
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