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
    public class EventCalendarManagementImplTests
    {        
        [TestMethod()]
        public void AddTest()
        {
            IEventCalendarManagement dao = new EventCalendarManagementImpl();
            EventCalendarModel model = new EventCalendarModel();
            model.AcademicYear = "1382f5ab-3ba1-49c2-bfca-bc9708124bf3"; //99學年度
            model.ContentTime = DateTime.Now;
            model.ContentName = "Test Title";
            model.Description = "Test Description";
            model.ContentCreator = "3319af4a-c676-429c-9775-8baaa974cb2f"; // admin
            model.ContentUpdater = "3319af4a-c676-429c-9775-8baaa974cb2f";
            Boolean result = dao.Add(model);
            Assert.AreEqual(true,result);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            IEventCalendarManagement dao = new EventCalendarManagementImpl();
            EventCalendarModel model = new EventCalendarModel();
            model.ContentID = "ebc6fa30-0a93-4bb9-b1ff-7c163d7105cd";
            model.AcademicYear = "36cf8208-6592-4aaa-98ec-6f77cf72b798";//101學年度
            model.ContentTime = Convert.ToDateTime("2011-01-01");
            model.ContentName = "Test Update Title";
            model.Description = "Test Update Description";
            model.ContentUpdater = "3319af4a-c676-429c-9775-8baaa974cb2f";
            Boolean result = dao.Update(model);
            Assert.AreEqual(true,result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            IEventCalendarManagement dao = new EventCalendarManagementImpl();
            string ContentId = "7b5d4f7c-ffa0-43da-86e2-7dbd1ccc404d";
            Boolean result = dao.Delete(ContentId);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void GetByContentIdTest()
        {
            IEventCalendarManagement dao = new EventCalendarManagementImpl();
            string ContentId = "597f77f3-c1bb-44ac-91a4-09eb6b0aa452";
            EventCalendarModel model = dao.GetByContentID(ContentId);
            Assert.AreEqual("Test Title", model.ContentName);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            IEventCalendarManagement dao = new EventCalendarManagementImpl();
            PagenationModel model = new PagenationModel();
            List<EventCalendarModel> list = dao.GetAll(model);
            Assert.AreEqual(3, list.Count);
        }
    }
}
