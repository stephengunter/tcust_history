using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TzuChiBackend.ViewModels
{
	public class Rootobject
	{
		public List<PostViewModel> result { get; set; }
		public string status { get; set; }
	}

	public class Result
	{
		public string id { get; set; }
		public string author { get; set; }
	}




	public class PostSearchModel
	{
		public IEnumerable<PostViewModel> PostViewList { get; set; }
	}
	public class PostViewModel
	{
		public string Id { get; set; }
		public string Author { get; set; }

		//public int CreateYear { get; set; }

		//public int CreateMonth { get; set; }

		//public bool Top { get; set; }

		//public string Summary { get; set; }


		//public ICollection<MediaViewModel> Attachments { get; set; }


		//public ICollection<Category> Categories { get; }
	}

	public class MediaViewModel
	{
		public int Id { get; set; }
		public int PostId { get; set; }
		public string ItemOID { get; set; }
		public string Path { get; set; }
		public string Name { get; set; }
		public bool Top { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}
}