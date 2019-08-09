using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    //類別管理
    public class CategoryModel
    {
        public const string CATEGORY_CATEGORYTYPEID_YEAR = "Year"; //年度
        public const string CATEGORY_CATEGORYTYPEID_TERM = "Term"; //學期
        public const string CATEGORY_CATEGORYTYPEID_DEPARTMENT = "Department"; //科系
        public const string CATEGORY_CATEGORYTYPEID_CLASS = "Class"; //班級 
        public const string CATEGORY_CATEGORYTYPEID_OUTSIDE = "Outside"; //境外
        public const string CATEGORY_CATEGORYTYPEID_CONTRACTDE = "ContractDe"; //境外 國別
        public const string CATEGORY_CATEGORYTYPEID_COUNTRY = "Country"; //境外 國別
        public const string CATEGORY_CATEGORYTYPEID_INSIDE = "Prep"; //境內
        public const string CATEGORY_CATEGORYTYPEID_PARTITION = "Partition"; //境內 分區
        public const string CATEGORY_CATEGORYTYPEID_SITE = "Site"; //境內 所在地

        #region for search const string  
        public const string CATEGORY_EVENTCALENDAR_SEARCH = "EC_Search";
        public const string CATEGORY_SCHOOLDIARY_SEARCH = "SD_Search";
        public const string CATEGORY_NEWS_SEARCH = "NS_Search";
        public const string CATEGORY_HONOURLIST_SEARCH = "HL_Search";
        public const string CATEGORY_PLANOUTSIDE_SEARCH = "PO_Search";
        public const string CATEGORY_PLANINSIDE_SEARCH = "PI_Search";
        #endregion

        public string CategoryID { get; set; }
        public string CategoryTypeID { get; set; }
        public string CategoryName { get; set; }
        public int Sort { get; set; }
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Updater { get; set; }

    }
}
