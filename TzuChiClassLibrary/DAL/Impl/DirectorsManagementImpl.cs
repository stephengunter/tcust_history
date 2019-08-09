using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;
using TzuChiClassLibrary.Utils;

namespace TzuChiClassLibrary.DAL
{
    //創校緣起 -> 歷任董事
    public class DirectorsManagementImpl : IDirectorsManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Boolean ResetDirectorsData(List<DirectorsModel> list) 
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            DirectorsModel model;

                            if (list.Count > 0)
                            {

                                var directorsParameters = new List<object>();
                                db.Database.ExecuteSqlCommand(@"DELETE dbo.Directors;", directorsParameters.ToArray());

                                for (int i = 0; i < list.Count; i++)
                                {
                                    model = list[i];

                                    directorsParameters.Add(new SqlParameter("@SessionNumber", model.SessionNumber));
                                    directorsParameters.Add(new SqlParameter("@StartYear", model.StartYear));
                                    directorsParameters.Add(new SqlParameter("@EndYear", model.EndYear));
                                    directorsParameters.Add(new SqlParameter("@Names", model.Names));
                                    db.Database.ExecuteSqlCommand(@"INSERT INTO Directors
                                                                           (SessionNumber
                                                                           ,StartYear
                                                                           ,EndYear
                                                                           ,Names)
                                                                     VALUES
                                                                           (@SessionNumber
                                                                           ,@StartYear
                                                                           ,@EndYear
                                                                           ,@Names);", directorsParameters.ToArray());
                                    directorsParameters.Clear();
                                }
                                dbContextTransaction.Commit();
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
        public List<DirectorsModel> GetAll()
        {
            List<DirectorsModel> result = new List<DirectorsModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    result = db.Database.SqlQuery<DirectorsModel>(@"
                                SELECT d.SessionNumber
                                      ,d.StartYear
                                      ,d.EndYear
                                      ,d.Names
                                  FROM Directors d
                                  ORDER BY d.SessionNumber").ToList();

                    if (result.Count > 0) 
                    {
                        foreach (var item in result) 
                        {
                            item.NameList = item.Names.Split(',').ToList();
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
