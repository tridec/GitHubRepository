<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FolderListTabs.aspx.vb"
    Inherits="VANS.FolderListTabs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        html, body, form
        {
            height: 100%;
            margin: 0px;
            padding: 0px;
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
            </asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <div style="height:90%">
        <telerik:RadTabStrip ID="rtsFolderListTabs" runat="server">
        </telerik:RadTabStrip>
        <telerik:RadSplitter ID="rsContentSplitter" runat="server" Height="100%" Width="100%" >
            <telerik:RadPane ID="rpContent" runat="server" >
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    </form>
</body>
</html>
