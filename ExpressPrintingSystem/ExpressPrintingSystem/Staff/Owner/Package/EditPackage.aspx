<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="EditPackage.aspx.cs" Inherits="ExpressPrintingSystem.Staff.Owner.Package.EditPackage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            __doPostBack('DoubleClickEvent', '');
        }

        function __doPostBack(eventTarget, eventArgument) {
            document.forms.__EVENTTARGET.value = eventTarget;
            document.forms.__EVENTARGUMENT.value = eventArgument;
            document.forms.submit();
        }

        function ValidateCheckBoxList(sender, args) {
        var checkBoxList = document.getElementById("<%=cblSupport.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphStaffContent" runat="server">
    <h1>Add Package</h1>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label>
    
        <table>
            <tr>
                <td><asp:Label ID="lblID" runat="server" Text="Package ID:"></asp:Label></td>
                <td><asp:Label ID="lblPackageID" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label></td>
                <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ForeColor="Red" ErrorMessage="Please fill up the the Name field."></asp:RequiredFieldValidator></td>
            </tr>
            <tr style="vertical-align:top;">
                <td style="vertical-align:top;"><asp:Label ID="lblItems" runat="server" Text="Package's Items:"></asp:Label></td>
                <td style="vertical-align:top;">
                    <asp:gridview ID="gvPackageItem" runat="server" AutoGenerateColumns="false" OnRowDeleting="gvPackageItem_RowDeleting" OnRowDataBound="gvPackageItem_RowDataBound" OnRowDeleted="gvPackageItem_RowDeleted">
                        <Columns>
                            <asp:BoundField DataField="itemID" ReadOnly="true" HeaderText="Item ID" Visible="false" />
                            <asp:BoundField DataField="itemName" ReadOnly="true" HeaderText="Item Name" />
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" AutoPostBack="true" onFocus="this.select()" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:CommandField ShowDeleteButton="True" HeaderText="Remove" />                          
                        </Columns>
                    </asp:gridview>
                </td>
                <td><asp:TextBox ID="txtSearchItem" placeholder="Search Item" AutoComplete="off" TextMode="Search" Width="200px" runat="server" ClientIDMode="Static"/><br />
                    <asp:ListBox ID="ItemList" runat="server" ondblclick="addItemWhenDoublcClick();" ToolTip="Double-click to add item to package." Width="200px" DataSourceID="sdsItem" ClientIDMode="Static" DataTextField="ItemName" DataValueField="ItemID"></asp:ListBox>
                    <asp:SqlDataSource ID="sdsItem" runat="server" ConnectionString="<%$ ConnectionStrings:printDBServer %>" SelectCommand="SELECT [ItemID], [ItemName] FROM [Item]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblType" runat="server" Text="Type of Package:"></asp:Label></td>
                <td><asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" Height="17px" Width="128px" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr id="trSupport" runat="server">
                <td style="vertical-align:top;"><asp:Label ID="lblSupport" runat="server" Text="Support Files:"></asp:Label></td>
                <td><asp:CheckBoxList ID="cblSupport" runat="server"></asp:CheckBoxList></td>
                <td>
                    <asp:CustomValidator ID="cvSupport" ForeColor="Red" runat="server" ClientValidationFunction="ValidateCheckBoxList" ErrorMessage="Please select at least 1 format type"></asp:CustomValidator>
                </td>
            </tr>
            <tr id="trPricePerPaper" runat="server">
                <td><asp:Label ID="lblPricePerPaper" runat="server" Text="Price for each Paper (RM):"></asp:Label></td>
                <td><asp:TextBox ID="txtPricePerPaper" runat="server"></asp:TextBox></td>
                <td>
                    <asp:CompareValidator ID="cvValidValuePricePrinting" runat="server" Operator="GreaterThan" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPricePerPaper" ValueToCompare="0" ErrorMessage="Price cannot be less than 0."/>
                    <asp:CompareValidator ID="cvTypePricePrinting" runat="server" Operator="DataTypeCheck" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPricePerPaper" Type="Currency" ErrorMessage="Please key in a valid number."/>
                    <asp:RequiredFieldValidator ID="rfvPricePrinting" runat="server" ControlToValidate="txtPricePerPaper" ForeColor="Red" Display="Dynamic" ErrorMessage="Price per Paper field cannot be empty."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblEstPrice" runat="server" Text="Estimated Price (RM):"></asp:Label></td>
                <td><asp:Label ID="lblEstimatedPrice" runat="server" Text="0"/></td>               
            </tr>
            <tr>
                <td><asp:Label ID="lblPrice" runat="server" Text="Package Price (RM):"></asp:Label></td>
                <td><asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
                <td>
                    <asp:CompareValidator ID="cvValidValuePrice" runat="server" Operator="GreaterThanEqual" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPrice" ValueToCompare="0" ErrorMessage="Price cannot be less than 0."/>
                    <asp:CompareValidator ID="cvTypePrice" runat="server" Operator="DataTypeCheck" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPrice" Type="Currency" ErrorMessage="Please key in a valid number."/>
                    <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice" ForeColor="Red" Display="Dynamic" ErrorMessage="Price field cannot be empty."></asp:RequiredFieldValidator>
                </td>
            </tr>    
        </table>
            <asp:Button ID="btnSubmit" runat="server" Text="Edit Package" OnClick="btnSubmit_Click"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
</asp:Content>
