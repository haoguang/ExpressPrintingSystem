using ExpressPrintingSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ExpressPrintingSystem.Model
{
    public class UserVerification
    {
        public static User getUserBasicInfo(string username, string loginType)
        {
            DataTable result = null;
            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect = null;
                    if (loginType.Equals(ROLE_STAFF))
                        strSelect = "select StaffID AS ID, StaffName AS Name, StaffEmail AS Email from Staff where StaffEmail = @uname";
                    else
                        strSelect = "select CustomerID AS ID, CustomerName As Name, CustomerEmail As Email from Customer where CustomerEmail = @uname";


                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {
                        cmdSelect.Parameters.AddWithValue("@uname", username);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            result = new DataTable();
                            da.Fill(result);
                        }

                        return new User((string)result.Rows[0]["ID"], (string)result.Rows[0]["Name"], GetUserRoles(username, loginType), (string)result.Rows[0]["Email"]);

                        
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public static bool verifyUser(string username, string password, string loginType)
        {
            DataTable result = null;
            try {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect = null;
                    if (loginType.Equals(ROLE_STAFF))
                        strSelect = "select StaffPassword As Password, StaffSalt As Salt from Staff where StaffEmail = @uname";      
                    else
                        strSelect = "select CustomerPassword As Password, CustomerSalt As Salt from Customer where CustomerEmail = @uname";


                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {
                        cmdSelect.Parameters.AddWithValue("@uname", username);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            result = new DataTable();
                            da.Fill(result);
                        }

                        //retrieve password info
                        byte[] storedPassword = (byte[])result.Rows[0]["Password"];
                        byte[] storedSalt = (byte[])result.Rows[0]["Salt"];

                        //hash password from textbox
                        byte[] hashedPassword = ClassHashing.generateSaltedHash(password, storedSalt);

                        //compare the password and return the result
                        return ClassHashing.CompareByteArrays(storedPassword, hashedPassword);
                    }

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        //Get the Roles for this particular user
        public static string GetUserRoles(string username, string signInType)
        {

            DataTable result = null;
            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect;
                    if(signInType.Equals(ROLE_STAFF))
                        strSelect = "select TOP 1 StaffRole from Staff where StaffEmail = @uname";
                    else
                        strSelect = "select TOP 1 * from Customer where CustomerEmail = @uname";

                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {
                        cmdSelect.Parameters.AddWithValue("@uname", username);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            result = new DataTable();
                            da.Fill(result);
                        }
                        
                        if (result.Rows.Count == 1)
                        {
                            if(signInType.Equals(ROLE_STAFF))
                                return (string)result.Rows[0]["StaffRole"];
                            else
                                return ROLE_CUSTOMER;
                        }


                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            //user id not found, lets treat him as a guest        
            return ROLE_GUEST;
        }

        public const string ROLE_ADMIN = "Owner";
        public const string ROLE_STAFF = "Staff";
        public const string ROLE_CUSTOMER = "Customer";
        public const string ROLE_GUEST = "Guest";
    }
}