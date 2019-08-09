using System;
using System.Collections.Generic;
using System.Text;

namespace Tcust.Models
{

	public abstract class BasePost: BaseRecord
	{
		public string Title { get; set; }

		public string Content { get; set; }

		

		public bool Down { get; set; }

		
		public int DisplayOrder { get; set; }

		
		


	}

}
