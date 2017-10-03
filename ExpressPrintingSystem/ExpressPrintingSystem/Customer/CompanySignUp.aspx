<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanySignUp.aspx.cs" Inherits="ExpressPrintingSystem.Customer.CompanySignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
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

	/*stacking fieldsets above each other*/

	position: absolute;

}
        html{
             background-color :azure;
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

                <td class="auto-style25" colspan="3">
                    <div class="btn-group" role="group" aria-label="Basic example">
                 <asp:Button ID="btnCustomer" runat="server" CssClass="btn btn-success" Text="Customer" OnClick="btnCustomer_Click" CausesValidation="False"  />
                 <asp:Button ID="btnCompany" runat="server" CssClass="btn btn-default" Text="Company" OnClick="btnCompany_Click" CausesValidation="False"  />
                 </div>
                </td>
                <td class="auto-style24">&nbsp;</td>
                <td class="auto-style24">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style25" colspan="3">Sign Up Page</td>
                <td class="auto-style24"></td>
                <td class="auto-style24"></td>
            </tr>
            <tr>
                <td class="auto-style26" colspan="3">
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name is required!" ControlToValidate="txtName" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td>
                     &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">Company Address<br />
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style26">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Company Address is required!" ControlToValidate="txtAddress" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td class="auto-style26">
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">Company Contact Number<br />
                    <asp:TextBox ID="txtContNo" runat="server" MaxLength="11"></asp:TextBox>
                </td>
                <td class="auto-style26">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Company Contact Number is required!" ControlToValidate="txtContNo" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td class="auto-style26">
                <asp:RegularExpressionValidator
            ID="RegularExpressionValidator2" runat="server" 
            ErrorMessage="Must be at leat 10-digit Contact Number" 
            ControlToValidate="txtContNo" ValidationExpression="^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"></asp:RegularExpressionValidator>
                    
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">Company Email<br />
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style26">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Company email is required!" ControlToValidate="txtEmail" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td class="auto-style26">
                     <asp:RegularExpressionValidator
            ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="Invalid email format. Suggestion: someone@example.com"
            ControlToValidate="txtEmail" ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"></asp:RegularExpressionValidator>
                    
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26" colspan="3">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style26">
                    <asp:Button ID="btnCompanyCancel" runat="server" Text="Cancel" />
                </td>
                <td class="auto-style26" colspan="2">
                    <asp:Button ID="btnCompanySubmit" runat="server" Text="Submit" OnClick="btnCompanySubmit_Click1" />
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
