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
        .auto-style1 {
            height: 23px;
        }

           div[class="btn-group"] {
         margin: 8px 0;
   
    text-align:center;
    display: flex;
  align-items: center;
  justify-content: center;
    }
    #btnCustomer, #btnCompany{
        border-radius : 20px;
    }

        </style>




    <title></title>
    <link href="../styles/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <fieldset>
        <div>
<table style="width: 100%;">
            <tr>
                <td class="auto-style1" colspan="4">
                     <div class="btn-group" role="group" aria-label="Basic example">
                 <asp:Button ID="btnCustomer" runat="server" CssClass="btn btn-success" Text="Customer" OnClick="btnCustomer_Click" CausesValidation="False"  />
                 <asp:Button ID="btnCompany" runat="server" CssClass="btn btn-default" Text="Company" OnClick="btnCompany_Click" CausesValidation="False"  />
  
     </div>
                </td>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td class="auto-style25" colspan="4">Sign Up Page</td>
                <td class="auto-style24"></td>
                <td class="auto-style24"></td>
            </tr>
            <tr>
                <td class="auto-style26" colspan="4">
                        <hr/>
                    </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">Name<br />
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style26" colspan="2">
                    &nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26" colspan="2">Company Address<br />
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style26" colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26" colspan="4">Company Contact Number<br />
                    <asp:TextBox ID="txtContNo" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26" colspan="4">Company Email<br />
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26" colspan="4">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">
                    <asp:Button ID="btnCompanyCancel" runat="server" Text="Cancel" />
                </td>
                <td class="auto-style26" colspan="3">
                    <asp:Button ID="btnCompanySubmit" runat="server" Text="Submit" OnClick="btnCompanySubmit_Click" />
                </td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        </div>
        </fieldset>
        </div>
    </form>
</body>
</html>
