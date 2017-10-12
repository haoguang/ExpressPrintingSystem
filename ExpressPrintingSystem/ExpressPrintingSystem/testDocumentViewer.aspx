<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testDocumentViewer.aspx.cs" Inherits="ExpressPrintingSystem.testDocumentViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <GleamTech:DocumentViewer runat="server" 
        ID="documentViewer"
		Width="100%" 
		Height="600"
		Document="~/File/chapter-4.docx" />
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="BtnButton" runat="server" Text="Simple Button" OnClick="BtnButton_Click" />
    </div>
    </form>
</body>
</html>
