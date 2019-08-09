using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    //資訊查詢 -> 畢業紀念冊
    public class ClassBookManagementMock : IClassBookManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        Boolean IClassBookManagement.ResetClassBookData(List<ClassBookModel> list)
        {
            throw new NotImplementedException();
        }
        List<ClassBookModel> IClassBookManagement.GetQueryList(PagenationModel model)
        {
            throw new NotImplementedException();
        }
        List<ClassBookModel> IClassBookManagement.GetAllByAcademicYear(string academicYear)
        {
            throw new NotImplementedException();
        }
        ClassBookModel IClassBookManagement.GetPageByCondition(PagenationModel model)
        {
            throw new NotImplementedException();
        }
        ClassBookModel IClassBookManagement.GetPageByClassBookID(string classBookID)
        {
            throw new NotImplementedException();
        }
        List<ClassNumberModel> IClassBookManagement.GetClassNumberByAcademicYear(string academicYear)
        {
            throw new NotImplementedException();
        }
        List<string> IClassBookManagement.GetAllAcademicYearName()
        {
            throw new NotImplementedException();
        }
        List<string> IClassBookManagement.GetAllDepartmentName()
        {
            throw new NotImplementedException();
        }
        List<string> IClassBookManagement.GetAllClassName()
        {
            throw new NotImplementedException();
        }
    }
}
