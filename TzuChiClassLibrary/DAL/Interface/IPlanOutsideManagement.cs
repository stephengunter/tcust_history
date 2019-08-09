using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    //天涯比鄰 -> 境外
    public interface IPlanOutsideManagement
    {

        Boolean Add(PlanOutsideModel model);

        Boolean Update(PlanOutsideModel model);

        Boolean Delete(string contentID);

        PlanOutsideModel GetByContentID(string contentID);

        List<PlanOutsideModel> GetAll(PagenationModel model);

        List<PlanOutsideModel> GetForFrontPage(string CategoryOutsideID, string CategoryCountryID, string year);

        Dictionary<string, string> GetCountryList(string CategoryOutsideID);

        List<string> GetYearList(string categoryOutsideID);

        string GetAllCategoryID();
    }
}
