using Authenticator;
using Newtonsoft.Json;
using Registry.Models;
using ServiceProvider.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registry.Controllers
{
    public class UnpublishController : ApiController
    {
        InterfaceChannel iChannel = new InterfaceChannel();
        AuthenticatorInterface iserverChannel;
        public IHttpActionResult Get(string endpoint, int token)
        {
            //StreamReader reader = new StreamReader(servicelocation);
            iserverChannel = iChannel.generateChannel();
            string validateResult = iserverChannel.Validate(token);

            if (validateResult == "Validated")
            {
                string servicelocation = @"D:\Downloads\VS\ThevinRahu\Services\services.txt";
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(servicelocation).ToList();
                List<Service> data = new List<Service>();
                Service services;
                Service removedService = new Service();

                foreach (string line in lines)
                {
                    services = new Service();
                    services = JsonConvert.DeserializeObject<Service>(line);
                    data.Add(services);
                }

                lines.Clear();
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].apiEndPoint.Equals(endpoint))
                    {
                        removedService = data[i];
                        data.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < data.Count; i++)
                {
                    string jsonstring = JsonConvert.SerializeObject(data[i]);
                    lines.Add(jsonstring);
                }
                File.WriteAllLines(servicelocation, lines);
                return Ok(removedService);
            }
            else
            {
                AuthFail af = new AuthFail("Denied", "Authentication Error");
                return Ok(af);
            }
        }
    }
}
