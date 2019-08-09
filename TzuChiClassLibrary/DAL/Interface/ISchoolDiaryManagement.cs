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
    //資訊查詢 -> 校園日誌
    public interface ISchoolDiaryManagement
    {

        Boolean Add(SchoolDiaryModel model);

        Boolean Update(SchoolDiaryModel model);

        Boolean Delete(string contentID);

        SchoolDiaryModel GetByContentID(string contentID);

        List<SchoolDiaryModel> GetAll(PagenationModel model);

        List<SchoolDiaryModel> GetAllForFrontPage(PagenationModel model);

        Boolean checkSerialNo(string serialNo);


        SchoolDiaryModel GetFiles(string contentID);

    }
}
