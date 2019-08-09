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
    public class CategoryManagementMock : ICategoryManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();



        bool ICategoryManagement.Add(CategoryModel model)
        {
            throw new NotImplementedException();
        }

        bool ICategoryManagement.Update(CategoryModel model)
        {
            throw new NotImplementedException();
        }

        bool ICategoryManagement.Delete(string categoryID)
        {
            throw new NotImplementedException();
        }

        List<CategoryModel> ICategoryManagement.GetByCategoryTypeID(string CategoryTypeID)
        {
            throw new NotImplementedException();
        }

        bool ICategoryManagement.UpdateOrder(List<CategoryModel> models)
        {
            throw new NotImplementedException();
        }


        public List<CategoryModel> SchoolDiaryGetByCategoryTypeID(string CategoryTypeID)
        {
            throw new NotImplementedException();
        }


        public List<CategoryModel> NewsGetYearCategory()
        {
            throw new NotImplementedException();
        }
    }
}
