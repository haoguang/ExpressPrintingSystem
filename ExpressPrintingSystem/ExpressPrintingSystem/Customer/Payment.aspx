<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ExpressPrintingSystem.Customer.Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
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
    <div class="btn-group" role="group" aria-label="Basic example">
                 <asp:Button ID="btnCustomer" runat="server" CssClass="btn btn-success" Text="Customer" OnClick="btnCustomer_Click"  />
                 <asp:Button ID="btnCompany" runat="server" CssClass="btn btn-default" Text="Company" OnClick="btnCompany_Click"  />
  
     </div>


        <asp:Label ID="Label1" runat="server" Text="Payment"></asp:Label>
    
    </div>
    </form>
</body>
</html>
