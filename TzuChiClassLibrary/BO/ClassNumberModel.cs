using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    //資訊查詢 -> 畢業紀念冊 -> 系別 對 班級數目
    public class ClassNumberModel
    {
        public string Department { get; set; }          //科系
        public List<string> ClassList { get; set; }     //班級名稱List
        public string Class { get; set; }
    }
}
