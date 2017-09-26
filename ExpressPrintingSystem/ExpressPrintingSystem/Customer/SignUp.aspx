<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="ExpressPrintingSystem.Customer.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 159px;
        }
        .auto-style2 {
            width: 183px;
        }
        .auto-style3 {
            width: 183px;
            height: 23px;
        }
        .auto-style4 {
            width: 159px;
            height: 23px;
        }
        .auto-style5 {
            height: 23px;
        }
        .auto-style6 {
            width: 183px;
            height: 25px;
        }
        .auto-style7 {
            width: 159px;
            height: 25px;
        }
        .auto-style8 {
            height: 25px;
        }
        .auto-style9 {
            width: 183px;
            height: 26px;
        }
        .auto-style10 {
            width: 159px;
            height: 26px;
        }
        .auto-style11 {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>


            <table style="width:100%;">
                <tr>
                    <td class="auto-style3">Sign Up Page</td>
                    <td class="auto-style4"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <hr />
                    </td>
                    <td class="auto-style4"></td>
                    <td class="auto-style5"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">Customer Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style9">Customrt Email&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                    <td class="auto-style10">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style11"></td>
                    <td class="auto-style11"></td>
                </tr>
                <tr>
                    <td class="auto-style2">Customer Password&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">Customer Date Of Birth&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                    <td class="auto-style1">
                        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px">
                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                            <TodayDayStyle BackColor="#CCCCCC" />
                        </asp:Calendar>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">Customer Phone Number&nbsp;&nbsp;&nbsp; :</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style6">Customer Contact Method&nbsp; :</td>
                    <td class="auto-style7">
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </td>
                    <td class="auto-style8"></td>
                    <td class="auto-style8"></td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style1">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>


        </div>
    </form>
</body>
</html>
