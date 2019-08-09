using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TzuChiClassLibrary.DAL;

namespace TzuChiBackend.Security
{
    public class CustomerRoleProvider : RoleProvider
    {
        IAdminManagement adminManagement = new AdminManagementImpl();
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            //throw new NotImplementedException();
            ////取得帳號資訊
            //var account = adminManagement.GetAdmin()
            //    .Where(a => a.Account.Equals(username)).FirstOrDefault();

            ////判斷是否為"僅租借權限"
            List<string> roles = new List<string>();
            roles.Add("admin");
            //switch (account.IsPart)
            //{
            //    case true:
            //        roles.Add("IsPart");
            //        break;
            //    case false:
            //        roles.Add("All");
            //        break;
            //}

            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
            ////取得帳號資訊
            //var account = adminManagement.GetAdmin()
            //    .Where(a => a.Account.Equals(username)).FirstOrDefault();
            string accRole = string.Empty;

            //switch (account.IsPart)
            //{
            //    case true:
            //        accRole = "IsPart";
            //        break;
            //    case false:
            //        accRole = "All";
            //        break;
            //}

            return accRole == roleName;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}