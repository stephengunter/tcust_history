using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    //慈濟人文與博雅教育 -> 博雅教育 -> 語言教學中心
    public interface IEducationCenterManagement
    {

        Boolean Add(EducationCenterModel model);

        Boolean Update(EducationCenterModel model);

        Boolean Delete(string contentId);

        EducationCenterModel GetByContentId(string contentId);

        List<EducationCenterModel> GetAll(PagenationModel model);
    }
}
