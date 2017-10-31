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
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string detail;
            detail = "Order Detail" + "<br/>";
            detail += "------------------" + "<br/>";
            detail += "Item Name = " + "<br/>";
            detail += "Item price = " + "<br/>";
            detail += "package =" + "<br/>";
            detail += "Amount =" + "<br/>";
            detail += "<br/>";
            lblreceipt.Text = detail;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string Server_URL = "https://www.sandbox.paypal.com/cgi-bin/webscr?";

            //Assigning Cmd Path as Statically to Parameter
            string cmd = "_xclick";

            //Assigning business Id as Statically to Parameter
            string business = "Expressprintingsystem2017@gmail.com";// Enter your business account here 

            //Assigning item name as Statically to Parameter
            string item_name = "Item 1";

            //Passing Amount as Statically to parameter 
            double amount = 100.00;//Convert.ToDouble(txtpaymentTotal.Text);

            //Passing Currency as Statically to parameter
            string currency_code = "MYR";

            string redirect = "";

            //Pass your Server_Url,cmd,business,item_name,amount,currency_code variable.        
            redirect += Server_URL;
            redirect += "cmd=" + cmd;
            redirect += "&business=" + business;
            redirect += "&first_name=" + "Name";
            redirect += "&item_name=" + item_name;
            redirect += "&amount=" + amount;
            redirect += "&quantity=1";
            redirect += "&currency_code=" + currency_code;

            string successURL = ConfigurationManager.AppSettings["SuccessURL"].ToString();
            redirect += "&return=" + successURL;
            redirect += "&cancel_return=" + ConfigurationManager.AppSettings["FailedURL"].ToString();


            Session["amount"] = amount;
            Response.Redirect(redirect);





        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {


            if (txtCardName.Text == "" || txtCardNumber.Text == "" || txtCCV.Text == "" || txtExpiryYear.Text == "" || txtExpitymonth.Text == "")
            {
                Response.Write("<script>alert('Please fill in credit card detail!');</script>");

            }

            else
            {
                DateTime currentDate = DateTime.Now;
                Decimal totalPayment = Convert.ToDecimal(txtpaymentTotal.Text);
                string type = "Credir Card";

                Model.Entities.Payment newpayment = new Model.Entities.Payment(type, totalPayment, currentDate);
                Model.Entities.Request request = (Model.Entities.Request)Session["request"];
                //request.RequestLists[0].RequestItemID[0]
                request.Payment = newpayment;

                SqlConnection conTaxi;
                string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
                conTaxi = new SqlConnection(connStr);
                conTaxi.Open();

                string strInsert;
                SqlCommand cmdInsert;


                strInsert = "Insert Into Payment (PaymentType, PaymentAmount, PaymentDateTime) Values (@PaymentType, @PaymentAmount, @PaymentDateTime);SELECT MAX(PaymentID) from Payment where PaymentAmount=@PaymentAmount";
                cmdInsert = new SqlCommand(strInsert, conTaxi);

                Decimal totalamount = Convert.ToDecimal(txtpaymentTotal.Text);

                cmdInsert.Parameters.AddWithValue("@PaymentType", request.Payment.PaymentType);
                cmdInsert.Parameters.AddWithValue("@PaymentAmount", request.Payment.PaymentAmount);
                cmdInsert.Parameters.AddWithValue("@PaymentDateTime", request.Payment.PaymentDateTime);
                var getPaymentID = cmdInsert.ExecuteScalar();

                if (getPaymentID != null)
                {
                    request.Payment.PaymentID = (string)getPaymentID;
                    string path = (string)(Session["pathfile"]);

                    insertNewRequest(request);
                    generateQRcode(request);
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not
                    {
                        file.Delete();
                      
                    }

                    Response.Write("<script>alert('Successful payment');</script>");

                }
                else
                {
                    Response.Write("<script>alert('Upload Failed');</script>");
                }

                /*Close database connection*/


                conTaxi.Close();
                Response.Redirect("/Customer/masterPageTest.aspx");
            }



        }

        private void insertNewRequest(Model.Entities.Request request)
        {
            SqlConnection conPrintDB;
            string connStr = ConfigurationManager.ConnectionStrings["printDBServer"].ConnectionString;
            conPrintDB = new SqlConnection(connStr);
            conPrintDB.Open();

            string strInsert;
            SqlCommand cmdInsert;

            //request
            strInsert = "Insert Into Request (RequestDateTime, DueDateTime, PaymentID, CompanyID, CustomerID) Values (@requestDateTime, @dueDateTime, @paymentID, @companyID, @customerID);SELECT MAX(RequestID) from Request where CustomerID=@customerID AND RequestDateTime = @requestDateTime;";
            cmdInsert = new SqlCommand(strInsert, conPrintDB);

            cmdInsert.Parameters.AddWithValue("@requestDateTime", DateTime.Now);
            cmdInsert.Parameters.AddWithValue("@dueDateTime", request.DueDateTime);
            cmdInsert.Parameters.AddWithValue("@paymentID", request.Payment.PaymentID);
            cmdInsert.Parameters.AddWithValue("@companyID", request.CompanyID);
            cmdInsert.Parameters.AddWithValue("@customerID", request.CustomerID);

            var requestID = cmdInsert.ExecuteScalar();

            request.RequestID = (string)requestID;

            //Requestlist
            strInsert = "Insert Into Requestlist (RequestID, RequestItemID, RequestStatus, RequestType) Values (@RequestID, @RequestItemID, @RequestStatus, @RequestType);SELECT MAX(RequestlistID) from Requestlist where RequestID=@RequestID AND RequestItemID=@RequestItemID";
            cmdInsert = new SqlCommand(strInsert, conPrintDB);


            cmdInsert.Parameters.AddWithValue("@RequestID", request.RequestID);
            cmdInsert.Parameters.AddWithValue("@RequestItemID", request.RequestLists[0].RequestItemID);
            cmdInsert.Parameters.AddWithValue("@RequestStatus", request.RequestLists[0].RequestStatus);
            cmdInsert.Parameters.AddWithValue("@RequestType", request.RequestLists[0].RequestType);

            var requestlistID = cmdInsert.ExecuteScalar();

            request.RequestLists[0].RequestlistID = (string)requestlistID;

            //documentlist
            strInsert = "Insert Into Documentlist (RequestlistID, DocumentID, Sequences, DocumentColor, DocumentBothSide, DocumentPaperType, DocumentQuantity, DocumentDescription) Values (@RequestlistID, @DocumentID, @Sequences, @DocumentColor, @DocumentBothSide, @DocumentPaperType, @DocumentQuantity, @DocumentDescription)";
            cmdInsert = new SqlCommand(strInsert, conPrintDB);

            foreach (Model.Entities.Documentlist documentlist in request.RequestLists[0].DocumentList)
            {
                insertDocument(documentlist.Document, conPrintDB);//insert document contain in documentlist

                cmdInsert.Parameters.Clear();//clear parameter before loop
                cmdInsert.Parameters.AddWithValue("@RequestlistID", request.RequestLists[0].RequestlistID);
                cmdInsert.Parameters.AddWithValue("@DocumentID", documentlist.Document.DocumentID);
                cmdInsert.Parameters.AddWithValue("@Sequences", documentlist.Sequences);
                cmdInsert.Parameters.AddWithValue("@DocumentColor", documentlist.DocumentColor);
                cmdInsert.Parameters.AddWithValue("@DocumentBothSide", documentlist.DocumentBothSide);
                cmdInsert.Parameters.AddWithValue("@DocumentPaperType", documentlist.DocumentPaperType);
                cmdInsert.Parameters.AddWithValue("@DocumentQuantity", documentlist.DocumentQuantity);
                cmdInsert.Parameters.AddWithValue("@DocumentDescription", documentlist.DocumentDescription);
                cmdInsert.ExecuteNonQuery();
            }

            conPrintDB.Close();
            PrintingRequestHub.refreshTable();



        }

        private void insertDocument(Model.Entities.Document document, SqlConnection condocument)
        {



            string strInsert = "Insert Into Document (DocumentName, DocumentType, FileIDInCloud, CustomerID, Size, PageNumber) Values (@DocumentName, @DocumentType, @FileIDInCloud, @CustomerID, @Size, @PageNumber);SELECT MAX(DocumentID) from Document where DocumentName=@DocumentName and DocumentType=@DocumentType";
            SqlCommand cmdInsert = new SqlCommand(strInsert, condocument);

            cmdInsert.Parameters.AddWithValue("@DocumentName", document.DocumentName);
            cmdInsert.Parameters.AddWithValue("@DocumentType", document.DocumentType);
            cmdInsert.Parameters.AddWithValue("@FileIDInCloud", document.FileIDInCloud);
            cmdInsert.Parameters.AddWithValue("@CustomerID", document.CustomerID);
            cmdInsert.Parameters.AddWithValue("@Size", document.Size);
            cmdInsert.Parameters.AddWithValue("@PageNumber", document.PageNumber);

            var getDocumentID = cmdInsert.ExecuteScalar();

            document.DocumentID = getDocumentID.ToString();


        }

        private void generateQRcode(Model.Entities.Request request)
        {
            string code;
            string title = "Express Printing System Shop";
            string paymentID = request.Payment.PaymentID;
            code = title + "\n";
            code += "------------------------------------" + "\n";
            code += "Payment ID     :" + paymentID + "\n";
            code += "Payment Date   :" + request.Payment.PaymentDateTime + "\n";
            code += "Payment Amount :" + request.Payment.PaymentAmount + "\n";
            code += "Best Regards" + "\n";


            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 250;
            imgBarCode.Width = 250;
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Booking.aspx");
        }


    }
}