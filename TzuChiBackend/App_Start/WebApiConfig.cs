﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TzuChiBackend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

			// WebAPI when dealing with JSON & JavaScript!
			// Setup json serialization to serialize classes to camel (std. Json format)


			var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
			formatter.SerializerSettings.ContractResolver =
				new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();


			config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
