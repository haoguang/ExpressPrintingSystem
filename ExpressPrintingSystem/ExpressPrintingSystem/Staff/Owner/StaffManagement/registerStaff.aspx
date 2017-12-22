<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="registerStaff.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.StaffManagement.registerStaff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Register Staff</h1>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label>

    <table>
        <tr>
            <td><asp:Label ID="lblName" runat="server" Text="Name :"></asp:Label></td>
            <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ForeColor="Red" ErrorMessage="Please fill up the the Name field."></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblEmail" runat="server" Text="Email :"></asp:Label></td>
            <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Please fill up the the Email field." Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ForeColor ="Red"  ErrorMessage="Invalid email format. Suggestion: someone@example.com"
            ControlToValidate="txtEmail" ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblNRIC" runat="server" Text="NRIC :"></asp:Label></td>
            <td><asp:TextBox ID="txtNRIC" runat="server" MaxLength="12" TabIndex="7"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="rfvNRIC" runat="server" ControlToValidate="txtNRIC" ForeColor="Red" ErrorMessage="Please fill up the the NRIC field." Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revNRIC" runat="server" ControlToValidate="txtNRIC" ForeColor="Red" ErrorMessage="IC must have 12 digit." ValidationExpression="\d{12}$" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPhoneNo" runat="server" Text="Phone No :"></asp:Label></td>
            <td><asp:TextBox ID="txtPhoneNo" runat="server"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="rfvPhoneNo" runat="server" ControlToValidate="txtPhoneNo" ForeColor="Red" ErrorMessage="Please fill up the the PhoneNo field." Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revPhoneNo" runat="server" ForeColor="Red" ControlToValidate="txtPhoneNo" ErrorMessage="Phone No format is invalid. It should be XXX-XXXXXXX." Display="Dynamic" ValidationExpression="^\(?([0-9]{3}|[0-9]{2})\)?[-. ]?([0-9]{3}|[0-9]{4})[-. ]?([0-9]{4})$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblBOD" runat="server" Text="Birthday :"></asp:Label></td>
            <td>
                <asp:Calendar ID="cldBOD" runat="server" BackColor="White" BorderColor="#999999" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" CellPadding="4" DayNameFormat="Shortest">
                            <DayHeaderStyle Font-Bold="True" Font-Size="7pt" BackColor="#CCCCCC" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <OtherMonthDayStyle ForeColor="#808080" />
                            <SelectedDayStyle BackColor="#666666" ForeColor="White" Font-Bold="True" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <WeekendDayStyle BackColor="#FFFFCC" />
                        </asp:Calendar>
            </td>
            <td></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPassword" runat="server" Text="Password :"></asp:Label></td>
            <td>
                <asp:CheckBox ID="chkPassSet" AutoPostBack="true" runat="server" Text ="set password now?" ToolTip="setting password in their own device when verify their account if it is not checked." OnCheckedChanged="chkPassSet_CheckedChanged" />
            </td>
        </tr>
        <tr id="passRow" runat="server">
            <td></td>
            <td>
                <asp:TextBox ID="txtPassword" placeholder="password" TextMode="Password" runat="server"></asp:TextBox><br />
                <asp:TextBox ID="txtConfirmPass" placeholder ="confirm password" TextMode="Password" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:CompareValidator ID="cvPassword" runat="server" ErrorMessage="Password does not match." ControlToValidate="txtConfirmPass" ControlToCompare="txtPassword" ForeColor="Red" Operator="Equal" Display="Dynamic"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="rfvPassword" ForeColor="red" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please fill in the password field." Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvConfirmPassword" ForeColor="red" runat="server" ControlToValidate="txtConfirmPass" ErrorMessage="Please fill in the confirm password field." Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
    </table>
    <asp:Button ID="btnSubmit" runat="server" Text="Register" OnClick="btnSubmit_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
</asp:Content>
