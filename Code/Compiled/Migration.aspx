<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Migration.aspx.vb" Inherits="VANS.Migration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <b>Remember to remove the LoginID identity before running this and reset it after running this.</b><br />
        <asp:Button ID="btnUsers" runat="server" Text="Migrate Users" /><br />
        <asp:Label ID="lblUsers" runat="server" Text=""></asp:Label>
        <hr />
        <asp:Button ID="btnTree" runat="server" Text="Migrate Tree" /><br />
        <asp:Label ID="lblTree" runat="server" Text=""></asp:Label>
        <hr />
        <asp:Button ID="btnRoles" runat="server" Text="Migrate Roles" /><br />
        <asp:Label ID="lblRoles" runat="server" Text=""></asp:Label>
        <hr />
        <asp:Button ID="btnRoleUsers" runat="server" Text="Migrate Users in Roles" /><br />
        <asp:Label ID="lblRoleUsers" runat="server" Text=""></asp:Label>
        <hr />
        <asp:Button ID="btnTreeRoles" runat="server" Text="Migrate Tree Roles" /><br />
        <asp:Label ID="lblTreeRoles" runat="server" Text=""></asp:Label>
        <hr />
        <asp:Button ID="btnTreeUsers" runat="server" Text="Migrate Tree Users" /><br />
        <asp:Label ID="lblTreeUsers" runat="server" Text=""></asp:Label>
        <hr />
        <asp:Button ID="btnDocuments" runat="server" Text="Migrate Documents" /><br />
        <asp:Label ID="lblDocuments" runat="server" Text=""></asp:Label>
        <hr />
        </div>
    </form>
</body>
</html>
