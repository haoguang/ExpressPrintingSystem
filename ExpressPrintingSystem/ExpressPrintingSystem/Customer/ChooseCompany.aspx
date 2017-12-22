<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChooseCompany.aspx.cs" Inherits="ExpressPrintingSystem.Customer.ChooseCompany" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        div[class=head] {
        padding: 200px;


	
    text-align :center;
	


	
        }

        html {
            background-color :azure;
        }


    </style>


    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="head">
        <asp:Label ID="lbltitle" runat="server" Text="Please Choose Company For Printing" Font-Bold="True" Font-Size="X-Large" Font-Underline="True"></asp:Label>
        <br />
        <br/>
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="CompanyName" DataValueField="CompanyID" Font-Size="X-Large">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PrintDBConnectionString %>" SelectCommand="SELECT [CompanyID], [CompanyName] FROM [Company]"></asp:SqlDataSource>
        
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Font-Size="X-Large" />
    </div>
    </form>
</body>
</html>
