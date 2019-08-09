using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    //天涯比鄰 -> 境內
    public interface IPlanInsideManagement
    {

        Boolean Add(PlanInsideModel model);

        Boolean Update(PlanInsideModel model);

        Boolean Delete(string contentID);

        PlanInsideModel GetByContentID(string contentID);

        List<PlanInsideModel> GetAll(PagenationModel model);

        List<PlanInsideModel> GetForFrontPage(string categoryPrepID, string categoryPartitionID, string categorySiteID, string department);

        List<string> GetDepartList(string categoryPrepID);

    }
}
