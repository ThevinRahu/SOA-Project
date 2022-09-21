using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Services
    {
        public Services(string name, string description, string aPIEndPoint, int noOfOperands, string operandType)
        {
            Name = name;
            Description = description;
            APIEndPoint = aPIEndPoint;
            NoOfOperands = noOfOperands;
            OperandType = operandType;
            Status = "";
            Reason = "";
        }

        public Services(string status, string reason)
        {
            Status = status;
            Reason = reason;
        }
        public Services()
        {
            Name = "";
            Description = "";
            APIEndPoint = "";
            NoOfOperands = 0;
            OperandType = "";
            Status = "";
            Reason = "";
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string APIEndPoint { get; set; }
        public int NoOfOperands { get; set; }
        public string OperandType { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
