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
    public interface IFileUploadManagement
    {

        Boolean Add(string contentID, FileUploadModel model);

        Boolean DeleteByContentID(string contentID);

        List<FileUploadModel> GetByContentID(string contentID);

        List<FileUploadModel> GetCoverImageByContentID(string contentID);

    }
}
