using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;
using System.Data.SqlClient;

namespace TzuChiClassLibrary.DAL
{
    //資訊查詢 -> 大愛新聞
    public class NewsManagementMock : INewsManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        bool INewsManagement.Add(NewsModel model)
        {
            throw new NotImplementedException();
        }

        bool INewsManagement.Update(NewsModel model)
        {
            throw new NotImplementedException();
        }

        bool INewsManagement.Delete(string contentID)
        {
            throw new NotImplementedException();
        }

        NewsModel INewsManagement.GetByContentID(string contentID)
        {
            throw new NotImplementedException();
        }
        NewsModel INewsManagement.GetLatestNews() 
        {
            throw new NotImplementedException();
        }
        List<NewsModel> INewsManagement.GetAll(PagenationModel model)
        {
            throw new NotImplementedException();
        }

        List<NewsModel> INewsManagement.GetAllForFrontPage(PagenationModel model)
        {
            throw new NotImplementedException();
        }
    }
}
