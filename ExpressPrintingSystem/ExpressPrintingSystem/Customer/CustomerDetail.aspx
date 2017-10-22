<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerDetail.aspx.cs" Inherits="ExpressPrintingSystem.Customer.CustomerDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 259px;
        }
             #form1 {

    width: 600px;

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

    font-size: 20px;

	position: absolute;

}
          html {
            background-color :azure;
        }
       
    #form1 input{

	

	border: 1px solid #ccc;

	border-radius: 3px;

	margin-bottom: 10px;

	
    
	box-sizing: border-box;

	font-family: montserrat;

	color: #2C3E50;

	font-size: 20px;

}
   
        .auto-style2 {
            width: 43px;
        }
        .auto-style3 {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
        <div>
            <table style="width: 100%;">
                <tr>
                    <td colspan="3">Customer Detail<br />
                    <hr/>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">User Name<br />
                        <br />
                    </td>
                    <td>&nbsp;</td>
                    <td>:<asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                        <br />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">User Email<br />
                        <br />
                    </td>
                    <td>:<asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label>
                        <br />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">User Date of birth<br />
                        <br />
                    </td>
                    <td>:<asp:Label ID="lblDOB" runat="server" Text="Label"></asp:Label>
                        <br />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">User Phone Number<br />
                        <br />
                    </td>
                    <td>:<asp:Label ID="lblPhoneNumber" runat="server" Text="Label"></asp:Label>
                        <br />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" class="auto-style3">User Contact Method<br />
                        <br />
                    </td>
                    <td class="auto-style3">:<asp:Label ID="lblContMethod" runat="server" Text="Label"></asp:Label>
                        <br />
                    </td>
                    <td class="auto-style3"></td>
                    <td class="auto-style3"></td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="Button1_Click" />
                    </td>
                    <td class="auto-style2">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" OnClick="Button2_Click" Text="Edit" 
                            />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            
        </div>
            </fieldset>
    </form>
</body>
</html>
