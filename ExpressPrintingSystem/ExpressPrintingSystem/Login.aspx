<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ExpressPrintingSystem.Customer.Login" %>


<!DOCTYPE html>

<html>
    <head runat="server">
<style>

  

input[type=text], select {
    width: 100%;
   
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    border-radius: 4px;
    box-sizing: border-box;
}
    #btnSubmit {
    width: 100%;
    background-color: #4CAF50;
    color: white;
   
    margin: 8px 0;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    }


    div[class=login]{
    
   border-radius: 5px;
    background-color: #f2f2f2;
    padding: 20px;
}
    

    div[class=heard] {
        padding-top : 50px;
        padding-bottom:150px;
    }
    div[class=bottom] {
        
        border-radius: 5px;
    background-color: #f2f2f2;
   padding: 20px;
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
    #txtPassword {
    width: 100%;
   
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    border-radius: 4px;
    box-sizing: border-box;
    }

   
   
</style>

        <title></title>
        <link href="styles/bootstrap.min.css" rel="stylesheet" type="text/css"/>
    </head>
<body>
    <div class ="heard" style="height:30px; line-height:30px; text-align:center;">
        
        <asp:Image ID="Image1" runat="server" Height="88px" ImageUrl="~/Images/logo/big_logo.png" />
        <br />       
        <asp:Label ID="lblTitle" runat="server" Text="Login to Express printing" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Larger"></asp:Label>
    </div>
    <form id="form1" runat="server">
        <div class="login" style="width:350px; margin:0 auto;">
            
             <div class="btn-group" role="group" aria-label="Basic example">
                 <asp:Button ID="btnCustomer" runat="server" CssClass="btn btn-success" Text="Customer" OnClick="btnCustomer_Click" />
                 <asp:Button ID="btnCompany" runat="server" CssClass="btn btn-default" Text="Company" OnClick="btnCompany_Click" />
  
             </div>
            <br />
            
            <asp:Label ID="lblName" runat="server" Text="Email"></asp:Label>
            <br />
            <asp:TextBox ID="txtname" runat="server" Height="28px" ></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink1" runat="server">Forgot password ?</asp:HyperLink>
&nbsp;<asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Height="28px"></asp:TextBox>
            <br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Remember me next time" />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" runat="server" Text="Button" Height="28px" OnClick="btnSubmit_Click" />
               

        </div>
        <br/>
        <div class="bottom" style="width:350px; margin:0 auto; text-align:center;">
            <asp:Label ID="Label" runat="server" Text="New User?"></asp:Label>

            &nbsp;<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/SignUp.aspx">Sign Up</asp:HyperLink>

            </div>
    </form>
</body>
</html>
