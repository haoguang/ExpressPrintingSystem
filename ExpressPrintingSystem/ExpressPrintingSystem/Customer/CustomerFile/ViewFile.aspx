<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewFile.aspx.cs" Inherits="ExpressPrintingSystem.Customer.CustomerFile.ViewFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <h1>Document Viewer</h1>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <h2>Printing requirement</h2>
    <table>
        <tr>
            <td><asp:Label ID="lblColorText" runat="server" Text="Color :"></asp:Label></td>
            <td><asp:Label ID="lblColor" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblBothSideText" runat="server" Text="Print Option :"></asp:Label></td>
            <td><asp:Label ID="lblBothSide" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblPaperTypeText" runat="server" Text="Paper Type :"></asp:Label></td>
            <td><asp:Label ID="lblPaperType" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblQuantityText" runat="server" Text="Print Quantity :"></asp:Label></td>
            <td><asp:Label ID="lblQuantity" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDescriptionText" runat="server" Text="Description :"></asp:Label></td>
            <td><asp:Label ID="lblDescription" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
    <GleamTech:DocumentViewer runat="server" 
        ID="documentViewer"
		Width="100%" Height="900px" /> 
        </div>
    </form>
</body>
</html>
