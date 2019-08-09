using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Tcust.Models
{
	public abstract class BsseUploadFile : BaseEntity
	{

		public string Path { get; set; }

		public string PreviewPath { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public string Type { get; set; }

		public string Name { get; set; }

		public string PS { get; set; }

		
	}
}
