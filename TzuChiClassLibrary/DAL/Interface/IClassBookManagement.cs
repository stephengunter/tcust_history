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
    public interface IClassBookManagement
    {
        Boolean ResetClassBookData(List<ClassBookModel> list);
        List<ClassBookModel> GetQueryList(PagenationModel model);
        List<ClassBookModel> GetAllByAcademicYear(string academicYear);
        ClassBookModel GetPageByCondition(PagenationModel model);
        ClassBookModel GetPageByClassBookID(string classBookID);
        List<ClassNumberModel> GetClassNumberByAcademicYear(string academicYear);
        List<string> GetAllAcademicYearName();
        List<string> GetAllDepartmentName();
        List<string> GetAllClassName();
    }
}
