<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="SendMessage.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Notify.SendMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Send Message</h1>

    <table>
        <tr>
            <td>
                <asp:Label ID="lblTitle" runat="server" Text="Title :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ForeColor="Red" ErrorMessage="*Please enter a title."></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" Text="Message :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMessage" TextMode="MultiLine" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtMessage" ForeColor="Red" ErrorMessage="*Please enter a message"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTargetCustomer" runat="server" Text="Target Customer :"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="rblCustomerRange" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblCustomerRange_SelectedIndexChanged">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>Custom</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr runat="server" id="customRangeControl">
            <td>
            </td>
            <td>
                <asp:DropDownList ID="ddlParameter" runat="server">
                    <asp:ListItem>Birthday Month</asp:ListItem>
                    <asp:ListItem>Age</asp:ListItem>
                </asp:DropDownList><asp:Button ID="btnAddParameter" runat="server" Text="Add" OnClick="btnAddParameter_Click" CausesValidation="False" />
                <asp:gridview ID="gvParameter" runat="server" AutoGenerateColumns="false" OnRowDeleting="gvParameter_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="ParameterName" ReadOnly="true" HeaderText="Parameter" />
                            <asp:TemplateField HeaderText="Operator">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlOperator" runat="server">
                                        <asp:ListItem Value="<=">smaller than and equal to</asp:ListItem>
                                        <asp:ListItem Value="<">smaller than</asp:ListItem>
                                        <asp:ListItem Value="=">equal</asp:ListItem>
                                        <asp:ListItem Value=">">bigger than</asp:ListItem>
                                        <asp:ListItem Value=">=">bigger than and equal to</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValue" AutoPostBack="true" onFocus="this.select()" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:CommandField ShowDeleteButton="True" HeaderText="Remove" />
                            <asp:TemplateField HeaderText="Error Message">
                                <ItemTemplate>
                                    <asp:RequiredFieldValidator ID="rfvValue" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtValue" ErrorMessage="*Please Enter a value in the parameter."></asp:RequiredFieldValidator>
                                </ItemTemplate>
                            </asp:TemplateField>                           
                        </Columns>
                    </asp:gridview>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnSubmit" runat="server" Text="Send Message" OnClick="btnSubmit_Click" />
</asp:Content>

