<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resetPassword.aspx.cs" Inherits="ExpressPrintingSystem.resetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }
        

        #form1 {

    width: 300px;

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
                    <asp:Label ID="Label1" runat="server" Text="Reset Password" Font-Size="Large"></asp:Label>
                    <br />
                <hr /></td>
                
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label2" runat="server" Text="New Password" Font-Size="Large"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="Comfirm Password" Font-Size="Large"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtComfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
               
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="Button2_Click" />
                </td>
                <td class="auto-style1">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </td>
               
            </tr>
        </table>
    </div>
            </fieldset>
    </form>
</body>
</html>
