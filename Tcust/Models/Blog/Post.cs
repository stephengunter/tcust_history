using System;
using System.Collections.Generic;
using System.Linq;
using Tcust.Core;

namespace Tcust.Models
{
	public class Post: BasePost
	{
		public string Author { get; set; }

		public int CreateYear { get; set; }

		public int CreateMonth { get; set; }

		public string ContentId { get; set; }

		public bool Top { get; set; }

		public string Summary { get; set; }
		

		public virtual ICollection<UploadFile> UploadFiles { get; set; }

		public virtual ICollection<Category> Categories { get; set; }

		public string GetDefaultSummary()
		{
			string str = this.Content.RemoveHtmlTags().Trim();
			return str.Substring(0, Math.Min(str.Length, 100));
		}

		public UploadFile CoverImage
		{
			get
			{
				if (this.UploadFiles.IsNullOrEmpty<UploadFile>())
					return (UploadFile)null;
				return this.UploadFiles.Where<UploadFile>((Func<UploadFile, bool>)(f => f.Top)).FirstOrDefault<UploadFile>();
			}
		}

		public Post()
		{
		}

		public Post(string author, string title, string content, string contentId, string updatedBy  , DateTime? createdAt=null)
		{
			this.SetValues(author, title, content, contentId,  updatedBy, createdAt);
		}

		public void SetValues(string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt = null)
		{
			if (string.IsNullOrEmpty(author))
				author = "";
			this.Author = author;
			if (string.IsNullOrEmpty(title))
				title = "";
			this.Title = title;
			if (string.IsNullOrEmpty(content))
				content = "";
			this.Content = content;

			this.ContentId = contentId;

			this.SetBaseRecord(updatedBy, createdAt);


			this.CreateYear = this.CreatedAt.Year;
			this.CreateMonth = this.CreatedAt.Month;
		}

		
	}
}
