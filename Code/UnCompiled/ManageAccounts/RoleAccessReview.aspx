<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RoleAccessReview.aspx.vb" Inherits="VANS.RoleAccessReview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Role Access Review</title>
<link href="~/styles/mainstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="rightTitle">
                    User Access Review &nbsp;
                    <asp:Label ID="Label1" runat="server">Role: </asp:Label>
                    <asp:Label ID="lblRoleName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="rightContent">
                    <asp:Label ID="lblInstructions" runat="server">Current nodes the role has permissions to.</asp:Label>
                    <br />
                    <br />
                    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                    </telerik:RadScriptManager>
                    <table width="500px">
                        <tr>
                            <td>
                                <telerik:RadTreeView ID="ControlTree" title="Folder Tree" runat="server" Skin="Windows7"
                                    AccessKey="T" TabIndex="1" DataValueField="NodeID" DataFieldID="NodeID" DataTextField="NodeName"
                                    DataFieldParentID="ParentID"  />
                                    <asp:Label ID="lblNoAccess" runat ="server" Visible ="false" CssClass ="valalert">Role has no node access.<br /><br /></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnReturn" runat ="server" text="Return" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
