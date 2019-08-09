using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TzuChiClassLibrary.BO;
namespace TzuChiClassLibrary.DAL.Tests
{
    [TestClass()]
    public class NewsManagementImplTests
    {
        [TestMethod()]
        public void AddTest()
        {
            INewsManagement dao = new NewsManagementImpl();
            NewsModel model = new NewsModel();
            model.ContentName = "News Title";
            model.Description = "News Description";
            model.ContentTime = Convert.ToDateTime("2011-01-01");
            model.AcademicYear = "1382f5ab-3ba1-49c2-bfca-bc9708124bf3"; //99學年度
            model.ContentCreator = "3319af4a-c676-429c-9775-8baaa974cb2f";
            model.ContentUpdater = "3319af4a-c676-429c-9775-8baaa974cb2f";
            Boolean result = dao.Add(model);
            Assert.AreEqual(true,result);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            INewsManagement dao = new NewsManagementImpl();
            NewsModel model = new NewsModel();
            model.ContentID = "6eec6f60-70bc-4f8c-b3af-d92544306cef";
            model.ContentTime = Convert.ToDateTime("2014-12-01");
            model.ContentName = "Update News Title";
            model.Description = "Update News Description";
            model.ContentUpdater = "3319af4a-c676-429c-9775-8baaa974cb2f";
            model.AcademicYear = "5f6ae37b-0660-4a41-9199-bd652f041a23";
            Boolean result = dao.Update(model);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            INewsManagement dao = new NewsManagementImpl();
            string ContentID = "6eec6f60-70bc-4f8c-b3af-d92544306cef";
            Boolean result = dao.Delete(ContentID);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void GetByContentIdTest()
        {
            INewsManagement dao = new NewsManagementImpl();
            string ContentID = "ce1f6c7e-a5cb-4e02-bf87-6f109ed8f79f";
            NewsModel model = dao.GetByContentID(ContentID);
            Assert.AreEqual("News Title", model.ContentName);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            INewsManagement dao = new NewsManagementImpl();
            PagenationModel model = new PagenationModel();
            List<NewsModel> list = dao.GetAll(model);
            Assert.AreEqual(1, list.Count);
        }
    }
}
