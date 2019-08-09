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
    //榮譽榜
    public class HonourListManagementImpl : IHonourListManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IFileUploadManagement gFileUploadManagement = new FileUploadManagementImpl();  
        
        public Boolean Add(HonourListModel model)
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
                            var honourListParameters = new List<object>();
                            honourListParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            honourListParameters.Add(new SqlParameter("@TypeID", HonourListModel.TYPEID));
                            honourListParameters.Add(new SqlParameter("@ContentTime", model.ContentTime));
                            honourListParameters.Add(new SqlParameter("@ContentName", model.ContentName));
                            honourListParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            honourListParameters.Add(new SqlParameter("@ContentText", model.ContentText ?? string.Empty));
                            honourListParameters.Add(new SqlParameter("@Author", model.Author ?? string.Empty));
                            honourListParameters.Add(new SqlParameter("@RelatedLink", model.RelatedLink ?? string.Empty));
                            honourListParameters.Add(new SqlParameter("@ContentCreator", model.ContentCreator ?? string.Empty));
                            honourListParameters.Add(new SqlParameter("@ContentCreateTime", now));
                            honourListParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater ?? string.Empty));
                            honourListParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.Content ( ContentID, TypeID, ContentTime, ContentName, Description, ContentText, Author ,RelatedLink, ContentCreator, ContentCreateTime, ContentUpdater, ContentUpdateTime)
                                   VALUES ( @ContentID, @TypeID, @ContentTime, @ContentName, @Description, @ContentText, @Author, @RelatedLink, @ContentCreator, @ContentCreateTime, @ContentUpdater, @ContentUpdateTime);",
                                honourListParameters.ToArray());

                            dbContextTransaction.Commit();

                            if (model.ContentImage != null)
                                gFileUploadManagement.Add(model.ContentID, model.ContentImage);

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

        public Boolean Update(HonourListModel model)
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
                            var honourListParameters = new List<object>();
                            honourListParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            honourListParameters.Add(new SqlParameter("@ContentTime", model.ContentTime));
                            honourListParameters.Add(new SqlParameter("@ContentName", model.ContentName));
                            honourListParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            honourListParameters.Add(new SqlParameter("@ContentText", model.ContentText ?? string.Empty));
                            honourListParameters.Add(new SqlParameter("@Author", model.Author ?? string.Empty));
                            honourListParameters.Add(new SqlParameter("@RelatedLink", model.RelatedLink ?? string.Empty));
                            honourListParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater ?? string.Empty));
                            honourListParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.Content SET ContentTime=@ContentTime, ContentName=@ContentName, Description=@Description, ContentText=@ContentText, Author=@Author,
                                                   RelatedLink=@RelatedLink, ContentUpdater=@ContentUpdater, ContentUpdateTime=@ContentUpdateTime
                                               WHERE ContentID=@ContentID;",
                                honourListParameters.ToArray());

                            dbContextTransaction.Commit();

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
                            var honourListParameters = new List<object>();
                            honourListParameters.Add(new SqlParameter("@ContentID", contentID));
                            db.Database.ExecuteSqlCommand(@"
                            DELETE FROM dbo.Content WHERE ContentID=@ContentID;",
                                honourListParameters.ToArray());

                            gFileUploadManagement.DeleteByContentID(contentID);

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

        public HonourListModel GetByContentID(string contentID)
        {
            HonourListModel result = new HonourListModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    result = db.Database.SqlQuery<HonourListModel>(@"
                                SELECT c.ContentID
	                                  ,c.ContentTime
                                      ,c.ContentName
                                      ,c.Description
                                      ,c.ContentText
                                      ,c.Author
                                      ,c.RelatedLink
                                      ,ac.Account ContentCreator
                                      ,c.ContentCreateTime
                                      ,au.Account ContentUpdater
                                      ,c.ContentUpdateTime
                                  FROM Content c
                                  INNER JOIN Admin ac ON c.ContentCreator = ac.AdminID
                                  INNER JOIN Admin au ON c.ContentUpdater = au.AdminID
                                WHERE c.ContentID=@ContentID", new SqlParameter("@ContentID", contentID)).Single();

                    result.PicUrlList = gFileUploadManagement.GetByContentID(contentID);

                    List<FileUploadModel> fileUploadList = new List<FileUploadModel>();
                    fileUploadList = gFileUploadManagement.GetByContentID(contentID);
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

        public List<HonourListModel> GetAll(PagenationModel model)
        {
            List<HonourListModel> result = new List<HonourListModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var honourListParameters = new List<object>();
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
                    honourListParameters.Add(new SqlParameter("@MIN", min));
                    honourListParameters.Add(new SqlParameter("@MAX", max));
                    honourListParameters.Add(new SqlParameter("@TypeID", HonourListModel.TYPEID));

                    StringBuilder sql = new StringBuilder();
                    string[] colOrder = { "c.ContentTime", "c.ContentName", "c.ContentTime" };
                    string orderTitle = (model != null && (colOrder.Length > model.OrderNum)) ? colOrder[model.OrderNum] : colOrder[0];
                    string order = (model != null) ? (model.Order ? "ASC" : "DESC") : "ASC";

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    lstWhere.Add(" c.TypeID = @TypeID ");
                    if (model != null && !String.IsNullOrEmpty(model.KeyWord))
                    {
                        if (model.QueryField == PagenationModel.HONOURLIST_SEARCH_TITLE)
                        {
                            lstWhere.Add(" c.ContentName LIKE @KeyWord ESCAPE '\\' ");
                            honourListParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.HONOURLIST_SEARCH_CONTENT)
                        {
                            lstWhere.Add(" c.Description LIKE @KeyWord ESCAPE '\\' ");
                            honourListParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
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
                                        c.ContentTime,
                                        c.ContentName,
                                        c.RelatedLink
                                    FROM Content c
                                    {2}
                                    ) TEMP
                                WHERE rowNumber between @MIN and @MAX
                                ORDER BY rowNumber", orderTitle, order, condition);

                    result = db.Database.SqlQuery<HonourListModel>(sql.ToString(), honourListParameters.ToArray()).ToList();

                    foreach (HonourListModel item in result)
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

        public List<HonourListModel> GetForFrontPage()
        {
            List<HonourListModel> result = new List<HonourListModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var honourListParameters = new List<object>();
                    honourListParameters.Add(new SqlParameter("@TypeID", HonourListModel.TYPEID));

                    result = db.Database.SqlQuery<HonourListModel>(@"
                                SELECT * FROM (									
                                    SELECT RANK() OVER( PARTITION BY YEAR(c.ContentTime) ORDER BY c.ContentTime DESC) AS YearNum,
                                        c.ContentID,
                                        c.ContentTime,
                                        c.ContentName,
                                        c.RelatedLink
                                    FROM Content c
                                    WHERE c.ContentID IN (
		                                SELECT ContentID 
		                                FROM ContentMultipleType 
		                                WHERE ContentType = @TypeID
                                    )) 
                                temp WHERE temp.YearNum BETWEEN 1 AND 3", honourListParameters.ToArray()).ToList();

                    foreach (HonourListModel item in result)
                    {
                        item.PicUrlList = gFileUploadManagement.GetCoverImageByContentID(item.ContentID).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return result;
        }

        public List<HonourListModel> GetByYear(string year)
        {
            List<HonourListModel> result = new List<HonourListModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var honourListParameters = new List<object>();
                    honourListParameters.Add(new SqlParameter("@TypeID", HonourListModel.TYPEID));
                    honourListParameters.Add(new SqlParameter("@Year", year));

                    StringBuilder sql = new StringBuilder();

                    result = db.Database.SqlQuery<HonourListModel>(@"
                                    SELECT 
	                                    c.ContentID,
                                        c.ContentTime,
                                        c.ContentName,
                                        c.RelatedLink 
                                    FROM Content c
                                    inner join ContentMultipleType cm
                                    ON c.ContentID = cm.ContentID
                                    WHERE cm.ContentType = @TypeID and Year(c.ContentTime) = @Year
                                    ORDER BY c.ContentTime", honourListParameters.ToArray()).ToList();

                    foreach (HonourListModel item in result)
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

        public List<HonourListModel> SearchByKeyWord(PagenationModel model)
        {
            List<HonourListModel> result = new List<HonourListModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var honourListParameters = new List<object>();
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
                    honourListParameters.Add(new SqlParameter("@MIN", min));
                    honourListParameters.Add(new SqlParameter("@MAX", max));
                    honourListParameters.Add(new SqlParameter("@TypeID", HonourListModel.TYPEID));

                    StringBuilder sql = new StringBuilder();

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    if (model != null && !String.IsNullOrEmpty(model.KeyWord))
                    {
                        lstWhere.Add(" (c.ContentName LIKE @KeyWord ESCAPE '\\' OR  c.Description LIKE @KeyWord ESCAPE '\\' OR  c.ContentTime LIKE @KeyWord ESCAPE '\\') ");
                        honourListParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                    }

                    if (lstWhere.Count > 0)
                        condition = string.Format("AND {0} ", string.Join(" AND ", lstWhere.ToArray()));

                    sql.AppendFormat(@"
                                SELECT *
                                FROM
                                (
                                    SELECT
                                        ROW_NUMBER() over(order by c.ContentTime) rowNumber, 
                                        COUNT(*) over() totalNum,
                                        c.ContentID,
                                        c.ContentTime,
                                        c.ContentName,
                                        c.RelatedLink
                                    FROM Content c
                                    inner join ContentMultipleType cm
                                    ON c.ContentID = cm.ContentID
                                    WHERE cm.ContentType = @TypeID
                                    {0}
                                    ) TEMP
                                WHERE rowNumber between @MIN and @MAX
                                ORDER BY rowNumber", condition);

                    result = db.Database.SqlQuery<HonourListModel>(sql.ToString(), honourListParameters.ToArray()).ToList();
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
