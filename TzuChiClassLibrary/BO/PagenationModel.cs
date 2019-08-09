using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    public class PagenationModel
    {
        // 大事紀要 - 搜尋欄位
        public const string EVENTCALENDAR_SEARCH_TITLE = "c696cd04-2d4a-4380-b2b4-ad06383ac8c4";
        public const string EVENTCALENDAR_SEARCH_CONTENT = "2b9f06d7-097e-4bdc-a50b-da8c7e99b6ac";
        public const string EVENTCALENDAR_SEARCH_YEAR = "b0769789-cfa8-4a5c-bedf-542f3681dba6";
        
        // 校園日誌 - 搜尋欄位
        public const string SCHOOLDIARY_SEARCH_SERIALNO = "25b16595-5102-427f-8035-9d82a1da285b";
        public const string SCHOOLDIARY_SEARCH_TITLE = "89464226-5674-4424-9438-f4a1fd75b6cf";
        public const string SCHOOLDIARY_SEARCH_CONTENT = "8a2e5be7-5f23-4868-bcfb-e805e70915aa";
        public const string SCHOOLDIARY_SEARCH_YEAR = "b731d185-efc3-4fc1-88f9-9786f6e01753";

        // 大愛新聞 - 搜尋欄位
        public const string NEWS_SEARCH_SERIALNO = "6c8fc52f-aac7-41d0-9406-73c2db8c599a";
        public const string NEWS_SEARCH_TITLE = "07fc13a0-ad40-44b3-bc87-a87c50b55d63";
        public const string NEWS_SEARCH_CONTENT = "2e64be34-e1b2-4cf8-8539-1525fde83b0d";
        public const string NEWS_SEARCH_YEAR = "6e4dbece-6d73-4e7a-a132-2c80918a10fa";

        // 榮譽榜 - 搜尋欄位
        public const string HONOURLIST_SEARCH_TITLE = "30f0b1df-bb89-45b5-99b2-4e10d049fd46";
        public const string HONOURLIST_SEARCH_CONTENT = "1550d59a-693b-423d-ba16-f044e1d12f7a";

        // 境外地圖 - 搜尋欄位
        public const string PLANOUTSIDE_SEARCH_TYPEOUTSIDE = "bfb4a77d-a152-40ea-a7e5-765ce299cd16"; //產學類型
        public const string PLANOUTSIDE_SEARCH_COUNTRY = "75895585-dc38-4914-a3cc-a29175549cc1";
        public const string PLANOUTSIDE_SEARCH_CONTRACTDEPARTMENT = "51c1d282-f782-4981-a182-3c4b5485fbd6"; //簽約單位
        public const string PLANOUTSIDE_SEARCH_SCHOOLNAMECH = "cf1161d6-a89d-4b4a-9f83-de0354dd2831";
        public const string PLANOUTSIDE_SEARCH_SCHOOLNAMEEN = "c0a583e1-202d-491b-affd-555b5b2f6870";
        public const string PLANOUTSIDE_SEARCH_ACTIVITYNAME = "e18e6cc5-b2c5-4ad1-8ac3-61485bd01907"; //活動名稱-[Content] Description

        // 境內地圖 - 搜尋欄位
        public const string PLANINSIDE_SEARCH_PREP = "a0e058e8-be11-4da5-82fd-f4ddfd8d709e"; //產學類型
        public const string PLANINSIDE_SEARCH_YEAR = "45250fb2-d02d-443d-b5eb-fa5d2e6529e8"; //學年度
        public const string PLANINSIDE_SEARCH_PARTITION = "889cc336-5a24-4a04-8389-48746d1e542d"; //分區
        public const string PLANINSIDE_SEARCH_SITE = "3e2496ce-7943-429e-8db0-373f42797a8a"; //所在地(台北)
        public const string PLANINSIDE_SEARCH_DEPARTMENT = "20674e41-93cf-4751-b95f-ece22413d167"; //對象(單位)
        public const string PLANINSIDE_SEARCH_DESCRIPTION = "887c1e6e-88cf-402f-a8cc-1cc130f0dbee"; //簡介
        public const string PLANINSIDE_SEARCH_AGENCIES = "b46629fc-cdc9-4b48-a0ed-60548cfd17c3"; //實習地點

        public PagenationModel()
        {
            Page = 1;
            CntPerPage = 10;
            OrderByField = string.Empty;
            OrderNum = 0;
            Order = false;
            QueryField = string.Empty;
            KeyWord = string.Empty;
            BeginDateTime = string.Empty;
            EndDateTime = string.Empty;
            IsPosted = false;

            Type= string.Empty;

        } // PagenationModel()

        public string Type { get; set; }

        public int Page { get; set; }
        public int CntPerPage { get; set; }
        public string OrderByField { get; set; }
        public int OrderNum { get; set; }
        public bool Order { get; set; }
        public string QueryField { get; set; }
        public string KeyWord { get; set; }

        public string BeginDateTime { get; set; }
        public string EndDateTime { get; set; }

        public bool IsPosted { get; set; }

        public string MultipleType { get; set; } // for 後台: 校園日誌、榮譽榜 分類搜尋使用
        public string Year { get; set; }         // for 前台: 校園日誌、大愛新聞使用 存放ex: 99 or 100 or 101~
        public string Month { get; set; }        // for 前台: 大愛新聞使用          存放ex: 1 or 2 or ... or 12
        public string Term { get; set; }         // for 前台: 校園日誌使用          存放ex:前台顯示的字串 
        public string Department { get; set; }   // for 前台: 畢業紀念冊使用         存放ex:護理系
        public string Class { get; set; }        // for 前台: 畢業紀念冊使用         存放ex:甲班
        public string StudentID { get; set; }     // for 前台: 畢業紀念冊使用         存放ex:88889
        public Boolean IsTurnLeft { get; set; }
        // KeyWord在畢業紀念冊代表"姓名"
    }
}
