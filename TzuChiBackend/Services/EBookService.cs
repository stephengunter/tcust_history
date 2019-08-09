
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TzuChiBackend.Context;
using TzuChiBackend.ViewModels;
using System.Web;
using System.Xml;
using TzuChiBackend.Helpers;

namespace TzuChiBackend.Services
{
    public class EBookService : BaseService
    {
       
        private readonly string type = "a265c195-9bc1-48d7-a0f3-b05cba54df20";

        private readonly string frontRootPath; //C:\Users\Stephen\Desktop\學校\校史館\tccn\program_code\TzuChi\TzuChiFrontend\
        private readonly string folder; //scripts/book/Images/book
        private readonly string savePath;

      

        public EBookService(MyContext context, string frontRootPath, string folder)
            : base(context)
        {
            this.frontRootPath = frontRootPath;
            this.folder = folder;
            this.savePath = System.IO.Path.Combine(frontRootPath, folder);
            if (!System.IO.Directory.Exists(savePath))
            {
                throw new Exception("eBook路徑不存在. " + savePath);
            }
        }

        public void Create(string title)
        {
			int count = GetAll().Count();
			int number = count +1 ; 

			Content entity = null;
			bool valid = false;
			while (!valid)
			{
				string strNumber = number.ToString();
				entity = new Content()
				{
					ContentID = Guid.NewGuid().ToString(),
					TypeID = this.type,
					SerialNo = strNumber,
					ContentName = title,
					ContentText = String.Format("{0}/{1}", this.folder, number),
					ContentCreateTime = DateTime.Now,
					ContentUpdateTime = DateTime.Now,

				};

				if (!CreateBookFolder(strNumber))
				{
					valid = false;
					number++;
					continue;
				} 
				

				valid = CreateXML(ConvertToEBookModel(entity));


				number++;
			}
			

            Context.Contents.Add(entity);
            Context.SaveChanges();

		
		}

        public void Update(string id, string title)
        {
            var entity = Context.Contents.Find(id);
            if (entity == null) throw new Exception("資料不存在. id=" + id);

            entity.ContentName = title;
            Context.SaveChanges();

            CreateXML(ConvertToEBookModel(entity));
        }

        public IEnumerable<EBookViewModel> GetAll()
        {
            var ebooklist = new List<EBookViewModel>();
            var contentList = Context.Contents.Where(c => c.TypeID == this.type).OrderBy(c=>c.ContentCreateTime);
            if (contentList.IsNullOrEmpty()) return ebooklist;

            foreach (var item in contentList)
            {
                ebooklist.Add(ConvertToEBookModel(item));
            }

            return ebooklist;
        }

        public EBookViewModel Edit(string id)
        {
            var entity = Context.Contents.Find(id);
            if (entity == null) return null;

            return ConvertToEBookModel(entity);
        }

        public void Upload(string id ,  HttpPostedFileBase file)
        {
            var model = Edit(id);
            if (model == null) throw new Exception("查無資料");

            var folderPath = GetBookImagePath(model);
            file.SaveAs(Path.Combine(folderPath, file.FileName));

            int sort = model.MaxPage + 1;
            var record = new FileUplaod()
            {
                ItemOID = Guid.NewGuid().ToString(),
                ContentID = id,
                Sort = sort,
                FileName = file.FileName,
                Path = String.Format("{0}/{1}/{2}", this.folder, model.Number, file.FileName)

            };

            Context.FileUplaods.Add(record);

            Context.SaveChanges();

           
        }

        public void RemovePage(string id)
        {
            var entity = Context.FileUplaods.Find(id);
            if (entity == null) return;

            RemoveImageFile(entity);

            Context.FileUplaods.Remove(entity);

            Context.SaveChanges();
        }

        public void UpdatePageOrder(string id,int order)
        {
            var entity = Context.FileUplaods.Find(id);
            if (entity == null) throw new Exception();

            entity.Sort = order;

            Context.SaveChanges();


        }

        private EBookViewModel ConvertToEBookModel(Content item)
        {
            var model = new EBookViewModel()
            {
                Id = item.ContentID,
                CreatedAt = Convert.ToDateTime(item.ContentCreateTime),
                Number = item.SerialNo,
                FileName = item.ContentText,
                Title = item.ContentName

            };

           var pageList = new List<EBookPageModel>();
            if (!item.FileUplaods.IsNullOrEmpty())
            {

                var files = item.FileUplaods.OrderBy(f=>f.Sort);
                foreach (var file in files)
                {
                    pageList.Add(ConvertToEBookPage(file));
                }
                

            }
            model.EBookPageList = pageList;
            return model;
        }
        private EBookPageModel ConvertToEBookPage(FileUplaod file)
        {
            return new EBookPageModel()
            {
                Id = file.ItemOID,
                EBookId= file.ContentID,
                Sort = Convert.ToInt16(file.Sort),
                Name = file.FileName,
                Path= file.Path

            };
          
        }


        private string GetBookImagePath(EBookViewModel model)
        {
            string id = model.Id;
           
            return Path.Combine(this.savePath, model.Number);
        }

        private bool CreateBookFolder(string number)
        {
            string path = Path.Combine(this.savePath, number);
            if (Directory.Exists(path))
            {
				// throw new Exception("eBook資料夾重複. " + path);
				return false;
			}

			DirectoryInfo di = Directory.CreateDirectory(path);

			return true;

		}

        public bool CreateXML(EBookViewModel model)
        {
			string filename = model.Number + ".xml";
			string path = Path.Combine(this.savePath, filename);
			
			if (File.Exists(path)) return false;

			XmlDocument doc = new XmlDocument();

           
            XmlElement root = doc.CreateElement("content");
            root.SetAttribute("width", "750");
            root.SetAttribute("height", "905");
            root.SetAttribute("name", model.Title);
            root.SetAttribute("number", model.Number);
            doc.AppendChild(root);


            if (!model.EBookPageList.IsNullOrEmpty())
            {

                string coverImgPath = "/scripts/book/assets/cover.jpg";
              
                XmlElement topCover = doc.CreateElement("page");
                topCover.SetAttribute("src", coverImgPath);
                root.AppendChild(topCover);

                foreach (var item in model.EBookPageList)
                {
                    XmlElement page = doc.CreateElement("page");
                    string src = String.Format("/{0}/{1}", model.FileName, item.Name);
                    page.SetAttribute("src", src);
                    root.AppendChild(page);

                }

                XmlElement endCover = doc.CreateElement("page");
                endCover.SetAttribute("src", coverImgPath);
                root.AppendChild(endCover);

            }

            

            XmlDeclaration xmldecl;
            xmldecl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(xmldecl, root);

		
			doc.Save(path);

			return true;
        }

        private void RemoveImageFile(FileUplaod file)
        {
            var model = ConvertToEBookPage(file);
            var filePath = Path.Combine(this.frontRootPath, model.Path);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            

           
        }



    }
}
