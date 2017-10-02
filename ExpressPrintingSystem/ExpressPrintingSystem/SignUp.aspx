<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="ExpressPrintingSystem.Customer.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        div[class="head"] {
            padding-bottom:50px;
        }

        /*input.underlined
{
   border:0;
   border-bottom:solid 1px #000;

   /*outline:none;  /*prevents textbox highlight in chrome*/
/*}*/
         ::-webkit-input-placeholder {
   
   font-weight : bold;

}
      
         input[type=text], select {
 
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    border-radius: 5px;
 height : 20px;
    width:250px;
    
}

      html, body {
    height: 100%;
    background-color :gainsboro;
  }
  #tableContainer-1 {
    height: 100%;
    width: 100%;
    display: table;
  }
  #tableContainer-2 {
    vertical-align: middle;
    display: table-cell;
    height: 100%;
  }
  #myTable {
    margin: 0 auto;
  }

        .auto-style1 {
            width: 315px;
        }
        .auto-style2 {
            width: 401px;
            height: 1px;
        }
        .auto-style3 {
            width: 434px;
            height: -13px;
          
            
        }
        .auto-style4 {
            height: 20px;
        }
        .auto-style5 {
            width: 315px;
            height: 20px;
        }
        .auto-style9 {
            font-size :25px;
        }

        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="head">
        </div>

        
  <div id="tableContainer-1">
  <div id="tableContainer-2">
         <table id="myTable">
                <tr>
                    <td class="auto-style9">Sign Up Page</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style4">
                        <hr class="auto-style3" />
                    </td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
                        <br/>
                        <asp:TextBox ID="TextBox1" runat="server" BorderColor="#CCCCCC" placeholder="Name"></asp:TextBox>
                    </td>
                    <td class="auto-style11" rowspan="4">
                        <asp:Label ID="Label4" runat="server" Text="Date Of Birth"></asp:Label>
                        <br />
                        <br />
                        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" CellPadding="4" DayNameFormat="Shortest">
                            <DayHeaderStyle Font-Bold="True" Font-Size="7pt" BackColor="#CCCCCC" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <OtherMonthDayStyle ForeColor="#808080" />
                            <SelectedDayStyle BackColor="#666666" ForeColor="White" Font-Bold="True" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <WeekendDayStyle BackColor="#FFFFCC" />
                        </asp:Calendar>
                    </td>
                    <td class="auto-style11" rowspan="4">
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label2" runat="server" Text="Email Address"></asp:Label>
                        <br />
                        <asp:TextBox ID="TextBox2" runat="server" placeholder="xxx@mail.com"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>
                        <br />
                        <asp:TextBox ID="TextBox3" runat="server" placeholder="1233456"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label5" runat="server" Text="Phone Number"></asp:Label>
                        <br />
                        <asp:TextBox ID="TextBox5" runat="server" placeholder="xxx-xxxx"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label6" runat="server" Text="Contact Method"></asp:Label>
                        <br />
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" Height="16px" Width="95px">
                            <asp:ListItem>Whatsapp</asp:ListItem>
                            <asp:ListItem>Email</asp:ListItem>
                            <asp:ListItem>SMS</asp:ListItem>
                            <asp:ListItem>WeChat</asp:ListItem>
                        </asp:CheckBoxList>
                        <br/>
                    </td>
                    <td class="auto-style10">
                        &nbsp;</td>
                    <td class="auto-style11"></td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="whatsapp" />
                        <br/>
                    </td>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <br/>
                        <br/>
                    </td>
                    <td class="auto-style1">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
                        </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <br/>
                    </td>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <br/>
                    </td>
                    <td class="auto-style2">
                        &nbsp;</td>
                    <td class="auto-style2"></td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        &nbsp;</td>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

       </div>
       </div>

           
    </form>
</body>
</html>
