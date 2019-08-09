using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace TzuChiClassLibrary.DAL
{
    //大事記要
    public class EventCalendarManagementMock : IEventCalendarManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        bool IEventCalendarManagement.Add(EventCalendarModel model)
        {
            throw new NotImplementedException();
        }

        bool IEventCalendarManagement.Update(EventCalendarModel model)
        {
            throw new NotImplementedException();
        }

        bool IEventCalendarManagement.Delete(string contentID)
        {
            throw new NotImplementedException();
        }

        EventCalendarModel IEventCalendarManagement.GetByContentID(string contentID, bool includeAdmin = true)
        {
            throw new NotImplementedException();
        }

        List<EventCalendarModel> IEventCalendarManagement.GetAll(PagenationModel model)
        {
            throw new NotImplementedException();
        }

        List<EventCalendarModel> IEventCalendarManagement.SearchByKeyWord(PagenationModel model)
        {
            throw new NotImplementedException();
        }

        List<EventCalendarModel> IEventCalendarManagement.GetForFrontPage()
        {
            throw new NotImplementedException();
        }

        List<EventCalendarModel> IEventCalendarManagement.GetByYear(PagenationModel model)
        {
            throw new NotImplementedException();
        }
    }
}
