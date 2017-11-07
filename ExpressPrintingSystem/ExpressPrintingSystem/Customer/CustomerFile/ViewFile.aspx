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
    <GleamTech:DocumentViewer runat="server" 
        ID="documentViewer"
		Width="100%" Height="900px" /> 
        </div>
    </form>
</body>
</html>
