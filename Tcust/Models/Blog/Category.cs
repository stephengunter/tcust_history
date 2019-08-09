using System;
using System.Collections.Generic;
using System.Text;

namespace Tcust.Models
{
	public class Category
	{
		public int Id { get; set; }

		public string Code { get; set; }

		public string Name { get; set; }

		public int Order { get; set; }

		public bool Active { get; set; }

		public virtual ICollection<Post> Posts { get; set; }
	}
}
