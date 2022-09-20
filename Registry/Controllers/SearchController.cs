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
using Utilities;

namespace Registry.Controllers
{
    public class SearchController : ApiController
    {
        InterfaceChannel iChannel = new InterfaceChannel();
        AuthenticatorInterface iserverChannel;
        public IHttpActionResult Post([FromBody] string description, [FromUri()] int token)
        {
            string servicelocation = Paths.SERVICES_FILE_PATH;
            StreamReader reader = new StreamReader(servicelocation);
            iserverChannel = iChannel.generateChannel();
            string validateResult = iserverChannel.Validate(token);

            if (validateResult == "Validated")
            {
                /*while ((reader.ReadLine()) != null)
                {
                    //deserialize the object 
                    Service service = javaScriptSerializer.Deserialize<Service>(reader.ReadLine());
                    //if the name is equal to the name that user entered
                    if(service.name == word)
                    {
                        service(service)
                }
            }
            sr.Close();

                } */
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(servicelocation).ToList();
                List<string> data = new List<string>();
                for (int i = 0; i < lines.Count; i++)
                {
                    Service services = JsonConvert.DeserializeObject<Service>(lines[i]);
                    if (services.name.Contains(description))
                    {
                        data.Add(JsonConvert.SerializeObject(services));
                    }
                }
                return Ok(data);
            }
            else
            {
                AuthFail af = new AuthFail("Denied", "Authentication Error");
                return Ok(af);
            }

        
        }
    }
}
