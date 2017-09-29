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
        public static bool verifyUser(string username, string password)
        {
            DataTable result = null;
            try {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect = "select CustomerPassword, CustomerSalt from Customer where CustomerEmail = @uname";
                    using (SqlCommand cmdSelect = new SqlCommand(strSelect, conPrintDB))
                    {
                        cmdSelect.Parameters.AddWithValue("@uname", username);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmdSelect))
                        {
                            result = new DataTable();
                            da.Fill(result);
                        }

                        //retrieve password info
                        byte[] storedPassword = (byte[])result.Rows[0]["CustomerPassword"];
                        byte[] storedSalt = (byte[])result.Rows[0]["CustomerSalt"];

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
        public static string GetUserRoles(string username)
        {
            DataTable result = null;
            try
            {
                using (SqlConnection conPrintDB = new SqlConnection(ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString))
                {
                    string strSelect = "select TOP 1 * from Customer where CustomerEmail = @uname";
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
            return "guest";
        }

        const string ROLE_ADMIN = "Owner";
        const string ROLE_STAFF = "Staff";
        const string ROLE_CUSTOMER = "Customer";
    }
}