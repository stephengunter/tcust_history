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
    public class SchoolDiaryManagementMock : ISchoolDiaryManagement
    {
        
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public SchoolDiaryModel GetFiles(string contentID)
        {
            throw new NotImplementedException();
        }

        bool ISchoolDiaryManagement.Add(SchoolDiaryModel model)
        {
            throw new NotImplementedException();
        }
        
        bool ISchoolDiaryManagement.Update(SchoolDiaryModel model)
        {
            throw new NotImplementedException();
        }

        bool ISchoolDiaryManagement.Delete(string contentID)
        {
            throw new NotImplementedException();
        }

        SchoolDiaryModel ISchoolDiaryManagement.GetByContentID(string contentID)
        {
            throw new NotImplementedException();
        }

        List<SchoolDiaryModel> ISchoolDiaryManagement.GetAll(PagenationModel model)
        {
            throw new NotImplementedException();
        }

        List<SchoolDiaryModel> ISchoolDiaryManagement.GetAllForFrontPage(PagenationModel model)
        {
            List<SchoolDiaryModel> resultList = new List<SchoolDiaryModel>();

            for (int i = 0; i < 10; i++)
            {
                SchoolDiaryModel resuleModel = new SchoolDiaryModel();       

                resuleModel.ContentID = "ContentID_" + i;
                resuleModel.ContentName = "ContentName_" + i;
                resuleModel.Description = "Description_" + i; //字數限制
                resuleModel.ContentTime = DateTime.Now;
                resuleModel.AcademicYear = "96";
                resuleModel.Semester = "1";
                resuleModel.RelatedLink = "~/Scripts/inform/images/inform/new1.jpg"; //QR-CODE

                resultList.Add(resuleModel);
            }
            //ContentID
            //ContentName//標題
            //Description//內容
            // ContentTime//日期
            //AcademicYear //學年
            //Semester//學期
            //RelatedLink// 相關連結 ( for create QR code ) 20140929

            return resultList;
            
        }


        public bool checkSerialNo(string serialNo)
        {
            throw new NotImplementedException();
        }
    }
}


