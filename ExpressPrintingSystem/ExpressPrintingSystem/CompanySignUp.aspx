<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanySignUp.aspx.cs" Inherits="ExpressPrintingSystem.CompanySignUp" %>

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


        .auto-style1 {
            width: 191px;
        }
        .auto-style2 {
            width: 434px;
        }


        .auto-style3 {
            width: 191px;
            height: 26px;
        }
        .auto-style4 {
            height: 26px;
        }


        .auto-style5 {
            width: 9px;
        }
        .auto-style6 {
            height: 26px;
            width: 9px;
        }
        .auto-style25 {
            font-size:25px;
        }
        #txtStaffPassword {
            margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    border-radius: 5px;
 height : 20px;
    width:250px;
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

                <td  colspan="3">
                    <div class="btn-group" role="group" aria-label="Basic example">
                 <asp:Button ID="btnCustomer" runat="server" CssClass="btn btn-success" Text="Customer" OnClick="btnCustomer_Click" CausesValidation="False"  />
                 <asp:Button ID="btnCompany" runat="server" CssClass="btn btn-default" Text="Company" OnClick="btnCompany_Click" CausesValidation="False"  />
                 </div>
                </td>
                <td class="auto-style24">&nbsp;</td>
                <td class="auto-style24">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style25" colspan="3">Company Registration</td>
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
                <td class="auto-style1">Company Name<br />
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style26" colspan="2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Staff Name is required!" ControlToValidate="txtName" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td>
                     &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Company Address<br />
                    <asp:TextBox ID="txtAddress" runat="server" TabIndex="1"></asp:TextBox>
                </td>
                <td class="auto-style5">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Company Address is required!" ControlToValidate="txtAddress" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td class="auto-style26">
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Company Contact Number<br />
                    <asp:TextBox ID="txtContNo" runat="server" MaxLength="11" TabIndex="2"></asp:TextBox>
                </td>
                <td class="auto-style5">
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
                <td class="auto-style1">Company Email<br />
                    <asp:TextBox ID="txtEmail" runat="server" TabIndex="3"></asp:TextBox>
                </td>
                <td class="auto-style5">
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
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style26">
                     &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style25">Owner Detail</td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style26">
                     &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr >
                <td class="auto-style1" colspan="3">
                        <hr class="auto-style2"/>
                </td>
               
            </tr>
            <tr>
                <td class="auto-style3">Staff Name<br />
                    <asp:TextBox ID="txtStaffName" runat="server" TabIndex="4"></asp:TextBox>
                </td>
                <td class="auto-style6">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStaffName" ErrorMessage="Staff Name is required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                <td class="auto-style4">
                     &nbsp;</td>
                <td class="auto-style4"></td>
                <td class="auto-style4"></td>
            </tr>
            <tr>
                <td class="auto-style1">Staff Email<br />
                    <asp:TextBox ID="txtStaffEmail" runat="server" TabIndex="5"></asp:TextBox>
                </td>
                <td class="auto-style5">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtStaffEmail" ErrorMessage="Staff Email is required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td class="auto-style26">
                     <asp:RegularExpressionValidator
            ID="RegularExpressionValidator4" runat="server" 
            ErrorMessage="Invalid email format. Suggestion: someone@example.com"
            ControlToValidate="txtEmail" ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"></asp:RegularExpressionValidator>
                    
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Staff Password<br />
                    <asp:TextBox ID="txtStaffPassword" runat="server" MaxLength="8" TabIndex="6" TextMode="Password"></asp:TextBox>
                </td>
                <td class="auto-style5">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtStaffPassword" ErrorMessage="Staff Password is required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td class="auto-style26">
                     &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Staff NRIC<br />
                    <asp:TextBox ID="txtStaffNRIC" runat="server" MaxLength="12" TabIndex="7"></asp:TextBox>
                </td>
                <td class="auto-style5">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtStaffNRIC" ErrorMessage="Staff NRIC is required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td class="auto-style26">
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtStaffNRIC" ErrorMessage="Invalid Input" ValidationExpression="^[1-9]\d$"></asp:RegularExpressionValidator>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Staff DOB<br />
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="261px" TabIndex="8">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>
                </td>
                <td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style26">
                     &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">Staff Phone Number<br />
                    <asp:TextBox ID="txtStaffPhoneNumber" runat="server" MaxLength="12" TabIndex="9"></asp:TextBox>
                </td>
                <td class="auto-style5">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtStaffPhoneNumber" ErrorMessage="Staff Phone Number is required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
                <td class="auto-style26">
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtStaffPhoneNumber" ErrorMessage="Phone Number at least 10 number" MaximumValue="12" MinimumValue="10"></asp:RangeValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtStaffPhoneNumber" ErrorMessage="Invalid Input" ValidationExpression="^[1-9]\d$"></asp:RegularExpressionValidator>
                    
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
                <td class="auto-style1">
                    <asp:Button ID="btnCompanyCancel" runat="server" Text="Cancel" TabIndex="10" OnClick="btnCompanyCancel_Click" />
                </td>
                <td class="auto-style26" colspan="2">
                    <asp:Button ID="btnCompanySubmit" runat="server" Text="Submit" OnClick="btnCompanySubmit_Click1" TabIndex="11" />
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
