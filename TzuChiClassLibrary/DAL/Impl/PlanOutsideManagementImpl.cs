using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace TzuChiClassLibrary.DAL
{
    //天涯比鄰 -> 境外
    public class PlanOutsideManagementImpl : IPlanOutsideManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IFileUploadManagement gFileUploadManagement = new FileUploadManagementImpl();
        ICoordinateManagement gCoordinateManagement = new CoordinateManagementImpl();

        public Boolean Add(PlanOutsideModel model)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var now = DateTime.Now;
                            model.ContentID = Guid.NewGuid().ToString();
                            var planOutsideParameters = new List<object>();
                            planOutsideParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            planOutsideParameters.Add(new SqlParameter("@TypeID", PlanOutsideModel.TYPEID));
                            planOutsideParameters.Add(new SqlParameter("@ContentName", model.ContentName ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@ContentTime", model.ContentTime ?? (object)DBNull.Value));
                            planOutsideParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@ContentCreator", model.ContentCreator ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@ContentCreateTime", now));
                            planOutsideParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.Content ( ContentID, TypeID, ContentName, ContentTime, 
                                                      Description, ContentCreator, ContentCreateTime, 
                                                      ContentUpdater, ContentUpdateTime)
                                             VALUES ( @ContentID, @TypeID, @ContentName, @ContentTime, 
                                                      @Description, @ContentCreator, @ContentCreateTime, 
                                                      @ContentUpdater, @ContentUpdateTime);",
                                planOutsideParameters.ToArray());

                            if (model.ImageXY == null)
                            {
                                model.ImageXY = model.ContentID;
                                model.Coordinate.PointID = model.ContentID;
                                model.Coordinate.TypeID = PlanOutsideModel.TYPEID;
                                gCoordinateManagement.Add(model.Coordinate);
                            }

                            planOutsideParameters.Clear();
                            planOutsideParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            planOutsideParameters.Add(new SqlParameter("@CategoryOutsideID", model.CategoryOutsideID));
                            planOutsideParameters.Add(new SqlParameter("@CategoryCountryID", model.CategoryCountryID));
                            planOutsideParameters.Add(new SqlParameter("@CategoryDepartmentID", model.CategoryDepartmentID ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@EnglishName", model.EnglishName ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@EndTime", model.EndTime ?? (object)DBNull.Value ));
                            planOutsideParameters.Add(new SqlParameter("@IntroCh", model.IntroCh ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@IntroEn", model.IntroEn ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@ImageXY", model.ImageXY));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.PlanOutside ( ContentID, CategoryOutsideID, CategoryCountryID, 
                                                          CategoryDepartmentID, EnglishName, EndTime, 
                                                          IntroCh, IntroEn, ImageXY)
                                                 VALUES ( @ContentID, @CategoryOutsideID, @CategoryCountryID, 
                                                          @CategoryDepartmentID, @EnglishName, @EndTime, 
                                                          @IntroCh, @IntroEn, @ImageXY);",
                                planOutsideParameters.ToArray());

                            dbContextTransaction.Commit();

                            if (model.PicUrlList != null)
                                foreach (FileUploadModel image in model.PicUrlList)
                                {
                                    image.Creator = model.ContentCreator;
                                    gFileUploadManagement.Add(model.ContentID, image);
                                }
                            if (model.AttachPicUrl != null)                                
                                {
                                    model.AttachPicUrl.Creator = model.ContentCreator;
                                    gFileUploadManagement.Add(model.ContentID, model.AttachPicUrl);
                                }
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

        public Boolean Update(PlanOutsideModel model)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var now = DateTime.Now;
                            var planOutsideParameters = new List<object>();
                            planOutsideParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            planOutsideParameters.Add(new SqlParameter("@ContentName", model.ContentName ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@ContentTime", model.ContentTime ?? (object)DBNull.Value));
                            planOutsideParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.Content SET 
                                        ContentName=@ContentName, 
                                        ContentTime=@ContentTime, 
                                        Description=@Description, 
                                        ContentUpdater=@ContentUpdater, 
                                        ContentUpdateTime=@ContentUpdateTime 
                            WHERE ContentID=@ContentID;",
                                planOutsideParameters.ToArray());

                            if (model.ImageXY == null)
                            {
                                model.ImageXY = model.ContentID;
                                model.Coordinate.PointID = model.ContentID;
                                model.Coordinate.TypeID = PlanOutsideModel.TYPEID;
                                gCoordinateManagement.Delete(model.ContentID);
                                gCoordinateManagement.Add(model.Coordinate);
                            }

                            planOutsideParameters.Clear();
                            planOutsideParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            planOutsideParameters.Add(new SqlParameter("@CategoryOutsideID", model.CategoryOutsideID));
                            planOutsideParameters.Add(new SqlParameter("@CategoryCountryID", model.CategoryCountryID));
                            planOutsideParameters.Add(new SqlParameter("@CategoryDepartmentID", model.CategoryDepartmentID ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@EnglishName", model.EnglishName ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@EndTime", model.EndTime ?? (object)DBNull.Value));
                            planOutsideParameters.Add(new SqlParameter("@IntroCh", model.IntroCh ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@IntroEn", model.IntroEn ?? string.Empty));
                            planOutsideParameters.Add(new SqlParameter("@ImageXY", model.ImageXY));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.PlanOutside SET 
                                            CategoryOutsideID=@CategoryOutsideID,
                                            CategoryCountryID=@CategoryCountryID,
                                            CategoryDepartmentID=@CategoryDepartmentID,
                                            EnglishName=@EnglishName,
                                            EndTime=@EndTime,
                                            IntroCh=@IntroCh, 
                                            IntroEn=@IntroEn, 
                                            ImageXY=@ImageXY
                            WHERE ContentID=@ContentID;",
                                planOutsideParameters.ToArray());   

                            dbContextTransaction.Commit();

                            gFileUploadManagement.DeleteByContentID(model.ContentID);
                            if (model.PicUrlList != null)
                                foreach (FileUploadModel image in model.PicUrlList)
                                {
                                    image.Creator = model.ContentCreator;
                                    gFileUploadManagement.Add(model.ContentID, image);
                                }
                            if (model.AttachPicUrl != null)
                            {
                                model.AttachPicUrl.Creator = model.ContentCreator;
                                gFileUploadManagement.Add(model.ContentID, model.AttachPicUrl);
                            }
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

        public Boolean Delete(string contentID)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var planOutsideParameters = new List<object>();
                            planOutsideParameters.Add(new SqlParameter("@ContentID", contentID));
                            db.Database.ExecuteSqlCommand(@"
                            DELETE dbo.FileUplaod WHERE ContentID = @ContentID;
                            DELETE FROM dbo.PlanOutside WHERE ContentID=@ContentID;
                            DELETE FROM dbo.Coordinate WHERE PointID=@ContentID;
                            DELETE FROM dbo.Content WHERE ContentID=@ContentID;",
                                planOutsideParameters.ToArray());
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

        public PlanOutsideModel GetByContentID(string contentID)
        {
            PlanOutsideModel result = new PlanOutsideModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    result = db.Database.SqlQuery<PlanOutsideModel>(@"
                                SELECT c.ContentID
								      ,p.CategoryOutsideID
                                      ,p.CategoryCountryID
                                      ,p.CategoryDepartmentID
                                      ,c.ContentName
                                      ,p.EnglishName
                                      ,c.ContentTime
                                      ,p.EndTime 
                                      ,c.Description    
                                      ,p.IntroCh
                                      ,p.IntroEn
                                      ,p.ImageXY
                                      ,ac.Account ContentCreator
                                      ,c.ContentCreateTime
                                      ,au.Account ContentUpdater
                                      ,c.ContentUpdateTime
                                  FROM Content c
                                  LEFT JOIN Admin ac ON c.ContentCreator = ac.AdminID
                                  LEFT JOIN Admin au ON c.ContentUpdater = au.AdminID
                                  INNER JOIN PlanOutside p ON c.ContentID = p.ContentID
                                WHERE c.ContentID=@ContentID", new SqlParameter("@ContentID", contentID)).Single();

                    result.Coordinate = gCoordinateManagement.GetByPointID(result.ImageXY);

                    result.PicUrlList = gFileUploadManagement.GetByContentID(contentID);

                    List<FileUploadModel> fileUploadList = new List<FileUploadModel>();
                    fileUploadList = gFileUploadManagement.GetByContentID(contentID);
                    result.PicUrlList = fileUploadList.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_IMAGE)).ToList();
                    result.AttachPicUrl = fileUploadList.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_ATTACHMENTIMAGE)).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<PlanOutsideModel> GetAll(PagenationModel model)
        {
            List<PlanOutsideModel> result = new List<PlanOutsideModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var planOutsideParameters = new List<object>();
                    var min = 0;
                    var max = 200000000;
                    if (model != null)
                    {
                        min = (model.Page - 1) * model.CntPerPage + 1;
                        max = model.Page * model.CntPerPage;
                        if (min >= max)
                        {
                            min = 0;
                            max = 10;
                        }
                    }
                    planOutsideParameters.Add(new SqlParameter("@MIN", min));
                    planOutsideParameters.Add(new SqlParameter("@MAX", max));
                    planOutsideParameters.Add(new SqlParameter("@TypeID", PlanOutsideModel.TYPEID));

                    StringBuilder sql = new StringBuilder();
                    string[] colOrder = { "ContentTime", "ContentName", "cgo.CategoryName", "cgc.CategoryName" };
                    string orderTitle = (model != null && (colOrder.Length > model.OrderNum)) ? colOrder[model.OrderNum] : colOrder[0];
                    string order = (model != null) ? (model.Order ? "ASC" : "DESC") : "ASC";

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    lstWhere.Add(" c.TypeID = @TypeID ");
                    if (model != null && !String.IsNullOrEmpty(model.KeyWord))
                    {
                        if (model.QueryField == PagenationModel.PLANOUTSIDE_SEARCH_TYPEOUTSIDE)
                        {
                            lstWhere.Add(" cgo.CategoryName LIKE @KeyWord ESCAPE '\\' ");
                            planOutsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.PLANOUTSIDE_SEARCH_COUNTRY)
                        {
                            lstWhere.Add(" cgc.CategoryName LIKE @KeyWord ESCAPE '\\' ");
                            planOutsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.PLANOUTSIDE_SEARCH_CONTRACTDEPARTMENT)
                        {
                            lstWhere.Add(" cgd.CategoryName LIKE @KeyWord ESCAPE '\\' ");
                            planOutsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.PLANOUTSIDE_SEARCH_SCHOOLNAMECH)
                        {
                            lstWhere.Add(" c.ContentName LIKE @KeyWord ESCAPE '\\' ");
                            planOutsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.PLANOUTSIDE_SEARCH_SCHOOLNAMEEN)
                        {
                            lstWhere.Add(" p.EnglishName LIKE @KeyWord ESCAPE '\\' ");
                            planOutsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.PLANOUTSIDE_SEARCH_ACTIVITYNAME)
                        {
                            lstWhere.Add(" c.Description LIKE @KeyWord ESCAPE '\\' ");
                            planOutsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                    }

                    if (lstWhere.Count > 0)
                        condition = string.Format(" WHERE {0} ", string.Join(" AND ", lstWhere.ToArray()));

                    sql.AppendFormat(@"
                                 SELECT *
                                    FROM
                                    (
                                        SELECT
                                            ROW_NUMBER() over(order by {0} {1}) rowNumber, COUNT(*) over() totalNum,
                                            c.ContentID
                                           ,cgo.CategoryName as CategoryOutsideID
                                           ,cgc.CategoryName as CategoryCountryID
                                           ,cgd.CategoryName as CategoryDepartmentID
                                           ,c.ContentName
                                           ,p.EnglishName
                                           ,c.ContentTime
                                           ,p.EndTime
                                           ,c.Description
                                           ,p.IntroCh
                                           ,p.IntroEn
                                           ,p.ImageXY
                                           ,ac.Account ContentCreator
                                           ,c.ContentCreateTime
                                           ,au.Account ContentUpdater
                                           ,c.ContentUpdateTime                                       
                                      FROM Content c
                                      LEFT JOIN Admin ac ON c.ContentCreator = ac.AdminID
                                      LEFT JOIN Admin au ON c.ContentUpdater = au.AdminID
                                      INNER JOIN PlanOutside p ON c.ContentID = p.ContentID
                                      LEFT JOIN Category cgo ON cgo.CategoryID = p.CategoryOutsideID
                                      LEFT JOIN Category cgc ON cgc.CategoryID = p.CategoryCountryID
                                      LEFT JOIN Category cgd ON cgd.CategoryID = p.CategoryDepartmentID
                                      {2}
                                        ) TEMP
                                    WHERE rowNumber between @MIN and @MAX
                                    ORDER BY rowNumber", orderTitle, order, condition);

                    result = db.Database.SqlQuery<PlanOutsideModel>(sql.ToString(), planOutsideParameters.ToArray()).ToList();

                    foreach (PlanOutsideModel item in result)
                    {
                        List<FileUploadModel> files = item.PicUrlList = gFileUploadManagement.GetByContentID(item.ContentID).ToList();
                        item.PicUrlList = files.Where(f => f.CoverImage).ToList();
                        item.AttachPicUrl = files.Where(f => f.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_ATTACHMENTIMAGE)).SingleOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<PlanOutsideModel> GetForFrontPage(string categoryOutsideID, string categoryCountryID, string year)
        {
            List<PlanOutsideModel> result = new List<PlanOutsideModel>();
            int displacementX = -15;
            int displacementY = -15;
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var planOutsideParameters = new List<object>();
                    planOutsideParameters.Add(new SqlParameter("@TypeID", PlanOutsideModel.TYPEID));
                    planOutsideParameters.Add(new SqlParameter("@CategoryOutsideID", categoryOutsideID));

                    StringBuilder sql = new StringBuilder();
                    string condition = string.Empty;
                    string conditionYear = string.Empty;
                    if (!string.IsNullOrEmpty(categoryCountryID)) 
                    {
                        condition = "AND p.CategoryCountryID = @CategoryCountryID";
                        planOutsideParameters.Add(new SqlParameter("@CategoryCountryID", categoryCountryID));
                    }
                    if(!string.IsNullOrEmpty(year)){
                        conditionYear = "AND YEAR(c.ContentTime) = @Year";
                        planOutsideParameters.Add(new SqlParameter("@Year", year));
                    }

                    sql.AppendFormat(@"
                                 SELECT c.ContentID
                                       ,c.ContentName
                                       ,c.ContentTime
                                       ,p.EnglishName
                                       ,p.EndTime
                                       ,p.IntroCh
                                       ,p.IntroEn
                                       ,p.ImageXY
                                       ,cgp.CategoryName CategoryDepartmentID
                                       ,cg.CategoryName CategoryCountryID
                                  FROM Content c
                                  INNER JOIN PlanOutside p ON c.ContentID = p.ContentID
                                  INNER JOIN Category cg ON cg.categoryID = p.CategoryCountryID
                                  LEFT JOIN Category cgp ON cgp.categoryID = p.CategoryDepartmentID
                                 WHERE c.TypeID=@TypeID
                                 AND p.CategoryOutsideID = @CategoryOutsideID
                                 {0}
                                 {1}
                                 ORDER BY c.ContentTime DESC", condition, conditionYear);

                    result = db.Database.SqlQuery<PlanOutsideModel>(sql.ToString(), planOutsideParameters.ToArray()).ToList();

                    foreach(PlanOutsideModel item in result)
                    {
                        item.PicUrlList = gFileUploadManagement.GetByContentID(item.ContentID);
                        item.Coordinate = gCoordinateManagement.GetByPointID(item.ImageXY);

                        //調整座標偏差值
                        if (item.Coordinate != null)
                        {
                            item.Coordinate.PointX = item.Coordinate.PointX + displacementX;
                            item.Coordinate.PointY = item.Coordinate.PointY + displacementY;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public Dictionary<string, string> GetCountryList(string categoryOutsideID)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string sql = @"SELECT p.CategoryCountryID, c.CategoryName
                           FROM dbo.PlanOutside p
                           INNER JOIN Category c
                           ON p.CategoryCountryID = c.CategoryID
                           WHERE CategoryOutsideID = @CategoryOutsideID
                           GROUP BY p.CategoryCountryID,c.CategoryName";

            SqlConnection SqlSvrCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            try
            {
                SqlCommand SqlCmd = new SqlCommand(sql, SqlSvrCon);
                SqlCmd.Parameters.AddWithValue("@CategoryOutsideID", categoryOutsideID);
                SqlSvrCon.Open();
                SqlDataReader data = SqlCmd.ExecuteReader();
                while (data.Read())
                {
                    result.Add(data["CategoryCountryID"].ToString(), data["CategoryName"].ToString());
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            finally
            {
                SqlSvrCon.Close();
            }
            return result;
        }

        public List<string> GetYearList(string categoryOutsideID)
        {
            List<string> result = new List<string>();
            string sql = @"SELECT YEAR(c.ContentTime) YEAR
                           FROM Content c
                           INNER JOIN PlanOutside p
                           ON p.ContentID = c.ContentID 
                           WHERE c.TypeID = @TypeID
                           AND p.CategoryOutsideID = @CategoryOutsideID
                           GROUP BY YEAR(c.ContentTime)
                           ORDER BY YEAR DESC";

            SqlConnection SqlSvrCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            try
            {
                SqlCommand SqlCmd = new SqlCommand(sql, SqlSvrCon);
                SqlCmd.Parameters.AddWithValue("@TypeID", PlanOutsideModel.TYPEID);
                SqlCmd.Parameters.AddWithValue("@CategoryOutsideID", categoryOutsideID);
                SqlSvrCon.Open();
                SqlDataReader data = SqlCmd.ExecuteReader();
                while (data.Read())
                {
                    result.Add(data["YEAR"].ToString());
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            finally
            {
                SqlSvrCon.Close();
            }
            return result;
        }

        public string GetAllCategoryID() {
            string result = String.Empty;
            string sql = @"  SELECT c.CategoryID
                             FROM Category c
                             WHERE c.CategoryName = 'All'";

            SqlConnection SqlSvrCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            try
            {
                SqlCommand SqlCmd = new SqlCommand(sql, SqlSvrCon);
                SqlSvrCon.Open();
                SqlDataReader data = SqlCmd.ExecuteReader();
                while (data.Read())
                {
                    result = data["CategoryID"].ToString();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            finally
            {
                SqlSvrCon.Close();
            }
            return result;
        }
    }
}
