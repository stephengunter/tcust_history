using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TzuChiClassLibrary.BO;

namespace TzuChiBackend.Security
{
    public class CustomMembershipUser : MembershipUser
    {
        #region Properties
        public string UserID { get; set; }
        public string UserName { get; set; }

        #endregion

        public CustomMembershipUser(AdminModel user)
            : base("CustomMembershipProvider", user.Account, user.AdminID, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserID = user.AdminID;
            UserName = user.Name;
        }
    }
}