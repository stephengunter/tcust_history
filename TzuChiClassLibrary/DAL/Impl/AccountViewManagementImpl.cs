using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    public class AccountViewManagementImpl : IAccountViewManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AccountViewModel CheckPassWord(AccountViewModel model){
            return model;
        }
        public void LoginRecord(AccountViewModel model, string IPAddress) 
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var accountViewParameters = new List<object>();
                            accountViewParameters.Add(new SqlParameter("@Account", model.Account));
                            accountViewParameters.Add(new SqlParameter("@LastLogonIP", IPAddress));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.Admin SET LastLogonIP=@LastLogonIP, LastLogonTime=GETDATE()
                                               WHERE Account=@Account;",
                                accountViewParameters.ToArray());
                            dbContextTransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            logger.Debug("(Debug)除錯" + e.Message);
                            dbContextTransaction.Rollback();
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Debug("(Debug)除錯" + e.Message);
                }
            }
        }
    }
}
