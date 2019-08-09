using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TzuChiClassLibrary.BO;
using System.Security.Cryptography;
namespace TzuChiClassLibrary.DAL.Tests
{
    [TestClass()]
    public class PlanOutsideManagementImplTests
    {
        [TestMethod()]
        public void AddTest()
        {
            IPlanOutsideManagement dao = new PlanOutsideManagementImpl();
            PlanOutsideModel model = new PlanOutsideModel();
            model.CategoryOutsideID = "1ed63661-7538-40df-a5e8-e58c76bbf3a5";       // 姊妹校
            model.Description = "境外內容";
            model.IntroCh = "校園簡介";
            model.IntroEn = "School Intro";
            model.Summary = "成果摘要";
            //model.CategoryCountryID = "8d5cf01e-623f-467f-86ec-f9aa54364e65";       // 美國
            //model.CategoryDepartmentID = "3a18ec8d-0ffa-4217-bc44-edf2c09541a1";    // 資工系
            //model.EnglishName = "English Name";
            model.ImageXY = "2,4";
            model.ContentCreator = "3319af4a-c676-429c-9775-8baaa974cb2f";          // admin
            model.ContentUpdater = "3319af4a-c676-429c-9775-8baaa974cb2f";
            Boolean result = dao.Add(model);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            IPlanOutsideManagement dao = new PlanOutsideManagementImpl();
            PlanOutsideModel model = new PlanOutsideModel();
            model.ContentID = "9c14a7c7-c8ef-410f-b690-6ecdde7a0da4";
            model.CategoryOutsideID = "1ed63661-7538-40df-a5e8-e58c76bbf3a5";       // 姊妹校
            model.Description = "境外內容123";
            model.IntroCh = "校園簡介123";
            model.IntroEn = "School Intro123";
            model.Summary = "成果摘要123";
            //model.CategoryCountryID = "8d5cf01e-623f-467f-86ec-f9aa54364e65";       // 美國
            //model.CategoryDepartmentID = "3a18ec8d-0ffa-4217-bc44-edf2c09541a1";    // 資工系
            //model.EnglishName = "English Name a b c";
            model.ImageXY = "3,5";
            model.ContentCreator = "3319af4a-c676-429c-9775-8baaa974cb2f";          // admin
            model.ContentUpdater = "3319af4a-c676-429c-9775-8baaa974cb2f";
            Boolean result = dao.Update(model);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            IPlanOutsideManagement dao = new PlanOutsideManagementImpl();
            string ContentID = "9c14a7c7-c8ef-410f-b690-6ecdde7a0da4";
            Boolean result = dao.Delete(ContentID);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void GetByContentIDTest()
        {
            IPlanOutsideManagement dao = new PlanOutsideManagementImpl();
            string ContentID = "0eb0ff9e-2120-45e0-b303-c921c77a7f5e";
            PlanOutsideModel model = dao.GetByContentID(ContentID);
            Assert.AreEqual("成果摘要123", model.Summary);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            IPlanOutsideManagement dao = new PlanOutsideManagementImpl();
            PagenationModel model = new PagenationModel();
            List<PlanOutsideModel> list = dao.GetAll(model);
            Assert.AreEqual(1, list.Count);
        }
        
    }
}
