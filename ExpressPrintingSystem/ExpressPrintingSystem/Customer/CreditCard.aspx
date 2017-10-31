<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreditCard.aspx.cs" Inherits="ExpressPrintingSystem.Customer.CreditCard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
    #form1 fieldset {

	background: white;

	border: 0 none;

	border-radius: 3px;

	box-shadow: 0 0 15px 1px rgba(0, 0, 0, 0.4);

	padding: 20px 30px;


	width: 80%;

	margin: 0 10%;

	position: absolute;

}
 #form1 {

    width: 800px;

	margin: 50px auto;

	position: relative;
        }
 
    
       #form1 input{

	padding: 5px;

	border: 1px solid #ccc;

	border-radius: 3px;

	margin-bottom: 10px;


	font-family: montserrat;

	color: #2C3E50;

	font-size: 15px;

}
         html {
            background-color :azure;
        }
    div[class="btn-group"], #Label2 {
         margin: 50px 0;
   
    text-align:center;
    display: flex;
  align-items: center;
  justify-content: center;
 
  
    }
        #btncashlink, #btnCreditCardlink {
         box-shadow: 0 0 15px 1px rgba(0, 0, 0, 0.4);
        
        }
        .auto-style1 {
            margin-bottom: 0px;
        }
        </style>


    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label2" runat="server" Text="Please choose you payment method." Font-Overline="False" Font-Size="X-Large" Font-Underline="True"></asp:Label>
       <div class="btn-group" role="group" aria-label="Basic example">
        
           <asp:ImageButton ID="btncashlink" runat="server" text="Cash" Height="300px" ImageUrl="~/Images/logo/cash.PNG" Width="300px" BorderStyle="Groove" />
           <asp:ImageButton ID="btnCreditCardlink" runat="server" text="Credit Card" CssClass="auto-style1" Height="300px" ImageUrl="~/Images/logo/creditcard.PNG" Width="300px" BorderStyle="Groove" OnClick="btnCreditCardlink_Click" />
                 
       </div>
    </form>
</body>
</html>
