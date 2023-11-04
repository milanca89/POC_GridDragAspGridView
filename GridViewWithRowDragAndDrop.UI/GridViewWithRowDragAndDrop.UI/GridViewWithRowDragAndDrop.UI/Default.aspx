<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GridViewWithFilter.UI._Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" href="../Assets/default.css" rel="stylesheet" type="text/css" media="screen"
        runat="server" />
    <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.tablednd.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
        <script type="text/javascript">

            $(document).ready(function () {
                $("#<%= grdViewOutstanding.ClientID %>").tableDnD({
                    onDragClass: "DragTableRow",
                    onDrop: function (table, row) {

                        var rows = table.tBodies[0].rows, newOrder = '', dragged = '';
                        dragged = $('#<%= eleDragged.ClientID %>').val();
                        dragged += row.cells[10].innerText == undefined ? row.cells[10].textContent : row.cells[10].innerText + ":";
                        for (var i = 1; i < rows.length; i++) {
                            newOrder += rows[i].cells[10].innerText == undefined ? rows[i].cells[10].textContent : rows[i].cells[10].innerText + ":";
                        }
                        $('#<%= eleNewSeq.ClientID %>').val(newOrder);
                        $('#<%= eleDragged.ClientID %>').val(dragged);
                    }
                });
            });
            </script>

    <div>
        <h1>
            GridView With Row Drag And Drop</h1>
    </div>
    <table style="width:100%">
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="upnlOutstanding" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <ContentTemplate>
                        <h4>
                            Outstanding Orders</h4>
                        <br />

                        <asp:GridView ID="grdViewOutstanding" runat="server" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#999999" BorderStyle="Solid" CellPadding="5" ForeColor="Black"
                            GridLines="Both" AllowPaging="True" CellSpacing="1" EmptyDataText="No outstanding orders found"
                            CssClass="Grid" PageSize="10" AllowSorting="true" OnPageIndexChanging="grdViewOutstanding_PageIndexChanging"
                             >
                            <FooterStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:BoundField DataField="SequenceId" HeaderText="SequenceId"  />
                                <asp:BoundField DataField="Item" HeaderText="Item" />
                                <asp:BoundField DataField="Order" HeaderText="Order" />
                                <asp:BoundField DataField="Line" HeaderText="Line" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:BoundField DataField="ToLocation" HeaderText="ToLocation" />
                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                <asp:BoundField DataField="RegDate" HeaderText="RegDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Location" HeaderText="Location" />
                                <asp:BoundField DataField="AllocQty" HeaderText="AllocQty" />
                                <asp:BoundField DataField="RecordId" HeaderText="RecordId" />
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" CssClass="pgr" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
                <asp:Button ID="btnUpdateOrderSequence" runat="server" OnClick="btnUpdateOrderSequence_Click" Text="Update Order Sequence" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>

   <label for="eleNewSeq">New Sequence </label> <input type="text" value="" runat="server" id="eleNewSeq" style="width:100%"/>
   <label for="eleDragged">Record Dragged </label>  <input type="text" value="" runat="server" id="eleDragged" style="width:100%" />

    </form>
</body>
</html>
