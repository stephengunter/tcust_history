using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    public interface IAccountViewManagement
    {
        AccountViewModel CheckPassWord(AccountViewModel model);

        void LoginRecord(AccountViewModel model, string IPAddress);
    }
}
