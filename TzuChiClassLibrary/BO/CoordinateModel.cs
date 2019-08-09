using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TzuChiClassLibrary.BO
{
    public class CoordinateModel
    {
        public const string EMPTY_COORDINATES = "1k0f2547-6197-42b4-8011-cca7e8kk29cc";
        public string PointID { get; set; }                    //ID
        public string TypeID { get; set; }                     //境內外類別
        public double PointX { get; set; }
        public double PointY { get; set; }
        public string ContentName { get; set; }                //標題
    }
}
