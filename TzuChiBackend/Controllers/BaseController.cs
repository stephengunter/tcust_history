using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiBackend.Context;

using Newtonsoft.Json;
using System.Text;

namespace TzuChiBackend.Controllers
{
    public abstract class BaseController : Controller
    {
        private MyContext context;

        public BaseController()
        {
            this.context = new MyContext();
        }

        protected MyContext Context
        {
            get { return this.context; }
        }

		

		protected void dd(Object obj)
		{
			
			string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings
			{
				PreserveReferencesHandling = PreserveReferencesHandling.Objects
			});
			

			Response.Write(json);
		}

		protected void dd(IEnumerable<Object> objList)
		{

			var builder = new StringBuilder();

			int i = 0;
			int count = objList.Count();
			foreach (var item in objList)
			{
				builder.Append(JsonConvert.SerializeObject(item, Formatting.Indented));
				i++;

				if (i < count) builder.Append(",");
			}

			
			Response.Write(builder.ToString());
			Response.End();



			
		}


	}
}