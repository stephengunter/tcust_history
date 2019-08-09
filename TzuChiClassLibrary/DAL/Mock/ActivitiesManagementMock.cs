using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    //慈濟人文與博雅教育 -> 博雅教育 -> 多元活動
    public class ActivitiesManagementMock : IActivitiesManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        bool IActivitiesManagement.Add(ActivitiesModel model)
        {
            throw new NotImplementedException();
        }

        bool IActivitiesManagement.Update(ActivitiesModel model)
        {
            throw new NotImplementedException();
        }

        bool IActivitiesManagement.Delete(string contentId)
        {
            throw new NotImplementedException();
        }

        ActivitiesModel IActivitiesManagement.GetByContentId(string contentId)
        {
            throw new NotImplementedException();
        }

        List<ActivitiesModel> IActivitiesManagement.GetAll(PagenationModel model)
        {
            throw new NotImplementedException();
        }
    }
}
