using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TzuChiBackend.Security;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.DAL;
using Core.Extensions;
using PagedList;

using TzuChiBackend.ViewModels;
using TzuChiBackend.Services;
using TzuChiBackend.Context;


namespace TzuChiBackend.Controllers
{
    public class GunterController : BaseController
	{
		private CategoryService categoryService;
		private PlanInsideService planInsideService;

		ICoordinateManagement gCoordinateManagement;

		public GunterController()
		{
			this.categoryService = new CategoryService(Context);
			this.planInsideService = new PlanInsideService(Context);

			this.gCoordinateManagement = new CoordinateManagementImpl();
		}
		public ActionResult test()
		{
			var context = new DefaultContext();
			var country = context.CountryAreas.FirstOrDefault();
			country.CoordinateId = null;
			
			

			context.SaveChanges();

			//List<CoordinateModel> coordinateType = gCoordinateManagement.GetAll(PlanInsideModel.TYPEID);
			//dd(coordinateType);
			//Response.End();

			//dd(internShips.FirstOrDefault());

			return Content("test");
		}
		public ActionResult copy()
		{
			var context =new DefaultContext();

			seedCountries(context);

			seedPartitions(context);

			setTaiwanAreas(context);


			return Content("done");
		}
		public ActionResult Index()
        {
            return Content("Gunter");
        }

		void seedDepartments(DefaultContext context)
		{
			var departments = categoryService.GetDepartments();

			foreach (var item in departments)
			{
				var exist = context.Departments.Where(d => d.Name == item.CategoryName).FirstOrDefault();
				if (exist == null)
				{
					var department = new Model.Department()
					{
						Active = true,
						CreateDate = DateTime.Now,
						Name = item.CategoryName
					};
					context.Departments.Add(department);
				}

			}

			context.SaveChanges();
		}

		void seedCountries(DefaultContext context)
		{
			var country = new CountryArea()
			{
				Name = "台灣",
				Active = true,
				Parent = 0
			};
			var exist = context.CountryAreas.Where(c => c.Name == country.Name).FirstOrDefault();
			if (exist == null)
			{
				context.CountryAreas.Add(country);
				context.SaveChanges();
			}
			else {
				country.Id = exist.Id;
			}

			var taiwanCountries = categoryService.GetTaiwanCountries();
			foreach (var item in taiwanCountries)
			{
				exist = context.CountryAreas.Where(c => c.Name == item.CategoryName).FirstOrDefault();
				if (exist == null)
				{
					context.CountryAreas.Add(new CountryArea()
					{
						Name=item.CategoryName,
						Active = true,
						Parent =  country.Id
					});
				}
			}

			context.SaveChanges();
		}

		void seedPartitions(DefaultContext context)
		{
			var parent= context.CountryAreas.Where(c => c.Name == "台灣").FirstOrDefault();
			if (parent == null) throw new Exception();

			var areas = this.categoryService.GetTaiwanAreas();
			foreach (var item in areas)
			{
				var exist = context.CountryAreas.Where(c => c.Name == item.CategoryName).FirstOrDefault();
				if (exist == null)
				{
					var area = new CountryArea()
					{
						Name = item.CategoryName,
						Active = true,
						Parent = parent.Id,
						IsPartition=true
					};
					context.CountryAreas.Add(area);
				}
			}
			

			context.SaveChanges();
		}

		void setTaiwanAreas(DefaultContext context)
		{
			var north= context.CountryAreas.Where(c => c.Name == "北區" && c.IsPartition).FirstOrDefault();
			if (north == null) throw new Exception();

			string[] countries = { "台北市", "新北市", "基隆巿", "桃園縣", "新竹巿", "新竹縣", "苗栗縣" };

			foreach (var name in countries)
			{
				var country= context.CountryAreas.Where(c => c.Name == name).FirstOrDefault();
				if (country == null) throw new Exception();

				country.PartitionId = north.Id;
			}

			context.SaveChanges();

			var center = context.CountryAreas.Where(c => c.Name == "中區" && c.IsPartition).FirstOrDefault();
			if (center == null) throw new Exception();

			string[] centerCountries = { "台中巿", "彰化縣", "南投縣", "雲林縣", "嘉義縣" };

			foreach (var name in centerCountries)
			{
				var country = context.CountryAreas.Where(c => c.Name == name).FirstOrDefault();
				if (country == null) throw new Exception();

				country.PartitionId = center.Id;
			}

			context.SaveChanges();

			var south = context.CountryAreas.Where(c => c.Name == "南區" && c.IsPartition).FirstOrDefault();
			if (south == null) throw new Exception();

			string[] southCountries = { "台南巿", "高雄巿", "屏東縣"};

			foreach (var name in southCountries)
			{
				var country = context.CountryAreas.Where(c => c.Name == name).FirstOrDefault();
				if (country == null) throw new Exception();

				country.PartitionId = south.Id;
			}

			context.SaveChanges();

			var east = context.CountryAreas.Where(c => c.Name == "東區" && c.IsPartition).FirstOrDefault();
			if (east == null) throw new Exception();

			string[] eastCountries = { "花蓮縣", "台東縣", "宜蘭縣" };

			foreach (var name in eastCountries)
			{
				var country = context.CountryAreas.Where(c => c.Name == name).FirstOrDefault();
				if (country == null) throw new Exception();

				country.PartitionId = east.Id;
			}

			context.SaveChanges();


			var other = context.CountryAreas.Where(c => c.Name == "離島" && c.IsPartition).FirstOrDefault();
			if (other == null) throw new Exception();

			string[] otherCountries = { "金門縣", "連江縣", "澎湖縣" };

			foreach (var name in otherCountries)
			{
				var country = context.CountryAreas.Where(c => c.Name == name).FirstOrDefault();
				if (country == null) throw new Exception();

				country.PartitionId = other.Id;
			}

			context.SaveChanges();

		}

		void seedCountryPoints()
		{
			List<CoordinateModel> coordinateType = gCoordinateManagement.GetAll(PlanInsideModel.TYPEID);
			dd(coordinateType);
		}
		#region Blog
		string UpdateAllQRCode()
		{
			var diaryList = schoolDairyService.GetDiaryList();
			foreach (var content in diaryList)
			{
				content.RelatedLink = GetBlogPostUrl(content.ContentID);
			}

			var honorList = schoolDairyService.GetHonorList();
			foreach (var content in honorList)
			{
				content.RelatedLink = GetBlogPostUrl(content.ContentID);
			}

			Context.SaveChanges();

			return diaryList.FirstOrDefault().ContentID;
		}

		string GetBlogPostUrl(string contentId)
		{
			string url = System.Configuration.ConfigurationManager.AppSettings["blog_url"];

			var post = postService.GetByContentId(contentId);
			if (post == null) return url;

			return String.Format("{0}/Posts/Details/{1}", url, post.Id);

		}



		public void Sync(string contentId, bool oldData = false)
		{
			var content = Context.Contents.Find(contentId);
			if (content == null) return;

			bool honor = schoolDairyService.IsHonor(content);
			Sync(content, honor);

			content.RelatedLink = GetBlogPostUrl(content.ContentID);
			this.Context.SaveChanges();


		}


		void Sync(Content content, bool honor, bool oldData = false)
		{
			DateTime createdAt = DateTime.Now;
			if (content.ContentCreateTime.HasValue) createdAt = (DateTime)content.ContentCreateTime;


			var files = new List<Blog.Models.UploadFile>();
			if (!content.FileUplaods.IsNullOrEmpty())
			{
				foreach (var item in content.FileUplaods)
				{
					var file = new Blog.Models.UploadFile();
					SetBlogFileValues(file, item);

					files.Add(file);
				}
			}

			string category = "";
			if (honor) category = "honor";

			string author = content.Author;
			string title = content.ContentName;
			string contentText = content.ContentText;
			string contentId = content.ContentID;
			string updatedBy = content.ContentUpdater;

			CreateOrUpdatePost(author, title, contentText, contentId, updatedBy, createdAt, files, category);

		}
		public bool ContentExist(string contentId)
		{
			var post = postService.GetByContentId(contentId);
			return post != null;
		}
		void CreateOrUpdatePost(string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt, IEnumerable<Blog.Models.UploadFile> files, string category = "")
		{
			if (this.ContentExist(contentId))
				this.Update(author, title, content, contentId, updatedBy, createdAt, files, category);
			else
				this.CreatePost(author, title, content, contentId, updatedBy, createdAt, files, category);
		}

		void CreatePost(string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt, IEnumerable<Blog.Models.UploadFile> files, string category = "")
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

		Post InitPost(string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt = null)
		{
			var post = new Post();
			SetPostValues(post, author, title, content, contentId, updatedBy, createdAt);

			return post;
		}
		void SetPostValues(Post post, string author, string title, string content, string contentId, string updatedBy, DateTime? createdAt = null)
		{
			if (string.IsNullOrEmpty(author)) author = "";
			post.Author = author;

			if (string.IsNullOrEmpty(title)) title = "";
			post.Title = title;

			if (string.IsNullOrEmpty(content)) content = "";
			post.Content = content;

			post.ContentId = contentId;
			post.SetBaseRecord(updatedBy, createdAt);


			post.CreateYear = post.CreatedAt.Year;
			post.CreateMonth = post.CreatedAt.Month;
		}
		void SetBlogFileValues(Blog.Models.UploadFile uploadFile, TzuChiBackend.Context.FileUplaod file)
		{
			uploadFile.ItemOID = file.ItemOID;
			uploadFile.Name = file.FileName;
			uploadFile.Path = file.Path;
			uploadFile.PreviewPath = file.PreviewPath;
			uploadFile.PS = file.ItemInfo;
			uploadFile.Top = file.CoverImage;
			uploadFile.Type = file.FunctionOID;

			if (file.ImageWidth.HasValue) uploadFile.Width = (int)file.ImageWidth;
			if (file.ImageHeight.HasValue) uploadFile.Height = (int)file.ImageHeight;
		}

		void DeletePost(string contentId)
		{
			var post = postService.GetByContentId(contentId);
			if (post != null) this.postService.Delete(post);

		}

		#endregion

	}//end class
}