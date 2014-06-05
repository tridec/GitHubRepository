<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserAccessReview.aspx.vb"
    Inherits="VANS.UserAccessReview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Access Review</title>
    <link href="~/styles/mainstyle.css" rel="stylesheet" type="text/css" />
    <%-- <script type="text/javascript">
        function treeExpandAllNodes() {
            alert("I am here!");
            var treeView = document.getElementById("ControlTree");
            var nodes = treeView.get_allNodes();
            for (var i = 0; i < nodes.length; i++) {

                if (nodes[i].get_nodes() != null) {
                    nodes[i].expand();
                }
            }
        }

    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="rightTitle">
                    User Access Review &nbsp;
                    <asp:Label ID="Label1" runat="server">User: </asp:Label>
                    <asp:Label ID="lblUserValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="rightContent">
<%--                    <asp:Label ID="lblInstructions" runat="server">Click a node to view what is granting permission to the node.<br /><br />Note*: Case Role Permissions are granted on the Case Permissions Management page.</asp:Label>
                    <br />
                    <br />--%>
                    <asp:Label ID="lblUserName" runat="server" Visible="false" />
                    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                    </telerik:RadScriptManager>
                    <table width="500px">
                        <tr>
                            <td>
                                <telerik:RadTreeView ID="ControlTree" title="Folder Tree" runat="server" Skin="Windows7"
                                    AccessKey="T" TabIndex="1" DataValueField="NodeID" DataFieldID="NodeID" DataTextField="NodeName"
                                    DataFieldParentID="ParentID" />
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnReturn" runat ="server" text="Return" />
                </td>
            </tr>
        </table>
        <%--  <script type="text/javascript">
            //call after page loaded
            window.onload = treeExpandAllNodes; 
        </script>--%>
    </div>
    </form>
</body>
</html>
