using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;
using System.Data.SqlClient;

namespace TzuChiClassLibrary.DAL
{
    //類別管理
    public interface ICategoryManagement
    {

        Boolean Add(CategoryModel model);

        Boolean Update(CategoryModel model);

        Boolean Delete(string categoryID);

        List<CategoryModel> GetByCategoryTypeID(string CategoryTypeID);

        List<CategoryModel> SchoolDiaryGetByCategoryTypeID(string CategoryTypeID);

        Boolean UpdateOrder(List<CategoryModel> models);

        List<CategoryModel> NewsGetYearCategory();

    }
}
