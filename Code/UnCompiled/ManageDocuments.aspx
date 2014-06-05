<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ManageDocuments.aspx.vb"
    Inherits="VANS.ManageDocuments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage Documents</title>
    <style type="text/css">
        html, body, form
        {
            height: 100%;
            margin: 0px;
            padding: 0px;
        }
    </style>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <link href="styles/mainstyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var a = Array;
        window.onload = function () {
            var inp = document.getElementById('inputSelect');
            var data = inp.value;
            if (data != "") {
                var rowsData = data.split(":");
                var i = 0;
                while (typeof (rowsData[i]) != "undefined") {
                    if (rowsData[i] != "") {
                        a[i] = rowsData[i];
                    }
                    i++;
                }
            }
        }
        function Selecting(sender, args) {
            var i = 0;
            while (typeof (a[i]) != "undefined") {
                if (a[i++] == args.get_itemIndexHierarchical()) {
                    args.set_cancel(true);
                }
            }
        }
        function UncheckMostCheckboxes(id, documentid) {
            var frm = document.forms[0];
            var status = document.getElementById(id).checked
            for (i = 0; i < frm.elements.length; i++) {
                if (frm.elements[i].type == "checkbox") {
                    frm.elements[i].checked = false
                }
            }
            document.getElementById(id).checked = status
            if (status == true) {
                document.getElementById('lblRowID').value = documentid
            }
            else {
                document.getElementById('lblRowID').value = ""
            }
        } 
    </script>
</head>
<body bgcolor="#DAE2E8">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
        width="100%">
        <tr valign="top">
            <td class="rightTitle">
                <b>Manage Documents</b>
            </td>
        </tr>
        <tr>
            <td class="rightContent">
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnAddDocument" runat="server" Text="Add New Document" />&nbsp;
                            <asp:Button ID="btnAddVersion" runat="server" Text="Add Version" />&nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return confirm('Once deleted, document will no longer be accessible.  Do you wish to continue?')" />&nbsp;
                            <asp:Button ID="btnPublish" runat="server" Visible="false" Text="Publish" OnClientClick="return confirm('Once published, document will be viewable.  Do you wish to continue?')" />&nbsp;
                            <asp:Button ID="btnUnPublish" runat="server" Visible="false" Text="UnPublish" OnClientClick="return confirm('Once unpublished, document will no longer be viewable.  Do you wish to continue?')" />&nbsp;
                            <asp:Button ID="btnUpdate" runat="server" Text="Update Title/Description" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblWarning" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblNodeID" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblEditOnly" runat="server" Visible="false"></asp:Label>
                            <input id="inputSelect" runat="server" type="hidden" />
                            <asp:HiddenField ID="lblRowID" runat="server" />
                            <telerik:RadGrid ID="rdgDocuments" runat="server" GridLines="None" AutoGenerateColumns="False"
                                GroupingEnabled="true" AllowMultiRowSelection="false" AllowMultiRowEdit="false"
                                Skin="Default" Width="100%">
                                <MasterTableView DataKeyNames="DocumentID, DocumentTitle" AllowSorting="true">
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocUploadIDValue" runat="server" Visible="false" Text='<%#Eval("DocumentID")%>'></asp:Label>
                                                <asp:Label ID="lblClientSelectColumn" runat="server" AssociatedControlID="chkSelected"><span class="hidden">Select document <%#Eval("DocumentTitle")%></span></asp:Label>
                                                <asp:CheckBox runat="server" ID="chkSelected" Width="5%"></asp:CheckBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Title">
                                            <ItemTemplate>
                                                <a href='DocumentView.aspx?DocumentUploadID=<%# Eval("DocumentUploadID") %>' target="_new">
                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("DocumentTitle") %>' for="<%=GridClientSelectColumn.ClientID%>"></asp:Label></a>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="DocumentDescription" HeaderText="Description"
                                            SortExpression="DocumentDescription">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Publish" Visible="false" HeaderText="Published"
                                            DataFormatString="" SortExpression="Publish">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Version" HeaderText="Version" SortExpression="Version">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UploadDate" HeaderText="Date Uploaded" SortExpression="UploadDate">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UserName" HeaderText="Uploaded By" SortExpression="UserName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="View History">
                                            <ItemTemplate>
                                                <a href='DocumentHistory.aspx?DocumentID=<%# Eval("DocumentID") %>?&NodeId=<%# Eval("NodeID") %>'>
                                                    History</a>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="False" />
                                    <ClientEvents OnRowSelecting="Selecting" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
