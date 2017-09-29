using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ExpressPrintingSystem.Model
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetAllRoles()
        {
            string[] roles = { ROLE_ADMIN, ROLE_STAFF, ROLE_CUSTOMER };

            return roles;
        }

        public override string[] GetRolesForUser(string username)
        {
            SqlConnection conPrintDB;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conPrintDB = new SqlConnection(connStr);
            conPrintDB.Open();

            

            conPrintDB.Close();
            return null;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            SqlConnection conPrintDB;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conPrintDB = new SqlConnection(connStr);
            conPrintDB.Open();

            if (roleName.Equals(ROLE_ADMIN) || roleName.Equals(ROLE_STAFF))
            {

            }
            else if (roleName.Equals(ROLE_CUSTOMER))
            {

            }
            else
                return false;
            return false;
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

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
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

        

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }


        const string ROLE_ADMIN = "Owner";
        const string ROLE_STAFF = "Staff";
        const string ROLE_CUSTOMER = "Customer";
    }
}