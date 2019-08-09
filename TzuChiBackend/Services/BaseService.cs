
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TzuChiBackend.Context;

namespace TzuChiBackend.Services
{
    public abstract class BaseService
    {
        private MyContext context;

        public BaseService(MyContext context)
        {
            this.context = context;
        }

        protected MyContext Context
        {
            get { return this.context; }
        }
    }
}
