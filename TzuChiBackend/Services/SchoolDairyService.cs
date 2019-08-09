
using System;
using System.Collections.Generic;
using System.Linq;
using TzuChiBackend.Context;
using System.Web.Mvc;
using System.Data.SqlClient;
using TzuChiBackend.Helpers;

namespace TzuChiBackend.Services
{
	public class SchoolDairyService : ContentService
	{
		string DAIRY_ID = "7f266b34-c03e-41ae-84a1-1e27dccc7039"; //校園日誌
		string HONOR_ID = "4af854f1-d569-4394-9a89-3ac1383734c0"; //榮譽榜
		string NEWS_ID = "5ccee554-0fc6-4752-bc58-3bf2b2f8cc97"; //大愛新聞

		private readonly string typeId;


		public SchoolDairyService(MyContext context)
			: base(context)
		{
			this.typeId = Context.ContentTypes.Where(t => t.TypeName == "校園日誌").FirstOrDefault().Id;

		}


		public string GetTypeName(string typeId)
		{
			if (typeId == this.DAIRY_ID) return "Dairy";
			if (typeId == this.HONOR_ID) return "Honor";
			return "";

		}
		public IEnumerable<SelectListItem> TypeSelectList(string id = "")
		{
			var list = new List<SelectListItem>();
			list.Add(new SelectListItem() { Value = DAIRY_ID, Text = "校園日誌", Selected = (id == DAIRY_ID) });
			list.Add(new SelectListItem() { Value = HONOR_ID, Text = "榮譽榜", Selected = (id == HONOR_ID) });

			return list;

		}

		public IEnumerable<Content> Search(string typeId, string yearId, string keyword)
		{
			IEnumerable<Content> postList;
			if (typeId == DAIRY_ID)
			{
				postList = GetDiaryList();
			}
			else if (typeId == HONOR_ID)
			{
				postList = GetHonorList();
			}
			else
			{
				return null;
			}


			if (!String.IsNullOrEmpty(yearId))
			{
				postList = postList.Where(p => p.Educated.CategoryYearID == yearId);
			}

			if (!String.IsNullOrEmpty(keyword))
			{
				postList = postList.Where(p => (
													  (p.SerialNo != null && p.SerialNo.Contains(keyword))
												   || (p.ContentName != null && p.ContentName.Contains(keyword))
												   || (p.ContentText != null && p.ContentText.Contains(keyword))
												   || (p.Author != null && p.Author.Contains(keyword))
											   )

										 );
			}

			return postList;
		}

		IList<string> HonorListIds()
		{
			return Context.Database.SqlQuery<string>(
					"SELECT DISTINCT ContentId FROM dbo.ContentMultipleType WHERE ContentType=@Type", new SqlParameter("Type", HONOR_ID)).ToList();

		}

		public bool IsHonor(Content content)
		{
			var ids = HonorListIds();
			return ids.Contains(content.ContentID);
		}

		public IEnumerable<Content> GetHonorList()
		{
			var ids = HonorListIds();
			return Context.Contents.Where(c => ids.Contains(c.ContentID));

		}

		//大愛
		public IEnumerable<Content> GetNewsList()
		{
			
			return Context.Contents.Where(c => c.TypeID== NEWS_ID);
		}

		public IEnumerable<Content> GetFamerList()
		{
			var date = new DateTime(2055,1,1);
			return Context.Contents.Where(c => c.ContentTime.HasValue && c.ContentTime > date);
		}

		public IEnumerable<Content> GetDiaryList()
		{


			var ids = Context.Database.SqlQuery<string>(
					"SELECT DISTINCT ContentId FROM dbo.ContentMultipleType WHERE ContentType=@Type", new SqlParameter("Type", DAIRY_ID)).ToList();

			return Context.Contents.Where(c => ids.Contains(c.ContentID));
		}

		public override IEnumerable<Content> GetAll()
		{
			return Context.Contents.Where(c => c.TypeID == this.typeId);
		}

		public string[] GetSchoolYearTerm(Content content)
		{
			string year = "";
			var categoryYear = Context.Categories.Find(content.Educated.CategoryYearID);
			if (categoryYear != null) year = categoryYear.CategoryName;

			string term = "";
			var categoryTerm = Context.Categories.Find(content.Educated.CategoryTermID);
			if (categoryTerm != null) term = categoryTerm.CategoryName;

			return new string[] { year, term };
		}

		public string GetTermNumber(Content content)
		{
			var arr = GetSchoolYearTerm(content);
			int number = 0;
			if (arr[1].Contains("一") || arr[1].Contains("1")) number = 1;
		    else if (arr[1].Contains("二") || arr[1].Contains("2")) number = 2;
			if (arr[1].Contains("三") || arr[1].Contains("3")) number = 3;
			if (arr[1].Contains("四")|| arr[1].Contains("4")) number = 4;

			return arr[0] + number.ToString();
		}



		//private void Sync(Content content, bool honor, bool oldData = false)
		//      {
		//	DateTime createdAt = DateTime.Now;
		//	if (content.ContentCreateTime.HasValue) createdAt = (DateTime)content.ContentCreateTime;


		//	var files = new List<Tcust.Models.UploadFile>();
		//	if (!content.FileUplaods.IsNullOrEmpty())
		//	{
		//		foreach (var item in content.FileUplaods)
		//		{
		//			var file = new Tcust.Models.UploadFile
		//			{
		//				ItemOID = item.ItemOID,
		//				Name = item.FileName,
		//				Path = item.Path,
		//				PreviewPath = item.PreviewPath,
		//				PS = item.ItemInfo,
		//				Top = item.CoverImage,
		//				Type = item.FunctionOID

		//			};



		//			if (item.ImageWidth.HasValue) file.Width = (int)item.ImageWidth;
		//			if (item.ImageHeight.HasValue) file.Height = (int)item.ImageHeight;

		//			files.Add(file);
		//		}
		//	}

		//	string category = "";
		//	if (honor) category = "honor";

		//	string author = content.Author;
		//	string title = content.ContentName;
		//	string contentText = content.ContentText;
		//	string contentId = content.ContentID;
		//	string updatedBy = content.ContentUpdater;

		//	this.postService.CreateOrUpdate(author, title, contentText, contentId, updatedBy, createdAt, files, category);




		//}
		//      public void SyncAll()
		//      {
		//          var  diaryList = this.GetDiaryList();        
		//          foreach (var content in diaryList)
		//          {
		//              Sync(content , false);
		//          }

		//          var honorList = this.GetHonorList();
		//          foreach (var content in honorList)
		//          {
		//              Sync(content, true);
		//          }

		//      }

		public override void ReFormatAll()
        {
          
            var diaryList = GetAll().ToList();
            foreach (var item in diaryList)
            {
                ReFormat(item);
            }


        }


       

		

	}
}