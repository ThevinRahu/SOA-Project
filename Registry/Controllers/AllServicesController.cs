﻿using Authenticator;
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
    public class AllServicesController : ApiController
    {
        InterfaceChannel iChannel = new InterfaceChannel();
        AuthenticatorInterface iserverChannel;
        public IHttpActionResult Get(int token)
        {
            string servicelocation = Directory.GetCurrentDirectory() + @"..\..\Services\services.txt";
            StreamReader reader = new StreamReader(servicelocation);
            iserverChannel = iChannel.generateChannel();
            string validateResult = iserverChannel.Validate(token);

            if (validateResult == "Validated")
            {
                return Ok(File.ReadAllLines(servicelocation));
            }
            else
            {
                AuthFail af = new AuthFail("Denied", "Authentication Error");
                return Ok(af);
            }
        }
    }
}