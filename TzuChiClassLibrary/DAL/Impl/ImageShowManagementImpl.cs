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
    public class ImageShowManagementImpl : IImageShowManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public Boolean Add(ImageShowModel model)
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
                            model.ImageShowID = Guid.NewGuid().ToString();
                            var imageShowParameters = new List<object>();
                            imageShowParameters.Add(new SqlParameter("@ImageShowID", model.ImageShowID));
                            imageShowParameters.Add(new SqlParameter("@TypeID", model.TypeID));
                            //imageShowParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            imageShowParameters.Add(new SqlParameter("@ImageName", model.ImageName));
                            imageShowParameters.Add(new SqlParameter("@ImageUrl", model.ImageUrl));
                            imageShowParameters.Add(new SqlParameter("@ImageWidth", model.ImageWidth));
                            imageShowParameters.Add(new SqlParameter("@ImageHeight", model.ImageHeight));
                            imageShowParameters.Add(new SqlParameter("@Creator", model.Creator));
                            imageShowParameters.Add(new SqlParameter("@CreateTime", now));
                            imageShowParameters.Add(new SqlParameter("@Updater", model.Updater));
                            imageShowParameters.Add(new SqlParameter("@UpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.ImageShow ( ImageShowID, TypeID, ImageName, ImageUrl, ImageWidth, ImageHeight, Creator, CreateTime, Updater, UpdateTime)
                                   VALUES ( @ImageShowID, @TypeID, @ImageName, @ImageUrl, @ImageWidth, @ImageHeight, @Creator, @CreateTime, @Updater, @UpdateTime);",
                                imageShowParameters.ToArray());

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

        public Boolean Update(ImageShowModel model)
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
                            var imageShowParameters = new List<object>();
                            imageShowParameters.Add(new SqlParameter("@ImageShowID", model.ImageShowID));
                            imageShowParameters.Add(new SqlParameter("@TypeID", model.TypeID));
                            //imageShowParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            imageShowParameters.Add(new SqlParameter("@ImageName", model.ImageName));
                            imageShowParameters.Add(new SqlParameter("@ImageUrl", model.ImageUrl));
                            imageShowParameters.Add(new SqlParameter("@ImageWidth", model.ImageWidth));
                            imageShowParameters.Add(new SqlParameter("@ImageHeight", model.ImageHeight));
                            imageShowParameters.Add(new SqlParameter("@Updater", model.Updater));
                            imageShowParameters.Add(new SqlParameter("@UpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.ImageShow SET TypeID=@TypeID, ImageName=@ImageName, ImageUrl=@ImageUrl, ImageWidth=@ImageWidth, 
                                                   ImageHeight=@ImageHeight, Updater=@Updater, UpdateTime=@UpdateTime
                                               WHERE ImageShowID=@ImageShowID;",
                                imageShowParameters.ToArray());
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

        public Boolean Delete(string imageShowID)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var imageShowParameters = new List<object>();
                            imageShowParameters.Add(new SqlParameter("@ImageShowID", imageShowID));
                            db.Database.ExecuteSqlCommand(@"
                            DELETE FROM dbo.ImageShow WHERE ImageShowID=@ImageShowID;",
                                imageShowParameters.ToArray());
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

        public ImageShowModel GetByImageShowID(string imageShowID)
        {
            ImageShowModel result = new ImageShowModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    result = db.Database.SqlQuery<ImageShowModel>(@"
                                SELECT i.ImageShowID
                                      ,i.TypeID
	                                  ,i.ImageName
                                      ,i.ImageUrl
                                      ,i.ImageWidth
                                      ,i.ImageHeight
                                      ,ac.Account Creator
                                      ,i.CreateTime
                                      ,au.Account Updater
                                      ,i.UpdateTime
                                  FROM ImageShow i
                                  INNER JOIN Admin ac ON i.Creator = ac.AdminID
                                  INNER JOIN Admin au ON i.Updater = au.AdminID
                                WHERE ImageShowID=@ImageShowID", new SqlParameter("@ImageShowID", imageShowID)).Single();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<ImageShowModel> GetAll(PagenationModel model)
        {
            List<ImageShowModel> result = new List<ImageShowModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var imageShowParameters = new List<object>();
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
                    imageShowParameters.Add(new SqlParameter("@MIN", min));
                    imageShowParameters.Add(new SqlParameter("@MAX", max));

                    StringBuilder sql = new StringBuilder();
                    string[] colOrder = { "c.TypeName", "i.ImageName", "c.TypeName"};
                    string orderTitle = (model != null && (colOrder.Length > model.OrderNum)) ? colOrder[model.OrderNum] : colOrder[0];
                    string order = (model != null) ? (model.Order ? "ASC" : "DESC") : "ASC";

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    if (model != null && !string.IsNullOrEmpty(model.KeyWord))
                    {
                        if (model.QueryField == EventCalendarModel.TYPEID)
                        {
                            lstWhere.Add(" c.ContentType = @ContentType ");
                            imageShowParameters.Add(new SqlParameter("@ContentType", EventCalendarModel.TYPEID));
                        }
                        else if (model.QueryField == HonourListModel.TYPEID)
                        {
                            lstWhere.Add(" c.ContentType = @ContentType ");
                            imageShowParameters.Add(new SqlParameter("@ContentType", HonourListModel.TYPEID));
                        }
                        else if (model.QueryField == ImageShowModel.SERVICELEARN_TYPEID)
                        {
                            lstWhere.Add(" c.ContentType = @ContentType ");
                            imageShowParameters.Add(new SqlParameter("@ContentType", ImageShowModel.SERVICELEARN_TYPEID));
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
                                       i.ImageShowID
                                      ,c.TypeName TypeID
	                                  ,i.ImageName
                                      ,i.ImageUrl
                                      ,i.ImageWidth
                                      ,i.ImageHeight
                                    FROM ImageShow i
                                    INNER JOIN dbo.ContentType c
                                    ON c.ContentType = i.TypeID
                                    {2}
                                    ) TEMP
                                WHERE rowNumber between @MIN and @MAX
                                ORDER BY rowNumber", orderTitle, order, condition);
                    result = db.Database.SqlQuery<ImageShowModel>(sql.ToString(), imageShowParameters.ToArray()).ToList();
                    }
                }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<ImageShowModel> GetByTypeID(string typeID)
        {
            List<ImageShowModel> result = new List<ImageShowModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var imageShowParameters = new List<object>();
                    imageShowParameters.Add(new SqlParameter("@TypeName", typeID));
                      
                    result = db.Database.SqlQuery<ImageShowModel>(@"
                                SELECT i.ImageShowID
                                      ,i.TypeID
	                                  ,i.ImageName
                                      ,i.ImageUrl
                                      ,i.ImageWidth
                                      ,i.ImageHeight
                                    FROM ImageShow i
                                    WHERE i.TypeID = @TypeName", imageShowParameters.ToArray()).ToList();
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
