using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiBackend.Context;

namespace TzuChiBackend.ViewModels
{
    public class CategoryViewModel
    {
		
		public string CategoryTypeID { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
