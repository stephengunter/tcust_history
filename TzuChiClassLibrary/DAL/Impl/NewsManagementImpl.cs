using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace TzuChiClassLibrary.DAL
{
    //資訊查詢 -> 大愛新聞
    public class NewsManagementImpl : INewsManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IFileUploadManagement gFileUploadManagement = new FileUploadManagementImpl();  

        public Boolean Add(NewsModel model)
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
                            var newsParameters = new List<object>();
                            newsParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            newsParameters.Add(new SqlParameter("@SerialNo", model.SerialNo));
                            newsParameters.Add(new SqlParameter("@DisplayOrder", model.DisplayOrder));
                            newsParameters.Add(new SqlParameter("@TypeID", NewsModel.TYPEID));
                            newsParameters.Add(new SqlParameter("@ContentTime", model.ContentTime));
                            newsParameters.Add(new SqlParameter("@ContentName", model.ContentName));
                            newsParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            newsParameters.Add(new SqlParameter("@ContentText", model.ContentText ?? string.Empty));
                            newsParameters.Add(new SqlParameter("@Author", model.Author ?? string.Empty));
                            newsParameters.Add(new SqlParameter("@RelatedLink", model.RelatedLink ?? string.Empty));
                            newsParameters.Add(new SqlParameter("@ShowDate", model.ShowDate));
                            newsParameters.Add(new SqlParameter("@OpenTime", model.OpenTime ?? (object)DBNull.Value));
                            newsParameters.Add(new SqlParameter("@CloseTime", model.CloseTime ?? (object)DBNull.Value));
                            newsParameters.Add(new SqlParameter("@ContentCreator", model.ContentCreator ?? string.Empty));
                            newsParameters.Add(new SqlParameter("@ContentCreateTime", now));
                            newsParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater ?? string.Empty));
                            newsParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.Content ( ContentID, TypeID, SerialNo, DisplayOrder, 
                                                      ContentTime, ContentName, Description, ContentText, Author,
                                                      RelatedLink, ShowDate, OpenTime, CloseTime,
                                                      ContentCreator, ContentCreateTime, 
                                                      ContentUpdater, ContentUpdateTime)
                                             VALUES ( @ContentID, @TypeID, @SerialNo, @DisplayOrder,
                                                      @ContentTime, @ContentName, @Description, @ContentText, @Author,  
                                                      @RelatedLink, @ShowDate, @OpenTime, @CloseTime,
                                                      @ContentCreator, @ContentCreateTime,  
                                                      @ContentUpdater, @ContentUpdateTime);",
                                newsParameters.ToArray());

                            newsParameters.Clear();
                            newsParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            newsParameters.Add(new SqlParameter("@CategoryYearID", model.AcademicYear));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.Educated ( ContentID, CategoryYearID)
                                   VALUES ( @ContentID, @CategoryYearID);",
                                newsParameters.ToArray());

                            dbContextTransaction.Commit();

                            if (model.CoverImage != null)
                            {
                                model.CoverImage.Creator = model.ContentCreator;
                                gFileUploadManagement.Add(model.ContentID, model.CoverImage);
                            }

                            if (model.ContentImage != null)
                            {
                                model.ContentImage.Creator = model.ContentCreator;
                                gFileUploadManagement.Add(model.ContentID, model.ContentImage);
                            }

                            if (model.PicUrlList != null)
                                foreach (FileUploadModel image in model.PicUrlList)
                                {
                                    image.Creator = model.ContentCreator;
                                    gFileUploadManagement.Add(model.ContentID, image);
                                }

                            if (model.VideoUrlList != null)
                                foreach (FileUploadModel video in model.VideoUrlList)
                                {
                                    video.Creator = model.ContentCreator;
                                    gFileUploadManagement.Add(model.ContentID, video);
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

        public Boolean Update(NewsModel model)
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
                            var newsParameters = new List<object>();
                            newsParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            newsParameters.Add(new SqlParameter("@SerialNo", model.SerialNo));
                            newsParameters.Add(new SqlParameter("@DisplayOrder", model.DisplayOrder));
                            newsParameters.Add(new SqlParameter("@ContentTime", model.ContentTime));
                            newsParameters.Add(new SqlParameter("@ContentName", model.ContentName));
                            newsParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            newsParameters.Add(new SqlParameter("@ContentText", model.ContentText ?? string.Empty));
                            newsParameters.Add(new SqlParameter("@Author", model.Author ?? string.Empty));
                            newsParameters.Add(new SqlParameter("@RelatedLink", model.RelatedLink ?? string.Empty)); 
                            newsParameters.Add(new SqlParameter("@ShowDate", model.ShowDate));
                            newsParameters.Add(new SqlParameter("@OpenTime", model.OpenTime ?? (object)DBNull.Value));
                            newsParameters.Add(new SqlParameter("@CloseTime", model.CloseTime ?? (object)DBNull.Value));
                            newsParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater ?? string.Empty));
                            newsParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.Content SET 
                            SerialNo=@SerialNo, 
                            DisplayOrder=@DisplayOrder,
                            ContentTime=@ContentTime, 
                            ContentName=@ContentName, 
                            Description=@Description, 
                            ContentText=@ContentText,
                            Author=@Author,
                            RelatedLink = @RelatedLink, 
                            ShowDate = @ShowDate, 
                            OpenTime = @OpenTime, 
                            CloseTime = @CloseTime,                                         
                            ContentUpdater=@ContentUpdater, 
                            ContentUpdateTime=@ContentUpdateTime
                            WHERE ContentID=@ContentID;",
                                newsParameters.ToArray());

                            newsParameters.Clear();
                            newsParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            newsParameters.Add(new SqlParameter("@CategoryYearID", model.AcademicYear));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.Educated SET CategoryYearID=@CategoryYearID WHERE ContentID=@ContentID;",
                                newsParameters.ToArray());

                            dbContextTransaction.Commit();

                            gFileUploadManagement.DeleteByContentID(model.ContentID);
                            if (model.CoverImage != null)
                            {
                                model.CoverImage.Creator = model.ContentCreator;
                                gFileUploadManagement.Add(model.ContentID, model.CoverImage);
                            }

                            if (model.ContentImage != null)
                            {
                                model.ContentImage.Creator = model.ContentCreator;
                                gFileUploadManagement.Add(model.ContentID, model.ContentImage);
                            }

                            if (model.PicUrlList != null)
                                foreach (FileUploadModel image in model.PicUrlList)
                                {
                                    image.Creator = model.ContentCreator;
                                    gFileUploadManagement.Add(model.ContentID, image);
                                }

                            if (model.VideoUrlList != null)
                                foreach (FileUploadModel video in model.VideoUrlList)
                                {
                                    video.Creator = model.ContentCreator;
                                    gFileUploadManagement.Add(model.ContentID, video);
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
                            var newsParameters = new List<object>();
                            newsParameters.Add(new SqlParameter("@ContentID", contentID));
                            db.Database.ExecuteSqlCommand(@"
                            DELETE FROM dbo.Educated WHERE ContentID=@ContentID;",
                                newsParameters.ToArray());

                            gFileUploadManagement.DeleteByContentID(contentID);

                            newsParameters.Clear();
                            newsParameters.Add(new SqlParameter("@ContentID", contentID));
                            db.Database.ExecuteSqlCommand(@"
                            DELETE FROM dbo.Content WHERE ContentID=@ContentID;",
                                newsParameters.ToArray());

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

        public NewsModel GetByContentID(string contentID)
        {
            NewsModel result = new NewsModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    result = db.Database.SqlQuery<NewsModel>(@"
                                SELECT c.ContentID
                                      ,c.SerialNo
                                      ,c.DisplayOrder
                                      ,c.ContentTime
                                      ,c.ContentName
                                      ,c.Description
                                      ,c.ContentText
                                      ,c.Author
                                      ,e.CategoryYearID AcademicYear
	                                  ,c.ContentTime EventDate
                                      ,ac.Account ContentCreator
                                      ,c.RelatedLink
                                      ,c.ShowDate
                                      ,c.OpenTime
                                      ,c.CloseTime
                                      ,c.ContentCreateTime
                                      ,au.Account ContentUpdater
                                      ,c.ContentUpdateTime
                                  FROM Content c
                                  INNER JOIN Admin ac ON c.ContentCreator = ac.AdminID
                                  INNER JOIN Admin au ON c.ContentUpdater = au.AdminID
                                  INNER JOIN Educated e ON c.ContentID = e.ContentID
                                WHERE c.ContentID=@ContentID", new SqlParameter("@ContentID", contentID)).Single();

                    result.PicUrlList = gFileUploadManagement.GetByContentID(contentID);

                    List<FileUploadModel> fileUploadList = new List<FileUploadModel>();
                    fileUploadList = gFileUploadManagement.GetByContentID(contentID);
                    result.CoverImage = fileUploadList.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_COVERIMAGE)).SingleOrDefault();
                    result.ContentImage = fileUploadList.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_CONTNETIMAGE)).SingleOrDefault();
                    result.PicUrlList = fileUploadList.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_IMAGE)).ToList();
                    result.VideoUrlList = fileUploadList.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_VIDEO)).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public NewsModel GetLatestNews() 
        {
            NewsModel result = new NewsModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    result = db.Database.SqlQuery<NewsModel>(@"
                                SELECT TOP 1 c.ContentID FROM Content c
                                WHERE c.TypeID=@TypeID AND ( c.ShowDate=0 OR 
                                    ( c.ShowDate=1 AND GETDATE() BETWEEN c.OpenTime AND c.CloseTime+1 )) ", new SqlParameter("@TypeID", NewsModel.TYPEID)).Single();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<NewsModel> GetAll(PagenationModel model)
        {
            List<NewsModel> result = new List<NewsModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var newsParameters = new List<object>();
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
                    newsParameters.Add(new SqlParameter("@MIN", min));
                    newsParameters.Add(new SqlParameter("@MAX", max));
                    newsParameters.Add(new SqlParameter("@TypeID", NewsModel.TYPEID));

                    StringBuilder sql = new StringBuilder();
                    string[] colOrder = { "SerialNo", "SerialNo", "ContentName", "(cgy.CategoryName)", "ContentTime" };
                    string orderTitle = (model != null && (colOrder.Length > model.OrderNum)) ? colOrder[model.OrderNum] : colOrder[0];
                    string order = (model != null) ? (model.Order ? "ASC" : "DESC") : "ASC";

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    lstWhere.Add(" c.TypeID = @TypeID ");
                    if (model != null && !String.IsNullOrEmpty(model.KeyWord))
                    {
                        if (model.QueryField == PagenationModel.NEWS_SEARCH_SERIALNO)
                        {
                            lstWhere.Add(" c.SerialNo LIKE @KeyWord ESCAPE '\\' ");
                            newsParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.NEWS_SEARCH_TITLE)
                        {
                            lstWhere.Add(" c.ContentName LIKE @KeyWord ESCAPE '\\' ");
                            newsParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.NEWS_SEARCH_CONTENT)
                        {
                            lstWhere.Add(" c.Description LIKE @KeyWord ESCAPE '\\' ");
                            newsParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.NEWS_SEARCH_YEAR)
                        {
                            lstWhere.Add(" cgy.CategoryName LIKE @KeyWord ESCAPE '\\' ");
                            newsParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
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
                                        c.ContentID,
                                        c.DisplayOrder,
                                        c.SerialNo,
                                        c.ContentTime,
                                        c.RelatedLink,
                                        cgy.CategoryName as AcademicYear,
                                        c.ContentName
                                    FROM Content c
                                    INNER JOIN dbo.Educated e
                                    ON c.ContentID = e.ContentID
                                    INNER JOIN dbo.Category cgy
                                    ON cgy.CategoryID = e.CategoryYearID
                                    {2}
                                    ) TEMP
                                WHERE rowNumber between @MIN and @MAX
                                ORDER BY rowNumber", orderTitle, order, condition);

                    result = db.Database.SqlQuery<NewsModel>(sql.ToString(), newsParameters.ToArray()).ToList();

                    foreach (NewsModel item in result)
                    {
                        item.PicUrlList = gFileUploadManagement.GetCoverImageByContentID(item.ContentID).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }


        public List<NewsModel> GetAllForFrontPage(PagenationModel model)
        {
            List<NewsModel> result = new List<NewsModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var newsParameters = new List<object>();
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

                    max = 200000000;

                    newsParameters.Add(new SqlParameter("@MIN", min));
                    newsParameters.Add(new SqlParameter("@MAX", max));
                    newsParameters.Add(new SqlParameter("@TypeID", NewsModel.TYPEID));

                    StringBuilder sql = new StringBuilder();

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    lstWhere.Add(" c.TypeID = @TypeID ");
                    if (model != null)
                    {
                        if (!string.IsNullOrEmpty(model.Year))          //年度
                        {
                            lstWhere.Add(" cgy.CategoryName LIKE @Year");
                            newsParameters.Add(new SqlParameter("@Year", "%" + Regex.Replace(model.Year, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        if (!string.IsNullOrEmpty(model.Month))         //月份
                        {
                            lstWhere.Add(" MONTH(c.ContentTime) = @Month ");
                            newsParameters.Add(new SqlParameter("@Month", model.Month));
                        }
                        if (!string.IsNullOrEmpty(model.KeyWord))       //關鍵字
                        {
                            lstWhere.Add(" ( c.ContentName LIKE @KeyWord OR c.ContentTime LIKE @KeyWord OR c.Description LIKE @KeyWord) ");
                            newsParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                    }

                    if (lstWhere.Count > 0)
                        condition = string.Format(" WHERE {0} ", string.Join(" AND ", lstWhere.ToArray()));

                    sql.AppendFormat(@"
                                SELECT *
                                FROM
                                (
                                    SELECT
                                        ROW_NUMBER() over(order by c.DisplayOrder DESC,c.ContentTime DESC) rowNumber, COUNT(*) over() totalNum,
                                        c.ContentID,
                                        c.DisplayOrder,
                                        c.SerialNo,
                                        c.ContentTime,
                                        c.Description,
                                        c.ContentText,
                                        c.RelatedLink,
                                        cgy.CategoryName as AcademicYear,
                                        c.ContentName
                                    FROM Content c
                                    INNER JOIN dbo.Educated e
                                    ON c.ContentID = e.ContentID
                                    INNER JOIN dbo.Category cgy
                                    ON cgy.CategoryID = e.CategoryYearID
                                    {0}
                                    ) TEMP
                                WHERE rowNumber between @MIN and @MAX
                                ORDER BY rowNumber", condition);

                    result = db.Database.SqlQuery<NewsModel>(sql.ToString(), newsParameters.ToArray()).ToList();

                    List<FileUploadModel> fileUploadList = new List<FileUploadModel>();
                    foreach (NewsModel item in result)
                    {
                        item.PicUrlList = gFileUploadManagement.GetCoverImageByContentID(item.ContentID).ToList();
                        item.VideoUrlList = gFileUploadManagement.GetByContentID(item.ContentID).Where(m => m.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_VIDEO)).ToList();                         
                    }

                    result.OrderByDescending(item => item.VideoUrlList);

                    
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }
    }
}
