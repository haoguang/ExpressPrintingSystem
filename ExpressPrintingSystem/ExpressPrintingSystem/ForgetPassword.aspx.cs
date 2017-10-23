using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ExpressPrintingSystem
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        private static Random random = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conPrint;
                string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                conPrint = new SqlConnection(connStr);
                conPrint.Open();

                string strInsert;
                SqlCommand cmdInsert;

                strInsert = "select CustomerID, CustomerName, CustomerPassword from Customer where CustomerEmail = @customeremail";


                cmdInsert = new SqlCommand(strInsert, conPrint);

                cmdInsert.Parameters.AddWithValue("@customeremail", TextBox1.Text);
                SqlDataReader dtr;
                dtr = cmdInsert.ExecuteReader();



                if (dtr.Read())
                {
                    string customerID = dtr["CustomerID"].ToString();

                    string Name = dtr["CustomerName"].ToString();

                    //pass value to sendMail().
                    ViewState["CustomerID"] = customerID;
                    ViewState["CustomerName"] = Name;

                    SendMail();

                    //get value from generatecode.
                    string verificationcode = (string)ViewState["generateCode"];

                    //pass value to verification password page
                    Session["VERIFICATIONCODE"] = verificationcode;

                    //pass value to resetpassword page.
                    Session["CustomerID"] = customerID;
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Valid email')</script>");

                    Response.Redirect("VerificationPassword.aspx");

                }
                else
                {
                  
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Email invalid')</script>");
                }
                conPrint.Close();

            }
            catch {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Plase fill in value.')</script>");
            }
           
        }


        public static string generateCode()
        {
            int length = 5;

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;

        }

      
        protected void SendMail()
        {
         
            string CustomerID = (string)ViewState["CustomerID"];
            string CustomerName = (string)ViewState["CustomerName"];
            string CustomerPassword = (string)ViewState["CustomerPassword"];
          
           //get verification code from generateCode.
            string asgenerateCode = generateCode();
            
            ViewState["generateCode"] = asgenerateCode;

            var fromAddress = "darrenlai95@gmail.com";
            var toAddress = TextBox1.Text;
            const string fromPassword = "940917105277";

            
            string strbody;
            strbody = "Hi " + CustomerName + ", based on our record, you have requested to reset your password, please enter the verification code provided into the field." + "<br/>";
            strbody += "&nbsp;&nbsp;&nbsp;" + "Express Printing System" + "<br/>";
            strbody += "---------------------------------" + "<br/>";
            strbody += "User ID                  : " + CustomerID + "<br/>";
            strbody += "User Name            : " + CustomerName + "<br/>";
            strbody += "Verification Code  : " + asgenerateCode + "<br/>";
            strbody += "Please ignore This Email if you did not requested to reset password." + "<br/>";
            strbody += "Best Regards, " + "<br/>";
            strbody += "Express Printing System Admin";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("darrenlai95@gmail.com", "Express Printing System");
            mail.To.Add(TextBox1.Text);
            mail.Subject = "Reset Password";
            mail.Body = strbody;
            mail.IsBodyHtml = true;
           
            var smtp = new System.Net.Mail.SmtpClient();
            
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            
            // Passing values to smtp object
            smtp.Send(mail);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}