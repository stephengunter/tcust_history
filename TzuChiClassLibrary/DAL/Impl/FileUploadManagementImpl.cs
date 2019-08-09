using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    public class FileUploadManagementImpl : IFileUploadManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Boolean Add(string contentID, FileUploadModel model)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var fileUploadParameters = new List<object>();
                            fileUploadParameters.Add(new SqlParameter("@ItemOID", model.ItemOID));
                            fileUploadParameters.Add(new SqlParameter("@ContentID", contentID));
                            fileUploadParameters.Add(new SqlParameter("@FunctionOID", model.FunctionOID));
                            fileUploadParameters.Add(new SqlParameter("@FunctionType", model.FunctionType ?? string.Empty));
                            fileUploadParameters.Add(new SqlParameter("@ItemName", model.ItemName ?? string.Empty));
                            fileUploadParameters.Add(new SqlParameter("@ItemInfo", model.ItemInfo ?? string.Empty));
                            fileUploadParameters.Add(new SqlParameter("@FileName", model.FileName ?? string.Empty));
                            fileUploadParameters.Add(new SqlParameter("@Path", model.Path));
                            fileUploadParameters.Add(new SqlParameter("@Bit", model.Bit ?? string.Empty));
                            fileUploadParameters.Add(new SqlParameter("@Sort", model.Sort));
                            fileUploadParameters.Add(new SqlParameter("@ImageWidth", model.ImageWidth));
                            fileUploadParameters.Add(new SqlParameter("@ImageHeight", model.ImageHeight));
                            fileUploadParameters.Add(new SqlParameter("@Creator", model.Creator ?? string.Empty));
                            fileUploadParameters.Add(new SqlParameter("@CoverImage", model.CoverImage));
                            fileUploadParameters.Add(new SqlParameter("@PreviewPath", model.PreviewPath ?? string.Empty));
                            db.Database.ExecuteSqlCommand(@" 
                           INSERT INTO dbo.FileUplaod 
                           (ItemOID,ContentID,FunctionOID,FunctionType,ItemName,ItemInfo,FileName,
                            Path,Bit,Sort,ImageWidth,ImageHeight,Creator,CoverImage,PreviewPath)
                            VALUES
                           (@ItemOID,@ContentID,@FunctionOID,@FunctionType,@ItemName,@ItemInfo,@FileName,
                            @Path,@Bit,@Sort,@ImageWidth,@ImageHeight,@Creator,@CoverImage,@PreviewPath)",
                            fileUploadParameters.ToArray());
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

        public Boolean DeleteByContentID(string contentID)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.Database.ExecuteSqlCommand(@"DELETE dbo.FileUplaod WHERE ContentID = @ContentID",
                            new SqlParameter("@ContentID", contentID));
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

        public List<FileUploadModel> GetByContentID(string contentID)
        {
            List<FileUploadModel> result = null;
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var fileUploadParameters = new List<object>();
                    fileUploadParameters.Add(new SqlParameter("@ContentID", contentID));
                    result = db.Database.SqlQuery<FileUploadModel>(@"
                                SELECT ItemOID
                                    ,ContentID
                                    ,FunctionOID
                                    ,FunctionType
                                    ,ItemName
                                    ,ItemInfo
                                    ,FileName
                                    ,Path
                                    ,Bit
                                    ,ImageWidth
                                    ,ImageHeight
                                    ,Creator
                                    ,CreateTime
                                    ,ClickCount
                                    ,CoverImage
                                    ,PreviewPath
                                FROM dbo.FileUplaod
                                WHERE ContentID = @ContentID
                                ORDER BY CoverImage, FileName", fileUploadParameters.ToArray()).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<FileUploadModel> GetCoverImageByContentID(string contentID)
        {
            List<FileUploadModel> result = null;
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    result = db.Database.SqlQuery<FileUploadModel>(@"
                                SELECT ItemOID
                                    ,ContentID
                                    ,FunctionOID
                                    ,FunctionType
                                    ,ItemName
                                    ,ItemInfo
                                    ,FileName
                                    ,Path
                                    ,Bit
                                    ,ImageWidth
                                    ,ImageHeight
                                    ,Creator
                                    ,CreateTime
                                    ,ClickCount
                                    ,CoverImage
                                    ,PreviewPath
                                FROM dbo.FileUplaod
                                WHERE ContentID = @ContentID
                                AND FunctionOID = 'Image'
                                AND CoverImage = 1", new SqlParameter("@ContentID", contentID)).ToList();
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
