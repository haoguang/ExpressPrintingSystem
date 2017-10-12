<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="booking.aspx.cs" Inherits="ExpressPrintingSystem.Customer.booking" %>


<!DOCTYPE html>

<html>
<head>
    <style>
        @import url(http://fonts.googleapis.com/css?family=Montserrat);

/*basic reset*/

* {margin: 0; padding: 0;}



html {

	height: 100%;

	/*Image only BG fallback*/

	background: url('http://thecodeplayer.com/uploads/media/gs.png');

	/*background = gradient + image pattern combo*/

	background: 

		linear-gradient(rgba(196, 102, 0, 0.2), rgba(155, 89, 182, 0.2)), 

		url('http://thecodeplayer.com/uploads/media/gs.png');

}



body {

	font-family: montserrat, arial, verdana;

}

/*form styles*/

#msform {

	width: 1000px;

	margin: 50px auto;

	text-align: center;

	position: relative;

}

#msform fieldset {

	background: white;

	border: 0 none;

	border-radius: 3px;

	box-shadow: 0 0 15px 1px rgba(0, 0, 0, 0.4);

	padding: 20px 30px;

	

	box-sizing: border-box;

	width: 80%;

	margin: 0 10%;

	

	/*stacking fieldsets above each other*/

	position: absolute;

}

/*Hide all except first fieldset*/

#msform fieldset:not(:first-of-type) {

	display: none;

}

/*inputs*/

#msform input, #msform textarea, #rbtDocumentColor, #rbtDocumentSide, #ddlPaperType{

	padding: 15px;

	border: 1px solid #ccc;

	border-radius: 3px;

	margin-bottom: 10px;

	width: 100%;

	box-sizing: border-box;

	font-family: montserrat;

	color: #2C3E50;

	font-size: 15px;

}


/*buttons*/

#msform .action-button {

	width: 100px;

	background: #27AE60;

	font-weight: bold;

	color: white;

	border: 0 none;

	border-radius: 1px;

	cursor: pointer;

	padding: 10px 5px;

	margin: 10px 5px;

}

#msform .action-button:hover, #msform .action-button:focus {

	box-shadow: 0 0 0 2px white, 0 0 0 3px #27AE60;

}

/*headings*/

.fs-title {

	font-size: 15px;

	text-transform: uppercase;

	color: #2C3E50;

	margin-bottom: 10px;

}

.fs-subtitle {

	font-weight: normal;

	font-size: 13px;

	color: #666;

	margin-bottom: 20px;

}

/*progressbar*/

#progressbar {

	margin-bottom: 30px;

	overflow: hidden;

	/*CSS counters to number the steps*/

	counter-reset: step;

}

#progressbar li {

	list-style-type: none;

	color: white;

	text-transform: uppercase;

	font-size: 15px;

	width: 33.33%;

	float: left;

	position: relative;

}

#progressbar li:before {

	content: counter(step);

	counter-increment: step;

	width: 50px;

	line-height: 50px;

	display: block;

	font-size: 20px;

	color: #333;

	background: white;

	border-radius: 3px;

	margin: 0 auto 5px auto;

}

/*progressbar connectors*/

#progressbar li:after {

	content: '';

	width: 100%;

	height: 3px;

	background: white;

	position: absolute;

	left: -50%;

	top: 21px;

	z-index: -1; /*put it behind the numbers*/

}

#progressbar li:first-child:after {

	/*connector not needed before the first step*/

	content: none; 

}

/*marking active/completed steps green*/

/*The number of the step and the connector before it = green*/

#progressbar li.active:before,  #progressbar li.active:after{

	background: #27AE60;

	color: white;

}

        #rbtDocumentColor{
          padding-left: 70px;
          
        }

        #txtcustomerID{
            padding: 15px;

	margin-bottom: 10px;
    max-width:200px;
	font-family: montserrat;

	color: #2C3E50;

	font-size: 15px;
        }

    </style>
    <title></title>
</head>
<body>
    <form id="msform" runat="server">

	<!-- progressbar -->

	<ul id="progressbar">

		<li class="active">Upload Your File</li>

		<li>Request Detail</li>

		<li>COMFIRMATION</li>

  </ul>

	<!-- fieldsets -->

	<fieldset>

		<h2 class="fs-title">Upload Your File</h2>

		<h3 class="fs-subtitle"></h3>
        <asp:TextBox ID="txtcustomerID" runat="server" Text="jdsdf" Enabled="False">jdsdf</asp:TextBox>
        <br/>
        <asp:Label ID="lblUploadDocument" runat="server" Text="Upload Document"></asp:Label>
        <asp:FileUpload ID="FileUpload1" runat="server" />

        <asp:Label ID="lblPage" runat="server" Text=""></asp:Label><asp:Button ID="Button2" runat="server" Text="Button" />

        <asp:Label ID="Label1" runat="server" Text="Document Type"></asp:Label>
		

		<input type="button" name="next" class="next action-button" value="Next" />

	</fieldset>

	<fieldset>

		<h2 class="fs-title">Request Detail</h2>

		<h3 class="fs-subtitle"></h3>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Document Color" Font-Underline="True"></asp:Label>
        <asp:RadioButtonList ID="rbtDocumentColor" runat="server" RepeatDirection="Horizontal">
             <asp:ListItem>Color</asp:ListItem>
             <asp:ListItem>Non-Color</asp:ListItem>
        </asp:RadioButtonList>
      
        <br />
        <asp:Label ID="Label3" runat="server" Text="Both Side" Font-Underline="True"></asp:Label>
        <asp:RadioButtonList ID="rbtDocumentSide" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem>Single Side</asp:ListItem>
            <asp:ListItem>Double Side</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Paper Type" Font-Underline="True"></asp:Label>
        <asp:DropDownList ID="ddlPaperType" runat="server">
            <asp:ListItem>70gsm</asp:ListItem>
            <asp:ListItem>80gsm</asp:ListItem>
            <asp:ListItem>90gsm</asp:ListItem>
            <asp:ListItem>100gsm</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Document Quantity"></asp:Label>
        <asp:TextBox ID="txtDocumentQuantity" runat="server"></asp:TextBox>

        <asp:Label ID="Label6" runat="server" Text="Document Description"></asp:Label>
        <asp:TextBox ID="txtDocumentDescription" runat="server"></asp:TextBox>


       
		<input type="button" name="previous" class="previous action-button" value="Previous" />

		<input type="button" name="next" class="next action-button" value="Next" />

	</fieldset>

	<fieldset>

		<h2 class="fs-title">Personal Details</h2>

		<h3 class="fs-subtitle">We will never sell it</h3>

		<input type="text" name="fname" placeholder="First Name" />

		<input type="text" name="lname" placeholder="Last Name" />

		<input type="text" name="phone" placeholder="Phone" />

		<textarea name="address" placeholder="Address"></textarea>
        
        
		<input type="button" name="previous" class="previous action-button" value="Previous"/>
        <asp:Button ID="Button1" runat="server" Text="Submit" class="submit action-button" OnClick="Button1_Click"/>
		

	</fieldset>

</form>

<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<!-- jQuery easing plugin --> 
<script src="js/jquery.easing.min.js" type="text/javascript"></script> 

<script>
var current_fs, next_fs, previous_fs; //fieldsets

var left, opacity, scale; //fieldset properties which we will animate

var animating; //flag to prevent quick multi-click glitches



$(".next").click(function(){

	if(animating) return false;

	animating = true;

	

	current_fs = $(this).parent();

	next_fs = $(this).parent().next();

	

	//activate next step on progressbar using the index of next_fs

	$("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

	

	//show the next fieldset

	next_fs.show(); 

	//hide the current fieldset with style

	current_fs.animate({opacity: 0}, {

		step: function(now, mx) {

			//as the opacity of current_fs reduces to 0 - stored in "now"

			//1. scale current_fs down to 80%

			scale = 1 - (1 - now) * 0.2;

			//2. bring next_fs from the right(50%)

			left = (now * 50)+"%";

			//3. increase opacity of next_fs to 1 as it moves in

			opacity = 1 - now;

			current_fs.css({'transform': 'scale('+scale+')'});

			next_fs.css({'left': left, 'opacity': opacity});

		}, 

		duration: 800, 

		complete: function(){

			current_fs.hide();

			animating = false;

		}, 

		//this comes from the custom easing plugin

		easing: 'easeInOutBack'

	});

});



$(".previous").click(function(){

	if(animating) return false;

	animating = true;

	

	current_fs = $(this).parent();

	previous_fs = $(this).parent().prev();

	

	//de-activate current step on progressbar

	$("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

	

	//show the previous fieldset

	previous_fs.show(); 

	//hide the current fieldset with style

	current_fs.animate({opacity: 0}, {

		step: function(now, mx) {

			//as the opacity of current_fs reduces to 0 - stored in "now"

			//1. scale previous_fs from 80% to 100%

			scale = 0.8 + (1 - now) * 0.2;

			//2. take current_fs to the right(50%) - from 0%

			left = ((1-now) * 50)+"%";

			//3. increase opacity of previous_fs to 1 as it moves in

			opacity = 1 - now;

			current_fs.css({'left': left});

			previous_fs.css({'transform': 'scale('+scale+')', 'opacity': opacity});

		}, 

		duration: 800, 

		complete: function(){

			current_fs.hide();

			animating = false;

		}, 

		//this comes from the custom easing plugin

		easing: 'easeInOutBack'

	});

});



$(".submit").click(function(){

	return true;

});
    </script>


<!-- jQuery -->

<script src="http://thecodeplayer.com/uploads/js/jquery-1.9.1.min.js" type="text/javascript"></script>

<!-- jQuery easing plugin -->

<script src="http://thecodeplayer.com/uploads/js/jquery.easing.min.js" type="text/javascript"></script>
</body>
</html>
