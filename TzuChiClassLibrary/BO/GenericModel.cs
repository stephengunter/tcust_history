using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    public class GenericModel
    {
        public string ContentCreator { get; set; }                  // 建立者
        public DateTime ContentCreateTime { get; set; }             // 建立時間
        public string ContentUpdater { get; set; }                  // 更新者
        public DateTime ContentUpdateTime { get; set; }             // 更新時間
        public Int32 TotalNum { get; set; }
    }
}
