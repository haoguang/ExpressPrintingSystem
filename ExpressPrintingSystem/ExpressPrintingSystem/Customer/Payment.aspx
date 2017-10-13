<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ExpressPrintingSystem.Customer.Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
  
 

   <style>
  #msform fieldset:not(:first-of-type) {

	display: none;

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
 #form1 {

    width: 1000px;

	margin: 50px auto;

	position: relative;
        }
 div[class=head]{

     padding-bottom:20px;
 }

 
 </style>   
    
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="auto-style1" method="POST">
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>

   <div class="head">
       <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" Font-Size="X-Large">

           <asp:ListItem Value="company">cash</asp:ListItem>
           <asp:ListItem value="individual">payment</asp:ListItem>
       </asp:RadioButtonList>

      
    <%--<input type="radio" name="user-type" value="company" checked>Company<br>
    <input type="radio" name="user-type" value="individual">Individual<br>--%>
  </div>

  <fieldset class="company-fieldset">
    <table style="width: 100%;">
            <tr>
                <td><h1>Cash</h1></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><hr/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Cash<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
  </fieldset>

  <fieldset class="individual-fieldset">
     <table style="width: 100%;">
            <tr>
                <td><h1>Payment</h1></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><hr/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Choose Payment Method<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
  </fieldset>

  
 </form>

<script>
    $(function () {
        $("input[type=radio]").change(function () {
            if ($(this).is(":checked")) {
                $("fieldset:not(.main-fieldset)").hide();//hide everything
                $(this).parent().show();//show this ones container
                $("fieldset[class^='" + $(this).val() + "']").show(); //show the matching fieldset
                $("fieldset[class^='" + $(this).val() + "']>input:checked").change();//show children as required.
            }
        });
        $("input[value=company]").change();
    });

    </script>
    
</body>
</html>
