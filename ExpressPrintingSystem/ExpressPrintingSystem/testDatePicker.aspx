<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testDatePicker.aspx.cs" Inherits="ExpressPrintingSystem.testDatePicker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="scripts/jquery-3.2.1.min.js"></script>


    <script type="text/javascript" src="scripts/bootstrap/dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.datetimepicker.full.js"></script>
    <script type="text/javascript" src="scripts/jquery.datetimepicker.full.min.js"></script>
    <link href="scripts/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="scripts/bootstrap/dist/css/jquery.datetimepicker.css" rel="stylesheet" />
<script type="text/javascript">
            $(function () {
                $('#datetimepicker1').datetimepicker();
            });
        </script>
   
</head>
<body>
    <form id="form1" runat="server">

   <div class="container">
    <input id="datetimepicker1" type="text" />
</div>

    </form>

</body>
</html>
