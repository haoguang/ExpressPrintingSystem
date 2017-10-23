<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThankYouPage.aspx.cs" Inherits="ExpressPrintingSystem.Customer.ThankYouPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {

    width: 600px;

	margin: 50px auto;

	position: relative;
        }
        #form1 fieldset {

	background: white;

	border: 0 none;

	border-radius: 3px;

	box-shadow: 0 0 15px 1px rgba(0, 0, 0, 0.4);

	padding: 20px 30px;

	box-sizing: border-box;

	width: 80%;

	margin: 0 10%;

    font-size: 20px;

	position: absolute;

}
          html {
            background-color :azure;
        }
       
    #form1 input{

	

	border: 1px solid #ccc;

	border-radius: 3px;

	margin-bottom: 10px;

	
    
	box-sizing: border-box;

	font-family: montserrat;

	color: #2C3E50;

	font-size: 20px;

}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
    <div>

    <h1>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Thanks for using</h1>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnBack" runat="server" Text="Back To Home Page" OnClick="btnBack_Click" />
    </div>
            </fieldset>
    </form>
</body>
</html>
