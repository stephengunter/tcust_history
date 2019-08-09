
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using TzuChiBackend.Helpers;


namespace TzuChiBackend.ViewModels
{
    public class PostSearchModel : BaseSearchForm
    {

        //public IPagedList<Post> PagedRecordResults { get; set; }

        public IEnumerable<PostViewModel> PostViewList { get; set; }

    }

    public class MenuItem
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public int Count { get; set; }

        public bool Active { get; set; }
    }

    public class PostViewModel
    {

        public PostViewModel()
        {

        }

        public PostViewModel(Post post, bool coverImageOnly = false)
        {
            this.Id = post.Id;
            this.Title = post.Title;
            this.Content = post.Content;
            this.Summary = post.Summary ;
            this.CreatedAt = post.CreatedAt;
            this.Author = post.Author;
			this.DisplayOrder = post.DisplayOrder;



			if (post.UploadFiles.IsNullOrEmpty()) return;
         
            this.MediaViewModels = new List<MediaViewModel>();
            if (coverImageOnly)
            {
                var coverImage = post.UploadFiles.Where(f => f.Top).FirstOrDefault();
                this.MediaViewModels.Add(new MediaViewModel(coverImage));
            }
            else
            {
                foreach (var item in post.UploadFiles)
                {
                    this.MediaViewModels.Add(new MediaViewModel(item));
                }
            }
                
           
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Author { get; set; }
		public int DisplayOrder { get; set; }

		public DateTime CreatedAt { get; set; }

        public virtual ICollection<MediaViewModel> MediaViewModels { get; set; }


        public static string GetDefaultSummary(string content)
        {
            string cleantext = content.RemoveHtmlTags().Trim();
            return cleantext.Substring(0, Math.Min(cleantext.Length, 160));
        }
    }

    public class MediaViewModel
    {
        public MediaViewModel()
        {

        }

        public MediaViewModel(UploadFile file)
        {
            this.Id = file.Id;
            this.ItemOID = file.ItemOID;
            this.Path = file.Path;
            this.Top = file.Top;
            this.Name = file.Name;
            this.Width = file.Width;
            this.Height = file.Height;

        }

        public int Id { get; set; }
        public int PostId { get; set; }
        public string ItemOID { get; set; }

        public string Path { get; set; }


        public string Name { get; set; }

        public bool Top { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class PostEditForm
    {
        public PostEditForm(Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Summary = post.Summary;
            DisplayOrder = post.DisplayOrder;

            if (this.Summary.IsNullOrEmpty()) this.Summary = PostViewModel.GetDefaultSummary(post.Content);

            if (post.UploadFiles.IsNullOrEmpty()) return;

            this.MediaViewModels = new List<MediaViewModel>();
            foreach (var item in post.UploadFiles.OrderByDescending(f=>f.Top))
            {
                if (item.Top) this.CoverId = item.Id;
                this.MediaViewModels.Add(new MediaViewModel(item));
            }


        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }

        public int DisplayOrder { get; set; }
        public int CoverId { get; set; }

        public virtual ICollection<MediaViewModel> MediaViewModels { get; set; }

    }
}