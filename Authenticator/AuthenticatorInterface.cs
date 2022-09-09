using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator
{
    [ServiceContract]
    public interface AuthenticatorInterface
    {
        [OperationContract]
        string Register(string name, string password);
        [OperationContract]
        int Login(string name, string password);
        [OperationContract]
        string Validate(int token);
        void clearTokens();
    }
}
