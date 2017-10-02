<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanySignUp.aspx.cs" Inherits="ExpressPrintingSystem.Customer.CompanySignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
           #form1 {

            width: 1000px;

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

	/*stacking fieldsets above each other*/

	position: absolute;

}
      

         ::-webkit-input-placeholder {
   
   font-weight : bold;

}
         input[type=text], select {
   
   
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    border-radius: 5px;
 height : 20px;
    width:250px;
    
}
        </style>




    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <fieldset>
        <div>
<table style="width: 100%;">
            <tr>
                <td class="auto-style25">Sign Up Page</td>
                <td class="auto-style24"></td>
                <td class="auto-style24"></td>
            </tr>
            <tr>
                <td class="auto-style26">
                        <hr/>
                    </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">Name<br />
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">Company Address<br />
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">Company Contact Number<br />
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">Company Email<br />
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">
                    <asp:Button ID="btnCompanyCancel" runat="server" Text="Cancel" />
                </td>
                <td>
                    <asp:Button ID="btnCompanySubmit" runat="server" Text="Submit" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        </div>
        </fieldset>
        </div>
    </form>
</body>
</html>
