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
    public class CategoryManagementImpl : ICategoryManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Boolean Add(CategoryModel model)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var categoryParameters = new List<object>();
                            categoryParameters.Add(new SqlParameter("@CategoryID", model.CategoryID));
                            categoryParameters.Add(new SqlParameter("@CategoryTypeID", model.CategoryTypeID));
                            categoryParameters.Add(new SqlParameter("@CategoryName", model.CategoryName));
                            categoryParameters.Add(new SqlParameter("@Sort", model.Sort));
                            categoryParameters.Add(new SqlParameter("@Creator", model.Creator ?? string.Empty));
                            categoryParameters.Add(new SqlParameter("@Updater", model.Updater ?? string.Empty));
                            db.Database.ExecuteSqlCommand(@" 
                            INSERT INTO dbo.Category(CategoryID, CategoryTypeID, CategoryName, Sort, Creator, Updater)
                            VALUES(@CategoryID, @CategoryTypeID, @CategoryName, @Sort, @Creator, @Updater);",
                                categoryParameters.ToArray());
                            dbContextTransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            logger.Debug("(Debug)除錯" + e.Message);
                            dbContextTransaction.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Debug("(Debug)除錯" + e.Message);
                    return false;
                }
                return true;
            }
        }

        public Boolean Update(CategoryModel model)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var categoryParameters = new List<object>();
                            model.CategoryID = Guid.NewGuid().ToString();
                            categoryParameters.Add(new SqlParameter("@CategoryID", model.CategoryID));
                            categoryParameters.Add(new SqlParameter("@CategoryTypeID", model.CategoryTypeID));
                            categoryParameters.Add(new SqlParameter("@CategoryName", model.CategoryName));
                            categoryParameters.Add(new SqlParameter("@Sort", model.Sort));
                            categoryParameters.Add(new SqlParameter("@Updater", model.Updater ?? string.Empty));
                            db.Database.ExecuteSqlCommand(@" 
                            UPDATE dbo.Category SET 
                              CategoryTypeID = @CategoryTypeID,
                              CategoryName = @CategoryName,
                              Sort = @Sort,
                              Updater = @Updater,
                              UpdateTime = GETDATE()
                              WHERE CategoryID = @CategoryID",
                                categoryParameters.ToArray());
                            dbContextTransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            logger.Debug("(Debug)除錯" + e.Message);
                            dbContextTransaction.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Debug("(Debug)除錯" + e.Message);
                    return false;
                }
                return true;
            }
        }

        public Boolean Delete(string categoryID)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var categoryParameters = new List<object>();
                            categoryParameters.Add(new SqlParameter("@CategoryID", categoryID));

                            db.Database.ExecuteSqlCommand(@"
                            DELETE dbo.Category WHERE CategoryID = @CategoryID;
                            ", categoryParameters.ToArray());
                            dbContextTransaction.Commit();

                        }
                        catch (Exception e)
                        {
                            logger.Debug("(Debug)除錯" + e.Message);
                            dbContextTransaction.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Debug("(Debug)除錯" + e.Message);
                    return false;
                }
                return true;
            }
        }

        public List<CategoryModel> SchoolDiaryGetByCategoryTypeID(string CategoryTypeID)
        {
            List<CategoryModel> result = new List<CategoryModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var categoryParameters = new List<object>();
                    categoryParameters.Add(new SqlParameter("@CategoryTypeID", CategoryTypeID));

                    result = db.Database.SqlQuery<CategoryModel>(@"
                         SELECT
                            cgy.CategoryID,
                            cgy.CategoryTypeID,
                            cgy.CategoryName,
                            cgy.Sort
                        FROM Category cgy
                        WHERE cgy.CategoryTypeID = 'Year'
                        AND cgy.CategoryName IN (
	                        SELECT 
	                           Distinct cgy.CategoryName as CategoryName
	                        FROM Content c
	                            INNER JOIN dbo.Educated e
	                                ON c.ContentID = e.ContentID
	                            LEFT JOIN dbo.Category cgy
	                                ON cgy.CategoryID = e.CategoryYearID
	                            LEFT JOIN dbo.Category cgt
	                                ON cgt.CategoryID = e.CategoryTermID
	                        WHERE c.ContentID IN(
		                        SELECT cmt.ContentID 
		                        FROM ContentMultipleType cmt
		                        WHERE cmt.ContentType = '7f266b34-c03e-41ae-84a1-1e27dccc7039') 
	                        ) 
                        Order By sort Desc ", categoryParameters.ToArray()).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<CategoryModel> NewsGetYearCategory()
        {
            List<CategoryModel> result = new List<CategoryModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var categoryParameters = new List<object>();
                    categoryParameters.Add(new SqlParameter("@CategoryTypeID", CategoryModel.CATEGORY_CATEGORYTYPEID_YEAR));
                    categoryParameters.Add(new SqlParameter("@TypeID", NewsModel.TYPEID));

                    result = db.Database.SqlQuery<CategoryModel>(@"
                         SELECT
                            cgy.CategoryID,
                            cgy.CategoryTypeID,
                            cgy.CategoryName,
                            cgy.Sort
                        FROM Category cgy
                        WHERE cgy.CategoryTypeID = @CategoryTypeID
                        AND cgy.CategoryName IN (
	                        SELECT 
	                           Distinct cgy.CategoryName as CategoryName
	                        FROM Content c
	                            INNER JOIN dbo.Educated e
	                                ON c.ContentID = e.ContentID
	                            LEFT JOIN dbo.Category cgy
	                                ON cgy.CategoryID = e.CategoryYearID
	                            LEFT JOIN dbo.Category cgt
	                                ON cgt.CategoryID = e.CategoryTermID
	                        WHERE c.TypeID = @TypeID
                        )
                        Order By sort Desc ", categoryParameters.ToArray()).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<CategoryModel> GetByCategoryTypeID(string CategoryTypeID)
        {
            List<CategoryModel> result = new List<CategoryModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var categoryParameters = new List<object>();
                    categoryParameters.Add(new SqlParameter("@CategoryTypeID", CategoryTypeID));

                    result = db.Database.SqlQuery<CategoryModel>(@"
                                SELECT
                                    c.CategoryID,
                                    c.CategoryTypeID,
                                    c.CategoryName,
	                                c.Sort
                                FROM Category c
                                WHERE c.CategoryTypeID = @CategoryTypeID
                                ORDER BY c.Sort", categoryParameters.ToArray()).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public Boolean UpdateOrder(List<CategoryModel> models)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (CategoryModel model in models)
                            {
                                var categoryParameters = new List<object>();
                                categoryParameters.Add(new SqlParameter("@CategoryID", model.CategoryID));
                                categoryParameters.Add(new SqlParameter("@Sort", model.Sort));
                                db.Database.ExecuteSqlCommand(@" 
                                UPDATE dbo.Category
                                SET Sort = @Sort
                                WHERE CategoryID = @CategoryID
                                ", categoryParameters.ToArray());
                            }
                            dbContextTransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            logger.Debug("(Debug)除錯" + e.Message);
                            dbContextTransaction.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                    logger.Debug("(Debug)除錯" + e.Message);
                    return false;
                }
                return true;
            }
        }

    }
}
