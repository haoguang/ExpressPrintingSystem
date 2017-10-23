<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerificationPassword.aspx.cs" Inherits="ExpressPrintingSystem.VerificationPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
         #form1 {

    width: 350px;

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
      .head {
              padding-top:200px;
          }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="head">
        </div>
        <fieldset>
    <div>
        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" Text="Verification Code" Font-Size="Large" Font-Underline="True"></asp:Label>
                    <br />
                    <br />
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    
                </td>
                
            </tr>
            <tr>
                <td>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </td>
                <td>
       
        <asp:Button ID="btnSubmit" runat="server" OnClick="Button2_Click" Text="Submit" />
    
                </td>
               
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
             
            </tr>
        </table>
       
    </div>
       </fieldset>
        
       

    </form>
</body>
</html>
