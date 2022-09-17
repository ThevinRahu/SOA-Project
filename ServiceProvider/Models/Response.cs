using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceProvider.Models
{
    public class Response
    {
        public string status { get; set; }
        public string reason { get; set; }
        public int result { get; set; }

        public Response(string status, string reason, int result)
        {
            this.status = status;
            this.reason = reason;
            this.result = result;

        }
    }
}