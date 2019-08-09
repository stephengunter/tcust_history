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
    public class CoordinateManagementImpl : ICoordinateManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public bool Add(CoordinateModel model)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var coordinateParameters = new List<object>();
                            coordinateParameters.Add(new SqlParameter("@PointID", model.PointID));
                            coordinateParameters.Add(new SqlParameter("@TypeID", model.TypeID));
                            coordinateParameters.Add(new SqlParameter("@PointX", model.PointX));
                            coordinateParameters.Add(new SqlParameter("@PointY", model.PointY));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.Coordinate ( PointID, TypeID, PointX, PointY)
                                             VALUES ( @PointID, @TypeID, @PointX, @PointY);",
                                coordinateParameters.ToArray());
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

        public bool Delete(string pointID)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var coordinateParameters = new List<object>();
                            coordinateParameters.Add(new SqlParameter("@PointID", pointID));
                            db.Database.ExecuteSqlCommand(@"
                            DELETE dbo.Coordinate WHERE PointID = @PointID;",
                                coordinateParameters.ToArray());
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

        public CoordinateModel GetByPointID(string pointID)
        {
            CoordinateModel result = new CoordinateModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    result = db.Database.SqlQuery<CoordinateModel>(@"
                                SELECT c.PointID
								      ,c.TypeID
                                      ,c.PointX
                                      ,c.PointY
                                  FROM Coordinate c
                                WHERE c.PointID=@PointID", new SqlParameter("@PointID", pointID)).Single();

                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<CoordinateModel> GetAll(string typeID)
        {
            List<CoordinateModel> result = new List<CoordinateModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    string sql = string.Empty;
                    if(PlanInsideModel.TYPEID.Equals(typeID))
                    {
                        sql = @"SELECT c.PointID
								      ,c.TypeID
                                      ,c.PointX
                                      ,c.PointY
                                      ,cg.CategoryName ContentName
                                FROM Coordinate c
                                INNER JOIN dbo.PlanInside p
                                ON p.ContentID = c.PointID
                                INNER JOIN dbo.Category cg
                                ON p.CategorySiteID = cg.CategoryID
                                WHERE c.TypeID=@TypeID";
                    }
                    else if (PlanOutsideModel.TYPEID.Equals(typeID))
                    {
                        sql = @"SELECT c.PointID
								      ,c.TypeID
                                      ,c.PointX
                                      ,c.PointY
                                      ,cc.ContentName ContentName
                                  FROM Coordinate c
                                INNER JOIN dbo.Content cc
                                ON cc.ContentID = c.PointID
                                WHERE c.TypeID=@TypeID";
                    }
                    
                    result = db.Database.SqlQuery<CoordinateModel>(sql, new SqlParameter("@TypeID", typeID)).ToList();
                    if (result != null && result.Count > 0)
                    {
                        foreach(CoordinateModel item in result)
                        {
                            item.ContentName = string.Format("{0} ({1},{2})", item.ContentName, item.PointX, item.PointY);
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
    }
}
