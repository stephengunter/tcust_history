using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Xml;
using System.IO;
using TzuChiClassLibrary.BO;
using System.Data;
using Microsoft.Win32;
using TzuChiClassLibrary.DAL;
using NLog;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace TzuChiClassLibrary.Utils
{
    public class ExcelCacheProcessing
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public const string ClassBook = "畢業紀念冊";
        public const string ClassBookFormat = "畢冊格式";
        public const string Directors = "歷屆董事";
        public const string OriginEternal = "緣起不滅";

        public const string TurnLeft = "左";
        public const string TurnRight = "右";

        private static string FilePath = HttpContext.Current.Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["ExcelSubPath"]);
        private static HSSFWorkbook workbook;
        private static ISheet sheet;
        private static CacheDependency dep = new CacheDependency(FilePath);
        private static CacheDependency depOriginEternal = new CacheDependency(FilePath);
        public static void CacheProcess(string TypeName)
        {
            if (HttpRuntime.Cache.Get("ExcelCache") == null)
            {
                try
                {                    
                    using (FileStream file = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                    {
                        workbook = new HSSFWorkbook(file);
                    }
                    ExcelHandler(TypeName);
                }
                catch (Exception e)
                {
                    logger.Debug("(Debug)除錯" + e.Message);
                }                
            }
        }

        private static void ExcelHandler(string sheetName)
        {
            
            switch (sheetName)
            {
                    //畢業紀念冊                 
                case ExcelCacheProcessing.ClassBook:                    
                    IClassBookManagement gClassBookManagement = new ClassBookManagementImpl();
                    gClassBookManagement.ResetClassBookData(ClassBookToModelList());
                    break;

                    //歷屆董事
                case ExcelCacheProcessing.Directors:
                    IDirectorsManagement gDirectorsManagement = new DirectorsManagementImpl();
                    gDirectorsManagement.ResetDirectorsData(DirectorsToModelList());
                    break;

                    //緣起不滅
                case ExcelCacheProcessing.OriginEternal:
                    HttpContext.Current.Cache.Insert("OriginEternal", OriginEternalToModelList(), depOriginEternal);
                    break;
            }
           
            HttpContext.Current.Cache.Insert("ExcelCache", "cache", dep);
        }
        #region ToModelLists
        private static List<ClassBookModel> ClassBookToModelList()
        {
            #region 設好左右翻之Dictionary
            Dictionary<string, string> dic = new Dictionary<string, string>();
            sheet = workbook.GetSheet(ExcelCacheProcessing.ClassBookFormat);
            for (int row = 1; row <= sheet.LastRowNum; row++)   
            {
                if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    dic.Add(sheet.GetRow(row).GetCell(0).StringCellValue, sheet.GetRow(row).GetCell(1).StringCellValue); 
                }
            }
            #endregion

            #region 取得畢冊資料
            sheet = workbook.GetSheet(ExcelCacheProcessing.ClassBook);
            int count = sheet.LastRowNum + 1;       //從0開始，實際資料
            string[][] data = new string[count][];
            for (int row = 1; row < count; row++)   
            {
                //if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    string[] tmp = new string[7];
                    for (int i = 0; i < tmp.Length; i++) 
                    {
                        try
                        {
                            tmp[i] = sheet.GetRow(row).GetCell(i).ToString();
                        }
                        catch (Exception)
                        {
                            tmp[i] = string.Empty;
                        }
                    }
                    data[row-1] = tmp;
                }
            }
            #endregion

            #region 將資料倒入Model
            List<ClassBookModel> result = new List<ClassBookModel>();

            try
            {
                for (int i = 0; i < data.Length; i++)
                {

                    string[] tempArray = data[i];
                    if (string.IsNullOrEmpty(tempArray[0])) break;   // 判斷是否為空列
                    string[] sidArray = tempArray[3].Split(',');
                    string[] nameArray = tempArray[4].Split(',');

                    //0:學年度 1:科系 2:班級 3:學號 4:姓名 5:Image 6:Page
                    if (sidArray.Length > 0)
                    {
                        for (int j = 0; j < sidArray.Length; j++)
                        {
                            ClassBookModel model = new ClassBookModel();
                            model.ClassBookID = Guid.NewGuid().ToString();
                            model.AcademicYear = tempArray[0];
                            model.Department = tempArray[1];
                            model.Class = tempArray[2];
                            model.StudentId = sidArray[j];
                            model.StudentName = nameArray[j];
                            model.PictureUrl = tempArray[5];
                            model.Page = tempArray[6];
                            if (dic.ContainsKey(tempArray[0]))
                            {
                                if (dic[tempArray[0]].Equals(ExcelCacheProcessing.TurnLeft))
                                {
                                    model.IsTurnLeft = true;
                                }
                                else if (dic[tempArray[0]].Equals(ExcelCacheProcessing.TurnRight))
                                {
                                    model.IsTurnLeft = false;
                                }
                            }
                            result.Add(model);
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                logger.Debug(e.Message);
            }

            #endregion
            return result; 
        }
        private static List<DirectorsModel> DirectorsToModelList()
        {
            #region 取得歷屆董事資料
            sheet = workbook.GetSheet(ExcelCacheProcessing.Directors);
            int count = sheet.LastRowNum + 1;       //從0開始，實際資料要補1
            string[][] data = new string[count][];
            for (int row = 0; row < count; row++)
            {
                //if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    string[] tmp = new string[4];
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        try
                        {
                            tmp[i] = sheet.GetRow(row).GetCell(i).ToString();
                        }
                        catch (Exception)
                        {
                            tmp[i] = string.Empty;
                        }
                    }
                    data[row] = tmp;
                }
            }
            #endregion

            List<DirectorsModel> result = new List<DirectorsModel>();

            //0:屆數 1:啟始日期 2:結束日期 3:名單
            for (int i = 0; i < data.Length; i++)
            {
                string[] temp = data[i];
                DirectorsModel model = new DirectorsModel();
                model.SessionNumber = temp[0];
                model.StartYear = temp[1];
                model.EndYear = temp[2];
                model.Names = temp[3];
                if (!string.IsNullOrEmpty(model.SessionNumber) && model.SessionNumber != "屆數")
                {
                    result.Add(model);
                }
            }
            return result;
        }
        private static List<OriginEternalModel> OriginEternalToModelList()
        {
            sheet = workbook.GetSheet(ExcelCacheProcessing.OriginEternal);
            int count = sheet.LastRowNum + 1;       //從0開始，實際資料要補1
            string[][] data = new string[count][];
            for (int row = 0; row < count; row++)
            {
                //if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    string[] tmp = new string[2];
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        try
                        {
                            tmp[i] = sheet.GetRow(row).GetCell(i).ToString();
                        }
                        catch (Exception)
                        {
                            tmp[i] = string.Empty;
                        }
                    }
                    data[row] = tmp;
                }
            }

            List<OriginEternalModel> result = new List<OriginEternalModel>();

            //0:年代 1:圖片位置
            for (int i = 0; i < data.Length; i++)
            {
                string[] temp = data[i];
                OriginEternalModel model = new OriginEternalModel();
                model.Year = temp[0];
                model.PictureUrl = temp[1];
                if (!string.IsNullOrEmpty(model.Year)) {
                    result.Add(model);
                }
            }
            return result;
        }
        #endregion
    }
}