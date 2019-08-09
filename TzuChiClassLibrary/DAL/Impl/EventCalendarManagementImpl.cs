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
    //大事記要
    public class EventCalendarManagementImpl : IEventCalendarManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IFileUploadManagement gFileUploadManagement = new FileUploadManagementImpl();  

        public Boolean Add(EventCalendarModel model)
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
                            var eventCalendarParameters = new List<object>();
                            eventCalendarParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            eventCalendarParameters.Add(new SqlParameter("@TypeID", EventCalendarModel.TYPEID));
                            eventCalendarParameters.Add(new SqlParameter("@ContentTime", model.ContentTime));
                            eventCalendarParameters.Add(new SqlParameter("@ContentName", model.ContentName));
                            eventCalendarParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            eventCalendarParameters.Add(new SqlParameter("@ContentText", model.ContentText ?? string.Empty));
                            eventCalendarParameters.Add(new SqlParameter("@ContentCreator", model.ContentCreator ?? string.Empty));
                            eventCalendarParameters.Add(new SqlParameter("@ContentCreateTime", now));
                            eventCalendarParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater ?? string.Empty));
                            eventCalendarParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.Content ( ContentID, TypeID, ContentTime, ContentName, Description, ContentText,ContentCreator, ContentCreateTime, ContentUpdater, ContentUpdateTime)
                                   VALUES ( @ContentID, @TypeID, @ContentTime, @ContentName, @Description, @ContentText, @ContentCreator, @ContentCreateTime, @ContentUpdater, @ContentUpdateTime);",
                                eventCalendarParameters.ToArray());

                            eventCalendarParameters.Clear();
                            eventCalendarParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            eventCalendarParameters.Add(new SqlParameter("@CategoryYearID", model.AcademicYear));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.Educated ( ContentID, CategoryYearID)
                                   VALUES ( @ContentID, @CategoryYearID);",
                                eventCalendarParameters.ToArray());

                            dbContextTransaction.Commit();

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

        public Boolean Update(EventCalendarModel model)
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
                            var eventCalendarParameters = new List<object>();
                            eventCalendarParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            eventCalendarParameters.Add(new SqlParameter("@ContentTime", model.ContentTime));
                            eventCalendarParameters.Add(new SqlParameter("@ContentName", model.ContentName));
                            eventCalendarParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            eventCalendarParameters.Add(new SqlParameter("@ContentText", model.ContentText ?? string.Empty));
                            eventCalendarParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater ?? string.Empty));
                            eventCalendarParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.Content SET ContentTime=@ContentTime, ContentName=@ContentName, Description=@Description, ContentText=@ContentText, 
                                                   ContentUpdater=@ContentUpdater, ContentUpdateTime=@ContentUpdateTime
                                               WHERE ContentID=@ContentID;",
                                eventCalendarParameters.ToArray());

                            eventCalendarParameters.Clear();
                            eventCalendarParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            eventCalendarParameters.Add(new SqlParameter("@CategoryYearID", model.AcademicYear));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.Educated SET CategoryYearID=@CategoryYearID 
                                               WHERE ContentID=@ContentID;",
                                eventCalendarParameters.ToArray());

                            dbContextTransaction.Commit();

                            gFileUploadManagement.DeleteByContentID(model.ContentID);
                            if (model.PicUrlList != null)
                                foreach (FileUploadModel image in model.PicUrlList)
                                {
                                    image.Creator = model.ContentUpdater;
                                    gFileUploadManagement.Add(model.ContentID, image);
                                }

                            if (model.VideoUrlList != null)
                                foreach (FileUploadModel video in model.VideoUrlList)
                                {
                                    video.Creator = model.ContentUpdater;
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
                            var eventCalendarParameters = new List<object>();
                            eventCalendarParameters.Add(new SqlParameter("@ContentID", contentID));
                            db.Database.ExecuteSqlCommand(@"
                            DELETE dbo.FileUplaod WHERE ContentID = @ContentID
                            DELETE dbo.Educated WHERE ContentID=@ContentID;
                            DELETE dbo.Content WHERE ContentID=@ContentID;",
                                eventCalendarParameters.ToArray());

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

        public EventCalendarModel GetByContentID(string contentID, bool includeAdmin = true)
        {
            EventCalendarModel result = new EventCalendarModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
					if (includeAdmin)
					{
						result = db.Database.SqlQuery<EventCalendarModel>(@"SELECT c.ContentID
                                      ,e.CategoryYearID as AcademicYear
	                                  ,c.ContentTime
                                      ,c.ContentName
                                      ,c.Description
                                      ,c.ContentText
                                      ,ac.Account ContentCreator
                                      ,c.ContentCreateTime
                                      ,au.Account ContentUpdater
                                      ,c.ContentUpdateTime
                                  FROM Content c
                                  INNER JOIN Admin ac ON c.ContentCreator = ac.AdminID
                                  INNER JOIN Admin au ON c.ContentUpdater = au.AdminID
                                  INNER JOIN Educated e ON c.ContentID = e.ContentID
                                WHERE c.ContentID=@ContentID", new SqlParameter("@ContentID", contentID)).Single();
					}
					else
					{
						result = db.Database.SqlQuery<EventCalendarModel>(@"
                                SELECT c.ContentID
                                      ,e.CategoryYearID as AcademicYear
	                                  ,c.ContentTime
                                      ,c.ContentName
                                      ,c.Description
                                      ,c.ContentText
                                      ,c.ContentCreateTime
                                      ,c.ContentUpdateTime
                                  FROM Content c
                                  INNER JOIN Educated e ON c.ContentID = e.ContentID
                                WHERE c.ContentID=@ContentID", new SqlParameter("@ContentID", contentID)).Single();
					}

					List<FileUploadModel> fileUploadList = new List<FileUploadModel>();
                    fileUploadList = gFileUploadManagement.GetByContentID(contentID);
                    result.PicUrlList = fileUploadList.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_IMAGE)).OrderByDescending(x => x.CoverImage).ToList();
                    result.VideoUrlList = fileUploadList.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_VIDEO)).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<EventCalendarModel> GetAll(PagenationModel model)
        {
            List<EventCalendarModel> result = new List<EventCalendarModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var eventCalendarParameters = new List<object>();
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
                    eventCalendarParameters.Add(new SqlParameter("@MIN", min));
                    eventCalendarParameters.Add(new SqlParameter("@MAX", max));
                    eventCalendarParameters.Add(new SqlParameter("@TypeID", EventCalendarModel.TYPEID));

                    StringBuilder sql = new StringBuilder();
                    string[] colOrder = { "ContentTime", "ContentName", "(cgy.CategoryName)", "ContentTime" };
                    string orderTitle = (model != null && (colOrder.Length > model.OrderNum)) ? colOrder[model.OrderNum] : colOrder[0];
                    string order = (model != null) ? (model.Order ? "ASC" : "DESC") : "ASC";

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    lstWhere.Add(" c.TypeID = @TypeID ");
                    if (model != null && !String.IsNullOrEmpty(model.KeyWord))
                    {
                        if (model.QueryField == PagenationModel.EVENTCALENDAR_SEARCH_TITLE)
                        {
                            lstWhere.Add(" c.ContentName LIKE @KeyWord ESCAPE '\\' ");
                            eventCalendarParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.EVENTCALENDAR_SEARCH_CONTENT)
                        {
                            lstWhere.Add(" c.Description LIKE @KeyWord ESCAPE '\\' ");
                            eventCalendarParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.EVENTCALENDAR_SEARCH_YEAR)
                        {
                            lstWhere.Add(" cgy.CategoryName LIKE @KeyWord ESCAPE '\\' ");
                            eventCalendarParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
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
                                        cgy.CategoryName as AcademicYear,
                                        c.ContentTime,
                                        c.ContentName
                                    FROM Content c
                                    INNER JOIN dbo.Educated e
                                    ON c.ContentID = e.ContentID
                                    LEFT JOIN dbo.Category cgy
                                    ON cgy.CategoryID = e.CategoryYearID
                                    {2}
                                    ) TEMP
                                WHERE rowNumber between @MIN and @MAX
                                ORDER BY rowNumber", orderTitle, order, condition);

                    result = db.Database.SqlQuery<EventCalendarModel>(sql.ToString(), eventCalendarParameters.ToArray()).ToList();

                    foreach (EventCalendarModel item in result)
                    {
                        item.PicUrlList = gFileUploadManagement.GetCoverImageByContentID(item.ContentID);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<EventCalendarModel> SearchByKeyWord(PagenationModel model)
        {
            return null;
        }

        //每年兩筆
        public List<EventCalendarModel> GetForFrontPage()
        {
            List<EventCalendarModel> result = new List<EventCalendarModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var eventCalendarParameters = new List<object>();
                    eventCalendarParameters.Add(new SqlParameter("@TypeID", EventCalendarModel.TYPEID));
                    result = db.Database.SqlQuery<EventCalendarModel>(@"
                                    SELECT ROW_NUMBER() over(order by cgy.CategoryName ASC, c.ContentTime DESC) rowNumber,
                                        c.ContentID,
                                        cgy.CategoryName as AcademicYear,
                                        c.ContentTime,
                                        c.ContentName
                                    FROM Content c
                                    INNER JOIN dbo.Educated e
                                    ON c.ContentID = e.ContentID
                                    LEFT JOIN dbo.Category cgy
                                    ON cgy.CategoryID = e.CategoryYearID
                                    WHERE c.TypeID=@TypeID", eventCalendarParameters.ToArray()).ToList();
                    foreach (EventCalendarModel item in result)
                    {
                        item.PicUrlList = gFileUploadManagement.GetCoverImageByContentID(item.ContentID);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        //該年所有資料
        public List<EventCalendarModel> GetByYear(PagenationModel model)
        {
            return null;
        }
        
    }
}
