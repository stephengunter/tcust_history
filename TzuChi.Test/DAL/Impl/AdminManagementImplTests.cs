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
    public class AdminManagementImplTests
    {
        [TestMethod()]
        public void AddTest()
        {
            IAdminManagement dao = new AdminManagementImpl();
            AdminModel model = new AdminModel();
            model.Account = "testAcc";
            model.Password = "123o12jenaja;vj923v10";
            //model.Name = "testName";
            //model.Email = "test@gmail.com";
            //model.Tel = "02-52103921";
            //model.Mobile = "0919-123456";
            Boolean result = dao.Add(model);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void UpdateTest()
        {
           
            IAdminManagement dao = new AdminManagementImpl();
            
            AdminModel model = new AdminModel();
            model.AdminID = "3319af4a-c676-429c-9775-8baaa974cb2f";
            model.Account = "admin";
            model.Password = dao.DesEncrypt("admin");
            model.Name = "管理員";
            model.Email = "test@gmail.com";
            model.Tel = "02-52103921";
            model.Mobile = "0919-123456";
            model.Enable = true;
            Boolean result = dao.Update(model);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            IAdminManagement dao = new AdminManagementImpl();
            string AdminID = "4db6bd68-18e4-4aa6-aec0-62dabaa3fbb9";
            Boolean result = dao.Delete(AdminID);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void GetByAdminIDTest()
        {
            IAdminManagement dao = new AdminManagementImpl();
            string AdminID = "3319af4a-c676-429c-9775-8baaa974cb2f";
            AdminModel model = dao.GetByAdminID(AdminID);
            Assert.AreEqual("admin", model.Account);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            IAdminManagement dao = new AdminManagementImpl();
            PagenationModel model = new PagenationModel();
            List<AdminModel> list = dao.GetAll(model);
            Assert.AreEqual(3, list.Count);
        }
    }
}
