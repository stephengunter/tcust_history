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
    public class ImageShowManagementImplTests
    {
        [TestMethod()]
        public void AddTest()
        {
            IImageShowManagement dao = new ImageShowManagementImpl();
            ImageShowModel model = new ImageShowModel();
            model.TypeID = NewsModel.TYPEID;
            model.ImageName = "testName";
            model.ImageUrl = "D:/Allen/test/a.jpg";
            model.ImageWidth = 800;
            model.ImageHeight = 600;
            model.Creator = "3319af4a-c676-429c-9775-8baaa974cb2f";
            model.Updater = "3319af4a-c676-429c-9775-8baaa974cb2f";
            Boolean result = dao.Add(model);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            IImageShowManagement dao = new ImageShowManagementImpl();
            ImageShowModel model = new ImageShowModel();
            model.ImageShowID = "f257dd6a-a4cf-485d-a4ce-13f0ecee3a59";
            model.TypeID = EventCalendarModel.TYPEID;
            model.ImageName = "testName";
            model.ImageUrl = "D:/Allen/test/b.jpg";
            model.ImageWidth = 200;
            model.ImageHeight = 400;
            model.Creator = "3319af4a-c676-429c-9775-8baaa974cb2f";
            model.Updater = "3319af4a-c676-429c-9775-8baaa974cb2f";
            Boolean result = dao.Update(model);
            Assert.AreEqual(true, result);
        }
    }
}
