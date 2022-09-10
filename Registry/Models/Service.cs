using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registry.Models
{
    public class Service
    { 
            public string name { get; set; }
            public string description { get; set; }
            public string apiEndPoint { get; set; }
            public int noOfOperands { get; set; }
            public string operandType { get; set; }


            public Service(string name, string description, string apiEndPoint, int noOfOperands, string operandType)
            {
                this.name = name;
                this.description = description;
                this.apiEndPoint = apiEndPoint;
                this.noOfOperands = noOfOperands;
                this.operandType = operandType;
            }

        }
    }