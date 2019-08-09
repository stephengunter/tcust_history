using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    //榮譽榜
    public interface IHonourListManagement
    {

        Boolean Add(HonourListModel model);

        Boolean Update(HonourListModel model);

        Boolean Delete(string contentId);

        HonourListModel GetByContentID(string contentId);

        List<HonourListModel> GetAll(PagenationModel model);

        List<HonourListModel> GetForFrontPage();//每年度取兩筆

        List<HonourListModel> GetByYear(string year);//依照年度

        List<HonourListModel> SearchByKeyWord(PagenationModel model);//關鍵字搜尋
    }
}
