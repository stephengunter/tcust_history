using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    public class FileUploadManagementMock : IFileUploadManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        bool IFileUploadManagement.Add(string contentID, FileUploadModel model)
        {
            throw new NotImplementedException();
        }

        bool IFileUploadManagement.DeleteByContentID(string contentID)
        {
            throw new NotImplementedException();
        }

        List<FileUploadModel> IFileUploadManagement.GetByContentID(string contentID)
        {
            throw new NotImplementedException();
        }

        List<FileUploadModel> IFileUploadManagement.GetCoverImageByContentID(string contentID)
        {
            throw new NotImplementedException();
        }
    }
}
