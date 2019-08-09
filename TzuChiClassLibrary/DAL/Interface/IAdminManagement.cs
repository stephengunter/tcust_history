using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TzuChiClassLibrary.BO;

namespace TzuChiClassLibrary.DAL
{
    public interface IAdminManagement
    {

        Boolean Add(AdminModel model);

        Boolean Update(AdminModel model);

        Boolean Delete(string adminID);

        AdminModel GetByAdminID(string adminID);

        List<AdminModel> GetAll(PagenationModel model);
        List<AdminModel> GetAdmin();
        string DesEncrypt(string pToEncrypt); //加密
        string DesDecrypt(string pToDecrypt); //解密
    }
}
