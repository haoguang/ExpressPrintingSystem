<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCustomerDetail.aspx.cs" Inherits="ExpressPrintingSystem.Customer.EditCustomerDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 263px;
        }
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
        .auto-style2 {
            width: 10px;
        }
        .auto-style3 {
            height: 80px;
        }
        .auto-style4 {
            width: 10px;
            height: 80px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
        <div>
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">Edit Customer Detail&nbsp;<br />
                    <hr/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Customer Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                        <br />
                        <br />
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        <br />
                    </td>
                    <td class="auto-style2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Customer Email&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
                        <br />
                        <br />
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        <br />
                    </td>
                    <td class="auto-style2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is Required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
            ID="RegularExpressionValidator2" runat="server" 
            ErrorMessage="Invalid email format. Suggestion: someone@example.com"
            ControlToValidate="txtEmail" ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" ForeColor="Red">*</asp:RegularExpressionValidator>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Customer Date of birth&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="customerDOB" runat="server" TextMode="Date"></asp:TextBox>
                    </td>
                    <td class="auto-style2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="customerDOB" ErrorMessage="customer date of birth is required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Customer Phone Number&nbsp;&nbsp;&nbsp;&nbsp; : 
                        <br />
                        <br />
                    </td>
                    <td class="auto-style2"> <asp:TextBox ID="txtPhoNo" runat="server" MaxLength="12"></asp:TextBox>
                        <br />
                    </td>
                    <td class="auto-style2">  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhoNo" ErrorMessage="Phone Number is Required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td> 
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">Customer Contact Method&nbsp;&nbsp; :
                        <br />
                    </td>
                    <td class="auto-style4">
                        <br />
                        <asp:RadioButtonList ID="rbtContMet" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>Email</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="auto-style4">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rbtContMet" ErrorMessage="Contact Method is required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td class="auto-style3"></td>
                    <td class="auto-style3"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                    <td>
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
                    </td>
                    <td>
                        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
            </fieldset>
    </form>
</body>
</html>
