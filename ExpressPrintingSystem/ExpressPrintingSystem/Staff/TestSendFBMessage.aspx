<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSendFBMessage.aspx.cs" Inherits="ExpressPrintingSystem.Staff.TestSendFBMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Send message to facebook test</h1>
    <form id="form1" defaultbutton="btnSubmit" runat="server">
    <div>
        <table>
            <tr>
                <td><asp:Label ID="lblRecipient" runat="server" Text="Recipient :"></asp:Label></td>
                <td><asp:TextBox ID="txtRecipient" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblMessage" runat="server" Text="Message :"></asp:Label></td>
                <td><asp:TextBox ID="txtMessage" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        <asp:Button ID="btnSubmit" runat="server" Text="Send" OnClick="btnSubmit_Click" />
    </div>
    </form>
</body>
</html>
