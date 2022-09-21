using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /*
     * This Class Stores the data that is returned from the Solution.
     */
    class CheckResponse
    {
        /*
         * This Overloaded constructer is used to handle api calls that returns integers(Add/Multiply)
         */
        public CheckResponse(int answer)
        {
            this.answer = answer;
            Status = "";
            Reason = "";
        }
        /*
         * This Overloaded constructer is used to handle api calls that returns List<int>(prime num to val  / prime num to range)
         */
        public CheckResponse(List<int> primeNumbers)
        {
            PrimeNumbers = primeNumbers;
            Status = "";
            Reason = "";
        }
        /*
         * This Overloaded constructer is used to handle api calls that returns Bolean (IsPrimeNumber)
         */
        public CheckResponse(bool isPrime)
        {
            this.isPrimeNumber = isPrime;
            Status = "";
            Reason = "";
        }
        /*
         * In an event where the token is detected as in valid this overloaded constructor will be used to
         * assign the error message to the object
         */
        public CheckResponse(string Status, string Reason)
        {
            this.Status = Status;
            this.Reason = Reason;
        }
        /*
         * Defualt Constructor of APIResponse class.
         */
        public CheckResponse()
        {
            Status = "";
            Reason = "";
        }
        //Properties
        public int answer { get; set; }
        public List<int> PrimeNumbers { get; set; }
        public bool isPrimeNumber { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
