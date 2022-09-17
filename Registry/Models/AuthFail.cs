using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registry.Models
{
    public class AuthFail
    {
        public string status { get; set; }
        public string reason { get; set; }

        public AuthFail(string status, string reason)
        {
            this.status = status;
            this.reason = reason;
        }
    }
}