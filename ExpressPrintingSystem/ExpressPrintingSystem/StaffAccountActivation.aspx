<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffAccountActivation.aspx.cs" Inherits="ExpressPrintingSystem.StaffAccountActivation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div id="messageHolder" runat="server">
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </div>
    <div id="passwordForm" runat="server">
        <h1>Activate Staff account</h1>
        <form id="form1" defaultbutton="btnSubmit" runat="server">
            <div>
                <p><asp:Label ID="lblText" runat="server" Text="Please enter your new password here:"></asp:Label></p>
                <p><asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password" runat="server"></asp:TextBox></p>
                <p><asp:TextBox ID="txtConfirmPassword" placeholder="Confirm Password" TextMode="Password" runat="server"></asp:TextBox></p>
                <p>
                    <asp:RequiredFieldValidator ID="rfvPassword" ForeColor="red" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please fill in the password field." Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" ForeColor="red" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Please fill in the confirm password field." Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvPassword" runat="server" ErrorMessage="Password does not match." ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" ForeColor="Red" Operator="Equal" Display="Dynamic"></asp:CompareValidator>
                </p>
                <p><asp:Button ID="btnSubmit" runat="server" Text="Confirm" OnClick="btnSubmit_Click" /></p>
            </div>
         </form>
    </div>
    
</body>
</html>
