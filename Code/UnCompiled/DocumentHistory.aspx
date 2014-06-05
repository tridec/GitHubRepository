<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DocumentHistory.aspx.vb"
    Inherits="VANS.DocumentHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Document History</title>
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
</head>
<body bgcolor="#DAE2E8">
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
        width="100%">
        <tr valign="top">
            <td class="rightTitle">
                History
            </td>
        </tr>
        <tr>
            <td class="rightContent">
                <br />
                <table width="800px">
                    <tr>
                        <td>
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>
                            <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
                                width="100%">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnReturn" runat="server" Text="Return to Document List" />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td class="rightSubTitle">
                                        <b>Document Version History</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="rdgVersion" runat="server" GridLines="None" AutoGenerateColumns="False"
                                            Skin="Default" Width="100%">
                                            <MasterTableView>
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Title">
                                                        <ItemTemplate>
                                                            <a href='DocumentView.aspx?DocumentUploadID=<%# Eval("DocumentUploadID") %>' target="_new">
                                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("DocumentTitle") %>'></asp:Label></a>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="Version" HeaderText="Version">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="UploadDate" HeaderText="Date Uploaded">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="UserName" HeaderText="Uploaded By">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                        <br />
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td class="rightSubTitle">
                                        <b>Document History</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="rdgDocuments" runat="server" GridLines="None" AutoGenerateColumns="False"
                                            Skin="Default" Width="100%">
                                            <MasterTableView>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Description" HeaderText="Description">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ActionDateTime" HeaderText="Date ">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="UserName" HeaderText="Performed By">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
