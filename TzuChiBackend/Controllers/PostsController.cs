using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TzuChiClassLibrary.DAL;
using TzuChiClassLibrary.BO;
using TzuChiBackend.Security;
using TzuChiBackend.ViewModels;
using TzuChiBackend.Services;
using TzuChiBackend.Context;
using PagedList;
using TzuChiBackend.Helpers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Http;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace TzuChiBackend.Controllers
{
	public class PostViewModel
	{
		public PostViewModel()
		{

		}
		public PostViewModel(Content content)
		{
			id = content.ContentID;
			title = content.ContentName;
			number = content.SerialNo;
			author = content.Author;
			this.content = content.ContentText;

			this.createdAt = Convert.ToDateTime(content.ContentCreateTime);
			this.updatedAt = Convert.ToDateTime(content.ContentUpdateTime);

			updatedBy = content.ContentUpdater;

			if (String.IsNullOrEmpty(updatedBy)) throw new Exception("updatedBy==null");

			if (content.ContentTime.HasValue)
			{
				date = Convert.ToDateTime(content.ContentTime).ToShortDateString();
			}
			else
			{
				date = this.createdAt.ToShortDateString();
			}


			fileIds = string.Join(",", content.FileUplaods.Select(f => f.ItemOID));

			
		}

		public string id { get; set; }
		public string title { get; set; }
		public string termNumber { get; set; }
		public string number { get; set; }
		public string author { get; set; }
		public string content { get; set; }
		public string summary { get; set; }
		public string date { get; set; }

		public string updatedBy { get; set; }


		public DateTime createdAt { get; set; }
		public DateTime updatedAt { get; set; }

		public string fileIds { get; set; }

		public int categoryId { get; set; }
		public string categoryName { get; set; }
	}

	public class MediaViewModel
	{
		public MediaViewModel() { }

		public MediaViewModel(FileUplaod file)
		{
			
			this.contentId = file.ContentID;
			this.type = file.FunctionType.ToLower();

			this.name = file.FileName;
			this.title = file.FileName.Split(new char[] { '.' })[0];

			this.updatedBy = file.Creator;


			this.order = file.CoverImage? 1 : 0;

			this.width = file.ImageWidth.HasValue? (int)file.ImageWidth : 0;
			this.height = file.ImageHeight.HasValue ? (int)file.ImageHeight : 0;

			this.path = file.Path;
			this.previewPath = file.PreviewPath;

			this.createdAt = (DateTime)file.CreateTime;
		}

		

		public string contentId { get; set; }

		public int order { get; set; }

		public string type { get; set; }

		public string path { get; set; }

		public string previewPath { get; set; }

		public string name { get; set; }

		public string title { get; set; }

		public string updatedBy { get; set; }


		public int width { get; set; }

		public int height { get; set; }

		public DateTime createdAt { get; set; }

	}



	public class PostsController : BaseController
	{
		
		private SchoolDairyService schoolDairyService;
	
		public PostsController()
		{
			
		
			this.schoolDairyService = new SchoolDairyService(this.Context);
			
		}
		public ActionResult test()
		{
			var famerList = schoolDairyService.GetFamerList();
			return Content(famerList.FirstOrDefault().ContentName + famerList.Count().ToString());
		}

		public ActionResult Export(int count=0)
		{
			DateTime date = new DateTime(2017,11,30);
			string category = "diary";
			var dairyList = schoolDairyService.GetDiaryList().Where(c=>c.ContentCreateTime > date);
				//.Where(c => c.ContentCreateTime.HasValue && Convert.ToDateTime(c.ContentCreateTime).Year >= 2013);



			if (count > 0) exportFile(category, dairyList.Take(count).ToList());
			else exportFile(category, dairyList.ToList());




			category = "honor";
			var honorList = schoolDairyService.GetHonorList().Where(c => c.ContentCreateTime > date);
			//.Where(c => c.ContentCreateTime.HasValue && Convert.ToDateTime(c.ContentCreateTime).Year >= 2013);



			if (count > 0) exportFile(category, honorList.Take(count).ToList());
			else exportFile(category, honorList.ToList());


			category = "famer";
			var famerList = schoolDairyService.GetFamerList();


			if (count > 0) exportFile(category, famerList.Take(count).ToList());
			else exportFile(category, famerList.ToList());



			return Content("done");
			
		}

		private void exportFile(string category, IEnumerable<Content> contents)
		{
			string date = DateTime.Now.ToString("yyyyMMdd");
			string fileName = String.Format("/Excel/{0}{1}.csv", date, category);
			string filepath = Server.MapPath(fileName);

			
			var posts = new List<PostViewModel>();
			var attachments = new List<MediaViewModel>();
			foreach (var item in contents)
			{
				var model = new PostViewModel(item);
				model.termNumber = schoolDairyService.GetTermNumber(item);
				model.categoryName = category;
				
				posts.Add(model);


				foreach (var uploadFile in item.FileUplaods)
				{
					var mediaModel = new MediaViewModel(uploadFile);
					attachments.Add(mediaModel);
				}
			}

			WritePosts(filepath, posts);

			fileName = String.Format("/Excel/{0}{1}_medias.csv", date, category);
			filepath = Server.MapPath(fileName);
			WriteAttachments(filepath, attachments);
		}

		void WritePosts(string filepath, List<PostViewModel> posts)
		{
			using (var sw = new StreamWriter(filepath))
			using (var writer = new CsvWriter(sw))
			{
				writer.WriteRecords(posts);
			}
		}

		void WriteAttachments(string filepath, List<MediaViewModel> attachments)
		{
			using (var sw = new StreamWriter(filepath))
			using (var writer = new CsvWriter(sw))
			{
				writer.WriteRecords(attachments);
			}
		}

		public ActionResult Read(string name)
		{
			string fileName = String.Format("/Excel/{0}.csv", name);
			string filepath = Server.MapPath(fileName);


			var posts = new List<PostViewModel>();
			using (var sw = new StreamReader(filepath))
			using (var reader = new CsvReader(sw))
			{
				reader.Configuration.HeaderValidated = null;
				var records=	reader.GetRecords<PostViewModel>();
				foreach (var item in records)
				{
					posts.Add(item);
				}
			   
			}

			return Content("done");
		}
	}
}  
