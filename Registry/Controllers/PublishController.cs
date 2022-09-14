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
    public class PublishController : ApiController
    {
        InterfaceChannel iChannel = new InterfaceChannel();
        AuthenticatorInterface iserverChannel;
        // POST: api/publish
        public IHttpActionResult Post([FromBody] string description, [FromUri()] int token)
        {

            Service services = JsonConvert.DeserializeObject<Service>(description);


            string servicelocation = @"..\..\Services\services.txt";

            iserverChannel = iChannel.generateChannel();

            string validateResult = iserverChannel.Validate(token);

            if (validateResult == "Validated")
            {
                using (StreamWriter sw = new StreamWriter(servicelocation))
                {
                    sw.WriteLine(description);
                    sw.Close();
                }
                return Ok(services);
            }
            else
            {
                AuthFail af = new AuthFail("Denied", "Authentication Error");
                return Ok(af);
            }

        }
    }
}
