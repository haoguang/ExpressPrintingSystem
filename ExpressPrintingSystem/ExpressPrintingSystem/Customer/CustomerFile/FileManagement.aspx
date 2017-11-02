<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileManagement.aspx.cs" Inherits="ExpressPrintingSystem.Customer.CustomerFile.FileManagement" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      
    <h1>View Document Room</h1>
                           
               <div style="padding:0;margin:0;width:70%;">
                 <asp:ListView ID="lvPackageItems" runat="server" ItemPlaceholderID="PlaceHolderDocumentList">
                     <LayoutTemplate>

                         <table style="width:100%;">
                                   <tr>
                                       <th style="display:none">Document ID</th>
                                       <th>Document Name</th>
                                       <th>Type</th>
                                       <th>Print Quantity</th>
                                       <th>Paper Type</th>
                                       <th>View</th>
                                       </tr>
                                       <asp:PlaceHolder runat="server" ID="PlaceHolderDocumentList" />
                                       </table>

                      </LayoutTemplate>
                    <ItemTemplate>
                                        <tr>
                                            <td style="display:none"><%#Eval ("DocumentID") %></td>
                                            <td><%#Eval ("DocumentName") %></td>
                                            <td><%#Eval ("DocumentType") %></td>
                                            <td><%#Eval ("Size") %></td>
                                            <td><%#Eval ("PageNumber") %></td>
                                            <td><asp:HyperLink ID="hplViewDocument" ToolTip="View Document" ImageUrl="~/Images/view.png" Target="_blank" NavigateUrl='<%# string.Format("ViewFile.aspx?documentID={0}",Eval("DocumentID"))%>' runat="server"></asp:HyperLink></td>
                                        </tr>
                     </ItemTemplate>
                      <EmptyDataTemplate>
                      <p style="font: italic bold 16px sans-serif;">There is no request received currently.</p>
                      </EmptyDataTemplate>
    
                                </asp:ListView>
                                </div>
        

    </form>
    <link rel="stylesheet" href="<%= Page.ResolveUrl("~/styles/expendableTable.css") %>" type="text/css" />
    <script src="<%= Page.ResolveUrl("~/scripts/jquery-1.6.4.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/scripts/jquery.signalR-2.2.2.js") %>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            /* used to hide or unhide sub table*/
            // THIS IS FOR HIDE ALL DETAILS ROW
            $(".childRow").hide();
            $(".btncolexp").click(function () {
                $(this).closest('tr').next('tr').toggle();
                //this is for change img of btncolexp button
                if ($(this).attr('class').toString() == "btncolexp collapserequest") {
                    $(this).addClass('expandrequest');
                    $(this).removeClass('collapserequest');
                }
                else {
                    $(this).removeClass('expandrequest');
                    $(this).addClass('collapserequest');
                }
            });

            /* SignalR function and connection*/
            function postBack(type) {
                __doPostBack("", type);
            }
            
            var con = $.hubConnection();
            var hub = con.createHubProxy('PrintingRequestHub');
            hub.on('displayTable', function () {
                postBack("ReloadTable");
            });

            con.start();

        });

        function sortTable(header) {
            __doPostBack("","SortTable;"+header);
        }
    </script>
</body>
</html>
