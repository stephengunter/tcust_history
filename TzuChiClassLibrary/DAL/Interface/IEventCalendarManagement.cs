using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    //大事記要
    public interface IEventCalendarManagement
    {
        Boolean Add(EventCalendarModel model);

        Boolean Update(EventCalendarModel model);

        Boolean Delete(string contentID);

        EventCalendarModel GetByContentID(string contentID, bool includeAdmin = true);

        List<EventCalendarModel> GetAll(PagenationModel model);

        List<EventCalendarModel> SearchByKeyWord(PagenationModel model);

        //每年兩筆
        List<EventCalendarModel> GetForFrontPage();

        //該年所有資料
        List<EventCalendarModel> GetByYear(PagenationModel model);

    }
}
