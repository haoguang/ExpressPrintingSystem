<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="ExpressPrintingSystem.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
    <style>


        /*input.underlined
{
   border:0;
   border-bottom:solid 1px #000;

   /*outline:none;  /*prevents textbox highlight in chrome*/
/*}*/
        #form1 {

    width: 1000px;

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



	position: absolute;

}
      

         ::-webkit-input-placeholder {
   
   font-weight : bold;

}
      
   
      
        html {
            background-color :azure;
        }
      #txtName, #txtEmail , #txtPassword, #txtPhoneNumber , #btnSubmit, #btnCancel, #rblMethod , #Calendar1{

	

	border: 1px solid #ccc;

	border-radius: 3px;

	margin-bottom: 10px;

	
    
	box-sizing: border-box;

	font-family: montserrat;

	color: #2C3E50;

	font-size: 20px;

}

     
        .auto-style1 {
            width: 650px;
        }
        .auto-style4 {
            width: 647px;
        }
        .auto-style5 {
            width: 646px;
        }
        .auto-style9 {
            width: 635px;
        }
        .auto-style11 {
            width: 630px;
        }
        .auto-style12 {
            width: 1400px;
        }
        .auto-style13 {
            margin-top: 8px;
        }
         .auto-style15 {
            font-size : 25px;
        }

       

     
        .auto-style17 {
            width: 1820px;
        }
        .auto-style18 {
            width: 321px;
        }
        .auto-style19 {
            width: 319px;
        }
        .auto-style20 {
            width: 1400px;
            height: 68px;
        }
        .auto-style21 {
            width: 321px;
            height: 68px;
        }

     
        .auto-style23 {
            height: 68px;
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

     
    </style>

     <title></title>
    <link href="styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
       
    <form id="form1" runat="server">
    
        
       <div>
            <fieldset>
            <table style="width:100%;">
                
                              
                <tr>
                    <td class="auto-style15" colspan="5">
                        <div class="btn-group" role="group" aria-label="Basic example">
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnCustomer" runat="server" CssClass="btn btn-success" Text="Customer" OnClick="btnCustomer_Click" CausesValidation="False"  />
                 <asp:Button ID="btnCompany" runat="server" CssClass="btn btn-default" Text="Company" OnClick="btnCompany_Click" CausesValidation="False"  />
  
                 </div>
                    </td>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style15" colspan="5">Sign Up Page</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style1" colspan="5">
                        <hr/>
                    </td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style18">Name<br />
                        <asp:TextBox ID="txtName" runat="server" BorderColor="#CCCCCC" placeholder="Name"></asp:TextBox>
                        <br />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name is required!" ControlToValidate ="txtName" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style17" colspan="2">
                        &nbsp;</td>
                    <td class="auto-style9" rowspan="5"><br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                    <td class="auto-style11" rowspan="9">
                        <br />
                    </td>
                    <td class="auto-style11" rowspan="9"></td>
                    <td class="auto-style11" rowspan="9"></td>
                </tr>
                <tr>
                    <td class="auto-style18">Email<br />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="auto-style13" placeholder="xxx@xxx.com"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Email is required!" ControlToValidate="txtEmail" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style12" colspan="2">
                        <br />
                        <asp:RegularExpressionValidator
            ID="RegularExpressionValidator2" runat="server" 
            ErrorMessage="Invalid email format. Suggestion: someone@example.com"
            ControlToValidate="txtEmail" ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" ForeColor="Red">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style18">Date Of Birth<br />
                        <asp:TextBox ID="txtDOB" TextMode="Date" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDOB" ErrorMessage="Customer DOB is required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style12" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style18">
                        <br />
                        Password<br />
                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="8" TextMode="Password"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style12" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style21">Phone Number&nbsp;<asp:TextBox ID="txtPhoneNumber" runat="server" placeholder="xxx-xxxxx" MaxLength="11"></asp:TextBox>
                    </td>
                    <td class="auto-style23">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Phone Number is required!" ControlToValidate="txtPhoneNumber" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style20" colspan="2">
                        <asp:RegularExpressionValidator
            ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="Must be at leat 10-digit Contact Number" 
            ControlToValidate="txtPhoneNumber" ValidationExpression="^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$" ForeColor="Red">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style19" colspan="3">Contact Method<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Contact Method is required!" ControlToValidate="rblMethod" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <br />
                        <asp:RadioButtonList ID="rblMethod" runat="server" BorderStyle="None">
                            <asp:ListItem>whatsapp</asp:ListItem>
                            <asp:ListItem>E-mail</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="auto-style9" colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style9" colspan="5">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style12" colspan="4">
                        <br />
                        &nbsp;<asp:Button ID="btnSubmit" runat="server" OnClick="Button1_Click" Text="Submit" Height="30px" Width="100px" />
                        <br />
                    </td>
                    <td class="auto-style9">
                        <asp:Button ID="btnCancel" runat="server" Height="30px" Text="Cancel" Width="100px" OnClick="btnCancel_Click" CausesValidation="False" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9" colspan="5">
                        <br />
                    </td>
                </tr>
                </table>
            </fieldset>
        </div>
            
 
    </form>
    
</body>
</html>
