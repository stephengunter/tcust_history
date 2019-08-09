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
    //天涯比鄰 -> 境內
    public class PlanInsideManagementMock : IPlanInsideManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        bool IPlanInsideManagement.Add(PlanInsideModel model)
        {
            throw new NotImplementedException();
        }

        bool IPlanInsideManagement.Update(PlanInsideModel model)
        {
            throw new NotImplementedException();
        }

        bool IPlanInsideManagement.Delete(string contentID)
        {
            throw new NotImplementedException();
        }

        PlanInsideModel IPlanInsideManagement.GetByContentID(string contentID)
        {
            PlanInsideModel modal = new PlanInsideModel();

            return modal;
        }

        List<PlanInsideModel> IPlanInsideManagement.GetAll(PagenationModel model)
        {
            throw new NotImplementedException();
        }


        public List<PlanInsideModel> GetForFrontPage(string categoryPrepID, string categoryPartitionID, string categorySiteID, string department)
        {
            throw new NotImplementedException();
        }

        public List<string> GetDepartList(string categoryPrepID)
        {
            throw new NotImplementedException();
        }
    }
}
