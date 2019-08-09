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
    public class ImageShowManagementMock : IImageShowManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        bool IImageShowManagement.Add(ImageShowModel model)
        {
            throw new NotImplementedException();
        }

        bool IImageShowManagement.Update(ImageShowModel model)
        {
            throw new NotImplementedException();
        }

        bool IImageShowManagement.Delete(string imageShowID)
        {
            throw new NotImplementedException();
        }

        ImageShowModel IImageShowManagement.GetByImageShowID(string imageShowID)
        {
            throw new NotImplementedException();
        }

        List<ImageShowModel> IImageShowManagement.GetAll(PagenationModel model)
        {
            throw new NotImplementedException();
        }

        List<ImageShowModel> IImageShowManagement.GetByTypeID(string typeID)
        {
            List<ImageShowModel> ImageShowModelList = new List<ImageShowModel>();
            
            ImageShowModel imageShowModel = new ImageShowModel();
            
            imageShowModel.ContentID = Guid.NewGuid().ToString();
            imageShowModel.TypeID = Guid.NewGuid().ToString();
            imageShowModel.ContentID = Guid.NewGuid().ToString();
            imageShowModel.ImageName = "test1";
            imageShowModel.ImageUrl = "image/test/test1";
            imageShowModel.ImageWidth = 1024;
            imageShowModel.ImageHeight = 768;            
            imageShowModel.TotalNum = 1;

            ImageShowModelList.Add(imageShowModel);

            return ImageShowModelList;
        }
    }
}
