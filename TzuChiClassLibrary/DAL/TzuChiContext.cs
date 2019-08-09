using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.DAL
{
    public class TzuChiContext : DbContext
    {
        public TzuChiContext()
            : base("MyConnection")
        {
            Database.SetInitializer<TzuChiContext>(null);
        }
    }
}
