using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    //創校緣起 -> 歷任董事
    public class DirectorsModel
    {
        public string SessionNumber { get; set; }       //第n屆
        public string StartYear { get; set; }           //起始年份
        public string EndYear { get; set; }             //結束年份
        public string Names { get; set; }               //未塞入List前的名單
        public List<string> NameList { get; set; }      //名單
    }
}
