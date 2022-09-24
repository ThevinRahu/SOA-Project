using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    //stores and checks the response sent 
    class CheckResponse
    {
        //used to handle the result given by the API calls 
        public CheckResponse(int result)
        {
            this.result = result;
            Status = "";
            Reason = "";
        }
        
        //when the token is invalid this constructor is used to pass the error message to object
        public CheckResponse(string Status, string Reason)
        {
            this.Status = Status;
            this.Reason = Reason;
        }
        //default constructor
        public CheckResponse()
        {
            Status = "";
            Reason = "";
        }
        //Properties
        public int result { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
