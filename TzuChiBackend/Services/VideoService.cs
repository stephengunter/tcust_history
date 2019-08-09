
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TzuChiBackend.Context;
using System.Web;
using System.Web.Mvc;

using TzuChiClassLibrary.DAL;
using TzuChiBackend.Helpers;

namespace TzuChiBackend.Services
{
    public class VideoService : BaseService
    {

        private readonly string contentType = "f2703828-4055-4c17-8cd5-153dc2ec1d86";

        private readonly string categoryType = "Video";

        private readonly string folder = "Video";

        private readonly string frontRootPath; //C:\Users\Stephen\Desktop\學校\校史館\tccn\program_code\TzuChi\TzuChiFrontend\       
        private readonly string savePath;

        private string CoverImageFolder
        {
            get { return Path.Combine(savePath, "img"); }
        }

        private ISchoolDiaryManagement gSchoolDiaryManagement = new SchoolDiaryManagementImpl();

        public VideoService(MyContext context, string frontRootPath="")
                : base(context)
        {
            if (!String.IsNullOrEmpty(frontRootPath))
            {
                this.frontRootPath = frontRootPath;
                this.savePath = Path.Combine(frontRootPath, this.folder);
                if (!System.IO.Directory.Exists(savePath))
                {
                    throw new Exception("video路徑不存在. " + savePath);
                }
            }
           
        }
       

        public Content GetById(string id)
        {
            return Context.Contents.Find(id);
        }

		public FileUplaod GetCoverImage(Content video)
		{
			
			return video.FileUplaods.Where(f => f.FunctionOID == "Image").FirstOrDefault();
			
		}

		FileUplaod SaveImage(string id ,HttpPostedFileBase image)
		{
			string imageDisplayName = image.FileName;
			string imageExtension = Path.GetExtension(imageDisplayName);
			string imageFileName = string.Format("{0}{1}", id, imageExtension);

			string imgPath = Path.Combine(this.CoverImageFolder, imageFileName);

			image.SaveAs(imgPath);

			var imageRecord = new FileUplaod()
			{
				ItemOID = Guid.NewGuid().ToString(),
				FunctionOID = "Image",
				FileName = imageFileName,
				Path = imgPath

			};

			return imageRecord;

		}

		public void Create(HttpPostedFileBase file, string title,string categoryId, int order, HttpPostedFileBase image)
        {
            var category = GetCategoryById(categoryId);
            if (category == null) throw new Exception("分類不存在.category id=" + categoryId);

            string id = Guid.NewGuid().ToString();

			var content = new Content
			{
				ContentID = id,
				TypeID = this.contentType,
				ContentName = title,
				SerialNo = categoryId,
				DisplayOrder = order,

				ContentCreateTime = DateTime.Now,
				ContentUpdateTime = DateTime.Now
			};

			var attachList = new List<FileUplaod>();

			string fileName = String.Format("{0}.mp4",id);
            string videoSavePath = Path.Combine(this.savePath, fileName);

			file.SaveAs(videoSavePath);

			var videoRecord = new FileUplaod()
			{
				ItemOID = Guid.NewGuid().ToString(),
				FunctionOID = "Video",
				FileName = fileName,
				Path = videoSavePath

			};
			attachList.Add(videoRecord);

			//封面圖
			var imageRecord = SaveImage( id, image);
			
			attachList.Add(imageRecord);



			content.FileUplaods = attachList;

            Context.Contents.Add(content);
            Context.SaveChanges();
			
           
        }

        public void Update(string id, string title, string categoryId, int order, HttpPostedFileBase image)
		{
            var entity = GetById(id);
            if (entity == null) throw new Exception("影片不存在. id=" + id);

            var category = GetCategoryById(categoryId);
            if (category == null) throw new Exception("分類不存在.category id=" + categoryId);

			//封面圖

			if (image != null)
			{
				var imageRecord = SaveImage(id, image);

				var coverImage = GetCoverImage(entity);
				if (coverImage == null)
				{
					imageRecord.ContentID = id;
					Context.FileUplaods.Add(imageRecord);
				}
				else
				{
					coverImage.Path = imageRecord.Path;
					coverImage.FunctionOID= imageRecord.FunctionOID;
					coverImage.FileName = imageRecord.FileName;
				}
			
			}
			

			entity.DisplayOrder = order;
            entity.SerialNo = categoryId;
            entity.ContentName = title;

            Context.SaveChanges();


        }

        public void Delete(string id)
        {
            var entity = GetById(id);
            if (entity == null) throw new Exception("影片不存在. id=" + id);
           
          

            bool result = gSchoolDiaryManagement.Delete(id);

            if (!result)
            {
                throw new Exception("刪除失敗");
            }


            string fileName = String.Format("{0}.mp4", id);
            string videoSavePath = Path.Combine(this.savePath, fileName);
             
            if(File.Exists(videoSavePath)) File.Delete(videoSavePath);

            fileName = String.Format("{0}.jpg", id);
            string imgPath = Path.Combine(this.CoverImageFolder, fileName);

            if (File.Exists(imgPath)) File.Delete(imgPath);



        }
        public void CreateCategory(string name)
        {
            string categoryId = Guid.NewGuid().ToString();
            Context.Categories.Add(new Category
            {
                CategoryID = categoryId,
                CategoryTypeID = this.categoryType,
                CategoryName = name,

                CreateTime = DateTime.Now,  
                             
                Sort = 0,
                UpdateTime=DateTime.Now
            });

            Context.SaveChanges();

          
        }

        public void UpdateCategory(string id, string name, int sort)
        {
            var entity = GetCategoryById(id);
            if (entity == null) throw new Exception("資料不存在. id=" + id);

            entity.CategoryName = name;
           
            entity.UpdateTime = DateTime.Now;

            entity.Sort = sort;

            Context.SaveChanges();
        }

        public Category GetCategoryById(string id)
        {
            return Context.Categories.Find(id);
        }

        private Category DefaultCategory()
        {
            return GetVideoCategories(1).FirstOrDefault();
        }

        public IEnumerable<Category> HasVideoCategories()
        {
            var activeCategories = GetVideoCategories(1);            
            if (activeCategories.IsNullOrEmpty()) return activeCategories;

            var hasVideoList = new List<Category>();
            foreach (var item in activeCategories)
            {
                if (GetByCategory(item.CategoryID).Count() > 0)
                {
                    hasVideoList.Add(item);
                }
            }

            return hasVideoList;

        }

        public IEnumerable<Category> GetVideoCategories(int active=0)
        {

            IEnumerable<Category> categories= Context.Categories.Where(c => c.CategoryTypeID == this.categoryType);
            if (active >= 0)
            {
                categories = categories.Where(c => c.Sort >= 0);
            }
            else
            {
                categories = categories.Where(c => c.Sort < 0);
            }

            if (categories.IsNullOrEmpty()) return categories;

            return categories.OrderByDescending(c => c.Sort);


        } 

        public IEnumerable<SelectListItem> CategorySelectList(string selected = "")
        {
            var list = new List<SelectListItem>();

            var categories = GetVideoCategories();
            if (categories.IsNullOrEmpty()) return list;

            return categories.Select(c =>
                       new SelectListItem
                       {
                           Text = c.CategoryName,
                           Value = c.CategoryID.ToString(),
                           Selected = selected == c.CategoryID
                       });
            
        }
        public IEnumerable<Content> GetByCategory(string category)
        {
            IEnumerable<Content> contentList = this.GetAll();
            if (String.IsNullOrEmpty(category))
            {
                var defaultCategory = DefaultCategory();
                if (defaultCategory != null)
                {
                    category = DefaultCategory().CategoryID;
                }
                else
                {
                    return contentList;
                }
               
            }

            return contentList.Where(c => c.SerialNo == category);
        }
        public IEnumerable<Content> Search(string category, string keyword)
        {
           
            if (!String.IsNullOrEmpty(keyword))
            {
                IEnumerable<Content> contentList = this.GetAll();

                contentList = contentList.Where(p => (
                                                      (p.SerialNo != null && p.SerialNo.Contains(keyword))
                                                   || (p.ContentName != null && p.ContentName.Contains(keyword))
                                                   || (p.ContentText != null && p.ContentText.Contains(keyword))

                                               )

                                         );

                return contentList;
            }
            else
            {
                return GetByCategory(category);

            }

           
        }
		public void UpdateOrder(string id, int order)
		{
			var entity = this.GetById(id);
			if (entity == null) throw new Exception("查無資料.  id= " + id);


			entity.DisplayOrder = order;

			Context.SaveChanges();
		}

		public void UpdateCategoryOrder(string id, int order)
		{
			var entity = GetCategoryById(id);
			if (entity == null) throw new Exception("查無資料.  id= " + id);


			entity.Sort = order;

			Context.SaveChanges();
		}
		private IEnumerable<Content> GetAll()
        {
            return Context.Contents.Where(c => c.TypeID == this.contentType);
        }


        

    }
}