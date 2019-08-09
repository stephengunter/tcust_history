using System;
using System.Collections.Generic;
using System.Linq;
using Tcust.Models;
using Tcust.DAL;
using Tcust.Core;
using System.Data.Entity;

namespace Tcust.Services
{
	public class PostService
	{
		private readonly BlogContext _context;
		public PostService()
		{
			this._context = new BlogContext();
		}

		BlogContext Context { get { return this._context; } }


		public IEnumerable<Post> GetAll()
		{
			return Context.Posts.Where(p => p.CreateYear >= 2013);
		}
		public Post GetById(int id)
		{
			return Context.Posts.Find(id);
		}

		public Post GetByContentId(string contentId)
		{
			return Context.Posts.Where(p => p.ContentId == contentId).FirstOrDefault();
		}
		public bool ContentExist(string contentId)
		{
			return this.GetByContentId(contentId) != null;
		}

		public void CreateOrUpdate(string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt, IEnumerable<UploadFile> files, string category = "")
		{
			if (this.ContentExist(contentId))
				this.Update(author, title, content, contentId, updatedBy, createdAt, files, category);
			else
				this.Create(author, title, content, contentId, updatedBy, createdAt, files, category);
		}

		private Category GetCategoryByCode(string code)
		{
			code = code.ToLower();
			return Context.Categories.Where(c => c.Code == code).FirstOrDefault();
		}

		public void Create(string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt, IEnumerable<UploadFile> files, string category = "")
		{
			Post entity = new Post(author, title, content, contentId, updatedBy, createdAt);
			if (!files.IsNullOrEmpty<UploadFile>())
				entity.UploadFiles = files.ToList<UploadFile>();
			if (!string.IsNullOrEmpty(category))
			{
				Category categoryByCode = this.GetCategoryByCode(category);
				if (categoryByCode != null)
				{
					entity.Categories = new List<Category>();
					entity.Categories.Add(categoryByCode);
				}
			}

			Context.Posts.Add(entity);

			Context.SaveChanges();
		}

		public void Update(string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt, IEnumerable<UploadFile> files, string category = "")
		{
			Post post = this.GetByContentId(contentId);

			List<UploadFile> list = Context.UploadFiles.Where(f => f.PostId == post.Id).ToList();

			if (!list.IsNullOrEmpty())
			{
				foreach (var entity in list)
				{
					Context.UploadFiles.Remove(entity);
					Context.SaveChanges();
				}

			}


			post.SetValues(author, title, content, contentId, updatedBy, createdAt);

			if (!files.IsNullOrEmpty())
			{
				post.UploadFiles = new List<UploadFile>();
				foreach (UploadFile file in files)
				{
					post.UploadFiles.Add(file);
				}

			}
			if (!string.IsNullOrEmpty(category))
			{
				Category categoryByCode = this.GetCategoryByCode(category);
				if (categoryByCode != null)
				{
					post.Categories = new List<Category>();
					post.Categories.Add(categoryByCode);
				}
			}
			Context.SaveChanges();
		}

		public void Delete(string contentId)
		{
			Post post = this.GetByContentId(contentId);
			if (post == null) return;

			Context.Posts.Remove(post);

			Context.SaveChanges();
		}


		public IEnumerable<Post> TopPosts()
		{
			return this.GetAll().Where(p => p.Top).OrderByDescending(p => p.DisplayOrder)
													.ThenByDescending(p => p.CreatedAt);
		}

		public void UpdateTopPost(int id, string title, string summary, int cover)
		{
			Post post = Context.Posts.Find(id);
			if (post == null) throw new Exception("查無資料.  id= " + id.ToString());

			summary = summary.RemoveSciptAndHtmlTags().Trim();
			title = title.RemoveSciptAndHtmlTags().Trim();
			if (!string.IsNullOrEmpty(summary)) post.Summary = summary;

			if (!string.IsNullOrEmpty(title)) post.Title = title;

			post.Top = true;
			if (cover > 0 && post.UploadFiles.Where(f => f.Id == cover).FirstOrDefault() != null)
			{
				foreach (UploadFile uploadFile in post.UploadFiles)
				{
					int num = uploadFile.Id == cover ? 1 : 0;
					uploadFile.Top = num != 0;
				}
			}


			Context.SaveChanges();
		}

		public void RemoveTopPost(int id)
		{
			Post post = Context.Posts.Find(id);
			if (post == null) throw new Exception("查無資料.  id= " + id.ToString());

			post.Top = false;
			Context.SaveChanges();
		}

		public void UpdateOrder(int id, int order)
		{
			Post post = Context.Posts.Find(id);
			if (post == null) throw new Exception("查無資料.  id= " + id.ToString());


			post.DisplayOrder = order;

			Context.SaveChanges();
		}


	}

}
