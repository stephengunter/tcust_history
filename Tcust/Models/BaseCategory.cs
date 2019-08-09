using System;
using System.Collections.Generic;
using System.Text;

namespace Tcust.Models
{
	public abstract class BaseCategory: BaseEntity
	{
		public string Code { get; set; }

		public string Name { get; set; }

		public int Order { get; set; }

		public bool Active { get; set; }

		
	}

	
}
