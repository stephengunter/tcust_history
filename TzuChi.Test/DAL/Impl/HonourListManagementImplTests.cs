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
    public class HonourListManagementImplTests
    {
        [TestMethod()]
        public void AddTest()
        {
            IHonourListManagement dao = new HonourListManagementImpl();
            HonourListModel model = new HonourListModel();
            model.ContentTime = Convert.ToDateTime("2013-05-06");
            model.ContentName = "HonourList ContentName";
            model.Description = "HonourList Description";
            model.RelatedLink = "http://tw.yahoo.com";
            model.ContentCreator = "3319af4a-c676-429c-9775-8baaa974cb2f"; // admin
            model.ContentUpdater = "3319af4a-c676-429c-9775-8baaa974cb2f";
            Boolean result = dao.Add(model);
            Assert.AreEqual(true,result);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            IHonourListManagement dao = new HonourListManagementImpl();
            HonourListModel model = new HonourListModel();
            model.ContentID = "3f2b1289-5a64-41c6-a62b-e8d3d687e42d";
            model.ContentTime = Convert.ToDateTime("2014-07-12");
            model.ContentName = "Update HonourList ContentName";
            model.Description = "Update HonourList Description";
            model.RelatedLink = "http://www.msn.com";
            model.ContentCreator = "3319af4a-c676-429c-9775-8baaa974cb2f"; // admin
            model.ContentUpdater = "3319af4a-c676-429c-9775-8baaa974cb2f";
            Boolean result = dao.Update(model);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            IHonourListManagement dao = new HonourListManagementImpl();
            string ContentID = "c73f07ca-4437-45bc-b4b5-b97db487df2b";
            Boolean result = dao.Delete(ContentID);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void GetByContentIdTest()
        {
            IHonourListManagement dao = new HonourListManagementImpl();
            string ContentID = "3f2b1289-5a64-41c6-a62b-e8d3d687e42d";
            HonourListModel model = dao.GetByContentID(ContentID);
            Assert.AreEqual("Update HonourList Description", model.Description);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            IHonourListManagement dao = new HonourListManagementImpl();
            PagenationModel model = new PagenationModel();
            List<HonourListModel> list = dao.GetAll(model);
            Assert.AreEqual(1, list.Count);
        }
    }
}
