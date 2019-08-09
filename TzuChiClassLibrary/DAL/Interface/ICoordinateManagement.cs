using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    public interface ICoordinateManagement
    {
        Boolean Add(CoordinateModel model);
        Boolean Delete(string pointID);
        CoordinateModel GetByPointID(string pointID);
        List<CoordinateModel> GetAll(string typeID);
    }
}
