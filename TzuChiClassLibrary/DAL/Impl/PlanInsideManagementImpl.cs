using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;
using System.Text.RegularExpressions;

namespace TzuChiClassLibrary.DAL
{
    //天涯比鄰 -> 境內
    public class PlanInsideManagementImpl : IPlanInsideManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IFileUploadManagement gFileUploadManagement = new FileUploadManagementImpl();
        ICoordinateManagement gCoordinateManagement = new CoordinateManagementImpl();

        public Boolean Add(PlanInsideModel model)
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
                            var planInsideParameters = new List<object>();
                            planInsideParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            planInsideParameters.Add(new SqlParameter("@TypeID", PlanInsideModel.TYPEID));
                            planInsideParameters.Add(new SqlParameter("@ContentName", model.ContentName ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@ContentTime", model.ContentTime ?? (object)DBNull.Value));
                            planInsideParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@ContentCreator", model.ContentCreator ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@ContentCreateTime", now));
                            planInsideParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.Content ( ContentID, TypeID, ContentName, ContentTime, 
                                                      Description, ContentCreator, ContentCreateTime, 
                                                      ContentUpdater, ContentUpdateTime)
                                             VALUES ( @ContentID, @TypeID, @ContentName, @ContentTime, 
                                                      @Description, @ContentCreator, @ContentCreateTime, 
                                                      @ContentUpdater, @ContentUpdateTime);",
                                planInsideParameters.ToArray());

                            if (model.ImageXY == null)
                            {
                                model.ImageXY = model.ContentID;
                                model.Coordinate.PointID = model.ContentID;
                                model.Coordinate.TypeID = PlanInsideModel.TYPEID;
                                gCoordinateManagement.Add(model.Coordinate);
                            }

                            planInsideParameters.Clear();
                            planInsideParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            planInsideParameters.Add(new SqlParameter("@CategoryPrepID", model.CategoryPrepID));
                            planInsideParameters.Add(new SqlParameter("@CategoryYearID", model.CategoryYearID ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@CategoryPartitionID", model.CategoryPartitionID ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@CategorySiteID", model.CategorySiteID ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@Department", model.Department ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@ImageXY", model.ImageXY));
                            planInsideParameters.Add(new SqlParameter("@EndTime", model.EndTime ?? (object)DBNull.Value));
                            planInsideParameters.Add(new SqlParameter("@Agencies", model.Agencies ?? string.Empty));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.PlanInside ( ContentID, CategoryPrepID, CategoryYearID, 
                                                         CategoryPartitionID, CategorySiteID, 
                                                         Department, ImageXY, EndTime, Agencies)
                                                VALUES ( @ContentID, @CategoryPrepID, @CategoryYearID, 
                                                         @CategoryPartitionID, @CategorySiteID, 
                                                         @Department, @ImageXY, @EndTime, @Agencies);",
                                planInsideParameters.ToArray());                            

                            dbContextTransaction.Commit();

                            if (model.AttachPicUrl != null)
                                {
                                    model.AttachPicUrl.Creator = model.ContentCreator;
                                    gFileUploadManagement.Add(model.ContentID, model.AttachPicUrl);
                                }
                            if (model.PicUrlList != null)
                                foreach (FileUploadModel image in model.PicUrlList)
                                {
                                    image.Creator = model.ContentCreator;
                                    gFileUploadManagement.Add(model.ContentID, image);
                                }

                            
                        }
                        catch (Exception e)
                        {
                            throw e;
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

        public Boolean Update(PlanInsideModel model)
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
                            var planInsideParameters = new List<object>();
                            planInsideParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            planInsideParameters.Add(new SqlParameter("@ContentName", model.ContentName ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@ContentTime", model.ContentTime ?? (object)DBNull.Value));
                            planInsideParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.Content SET 
                                          ContentName=@ContentName, 
                                          ContentTime=@ContentTime, 
                                          Description=@Description, 
                                          ContentUpdater=@ContentUpdater, 
                                          ContentUpdateTime=@ContentUpdateTime
                            WHERE ContentID=@ContentID;",
                                planInsideParameters.ToArray());

                            if (model.ImageXY == null)
                            {
                                model.ImageXY = model.ContentID;
                                model.Coordinate.PointID = model.ContentID;
                                model.Coordinate.TypeID = PlanInsideModel.TYPEID;
                                gCoordinateManagement.Delete(model.ContentID);
                                gCoordinateManagement.Add(model.Coordinate);
                            }

                            planInsideParameters.Clear();
                            planInsideParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            planInsideParameters.Add(new SqlParameter("@CategoryPrepID", model.CategoryPrepID));
                            planInsideParameters.Add(new SqlParameter("@CategoryYearID", model.CategoryYearID ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@CategoryPartitionID", model.CategoryPartitionID ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@CategorySiteID", model.CategorySiteID ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@Department", model.Department ?? string.Empty));
                            planInsideParameters.Add(new SqlParameter("@ImageXY", model.ImageXY));
                            planInsideParameters.Add(new SqlParameter("@EndTime", model.EndTime ?? (object)DBNull.Value));
                            planInsideParameters.Add(new SqlParameter("@Agencies", model.Agencies ?? string.Empty));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.PlanInside SET 
                                            CategoryPrepID=@CategoryPrepID, 
                                            CategoryYearID=@CategoryYearID, 
                                            CategoryPartitionID=@CategoryPartitionID, 
                                            CategorySiteID=@CategorySiteID, 
                                            Department=@Department, 
                                            ImageXY=@ImageXY, 
                                            EndTime=@EndTime, 
                                            Agencies=@Agencies
                            WHERE ContentID=@ContentID;",
                                planInsideParameters.ToArray());   

                            dbContextTransaction.Commit();

                            gFileUploadManagement.DeleteByContentID(model.ContentID);
                            if (model.AttachPicUrl != null)
                                {
                                    model.AttachPicUrl.Creator = model.ContentCreator;
                                    gFileUploadManagement.Add(model.ContentID, model.AttachPicUrl);
                                }
                            if (model.PicUrlList != null)
                                foreach (FileUploadModel image in model.PicUrlList)
                                {
                                    image.Creator = model.ContentCreator;
                                    gFileUploadManagement.Add(model.ContentID, image);
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
                            var planInsideParameters = new List<object>();
                            planInsideParameters.Add(new SqlParameter("@ContentID", contentID));
                            db.Database.ExecuteSqlCommand(@"
                            DELETE dbo.FileUplaod WHERE ContentID = @ContentID
                            DELETE FROM dbo.PlanInside WHERE ContentID=@ContentID;
                            DELETE FROM dbo.Coordinate WHERE PointID=@ContentID;
                            DELETE FROM dbo.Content WHERE ContentID=@ContentID;",
                                planInsideParameters.ToArray());

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

        public PlanInsideModel GetByContentID(string contentID)
        {
            PlanInsideModel result = new PlanInsideModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    result = db.Database.SqlQuery<PlanInsideModel>(@"
                                SELECT c.ContentID					  
                                      ,p.CategoryPrepID                                 
                                      ,p.CategoryYearID                               
                                      ,p.CategoryPartitionID
                                      ,p.CategorySiteID
                                      ,p.Department
                                      ,p.ImageXY
                                      ,c.ContentTime
                                      ,p.EndTime
                                      ,c.Description
                                      ,p.Agencies
                                      ,c.ContentName
                                      ,ac.Account ContentCreator
                                      ,c.ContentCreateTime
                                      ,au.Account ContentUpdater
                                      ,c.ContentUpdateTime
                                  FROM Content c
                                  LEFT JOIN Admin ac ON c.ContentCreator = ac.AdminID
                                  LEFT JOIN Admin au ON c.ContentUpdater = au.AdminID
                                  INNER JOIN PlanInside p ON c.ContentID = p.ContentID
                                WHERE c.ContentID=@ContentID", new SqlParameter("@ContentID", contentID)).Single();

                    result.Coordinate = gCoordinateManagement.GetByPointID(result.ImageXY);

                    result.PicUrlList = gFileUploadManagement.GetByContentID(contentID);

                    List<FileUploadModel> fileUploadList = new List<FileUploadModel>();
                    fileUploadList = gFileUploadManagement.GetByContentID(contentID);
                    result.AttachPicUrl = fileUploadList.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_ATTACHMENTIMAGE)).SingleOrDefault();
                    result.PicUrlList = fileUploadList.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_IMAGE)).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<PlanInsideModel> GetAll(PagenationModel model)
        {
            List<PlanInsideModel> result = new List<PlanInsideModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var planInsideParameters = new List<object>();
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
                    planInsideParameters.Add(new SqlParameter("@MIN", min));
                    planInsideParameters.Add(new SqlParameter("@MAX", max));
                    planInsideParameters.Add(new SqlParameter("@TypeID", PlanInsideModel.TYPEID));

                    StringBuilder sql = new StringBuilder();
                    string[] colOrder = { "ContentTime", "CategoryPrepID", "CategorySiteID", "Department" };
                    string orderTitle = (model != null && (colOrder.Length > model.OrderNum)) ? colOrder[model.OrderNum] : colOrder[0];
                    string order = (model != null) ? (model.Order ? "ASC" : "DESC") : "ASC";

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    lstWhere.Add(" c.TypeID = @TypeID ");
                    if (model != null && !String.IsNullOrEmpty(model.KeyWord))
                    {
                        if (model.QueryField == PagenationModel.PLANINSIDE_SEARCH_PREP)
                        {
                            lstWhere.Add(" cgpr.CategoryName LIKE @KeyWord ESCAPE '\\' ");
                            planInsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.PLANINSIDE_SEARCH_YEAR)
                        {
                            lstWhere.Add(" cgy.CategoryName LIKE @KeyWord ESCAPE '\\' ");
                            planInsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.PLANINSIDE_SEARCH_PARTITION)
                        {
                            lstWhere.Add(" cgpa.CategoryName LIKE @KeyWord ESCAPE '\\' ");
                            planInsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.PLANINSIDE_SEARCH_SITE)
                        {
                            lstWhere.Add(" cgs.CategoryName LIKE @KeyWord ESCAPE '\\' ");
                            planInsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.PLANINSIDE_SEARCH_DEPARTMENT)
                        {
                            lstWhere.Add(" p.Department LIKE @KeyWord ESCAPE '\\' ");
                            planInsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.PLANINSIDE_SEARCH_DESCRIPTION)
                        {
                            lstWhere.Add(" c.Description LIKE @KeyWord ESCAPE '\\' ");
                            planInsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.PLANINSIDE_SEARCH_AGENCIES)
                        {
                            lstWhere.Add(" p.Agencies LIKE @KeyWord ESCAPE '\\' ");
                            planInsideParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
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
                                       ,cgpr.CategoryName as CategoryPrepID
                                       ,cgy.CategoryName as CategoryYearID
                                       ,cgpa.CategoryName as CategoryPartitionID
                                       ,cgs.CategoryName as CategorySiteID
                                       ,p.Department
                                       ,p.ImageXY
                                       ,c.ContentTime
                                       ,p.EndTime
                                       ,c.Description
                                       ,p.Agencies
                                       ,c.ContentName
                                       ,ac.Account ContentCreator
                                       ,c.ContentCreateTime
                                       ,au.Account ContentUpdater
                                       ,c.ContentUpdateTime                                       
                                      FROM Content c
                                      LEFT JOIN Admin ac ON c.ContentCreator = ac.AdminID
                                      LEFT JOIN Admin au ON c.ContentUpdater = au.AdminID
                                      INNER JOIN PlanInside p ON c.ContentID = p.ContentID
                                      LEFT JOIN Category cgy ON cgy.CategoryID = p.CategoryYearID
                                      LEFT JOIN Category cgpr ON cgpr.CategoryID = p.CategoryPrepID
                                      LEFT JOIN Category cgpa ON cgpa.CategoryID = p.CategoryPartitionID
                                      LEFT JOIN Category cgs ON cgs.CategoryID = p.CategorySiteID
                                      {2}
                                    ) TEMP
                                WHERE rowNumber between @MIN and @MAX 
                                ORDER BY rowNumber", orderTitle, order, condition);

                    result = db.Database.SqlQuery<PlanInsideModel>(sql.ToString(), planInsideParameters.ToArray()).ToList();

                    foreach (PlanInsideModel item in result)
                    {
                        List<FileUploadModel> fileUploadList = gFileUploadManagement.GetByContentID(item.ContentID).ToList();
                        item.PicUrlList = fileUploadList.Where(m => m.CoverImage).ToList();
                        item.AttachPicUrl = fileUploadList.Where(m => m.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_ATTACHMENTIMAGE)).SingleOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<PlanInsideModel> GetForFrontPage(string categoryPrepID, string categoryPartitionID, string categorySiteID, string department)
        {
            List<PlanInsideModel> result = new List<PlanInsideModel>();
            int displacementX = 460;
            int displacementY = 240;
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var planInsideParameters = new List<object>();
                    planInsideParameters.Add(new SqlParameter("@TypeID", PlanInsideModel.TYPEID));
                    planInsideParameters.Add(new SqlParameter("@CategoryPrepID", categoryPrepID));

                    StringBuilder sql = new StringBuilder();
                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    if (categoryPartitionID != null)
                    {
                        lstWhere.Add("p.CategoryPartitionID = @CategoryPartitionID");
                        planInsideParameters.Add(new SqlParameter("@CategoryPartitionID", categoryPartitionID));
                    }
                    if (categorySiteID != null)
                    {
                        lstWhere.Add("p.CategorySiteID = @CategorySiteID");
                        planInsideParameters.Add(new SqlParameter("@CategorySiteID", categorySiteID));
                    }
                    if (department != null)
                    {
                        lstWhere.Add("p.Department LIKE @Department ESCAPE '\\' ");
                        planInsideParameters.Add(new SqlParameter("@Department", department));
                    }

                    if (lstWhere.Count > 0)
                        condition = string.Format(" AND {0} ", string.Join(" AND ", lstWhere.ToArray()));

                    sql.AppendFormat(@"
                                 SELECT c.ContentID
                                       ,c.ContentName
                                       ,c.ContentTime
                                       ,c.Description
                                       ,p.Agencies
                                       ,p.ImageXY
                                       ,p.Department
                                       ,p.EndTime
                                       ,cgy.CategoryName CategoryYearID
                                       ,cgp.CategoryName CategoryPartitionID
                                       ,cgs.CategoryName CategorySiteID
                                  FROM Content c
                                  INNER JOIN PlanInside p ON c.ContentID = p.ContentID
                                  LEFT JOIN Category cgy ON cgy.categoryID = p.CategoryYearID
                                  LEFT JOIN Category cgp ON cgp.categoryID = p.CategoryPartitionID
                                  LEFT JOIN Category cgs ON cgs.categoryID = p.CategorySiteID
                                  WHERE c.TypeID=@TypeID
                                  AND p.CategoryPrepID = @CategoryPrepID
                                  {0}
                                  ORDER BY c.ContentTime DESC", condition);

                    result = db.Database.SqlQuery<PlanInsideModel>(sql.ToString(), planInsideParameters.ToArray()).ToList();

                    foreach (PlanInsideModel item in result)
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

        public List<string> GetDepartList(string categoryPrepID)
        {
            List<string> result = new List<string>();
            string sql = @" SELECT p.Department 
                            FROM PlanInside p
                            WHERE p.CategoryPrepID = @CategoryPrepID
                            GROUP BY p.Department";

            SqlConnection SqlSvrCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            try
            {
                SqlCommand SqlCmd = new SqlCommand(sql, SqlSvrCon);
                SqlSvrCon.Open();
                SqlCmd.Parameters.AddWithValue("@CategoryPrepID", categoryPrepID);
                SqlDataReader data = SqlCmd.ExecuteReader();
                while (data.Read())
                {
                    if (!string.Empty.Equals(data["Department"].ToString()))
                    result.Add(data["Department"].ToString());
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
