using System;
using System.Collections.Generic;
using System.Linq;
using Blog.DAL;
using Blog.Models;
using ApplicationCore.Helpers;
using ApplicationCore.Specifications;

namespace TzuChiBackend.Services.Blog
{
	public interface IPostService
	{
		IEnumerable<Post> TopPosts();
		IEnumerable<Post> GetAll();
		Post GetById(int id);
		Post GetByContentId(string contentId);

		void CreateOrUpdate(string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt, IEnumerable<UploadFile> files, string category = "");

		void UpdateTopPost(int id, string title, string summary, int cover);
		void RemoveTopPost(int id);
		void UpdateOrder(int id, int order);

		void Delete(string contentId);
	}

	public class PostFilterSpecification : BaseSpecification<Post>
	{

		public PostFilterSpecification(string contentId) : base(p=>p.ContentId== contentId)
		{
			AddInclude(p => p.UploadFiles);
		}

	}

	public class UploadFileSpecification : BaseSpecification<UploadFile>
	{

		public UploadFileSpecification(int postId) : base(p => p.PostId == postId)
		{
		}

	}

	public class PostService: IPostService
	{
		private readonly IBlogRepository<Post> postRepository;
		private readonly IBlogRepository<Category> categoryRepository;
		private readonly IBlogRepository<UploadFile> uploadFileRepository;

		public PostService(IBlogRepository<Post> postRepository, IBlogRepository<Category> categoryRepository, IBlogRepository<UploadFile> uploadFileRepository)
		{
			this.postRepository = postRepository;
			this.categoryRepository = categoryRepository;
			this.uploadFileRepository = uploadFileRepository;
		}


		public IEnumerable<Post> GetAll()
		{
			return postRepository.ListAll().Where(p => p.CreateYear >= 2013);
		}
		public Post GetById(int id)
		{
			
			return postRepository.GetById(id);
		}

		public Post GetByContentId(string contentId)
		{
			var filter = new PostFilterSpecification(contentId);
			
			return postRepository.GetSingleBySpec(filter);
			
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
			return  categoryRepository.ListAll().Where(c=>c.Code==code).FirstOrDefault();
		}

		public void Create(string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt, IEnumerable<UploadFile> files, string category = "")
		{
			Post entity = InitPost(author, title, content, contentId, updatedBy, createdAt);
			if (!files.IsNullOrEmpty()) entity.UploadFiles = files.ToList();

			//if (!string.IsNullOrEmpty(category))
			//{
			//	Category categoryByCode = this.GetCategoryByCode(category);
			//	if (categoryByCode != null)
			//	{
			//		entity.Categories = new List<Category>();
			//		entity.Categories.Add(categoryByCode);
			//	}
			//}

			postRepository.Add(entity);
		}

		private void DeleteAttachFiles(int postId)
		{
			var filter = new UploadFileSpecification(postId);
			var files = uploadFileRepository.List(filter);

			foreach (var item in files)
			{
				uploadFileRepository.Delete(item);
			}
		}

		public void Update(string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt, IEnumerable<UploadFile> files, string category = "")
		{
			Post post = this.GetByContentId(contentId);
			foreach (var item in post.UploadFiles)
			{
				post.UploadFiles.Remove(item);
			}

			//DeleteAttachFiles(post.Id);

			


			//post.UploadFiles= new List<UploadFile>();

			//SetValues(post, author, title, content, contentId, updatedBy, createdAt);

			//foreach (UploadFile file in files)
			//{
			//	post.UploadFiles.Add(file);
			//}


			//if (!string.IsNullOrEmpty(category))
			//{
			//	Category categoryByCode = this.GetCategoryByCode(category);
			//	if (categoryByCode != null)
			//	{
			//		post.Categories = new List<Category>();
			//		post.Categories.Add(categoryByCode);
			//	}
			//}

			postRepository.Update(post);
		}

		public void Delete(string contentId)
		{
			Post post = this.GetByContentId(contentId);
			if (post == null) return;

			postRepository.Delete(post);
		}


		public IEnumerable<Post> TopPosts()
		{
			return this.GetAll().Where(p => p.Top).OrderByDescending(p => p.DisplayOrder)
													.ThenByDescending(p => p.CreatedAt);
		}

		public void UpdateTopPost(int id, string title, string summary, int cover)
		{
			Post post = GetById(id);
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


			postRepository.Update(post);
		}

		public void RemoveTopPost(int id)
		{
			Post post = GetById(id);
			if (post == null) throw new Exception("查無資料.  id= " + id.ToString());

			post.Top = false;
			postRepository.Update(post);
		}

		public void UpdateOrder(int id, int order)
		{
			Post post = GetById(id);
			if (post == null) throw new Exception("查無資料.  id= " + id.ToString());


			post.DisplayOrder = order;

			postRepository.Update(post);
		}


		private Post InitPost(string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt = null)
		{
			var post = new Post();
			SetValues(post, author,  title,  content,  contentId,  updatedBy, createdAt);

			return post;
		}

		

		private void SetValues(Post post, string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt = null)
		{
			if (string.IsNullOrEmpty(author))
				author = "";
			post.Author = author;
			if (string.IsNullOrEmpty(title))
				title = "";
			post.Title = title;
			if (string.IsNullOrEmpty(content))
				content = "";
			post.Content = content;

			post.ContentId = contentId;

			post.SetBaseRecord(updatedBy, createdAt);


			post.CreateYear = post.CreatedAt.Year;
			post.CreateMonth = post.CreatedAt.Month;
		}


	}


	
}