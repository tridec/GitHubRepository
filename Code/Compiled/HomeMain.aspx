<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HomeMain.aspx.vb" Inherits="VANS.HomeMain"
    Theme="" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <title>HomeMain</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="language" content="en" />
    <meta name="author" content=" " />
    <meta name="subject" content=" " />
    <meta name="keywords" content=" " />
    <meta name="datecreated" content=" " />
    <meta name="datereviewed" content=" " />
    <link href="styles/mainstyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function TimeOutRedirect() {
            try {
                if (top.location != location) {
                    top.location.href = document.location.href;
                }
            }
            catch (Exception) { }
        }
     
 
    </script>
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
<body onload="TimeOutRedirect();">
    <form id="form1" runat="server">
    <div id="ParentDivElement" style="height: 100%;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <div id="skiplink" class="hidden">
            <a href="#content">skip to page content</a>
        </div>
        <telerik:RadSplitter ID="MainSplitter" runat="server" Height="100%" Width="100%"
            Orientation="Horizontal" Skin="WebBlue">
            <telerik:RadPane ID="Header" runat="server" Height="80" MinHeight="80" MaxHeight="80"
                Scrolling="none" BackColor="#070048">
                <img src="./img/header.png" id="prBanner" height="80px" alt="VOA" title="VOA Projects" />
            </telerik:RadPane>
            <telerik:RadSplitBar ID="RadsplitbarTop" runat="server" CollapseMode="Forward" />
            <telerik:RadPane ID="MainPane" runat="server" Scrolling="none" MinWidth="500">
                <telerik:RadSplitter ID="NestedSplitter" runat="server" BorderSize="0" LiveResize="True"
                    PanesBorderSize="0" Skin="Outlook" SplitBarsSize="" Width="100%" Height="700">
                    <telerik:RadPane ID="LeftPane" runat="server" Width="200px" MinWidth="150" MaxWidth="400"
                        Height="100%" BackColor="#1E2D54">
                        <table bgcolor="#495A70" width="400px" border="0" cellpadding="0" cellspacing="0"
                            align="left">
                            <tr>
                                <td class="LeftTitle">
                                    <asp:LoginView ID="LoginView1" runat="server">
                                        <LoggedInTemplate>
                                            <a href="UserInformation.aspx" target="ContentPane">
                                                <asp:LoginName CssClass="hover" ID="LoginName1" Style="color: #FFF" runat="server" />
                                            </a>
                                            <%--<span style="color: #FFFFFF">&nbsp;|&nbsp;</span>--%>
                                        </LoggedInTemplate>
                                    </asp:LoginView>
                                    &nbsp;&nbsp;
                                    <%--<asp:LoginStatus ID="LoginStatus1" runat="server" ForeColor="White" />--%>
                                    <asp:LinkButton ID="lbLogout" Style="color: #FFF" runat="server">Logout</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <asp:Label ID="lblTreeInstructions" runat="server" AssociatedControlID="ControlTree">
                    <span class="hidden">
                    To access the tree, use alt-T.
                    </span>
                        </asp:Label>
                        <br />
                        <telerik:RadTreeView ID="ControlTree" title="Folder Tree" runat="server" Skin="Windows7"
                            AccessKey="T" TabIndex="1" ShowLineImages="true">
                            <Nodes>
                                <telerik:RadTreeNode ForeColor="White" Text="User Management" runat="server" Value="1">
                                </telerik:RadTreeNode>
                                <telerik:RadTreeNode ForeColor="White" Text="Appointment List" runat="server" Value="2">
                                </telerik:RadTreeNode>
                                <telerik:RadTreeNode ForeColor="White" Text="Template Management" runat="server"
                                    Value="3">
                                </telerik:RadTreeNode>
                                <telerik:RadTreeNode ForeColor="White" Text="Appointment List (Member)" runat="server"
                                    Value="4">
                                </telerik:RadTreeNode>
                                <telerik:RadTreeNode ForeColor="White" Text="Signature Management" runat="server"
                                    Value="5">
                                </telerik:RadTreeNode>
                                <%--<telerik:RadTreeNode ForeColor="White" Text="Services Test Page" runat="server" Value="4">
                                </telerik:RadTreeNode>--%>
                            </Nodes>
                        </telerik:RadTreeView>
                        <hr />
                    </telerik:RadPane>
                    <telerik:RadSplitBar ID="ContentSplitBar" runat="server" CollapseMode="Backward"
                        Visible="true" />
                    <telerik:RadPane ID="bpSkipNav" runat="server">
                        <span style="position: absolute;"><a name="#content"></a></span>
                        <telerik:RadSplitter ID="rsContentSplitter" runat="server">
                            <telerik:RadPane ID="ContentPane" runat="server" ContentUrl="~/blank.aspx" EnableEmbeddedBaseStylesheet="False"
                                EnableEmbeddedSkins="False" Index="2" Skin="">
                                <!-- Place the content of the pane here -->
                            </telerik:RadPane>
                        </telerik:RadSplitter>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
    </form>
</body>
</html>
