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
    //榮譽榜
    public class HonourListManagementMock : IHonourListManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        bool IHonourListManagement.Add(HonourListModel model)
        {
            throw new NotImplementedException();
        }

        bool IHonourListManagement.Update(HonourListModel model)
        {
            throw new NotImplementedException();
        }

        bool IHonourListManagement.Delete(string contentId)
        {
            throw new NotImplementedException();
        }

        HonourListModel IHonourListManagement.GetByContentID(string contentId)
        {
            HonourListModel model = new HonourListModel();

            return model;
        }

        List<HonourListModel> IHonourListManagement.GetAll(PagenationModel model)
        {
            throw new NotImplementedException();
        }

        List<HonourListModel> IHonourListManagement.GetForFrontPage()
        {
            List<HonourListModel> result = new List<HonourListModel>();
            HonourListModel model = new HonourListModel();
            FileUploadModel fuModel = new FileUploadModel();
            List<FileUploadModel> fuList = new List<FileUploadModel>();
            model.ContentID = Guid.NewGuid().ToString();
            model.ContentTime = DateTime.Now;
            model.ContentName = "民河會會明的、是以花成分！";
            model.Description = "民河會會明的、是以花成分！做完童名陸年到減都大國筆行山民上別受片，樹的來示過當聲準……陸刻象她公月：英來是。算檢是沒？不為這病費議覺聲個和往王的元來片及天統獲早品香各世女年底交來熱停形後，意資約再力人北前我關、活常主？氣把知王：得一林一正功際高是最毒光？";
            model.RelatedLink = null;

            fuModel.Path = "~/images/HonourList/news/news1.jpg";
            fuList.Add(fuModel);
            model.PicUrlList = fuList;
            result.Add(model);
            return result;
        }

        List<HonourListModel> IHonourListManagement.GetByYear(string year)
        {
            throw new NotImplementedException();
        }

        List<HonourListModel> IHonourListManagement.SearchByKeyWord(PagenationModel model)
        {
            throw new NotImplementedException();
        }
    }
}
