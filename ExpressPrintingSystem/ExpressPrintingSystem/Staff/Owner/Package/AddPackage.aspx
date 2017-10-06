<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="AddPackage.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Package.AddPackage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script type="text/javascript">
        $.noConflict();
        jQuery(document).ready(function ($) {
            $(document.getElementById("<%=txtSearchItem.ClientID%>")).on("keyup", function () {
                var search = this.value;
                $("option", document.getElementById("<%= ItemList.ClientID %>")).show().filter(function () {
                    return $(this).text().toLowerCase().indexOf(search.toLowerCase()) < 0;
    	        }).hide();
            }); 
        });

        function addItemWhenDoublcClick() {
            alert('Double Clicked!');
            __doPostBack('DoubleClickEvent', '');
        }

        function __doPostBack(eventTarget, eventArgument) {
            document.forms.__EVENTTARGET.value = eventTarget;
            document.forms.__EVENTARGUMENT.value = eventArgument;
            document.forms.submit();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Add Package</h1>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label>
    
        <table>
            <tr>
                <td><asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label></td>
                <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ForeColor="Red" ErrorMessage="Please fill up the the Name field."></asp:RequiredFieldValidator></td>
            </tr>
            <tr style="vertical-align:top;">
                <td style="vertical-align:top;"><asp:Label ID="lblItems" runat="server" Text="Package's Items:"></asp:Label></td>
                <td style="vertical-align:top;">
                    <asp:gridview ID="gvPackageItem" runat="server" AutoGenerateColumns="false" OnRowDeleting="gvPackageItem_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="itemID" ReadOnly="true" HeaderText="Item ID" Visible="false" />
                            <asp:BoundField DataField="itemName" ReadOnly="true" HeaderText="Item Name" />
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" onFocus="this.select()" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:CommandField ShowDeleteButton="True" HeaderText="Remove" />                          
                        </Columns>
                    </asp:gridview>
                </td>
                <td><asp:TextBox ID="txtSearchItem" placeholder="Search Item" TextMode="Search" Width="200px" runat="server" ClientIDMode="Static"/><br />
                    <asp:ListBox ID="ItemList" runat="server" ondblclick="addItemWhenDoublcClick();" ToolTip="Double-click to add item to package." Width="200px" DataSourceID="sdsItem" ClientIDMode="Static" DataTextField="ItemName" DataValueField="ItemID"></asp:ListBox>
                    <asp:SqlDataSource ID="sdsItem" runat="server" ConnectionString="<%$ ConnectionStrings:printDBServer %>" SelectCommand="SELECT [ItemID], [ItemName] FROM [Item]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;"><asp:Label ID="lblSupport" runat="server" Text="Support Files:"></asp:Label></td>
                <td><asp:CheckBoxList ID="cblSupport" runat="server"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblType" runat="server" Text="Type of Package:"></asp:Label></td>
                <td><asp:DropDownList ID="ddlType" runat="server" Height="17px" Width="128px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblEstPrice" runat="server" Text="Estimated Price (RM):"></asp:Label></td>
                <td><asp:Label ID="lblEstimatedPrice" runat="server" Text="0"/></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblPrice" runat="server" Text="Package Price (RM):"></asp:Label></td>
                <td><asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
            </tr>    
        </table>
            <asp:Button ID="btnSubmit" runat="server" Text="Add Package"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel"/>
</asp:Content>
