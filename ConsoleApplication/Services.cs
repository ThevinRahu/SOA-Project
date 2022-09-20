using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
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

        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string APIEndPoint { get; set; }
        public int NoOfOperands { get; set; }
        public string OperandType { get; set; }


        public string getName()
        {
            return Name;
        }
        public string getDesc()
        {
            return Description;
        }
        public string getEndpoint()
        {
            return APIEndPoint;
        }
        public int getNoOperands()
        {
            return NoOfOperands;
        }
        public string getOperandType()
        {
            return OperandType;
        }
    }
}

