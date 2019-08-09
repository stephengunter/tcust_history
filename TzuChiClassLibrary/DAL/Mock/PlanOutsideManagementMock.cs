using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    //天涯比鄰 -> 境外
    public class PlanOutsideManagementMock : IPlanOutsideManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        bool IPlanOutsideManagement.Add(PlanOutsideModel model)
        {
            throw new NotImplementedException();
        }

        bool IPlanOutsideManagement.Update(PlanOutsideModel model)
        {
            throw new NotImplementedException();
        }

        bool IPlanOutsideManagement.Delete(string contentID)
        {
            throw new NotImplementedException();
        }

        PlanOutsideModel IPlanOutsideManagement.GetByContentID(string contentID)
        {
            throw new NotImplementedException();
        }

        List<PlanOutsideModel> IPlanOutsideManagement.GetAll(PagenationModel model)
        {
            throw new NotImplementedException();
        }


        public List<PlanOutsideModel> GetForFrontPage(string CategoryOutsideID, string CategoryCountryID)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetCountryList(string CategoryOutsideID)
        {
            throw new NotImplementedException();
        }
    }
}
