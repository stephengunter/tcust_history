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
    public class PlanInsideManagementImplTests
    {
        [TestMethod()]
        public void AddTest()
        {
            IPlanInsideManagement dao = new PlanInsideManagementImpl();
            PlanInsideModel model = new PlanInsideModel();
            model.ContentName = "PlanInside ContentName";
            model.CategoryYearID = "2aa61b4c-2326-4c2f-acc0-8a1e8cdc61aa";           // 100年
            model.CategoryPrepID = "ab004ad5-47c6-4942-baae-f6fe97ca10bc";           // 一般
            model.CategorySiteID = "9afcc7e8-fb88-4575-b2e9-54607b25b128";           // 台北
            model.CategoryDepartmentID = "79e88174-147c-4081-90e7-34d30b6c8ae1";     // 放射醫學科學研究所
            model.Agencies = "Agencies";                                             // 合作機構       
            model.Moderator = "Moderator";                                           // 計畫主持人
            model.ImageXY = "3,6";                                                          
            model.ContentCreator = "3319af4a-c676-429c-9775-8baaa974cb2f";           // admin
            model.ContentUpdater = "3319af4a-c676-429c-9775-8baaa974cb2f";           // admin
            Boolean result = dao.Add(model);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            IPlanInsideManagement dao = new PlanInsideManagementImpl();
            PlanInsideModel model = new PlanInsideModel();
            model.ContentID = "a242d976-0e3f-4731-8a8d-208c682438c2";
            model.ContentName = "Update PlanInside ContentName";
            model.CategoryYearID = "2aa61b4c-2326-4c2f-acc0-8a1e8cdc61aa";           // 100年
            model.CategoryPrepID = "ab004ad5-47c6-4942-baae-f6fe97ca10bc";           // 一般
            model.CategorySiteID = "9afcc7e8-fb88-4575-b2e9-54607b25b128";           // 台北
            model.CategoryDepartmentID = "79e88174-147c-4081-90e7-34d30b6c8ae1";     // 放射醫學科學研究所
            model.Agencies = "Agencies";                                             // 合作機構       
            model.Moderator = "Moderator";                                           // 計畫主持人
            model.ImageXY = "3,6";
            model.ContentCreator = "3319af4a-c676-429c-9775-8baaa974cb2f";           // admin
            model.ContentUpdater = "3319af4a-c676-429c-9775-8baaa974cb2f";           // admin
            Boolean result = dao.Update(model);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            IPlanInsideManagement dao = new PlanInsideManagementImpl();
            string ContentID = "a242d976-0e3f-4731-8a8d-208c682438c2";
            Boolean result = dao.Delete(ContentID);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void GetByContentIDTest()
        {
            IPlanInsideManagement dao = new PlanInsideManagementImpl();
            string ContentID = "aace70ef-2867-4c87-8744-53c8d8c6d1a8";
            PlanInsideModel model = dao.GetByContentID(ContentID);
            Assert.AreEqual("PlanInside ContentName", model.ContentName);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            IPlanInsideManagement dao = new PlanInsideManagementImpl();
            PagenationModel model = new PagenationModel();
            List<PlanInsideModel> list = dao.GetAll(model);
            Assert.AreEqual(1, list.Count);
        }
    }
}
