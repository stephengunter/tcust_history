using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using NLog;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    public class AdminManagementImpl : IAdminManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const string sKey = "A2c4J9m3";
        public Boolean Add(AdminModel model)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            model.AdminID = Guid.NewGuid().ToString();
                            var adminParameters = new List<object>();
                            adminParameters.Add(new SqlParameter("@AdminID", model.AdminID));
                            adminParameters.Add(new SqlParameter("@Account", model.Account));
                            adminParameters.Add(new SqlParameter("@Password", model.Password));
                            adminParameters.Add(new SqlParameter("@Name", model.Name ?? string.Empty));
                            adminParameters.Add(new SqlParameter("@Tel", model.Tel ?? string.Empty));
                            adminParameters.Add(new SqlParameter("@Email", model.Email ?? string.Empty));
                            adminParameters.Add(new SqlParameter("@Mobile", model.Mobile ?? string.Empty));
                            adminParameters.Add(new SqlParameter("@LastLogonIP", model.LastLogonIP ?? string.Empty));
                            adminParameters.Add(new SqlParameter("@Enable", model.Enable));
                            db.Database.ExecuteSqlCommand(@"
                            INSERT INTO dbo.Admin ( AdminID, Account, Password, Name, Tel, Email, Mobile, LastLogonIP, Enable)
                                   VALUES ( @AdminID, @Account, @Password, @Name, @Tel, @Email, @Mobile, @LastLogonIP, @Enable);",
                                adminParameters.ToArray());
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

        public Boolean Update(AdminModel model)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var adminParameters = new List<object>();
                            adminParameters.Add(new SqlParameter("@AdminID", model.AdminID));
                            adminParameters.Add(new SqlParameter("@Password", model.Password));
                            adminParameters.Add(new SqlParameter("@Name", model.Name ?? string.Empty));
                            adminParameters.Add(new SqlParameter("@Tel", model.Tel ?? string.Empty));
                            adminParameters.Add(new SqlParameter("@Email", model.Email ?? string.Empty));
                            adminParameters.Add(new SqlParameter("@Mobile", model.Mobile ?? string.Empty));
                            adminParameters.Add(new SqlParameter("@Enable", model.Enable));
                            db.Database.ExecuteSqlCommand(@"
                            UPDATE dbo.Admin SET Password=@Password, Name=@Name, Tel=@Tel, 
                                                   Email=@Email, Mobile=@Mobile, Enable=@Enable
                                               WHERE AdminID=@AdminID;",
                                adminParameters.ToArray());
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

        public Boolean Delete(string adminID)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            //確保主要管理者不被刪除
                            if (!adminID.Equals("3319af4a-c676-429c-9775-8baaa974cb2f"))
                            {
                                var adminParameters = new List<object>();
                                adminParameters.Add(new SqlParameter("@AdminID", adminID));
                                db.Database.ExecuteSqlCommand(@"
                                DELETE FROM dbo.Admin WHERE AdminID=@AdminID;",
                                    adminParameters.ToArray());
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

        public AdminModel GetByAdminID(string adminID)
        {
            AdminModel result = new AdminModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    result = db.Database.SqlQuery<AdminModel>(@"
                                SELECT AdminID
                                      ,Account
                                      ,Password
                                      ,Name
                                      ,Tel
                                      ,Email
                                      ,Mobile
                                      ,LastLogonIP
                                      ,LastLogonTime
                                      ,Enable
                                  FROM Admin
                                WHERE AdminID=@AdminID", new SqlParameter("@AdminID", adminID)).Single();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }

        public List<AdminModel> GetAll(PagenationModel model)
        {
            List<AdminModel> result = new List<AdminModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var adminParameters = new List<object>();
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
                    adminParameters.Add(new SqlParameter("@MIN", min));
                    adminParameters.Add(new SqlParameter("@MAX", max));

                    StringBuilder sql = new StringBuilder();
                    string[] colOrder = { "Account", "Account", "Name", "Email", "Enable" };
                    string orderTitle = (model != null && (colOrder.Length > model.OrderNum)) ? colOrder[model.OrderNum] : colOrder[0];
                    string order = (model != null) ? (model.Order ? "DESC" : "ASC") : "DESC";
                    

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    if (!string.IsNullOrEmpty(model.KeyWord)) 
                    {
                        // 找帳號
                        lstWhere.Add(" Account LIKE @KeyWord ESCAPE '\\' ");
                        // 找名字
                        lstWhere.Add(" Name LIKE @KeyWord ESCAPE '\\' ");
                        adminParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                    }

                    if (lstWhere.Count > 0)
                        condition = string.Format(" WHERE {0} ", string.Join(" OR ", lstWhere.ToArray()));

                    sql.AppendFormat(@"
                                SELECT *
                                FROM
                                (
                                    SELECT
                                        ROW_NUMBER() over(order by {0} {1}) rowNumber, COUNT(*) over() totalNum,
                                        AdminID, Account, Password, Name, Tel, Email, Mobile, LastLogonIP, LastLogonTime, Enable
                                    FROM Admin {2}
                                    ) TEMP
                                WHERE rowNumber between @MIN and @MAX
                                ORDER BY rowNumber", orderTitle, order, condition);
                    result = db.Database.SqlQuery<AdminModel>(sql.ToString(), adminParameters.ToArray()).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }
        public List<AdminModel> GetAdmin()
        {
            List<AdminModel> adminList = new List<AdminModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    adminList = db.Database.SqlQuery<AdminModel>(@"
                                                    SELECT
	                                                    [AdminID]
                                                       ,[Account]
                                                       ,[Password]
                                                       ,[Name]
                                                       ,[Tel]
                                                       ,[Email]
                                                       ,[Mobile]
                                                       ,[LastLogonIP]
                                                       ,[LastLogonTime]
                                                       ,[Enable]
                                                    FROM dbo.Admin").ToList();
                }
            }
            catch (Exception e)
            {
            }
            return adminList;
        } // GetAdmin()
        public string DesEncrypt(string pToEncrypt)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte數組中  
            //原來使用的UTF8編碼，我改成Unicode編碼了，不行  
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            //byte[]  inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);  

            //建立加密對象的密鑰和偏移量  
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法  
            //使得輸入密碼必須輸入英文文本  
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //Write  the  byte  array  into  the  crypto  stream  
            //(It  will  end  up  in  the  memory  stream)  
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //Get  the  data  back  from  the  memory  stream,  and  into  a  string  
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                //Format  as  hex  
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        } //加密
        public string DesDecrypt(string pToDecrypt)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //Put  the  input  string  into  the  byte  array  
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            //建立加密對象的密鑰和偏移量，此值重要，不能修改  
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush  the  data  through  the  crypto  stream  into  the  memory  stream  
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //Get  the  decrypted  data  back  from  the  memory  stream  
            //建立StringBuild對象，CreateDecrypt使用的是流對象，必須把解密後的文本變成流對像  
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());  
        } //解密
        
    }
}
