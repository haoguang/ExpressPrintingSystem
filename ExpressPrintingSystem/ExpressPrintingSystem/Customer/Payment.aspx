<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ExpressPrintingSystem.Customer.Payment" %>

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
    position:absolute;

}
       #orderdetail {
           background: white;

	border: 0 none;

	border-radius: 3px;
 
	box-shadow: 0 0 15px 1px rgba(0, 0, 0, 0.4);

	padding: 20px 30px;
	margin: 0 10%;
  

       }
 #form1 {

    width: 1000px;

	margin: 50px auto;

	position: relative;
        }

 
       .auto-style1 {
           height: 20px;
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


 
       .auto-style3 {
           width: 1px;
       }


 
       .auto-style4 {
           height: 78px;
       }


 </style>   
    
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="auto-style1" method="POST">
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
      <div>
  <fieldset class="orderdetail">
      <asp:Label ID="lblreceipt" runat="server" Text="Label"></asp:Label>
  </fieldset>
      </div>


  <div style="padding-top:250px;">
  <fieldset class="CreditCard-fieldset">
     <table style="width: 100%;">
            <tr>
                <td colspan="4"><h1>Payment</h1></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4"><hr/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>You can choose Paypal to make payment :</td>
                <td class="auto-style3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:ImageButton ID="ImageButton1" runat="server" Height="53px" ImageUrl="~/Images/paypal.PNG" Width="154px" OnClick="ImageButton1_Click" />
                    <br />
                    <br />
                </td>
                <td>
                     <asp:Image ID="Image1" class="visa" runat="server"  ImageUrl="~/Images/visa.PNG" Height="73px" Width="104px" />
                </td>
                <td>
                    <asp:Image ID="Image3" class="mastercard" runat="server" ImageUrl="~/Images/mastercard.PNG" Height="68px" Width="119px" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" colspan="2">
                    Cardholder&#39;s Name<br />
                    <asp:TextBox ID="txtCardName" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style1" colspan="2">
                    Card Number<br />
                    <asp:TextBox ID="txtCardNumber" runat="server" MaxLength="16"></asp:TextBox>
                </td>
                <td class="auto-style1"></td>
                <td></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" class="auto-style4">
                    <br />
                    Expiry Date<br />
                    <asp:TextBox ID="txtExpitymonth" runat="server" MaxLength="2"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtExpiryYear" runat="server" MaxLength="2"></asp:TextBox>
                </td>
                <td colspan="2" class="auto-style4">
                    <br />
                    CVV/CVC<br />
                    <asp:TextBox ID="txtCCV" runat="server" MaxLength="3"></asp:TextBox>
                </td>
                <td class="auto-style4"></td>
                <td class="auto-style4"></td>
                <td class="auto-style4">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" class="auto-style1">
                    <br />
                    Total Payment<br />
                    RM<asp:TextBox ID="txtpaymentTotal" runat="server" Enabled="False">100</asp:TextBox>
                </td>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </td>
                <td colspan="2">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
  </fieldset>
</div>
        <div style="padding-top:700px">
        </div>
     
  
 </form>

<script>
  
    var textbox = document.getElementById('txtCardNumber');
    var image1 = document.getElementById('Image1');
    var image2 = document.getElementById("ImageButton1");
    var image3 = document.getElementById("Image3");

    textbox.addEventListener("keyup", keyDownEvent);

    var previousValue = '';

    function keyDownEvent(evt) {
        var value = textbox.value;

        if (value.length > 0) {


            var firstLetter = value.substring(0, 1);
            if (firstLetter === "4") {

                image3.style.borderColor = "blue";
                image3.style.borderStyle = "solid";
                image2.style.visibility = 'hidden';
                image1.style.visibility = 'hidden';
                previousValue = firstLetter;


            } else if (firstLetter === "5") {
                image1.style.borderColor = "blue";
                image1.style.borderStyle = "solid";
                image2.style.visibility = 'hidden';
                image3.style.visibility = 'hidden';

            }

        } else {
            image1.style.visibility = 'visible';
            image2.style.visibility = 'visible';
            image3.style.visibility = 'visible';
            image1.style.borderColor = "white";
            image2.style.borderColor = "white";
            image3.style.borderColor = "white";
        }


    }
    
    </script>
    
</body>
</html>
