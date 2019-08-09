using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;

namespace TzuChiClassLibrary.DAL
{
    //資訊查詢 -> 畢業紀念冊
    public class ClassBookManagementImpl : IClassBookManagement
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public Boolean ResetClassBookData(List<ClassBookModel> list)
        {
            using (TzuChiContext db = new TzuChiContext())
            {
                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            ClassBookModel model;

                            if (list.Count > 0)
                            {

                                var classBookParameters = new List<object>();
                                db.Database.ExecuteSqlCommand(@"DELETE dbo.ClassBook;", classBookParameters.ToArray());

                                for (int i = 0; i < list.Count; i++)
                                {
                                    model = list[i];

                                    classBookParameters.Add(new SqlParameter("@ClassBookID", model.ClassBookID));
                                    classBookParameters.Add(new SqlParameter("@AcademicYear", model.AcademicYear));
                                    classBookParameters.Add(new SqlParameter("@Department", model.Department));
                                    classBookParameters.Add(new SqlParameter("@Class", model.Class));
                                    classBookParameters.Add(new SqlParameter("@StudentId", model.StudentId));
                                    classBookParameters.Add(new SqlParameter("@StudentName", model.StudentName));
                                    classBookParameters.Add(new SqlParameter("@PictureUrl", model.PictureUrl));
                                    classBookParameters.Add(new SqlParameter("@Page", model.Page));
                                    classBookParameters.Add(new SqlParameter("@IsTurnLeft", model.IsTurnLeft));
                                    db.Database.ExecuteSqlCommand(@"INSERT INTO ClassBook
                                                                           (ClassBookID
                                                                           ,AcademicYear
                                                                           ,Department
                                                                           ,Class
                                                                           ,StudentId
                                                                           ,StudentName
                                                                           ,PictureUrl
                                                                           ,Page
                                                                           ,IsTurnLeft)
                                                                     VALUES
                                                                           (@ClassBookID
                                                                           ,@AcademicYear
                                                                           ,@Department
                                                                           ,@Class
                                                                           ,@StudentId
                                                                           ,@StudentName
                                                                           ,@PictureUrl
                                                                           ,@Page
                                                                           ,@IsTurnLeft);", classBookParameters.ToArray());
                                    classBookParameters.Clear();
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
                    throw e;
                    return false;
                }
                return true;
            }
        }
        public List<ClassBookModel> GetQueryList(PagenationModel model)
        {
            List<ClassBookModel> result = new List<ClassBookModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    //頁數
                    var classBookParameters = new List<object>();
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
                    classBookParameters.Add(new SqlParameter("@MIN", min));
                    classBookParameters.Add(new SqlParameter("@MAX", max));

                    StringBuilder sql = new StringBuilder();

                    string condition = string.Empty;
                    List<string> lstWhere = new List<string>();
                    lstWhere.Add(" c.StudentName <> '' ");
                    if (model != null)
                    {
                        if (!string.IsNullOrEmpty(model.StudentID))//學號
                        {
                            lstWhere.Add(" c.StudentId LIKE @StudentID ESCAPE '\\' ");
                            classBookParameters.Add(new SqlParameter("@StudentID", "%" + Regex.Replace(model.StudentID, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else if (!string.IsNullOrEmpty(model.KeyWord))  //姓名
                        {
                            lstWhere.Add(" c.StudentName LIKE @KeyWord ESCAPE '\\' ");
                            classBookParameters.Add(new SqlParameter("@KeyWord", "%" + Regex.Replace(model.KeyWord, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(model.Year))//學年度
                            {
                                lstWhere.Add(" c.AcademicYear LIKE @AcademicYear ESCAPE '\\' ");
                                classBookParameters.Add(new SqlParameter("@AcademicYear", "%" + Regex.Replace(model.Year, @"([\[\]\%\\_])", @"\$1") + "%"));
                            }

                            if (!string.IsNullOrEmpty(model.Department))//系所
                            {
                                lstWhere.Add(" c.Department LIKE @Department ESCAPE '\\' ");
                                classBookParameters.Add(new SqlParameter("@Department", "%" + Regex.Replace(model.Department, @"([\[\]\%\\_])", @"\$1") + "%"));
                            }

                            if (!string.IsNullOrEmpty(model.Class))//班級
                            {
                                lstWhere.Add(" c.Class LIKE @Class ESCAPE '\\' ");
                                classBookParameters.Add(new SqlParameter("@Class", "%" + Regex.Replace(model.Class, @"([\[\]\%\\_])", @"\$1") + "%"));
                            }
                        }
                    }

                    if (lstWhere.Count > 0)
                        condition = string.Format(" WHERE {0} ", string.Join(" AND ", lstWhere.ToArray()));

                    sql.AppendFormat(@"
                                SELECT c.ClassBookID
                                      ,c.AcademicYear
                                      ,c.Department
                                      ,c.Class
                                      ,c.StudentId
                                      ,c.StudentName
                                      ,c.PictureUrl
                                      ,c.Page
                                      ,c.IsTurnLeft
                                      ,cast(c.Page as integer) oPage
                                  FROM ClassBook c
                                  {0}
                                  ORDER BY c.AcademicYear, oPage", condition);

                    result = db.Database.SqlQuery<ClassBookModel>(sql.ToString(), classBookParameters.ToArray()).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }
        public List<ClassBookModel> GetAllByAcademicYear(string academicYear)
        {
            List<ClassBookModel> result = new List<ClassBookModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var classBookParameters = new List<object>();
                    classBookParameters.Add(new SqlParameter("@AcademicYear", "%" + Regex.Replace(academicYear, @"([\[\]\%\\_])", @"\$1") + "%"));

                    result = db.Database.SqlQuery<ClassBookModel>(@"
                                SELECT DISTINCT c.AcademicYear
                                      ,c.Department
                                      ,c.Class
                                      ,c.PictureUrl
                                      ,c.Page
                                      ,c.IsTurnLeft
                                      ,cast(c.Page as integer) oPage
                                  FROM ClassBook c
                                  WHERE c.AcademicYear LIKE @AcademicYear 
                                  ORDER BY c.AcademicYear, oPage"
                        , classBookParameters.ToArray()).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }
        public ClassBookModel GetPageByCondition(PagenationModel model)
        {
            ClassBookModel result = new ClassBookModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var classBookParameters = new List<object>();

                    StringBuilder sql = new StringBuilder();

                    string condition = string.Empty;
                    #region 調整正序倒序
                    string order = "ASC";
                    if (model.IsTurnLeft)
                    {
                        order = "DESC";
                    }
                    #endregion

                    List<string> lstWhere = new List<string>();
                    if (model != null)
                    {
                        if (!string.IsNullOrEmpty(model.Year))//學年度
                        {
                            lstWhere.Add(" c.AcademicYear LIKE @AcademicYear");
                            classBookParameters.Add(new SqlParameter("@AcademicYear", "%" + Regex.Replace(model.Year, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        if (!string.IsNullOrEmpty(model.Department))//系所
                        {
                            lstWhere.Add(" c.Department LIKE @Department");
                            classBookParameters.Add(new SqlParameter("@Department", "%" + Regex.Replace(model.Department, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                        if (!string.IsNullOrEmpty(model.Class))//班級
                        {
                            lstWhere.Add(" c.Class LIKE @Class");
                            classBookParameters.Add(new SqlParameter("@Class", "%" + Regex.Replace(model.Class, @"([\[\]\%\\_])", @"\$1") + "%"));
                        }
                    }

                    if (lstWhere.Count > 0)
                        condition = string.Format(" WHERE {0} ", string.Join(" AND ", lstWhere.ToArray()));

                    sql.AppendFormat(@"
                                SELECT TOP 1 c.ClassBookID
                                      ,c.AcademicYear
                                      ,c.Department
                                      ,c.Class
                                      ,c.StudentId
                                      ,c.StudentName
                                      ,c.PictureUrl
                                      ,c.Page
                                      ,c.IsTurnLeft
                                      ,cast(c.Page as integer) oPage
                                  FROM ClassBook c
                                  {0}
                                  ORDER BY c.AcademicYear, oPage {1}", condition, order);

                    result = db.Database.SqlQuery<ClassBookModel>(sql.ToString(), classBookParameters.ToArray()).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }
        public ClassBookModel GetPageByClassBookID(string classBookID)
        {
            ClassBookModel result = new ClassBookModel();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var classBookParameters = new List<object>();
                    classBookParameters.Add(new SqlParameter("@ClassBookID", classBookID));

                    result = db.Database.SqlQuery<ClassBookModel>(@"
                                SELECT c.ClassBookID
                                      ,c.AcademicYear
                                      ,c.Department
                                      ,c.Class
                                      ,c.StudentId
                                      ,c.StudentName
                                      ,c.PictureUrl
                                      ,c.Page
                                      ,c.IsTurnLeft
                                  FROM ClassBook c
                                  WHERE c.ClassBookID = @ClassBookID", classBookParameters.ToArray()).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }
        public List<ClassNumberModel> GetClassNumberByAcademicYear(string academicYear)
        {
            List<ClassNumberModel> result = new List<ClassNumberModel>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var classBookParameters = new List<object>();
                    classBookParameters.Add(new SqlParameter("@AcademicYear", "%" + Regex.Replace(academicYear, @"([\[\]\%\\_])", @"\$1") + "%"));

                    List<ClassNumberModel> temp = db.Database.SqlQuery<ClassNumberModel>(@"
                                         SELECT c.Department,c.Class 
                                             FROM ClassBook c WHERE c.AcademicYear LIKE @AcademicYear AND c.Class <> '' ORDER BY CAST(c.Page AS INTEGER)"
                               , classBookParameters.ToArray()).ToList();
                    //                                  SELECT c.Department,c.Class
                    //                                  FROM ClassBook c
                    //                                  WHERE c.AcademicYear LIKE @AcademicYear AND c.Class <> ''                                  
                    //                                  GROUP BY c.Department,c.Class
                    //                                  ORDER BY c.Department,CHARINDEX(SUBSTRING(c.Class,1,1),'甲乙丙丁戊己庚辛壬癸子丑寅卯辰已午未申酉戌亥') 
                    if (temp.Count > 0)
                    {
                        #region 處理 temp -> result
                        ClassNumberModel tempModel = new ClassNumberModel();
                        List<string> name = new List<string>();
                        string tempDepartment = string.Empty;
                        string tempClass = string.Empty;
                        for (int i = 0; i < temp.Count; i++)
                        {
                            if (tempDepartment.Equals(temp[i].Department) && i != temp.Count - 1)
                            {
                                if (!temp[i].Class.Equals(tempClass))
                                {
                                    tempClass = temp[i].Class;
                                    name.Add(temp[i].Class);
                                }
                            }
                            else if (i != temp.Count - 1)
                            {
                                if (!string.IsNullOrEmpty(tempDepartment))
                                {
                                    tempModel.ClassList = name;
                                    result.Add(tempModel);

                                    tempModel = new ClassNumberModel();
                                    name = new List<string>();
                                    tempDepartment = temp[i].Department;
                                    tempClass = temp[i].Class;
                                    tempModel.Department = temp[i].Department;
                                    name.Add(temp[i].Class);
                                }
                                else
                                {
                                    tempDepartment = temp[i].Department;
                                    tempClass = temp[i].Class;
                                    tempModel.Department = temp[i].Department;
                                    name.Add(temp[i].Class);
                                }
                            }
                            else if (i == temp.Count - 1)
                            {
                                if (!temp[i].Class.Equals(tempClass))
                                {
                                    tempClass = temp[i].Class;
                                    name.Add(temp[i].Class);
                                }
                                tempModel.ClassList = name;
                                result.Add(tempModel);
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }
        public List<string> GetAllAcademicYearName()
        {
            List<string> result = new List<string>();
            string sql = @" SELECT DISTINCT LEN( c.AcademicYear) len, c.AcademicYear 
                            FROM ClassBook c 
                            WHERE c.AcademicYear <> ''
                            ORDER BY LEN( c.AcademicYear) DESC, c.AcademicYear DESC ";

            SqlConnection SqlSvrCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            try
            {
                SqlCommand SqlCmd = new SqlCommand(sql, SqlSvrCon);
                SqlSvrCon.Open();
                SqlDataReader data = SqlCmd.ExecuteReader();
                while (data.Read())
                {
                    if (!string.Empty.Equals(data["AcademicYear"].ToString()))
                        result.Add(data["AcademicYear"].ToString());
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            finally
            {
                SqlSvrCon.Close();
            }
            return result;
        }
        public List<string> GetAllDepartmentName()
        {
            List<string> result = new List<string>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var classBookParameters = new List<object>();
                    result = db.Database.SqlQuery<string>(@"
                                SELECT DISTINCT c.Department 
                                FROM ClassBook c 
                                WHERE c.Department <> ''
                                ORDER BY c.Department ASC"
                        , classBookParameters.ToArray()).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Debug("(Debug)除錯" + e.Message);
            }
            return result;
        }
        public List<string> GetAllClassName()
        {
            List<string> result = new List<string>();
            try
            {
                using (TzuChiContext db = new TzuChiContext())
                {
                    var classBookParameters = new List<object>();
                    result = db.Database.SqlQuery<string>(@"
                                  SELECT co.Class FROM(
                                        SELECT DISTINCT c.Class ,CHARINDEX(SUBSTRING(c.Class,1,1),'甲乙丙丁戊己庚辛壬癸子丑寅卯辰已午未申酉戌亥') ClassOrder
                                        FROM ClassBook c 
                                        WHERE c.Class <> ''
                                  ) co ORDER BY co.ClassOrder ASC "
                        , classBookParameters.ToArray()).ToList();
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
