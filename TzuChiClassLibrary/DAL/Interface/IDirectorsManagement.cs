using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    //創校緣起 -> 歷任董事
    public interface IDirectorsManagement
    {
        Boolean ResetDirectorsData(List<DirectorsModel> list);
        List<DirectorsModel> GetAll();
    }
}
