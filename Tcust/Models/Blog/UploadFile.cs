
using System.Text.RegularExpressions;

namespace Tcust.Models
{
	public class UploadFile
	{
		public int Id { get; set; }

		public int PostId { get; set; }

		public string ItemOID { get; set; }

		public string Path { get; set; }

		public string PreviewPath { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public string Type { get; set; }

		public string Name { get; set; }

		public string PS { get; set; }

		public bool Top { get; set; }

		public Post Post { get; set; }

		public string ClearName
		{
			get
			{
				return Regex.Replace(this.Name, ".jpg", "", RegexOptions.IgnoreCase);
			}
		}
	}
}
