using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    public interface IHumanitiesManagement
    {
        List<HumanitiesModel> GetALL();
    }
}
