using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    //資訊查詢 -> 大愛新聞
    public interface INewsManagement
    {

        Boolean Add(NewsModel model);

        Boolean Update(NewsModel model);

        Boolean Delete(string contentID);

        NewsModel GetByContentID(string contentID);
        NewsModel GetLatestNews();

        List<NewsModel> GetAll(PagenationModel model);

        List<NewsModel> GetAllForFrontPage(PagenationModel model);

    }
}
