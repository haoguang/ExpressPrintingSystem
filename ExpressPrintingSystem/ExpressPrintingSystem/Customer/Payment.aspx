<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ExpressPrintingSystem.Customer.Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
  
 

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
  #msform fieldset:not(:first-of-type) {

	display: none;

}
        #head {
        

        }

        </style>
    
    
    <title></title>
    <link href="../styles/bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server" class="auto-style1" method="POST">
     <fieldset class="main-fieldset">
    <div class="head">

         <h1>Please choose you payment method</h1>

                       <input type="radio" name="user-type" value="cash" id="rdoCash" onclick ="cCash()"/>Cash
                       <input type="radio" name="user-type" value="payment" id="rdoPhone" onclick ="cPayment"/>Payment
     </div>
     </fieldset>
<div>

    <fieldset class="firstpage-fieldset">

        <table style="width: 100%;">
            <tr>
                <td>cash</td>
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

        <fieldset class="secondpage-fieldset">

        <table style="width: 100%;">
            <tr>
                <td>Payment</td>
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
    
 
 </div>
    </form>


    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<!-- jQuery easing plugin --> 
<script src="js/jquery.easing.min.js" type="text/javascript"></script> 

    <script>

   $(function(){
  $("input[type=radio]").change(function(){
    if($(this).is(":checked")){
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
