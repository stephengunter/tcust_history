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
    public interface IActivitiesManagement
    {

        Boolean Add(ActivitiesModel model);

        Boolean Update(ActivitiesModel model);

        Boolean Delete(string contentId);

        ActivitiesModel GetByContentId(string contentId);

        List<ActivitiesModel> GetAll(PagenationModel model);

    }
}
