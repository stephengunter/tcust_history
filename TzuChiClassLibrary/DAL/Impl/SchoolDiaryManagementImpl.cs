using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;
using System.Text.RegularExpressions;

namespace TzuChiClassLibrary.DAL
{
    //資訊查詢 -> 校園日誌
    public class SchoolDiaryManagementImpl : ISchoolDiaryManagement
    {
        IFileUploadManagement gFileUploadManagement = new FileUploadManagementImpl();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public Boolean Add(SchoolDiaryModel model)
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
                            var schoolDiaryParameters = new List<object>();
                            schoolDiaryParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            schoolDiaryParameters.Add(new SqlParameter("@SerialNo", model.SerialNo));
                            schoolDiaryParameters.Add(new SqlParameter("@TypeID", SchoolDiaryModel.TYPEID));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentName", model.ContentName ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentText", model.ContentText ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@Author", model.Author ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentTime", model.ContentTime));
                            schoolDiaryParameters.Add(new SqlParameter("@RelatedLink", model.RelatedLink ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@ShowDate", model.ShowDate));
                            schoolDiaryParameters.Add(new SqlParameter("@OpenTime", model.OpenTime ?? (object)DBNull.Value));
                            schoolDiaryParameters.Add(new SqlParameter("@CloseTime", model.CloseTime ?? (object)DBNull.Value));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentCreator", model.ContentCreator ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentCreateTime", now));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            db.Database.ExecuteSqlCommand(@"INSERT INTO dbo.Content ( ContentID, TypeID, SerialNo, 
                                                                     ContentTime, ContentName, Description, 
                                                                     ContentText , Author ,
                                                                     RelatedLink, IsPublish, ShowDate, OpenTime, CloseTime,
                                                                     ContentCreator, ContentCreateTime,
                                                                     ContentUpdater, ContentUpdateTime)
                                                            VALUES ( @ContentID, @TypeID, @SerialNo, 
                                                                     @ContentTime, @ContentName, @Description, 
                                                                     @ContentText , @Author ,

                                                                     @RelatedLink, 'true', @ShowDate, @OpenTime, @CloseTime,
                                                                     @ContentCreator, @ContentCreateTime,
                                                                     @ContentUpdater, @ContentUpdateTime);"
                                , schoolDiaryParameters.ToArray());
                            
                            //學年學期
                            schoolDiaryParameters.Clear();
                            schoolDiaryParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            schoolDiaryParameters.Add(new SqlParameter("@CategoryYearID", model.AcademicYear));
                            schoolDiaryParameters.Add(new SqlParameter("@CategoryTermID", model.Semester));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.Educated ( ContentID, CategoryYearID, CategoryTermID)
                                   VALUES ( @ContentID, @CategoryYearID, @CategoryTermID);",
                                schoolDiaryParameters.ToArray());

                            //類別
                            foreach(string item in model.ContentType)
                            {
                                schoolDiaryParameters.Clear();
                                schoolDiaryParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                                schoolDiaryParameters.Add(new SqlParameter("@ContentType", item));
                                db.Database.ExecuteSqlCommand(@"
                                INSERT INTO dbo.ContentMultipleType ( ContentID, ContentType)
                                       VALUES ( @ContentID, @ContentType);",
                                    schoolDiaryParameters.ToArray());
                            }

                            dbContextTransaction.Commit();

                            //封面圖 內容圖 相關圖檔影檔
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
                    throw e;
                    logger.Debug("(Debug)除錯" + e.Message);
                    return false;
                }
                return true;
            }
        }

        public Boolean Update(SchoolDiaryModel model)
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
                            var schoolDiaryParameters = new List<object>();
                            schoolDiaryParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            schoolDiaryParameters.Add(new SqlParameter("@SerialNo", model.SerialNo));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentName", model.ContentName ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@Description", model.Description ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentText", model.ContentText ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@Author", model.Author ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentTime", model.ContentTime));
                            schoolDiaryParameters.Add(new SqlParameter("@RelatedLink", model.RelatedLink ?? string.Empty));
                            schoolDiaryParameters.Add(new SqlParameter("@ShowDate", model.ShowDate));
                            schoolDiaryParameters.Add(new SqlParameter("@OpenTime", model.OpenTime ?? (object)DBNull.Value));
                            schoolDiaryParameters.Add(new SqlParameter("@CloseTime", model.CloseTime ?? (object)DBNull.Value));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentUpdater", model.ContentUpdater));
                            schoolDiaryParameters.Add(new SqlParameter("@ContentUpdateTime", now));
                            schoolDiaryParameters.Add(new SqlParameter("@TypeID", SchoolDiaryModel.TYPEID));
                            db.Database.ExecuteSqlCommand(@" 
                              UPDATE dbo.Content SET       
                              SerialNo = @SerialNo,                        
                              ContentName = @ContentName,
                              Description = @Description,
                              ContentText = @ContentText,
                              Author = @Author,
                              ContentTime = @ContentTime,
                              RelatedLink = @RelatedLink,
                              ShowDate = @ShowDate,
                              OpenTime = @OpenTime,
                              CloseTime = @CloseTime,
                              ContentUpdater = @ContentUpdater,
                              ContentUpdateTime = @ContentUpdateTime
                              WHERE ContentID = @ContentID
                              AND TypeID = @TypeID",
                                schoolDiaryParameters.ToArray());

                            //學年學期
                            schoolDiaryParameters.Clear();
                            schoolDiaryParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                            schoolDiaryParameters.Add(new SqlParameter("@CategoryYearID", model.AcademicYear));
                            schoolDiaryParameters.Add(new SqlParameter("@CategoryTermID", model.Semester));
                            db.Database.ExecuteSqlCommand(@"
                              UPDATE dbo.Educated SET 
                              CategoryYearID = @CategoryYearID,
                              CategoryTermID = @CategoryTermID
                              WHERE ContentID = @ContentID",
                                schoolDiaryParameters.ToArray());

                            //類別
                            db.Database.ExecuteSqlCommand(@"
                              DELETE dbo.ContentMultipleType WHERE ContentID = @ContentID;",
                                new SqlParameter("@ContentID", model.ContentID));
                            foreach (string item in model.ContentType)
                            {
                                schoolDiaryParameters.Clear();
                                schoolDiaryParameters.Add(new SqlParameter("@ContentID", model.ContentID));
                                schoolDiaryParameters.Add(new SqlParameter("@ContentType", item));
                                db.Database.ExecuteSqlCommand(@"
                                INSERT INTO dbo.ContentMultipleType ( ContentID, ContentType)
                                       VALUES ( @ContentID, @ContentType);",
                                    schoolDiaryParameters.ToArray());
                            }

                            dbContextTransaction.Commit();

                            //封面圖 內容圖 相關圖檔影檔
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
                            var schoolDiaryParameters = new List<object>();
                            schoolDiaryParameters.Add(new SqlParameter("@ContentID", contentID));
                            db.Database.ExecuteSqlCommand(@"
                            DELETE dbo.FileUplaod WHERE ContentID = @ContentID;
                            DELETE dbo.Educated WHERE ContentID = @ContentID;
                            DELETE dbo.Content WHERE ContentID = @ContentID;
                            DELETE dbo.ContentMultipleType WHERE ContentID = @ContentID;
                            ", schoolDiaryParameters.ToArray());
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

        public SchoolDiaryModel GetByContentID(string contentID) {
            SchoolDiaryModel result = new SchoolDiaryModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var schoolDiaryParameters = new List<object>();
                    schoolDiaryParameters.Add(new SqlParameter("@ContentID", contentID));

                    result = db.Database.SqlQuery<SchoolDiaryModel>(@"
                                SELECT
                                    c.ContentID,
                                    c.SerialNo,
                                    e.CategoryYearID as AcademicYear,
                                    e.CategoryTermID as Semester,
                                    c.ContentName,
                                    c.ContentTime,
                                    c.Description,
                                    c.ContentText,
                                    c.Author,
                                    c.RelatedLink,
                                    c.ShowDate,
                                    c.OpenTime,
                                    c.CloseTime,
	                                ac.Account as ContentCreator,
                                    c.ContentCreateTime,
                                    au.Account as ContentUpdater,
                                    c.ContentUpdateTime
                                FROM Content c
                                INNER JOIN dbo.Educated e
                                ON c.ContentID = e.ContentID
                                LEFT JOIN dbo.Admin ac
                                ON ac.AdminID = c.ContentCreator
                                LEFT JOIN dbo.Admin au
                                ON au.AdminID = c.ContentUpdater
                                WHERE c.ContentID = @ContentID", schoolDiaryParameters.ToArray()).SingleOrDefault();

                    result.ContentType = db.Database.SqlQuery<string>(@"
                                SELECT c.ContentType FROM dbo.ContentMultipleType c WHERE c.ContentID = @ContentID
                                ", new SqlParameter("@ContentID", contentID)).ToList();

                    List<FileUploadModel> fileUploadModels = gFileUploadManagement.GetByContentID(contentID);
                    result.CoverImage = fileUploadModels.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_COVERIMAGE)).SingleOrDefault();
                    result.ContentImage = fileUploadModels.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_CONTNETIMAGE)).SingleOrDefault();
                    result.PicUrlList = fileUploadModels.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_IMAGE)).ToList();
                    result.VideoUrlList = fileUploadModels.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_VIDEO)).ToList();

                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }


        public SchoolDiaryModel GetFiles(string contentID)
        {
            SchoolDiaryModel model = new SchoolDiaryModel()
            {
                 ContentID= contentID
            };
            List<FileUploadModel> fileUploadModels = gFileUploadManagement.GetByContentID(contentID);
            model.CoverImage = fileUploadModels.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_COVERIMAGE)).SingleOrDefault();
            model.ContentImage = fileUploadModels.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_CONTNETIMAGE)).SingleOrDefault();
            model.PicUrlList = fileUploadModels.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_IMAGE)).ToList();
            model.VideoUrlList = fileUploadModels.Where(item => item.FunctionOID.Equals(FileUploadModel.FUNCTIONOID_VIDEO)).ToList();

            return model;
        }

        public List<SchoolDiaryModel> GetAll(PagenationModel model) {
            List<SchoolDiaryModel> result = new List<SchoolDiaryModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var schoolDiaryParameters = new List<object>();
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
                    schoolDiaryParameters.Add(new SqlParameter("@MIN", min));
                    schoolDiaryParameters.Add(new SqlParameter("@MAX", max));
                    schoolDiaryParameters.Add(new SqlParameter("@TypeID", SchoolDiaryModel.TYPEID));

                    StringBuilder sql = new StringBuilder();
                    string[] colOrder = { "SerialNo", "SerialNo", "ContentName", "(cgy.CategoryName)", "Semester", "ContentTime" };
                    string orderTitle = (model != null && (colOrder.Length > model.OrderNum)) ? colOrder[model.OrderNum] : colOrder[0];
                    string order = (model != null) ? (model.Order ? "ASC" : "DESC") : "ASC";

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    lstWhere.Add(" c.TypeID = @TypeID ");
                    if (model != null && !String.IsNullOrEmpty(model.KeyWord))
                    {
                        if (model.QueryField == PagenationModel.SCHOOLDIARY_SEARCH_SERIALNO)
                        {
                            lstWhere.Add(" c.SerialNo LIKE @KeyWord ESCAPE '\\' ");
                            schoolDiaryParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.SCHOOLDIARY_SEARCH_TITLE)
                        {
                            lstWhere.Add(" c.ContentName LIKE @KeyWord ESCAPE '\\' ");
                            schoolDiaryParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.SCHOOLDIARY_SEARCH_CONTENT)
                        {
                            lstWhere.Add(" c.Description LIKE @KeyWord ESCAPE '\\' ");
                            schoolDiaryParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (model.QueryField == PagenationModel.SCHOOLDIARY_SEARCH_YEAR)
                        {
                            lstWhere.Add(" cgy.CategoryName LIKE @KeyWord ESCAPE '\\' ");
                            schoolDiaryParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                    }

                    if (lstWhere.Count > 0)
                        condition = string.Format(" AND {0} ", string.Join(" AND ", lstWhere.ToArray()));

                    string multipleType = string.Empty;
                    if(model.MultipleType != null){
                        multipleType = string.Format("AND cm.ContentType = @MultipleType");
                        schoolDiaryParameters.Add(new SqlParameter("@MultipleType", model.MultipleType));
                    }

                    sql.AppendFormat(@"
                                SELECT *
                                FROM
                                (
                                    SELECT
                                        ROW_NUMBER() over(order by {0} {1}) rowNumber, COUNT(*) over() totalNum,
                                        c.ContentID,
                                        c.SerialNo,
                                        cgy.CategoryName as AcademicYear,
                                        cgt.CategoryName as Semester,
                                        c.ContentName,
                                        c.Author,
                                        c.ContentTime,
                                        c.RelatedLink
                                    FROM Content c
                                    INNER JOIN dbo.Educated e
                                    ON c.ContentID = e.ContentID
                                    LEFT JOIN dbo.Category cgy
                                    ON cgy.CategoryID = e.CategoryYearID
                                    LEFT JOIN dbo.Category cgt
                                    ON cgt.CategoryID = e.CategoryTermID
                                    WHERE c.ContentID IN (
                                        SELECT c.ContentID
                                        FROM dbo.Content c
                                        INNER JOIN dbo.ContentMultipleType cm
                                        ON cm.ContentID = c.ContentID
                                        WHERE c.TypeID = @TypeID
                                        {3}
                                        GROUP BY c.ContentID
                                    )
                                    {2}
                                    ) TEMP
                                WHERE rowNumber between @MIN and @MAX
                                ORDER BY rowNumber", orderTitle, order, condition, multipleType);

                    result = db.Database.SqlQuery<SchoolDiaryModel>(sql.ToString(), schoolDiaryParameters.ToArray()).ToList();

                    foreach (SchoolDiaryModel item in result)
                    {
                        item.ContentType = db.Database.SqlQuery<string>(@"
                                SELECT ct.TypeName ContentType
                                FROM dbo.ContentMultipleType cm
                                INNER JOIN dbo.ContentType ct
                                ON cm.ContentType = ct.ContentType
                                WHERE cm.ContentID = @ContentID   
                                ", new SqlParameter("@ContentID", item.ContentID)).ToList();
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

        public Boolean checkSerialNo(string serialNo)
        {
            List<string> result = new List<string>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var schoolDiaryParameters = new List<object>();
                    schoolDiaryParameters.Add(new SqlParameter("@SerialNo", serialNo));

                    result = db.Database.SqlQuery<string>(@"
                                SELECT
                                    c.ContentID
                                FROM Content c
                                WHERE c.SerialNo = @SerialNo", schoolDiaryParameters.ToArray()).ToList();

                    if (result.Count == 0)
                    {
                        return true;
                    }

                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }

            return false;
        }

        public List<SchoolDiaryModel> GetAllForFrontPage(PagenationModel model) {
            List<SchoolDiaryModel> result = new List<SchoolDiaryModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var schoolDiaryParameters = new List<object>();
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
                    schoolDiaryParameters.Add(new SqlParameter("@MIN", min));
                    schoolDiaryParameters.Add(new SqlParameter("@MAX", max));
                    schoolDiaryParameters.Add(new SqlParameter("@TypeID", SchoolDiaryModel.TYPEID));

                    StringBuilder sql = new StringBuilder();                    

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    lstWhere.Add(@" c.ContentID IN(SELECT cmt.ContentID FROM ContentMultipleType cmt
													WHERE cmt.ContentType = @TypeID) ");
                    if (model != null)
                    {
                        if (!string.IsNullOrEmpty(model.Year))                   //學年
                        {
                            lstWhere.Add(" cgy.CategoryName LIKE @AcademicYear ESCAPE '\\' ");
                            schoolDiaryParameters.Add(new SqlParameter("@AcademicYear", "%" + Regex.Replace(model.Year, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        if (!string.IsNullOrEmpty(model.Term))                  //學期
                        {
                            lstWhere.Add(" cgt.CategoryName LIKE @Semester ESCAPE '\\' ");
                            schoolDiaryParameters.Add(new SqlParameter("@Semester", "%" + Regex.Replace(model.Term, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        if (!string.IsNullOrEmpty(model.KeyWord))              //關鍵字
                        {
                            lstWhere.Add(" ( c.ContentName LIKE @KeyWord ESCAPE '\\' OR  c.ContentTime LIKE @KeyWord ESCAPE '\\' OR c.Description LIKE @KeyWord ESCAPE '\\' ) ");
                            schoolDiaryParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                    }


                    if (lstWhere.Count > 0)
                        condition = string.Format(" WHERE {0} ", string.Join(" AND ", lstWhere.ToArray()));

                    sql.AppendFormat(@"
                                SELECT *
                                FROM
                                (
                                    SELECT
                                        ROW_NUMBER() over(order by c.ContentTime DESC, c.SerialNo) rowNumber, COUNT(*) over() totalNum,
                                        c.ContentID,
                                        c.SerialNo,
                                        cgy.CategoryName as AcademicYear,
                                        cgt.CategoryName as Semester,
                                        c.ContentName,
                                        c.ContentText,
                                        c.Author,
                                        c.Description,
                                        c.ContentTime,
                                        c.RelatedLink
                                    FROM Content c
                                    INNER JOIN dbo.Educated e
                                    ON c.ContentID = e.ContentID
                                    LEFT JOIN dbo.Category cgy
                                    ON cgy.CategoryID = e.CategoryYearID
                                    LEFT JOIN dbo.Category cgt
                                    ON cgt.CategoryID = e.CategoryTermID
                                    {0}
                                    ) TEMP
                                WHERE rowNumber between @MIN and @MAX
                                ORDER BY rowNumber", condition);

                    result = db.Database.SqlQuery<SchoolDiaryModel>(sql.ToString(), schoolDiaryParameters.ToArray()).ToList();

                    foreach (SchoolDiaryModel item in result)
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

    }
}
