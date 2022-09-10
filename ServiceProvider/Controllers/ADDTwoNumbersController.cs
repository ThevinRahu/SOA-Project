using Authenticator;
using ServiceProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceProvider.Controllers
{
    public class ADDTwoNumbersController : ApiController
    {
        InterfaceChannel iChannel = new InterfaceChannel();
        AuthenticatorInterface iserverChannel;

        // GET: api/ADDTwoNumbers/5/4
        public IHttpActionResult Get(int num1, int num2, int token)
        {
            iserverChannel = iChannel.generateChannel();

            string validateResult = iserverChannel.Validate(token);

            if (validateResult == "Validated")
            {
                int result = num1 + num2;

                Response response = new Response("Accept", "Validated", result);

                return Ok(response);
            }
            else
            {
                Response response = new Response("Denied", "Authentication Error", 0);
                return Ok(response);
            }
        }

    }
}
