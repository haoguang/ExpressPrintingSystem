using ExpressPrintingSystem.Model.Entities;
using ExpressPrintingSystem.Staff.Printing;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExpressPrintingSystem.Customer
{
    public partial class CreditCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

     
        protected void btnCreditCardlink_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.Cookies["UserCookie"] != null)
            {

                var Cookie = Request.Cookies["UserCookie"];

                if (Cookie.Values["UserInfo"] != null)
                {
                    string userString = ClassHashing.basicDecryption(Cookie.Values["UserInfo"].ToString());
                    User user = ExpressPrintingSystem.Model.Entities.User.toUserObject(userString);


                    SqlConnection conTaxi;
                    string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                    conTaxi = new SqlConnection(connStr);
                    conTaxi.Open();

                    string strCheck;
                    SqlCommand cmdCheck;
                    strCheck = "Select CustomerEmail from Customer Where CustomerID=@CustomerID";
                    cmdCheck = new SqlCommand(strCheck, conTaxi);
                    cmdCheck.Parameters.AddWithValue("@CustomerID", user.ID);
                    SqlDataReader dtr;
                    dtr = cmdCheck.ExecuteReader();

                    if (dtr.Read())
                    {

                        string customeremail = dtr["CustomerEmail"].ToString();

                        Session["email"] = customeremail;

                    }


                    conTaxi.Close();
                }
            }


            Response.Redirect("Payment.aspx");
        }

        protected void btncashlink_Click(object sender, ImageClickEventArgs e)
        {
            Model.Entities.Request request = (Model.Entities.Request)Session["request"];
            generateQRcode(request);
        }

        private void generateQRcode(Model.Entities.Request request)
        {
            string code;
            string title = "Express Printing Shop";
            string paymentID = request.Payment.PaymentID;
            code = title + System.Environment.NewLine;
            code += "------------------------------------" + System.Environment.NewLine;
            code += "Payment ID     :" + paymentID + System.Environment.NewLine;
            code += "Payment Date   :" + request.Payment.PaymentDateTime + System.Environment.NewLine;
            code += "Payment Amount :" + request.Payment.PaymentAmount + System.Environment.NewLine;



            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 300;
            imgBarCode.Width = 300;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    string result = Convert.ToBase64String(byteImage, 0, byteImage.Length); ;
                    CreateImage(result.ToString(), paymentID);

                }
                // plQRCode.Controls.Add(imgBarCode);
            }
        }

        private string CreateImage(string Byt, string paymentID)
        {

            try
            {
                byte[] data = Convert.FromBase64String(Byt);


                var filename = paymentID + ".png";// +System.DateTime.Now.ToString("fffffffffff") + ".png";
                var file = HttpContext.Current.Server.MapPath("~/QRcode/" + filename);
                System.IO.File.WriteAllBytes(file, data);
                //string ImgName = ".../QRcode/" + filename;
                SendMail(filename, paymentID);
                return filename;
            }
            catch (Exception e)
            {
                return "Error";

            }
        }
        private void SendMail(string filename, string paymentid)
        {

            string customeremail = (string)(Session["email"]);

            var fromAddress = "darrenlai95@gmail.com";
            var toAddress = customeremail;
            const string fromPassword = "940917105277";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("darrenlai95@gmail.com", "Express Printing System");
            mail.To.Add(customeremail);
            mail.Subject = "Receipt";
            mail.AlternateViews.Add(Mail_Body(filename, paymentid));
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
        private AlternateView Mail_Body(string filename, string paymentid)
        {

            string path = Server.MapPath("~/QRcode/" + filename);
            LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
            Img.ContentId = "MyImage";
            string str = @"  
            <table>  
                <tr>  
                    <td> '" + "Your Receipt ID:" + paymentid + @"'  
                    </td>  
<tr>
<td>
<hr/>
</td>
</tr>
<tr>
<td>
Please keep you receipt to express printing shop pay you printing fees.<br/>
</td>
</tr>
                </tr>  
                <tr>  
                    <td>  
                      <img src=cid:MyImage  id='img' alt='' width='100px' height='100px'/>   
                    </td>  
                    <td>
                </tr><br/>


<tr>
<td>
Best Regards,
</td>
</tr>
<tr>
<td>
Express Printing System Admin 
</td>
</tr>
</table>  
            ";
            AlternateView AV =
            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            AV.LinkedResources.Add(Img);
            return AV;
        }

    }
}