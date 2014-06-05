<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TemplateAddEdit.aspx.vb"
    Inherits="VANS.TemplateAddEdit" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Template</title>
    <link href="../styles/mainstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" align="left" width="100%" summary="This table is for layout purposes only.">
            <tr>
                <td colspan="2" class="rightTitle">
                    <asp:Label ID="lblTitle" runat="server">Template</asp:Label>
                    <asp:Label ID="lblReferURL" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblTemplateID" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="rightContent">
                    <table border="0" cellpadding="0" cellspacing="0" width="750px">
                        <tr>
                            <td class="rightSubTitle" colspan="2">
                                <asp:Label ID="lblTemplateMessage" runat="server" Text="Template Message"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtTemplateMessage" runat="server" TextMode="MultiLine" Height="100px"
                                    Width="750px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="revMessage" runat="server" ControlToValidate="txtTemplateMessage"
                                    ValidationExpression="^[\s\S]{0,2000}$" ErrorMessage="Maximum 2000 characters are allowed in the Message."
                                    Text="Maximum 2000 characters are allowed in the Message." ValidationGroup="Save"
                                    Display="Dynamic">Maximum 2000 characters are allowed in the Message.</asp:RegularExpressionValidator>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnDelete" runat="server" Text="Delete Template" Visible="false"
                                    OnClientClick="return confirm('Are you sure you wish to delete this template?');" />
                            </td>
                            <td align="right">
                                <%--                                <asp:Button ID="btnApprove" runat="server" Text="Approve" />
                                &nbsp;
                                <asp:Button ID="btnUnapprove" runat="server" Text="Reject" />--%>
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                &nbsp;
                                <asp:Button ID="btnPreview" runat="server" Text="Preview" ValidationGroup="Save" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" />
                                <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="Save" ShowMessageBox="true"
                                    Visible="true" ShowSummary="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="rightSubTitle">
                                <asp:Label ID="lblEditTitle" runat="server">Assigned Clinics</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgdClinicsAssigned" runat="server" Skin="WebBlue" GridLines="None"
                                    AutoGenerateColumns="false" EnableLinqExpressions="False" ShowGroupPanel="False"
                                    AllowSorting="true" AllowFilteringByColumn="true" ShowStatusBar="True" AllowPaging="true">
                                    <%--<GroupPanel Enabled="false" PanelStyle-Height="50px" PanelStyle-Width="100%" PanelStyle-BackColor="#DFEEFF">
                                        <PanelStyle BackColor="#DFEEFF" Height="50px" Width="100%"></PanelStyle>
                                    </GroupPanel>
                                    <GroupingSettings CaseSensitive="false" />--%>
                                    <MasterTableView PageSize="10" PagerStyle-PrevPageText="Previous Page" PagerStyle-NextPageText="Next Page"
                                        PagerStyle-LastPageText="Last Page" PagerStyle-FirstPageText="First Page">
                                        <%--<GroupHeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" OnCheckedChanged="chkHeader_CheckedChanged"
                                                AutoPostBack="true" />&nbsp;
                                            <asp:Label ID="lblHeader" runat="server" Text="Select All -"></asp:Label>&nbsp;
                                            <asp:Label ID="lblText" runat="server" Text='<%#"Location: " & DirectCast(Container, GridGroupHeaderItem).AggregatesValues("Location") %>'></asp:Label>
                                        </GroupHeaderTemplate>--%>
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="false" Groupable="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbnRemove" runat="server" Text="Remove" CommandName="Update"></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="TemplateClinicID" HeaderText="PK for Assigned Templates"
                                                UniqueName="TemplateClinicID" SortExpression="TemplateClinicID" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ClinicID" HeaderText="ClinicID" UniqueName="ClinicID"
                                                SortExpression="ClinicID">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Name" UniqueName="Name" SortExpression="Name">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PhysicalLocation" HeaderText="Physical Location"
                                                UniqueName="PhysicalLocation" SortExpression="PhysicalLocation">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Specialty" HeaderText="Specialty" UniqueName="Specialty"
                                                SortExpression="Specialty">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Enabled by default?" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEnabledDefault" runat="server" OnCheckedChanged="chkEnabledDefault_UpdateEnabled"
                                                        AutoPostBack="true" Checked='<%# Eval("DefaultEnabled") * -1 %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings AllowDragToGroup="True">
                                        <%--<ClientEvents OnFilterMenuShowing="filterMenuShowing" />--%>
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="rightSubTitle">
                                <asp:Label ID="lblAddTitle" runat="server">Available Clinics</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgdClinicsAvailable" runat="server" Skin="WebBlue" GridLines="None"
                                    AutoGenerateColumns="false" EnableLinqExpressions="False" ShowGroupPanel="False"
                                    AllowSorting="true" AllowFilteringByColumn="true" ShowStatusBar="True" AllowPaging="true">
                                    <%-- <GroupPanel Enabled="false" PanelStyle-Height="50px" PanelStyle-Width="100%" PanelStyle-BackColor="#DFEEFF">
                                        <PanelStyle BackColor="#DFEEFF" Height="50px" Width="100%"></PanelStyle>
                                    </GroupPanel>
                                    <GroupingSettings CaseSensitive="false" />--%>
                                    <MasterTableView PageSize="10" PagerStyle-PrevPageText="Previous Page" PagerStyle-NextPageText="Next Page"
                                        PagerStyle-LastPageText="Last Page" PagerStyle-FirstPageText="First Page">
                                        <%--<GroupHeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" OnCheckedChanged="chkHeader_CheckedChanged"
                                                AutoPostBack="true" />&nbsp;<asp:Label ID="lblHeader" runat="server" Text="Select All in Group"></asp:Label></GroupHeaderTemplate>--%>
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="false" Groupable="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbnAdd" runat="server" CommandName="Update" Text="Add"></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="ClinicID" HeaderText="ClinicID" UniqueName="ClinicID"
                                                SortExpression="ClinicID">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Name" UniqueName="Name" SortExpression="Name">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PhysicalLocation" HeaderText="Physical Location"
                                                UniqueName="PhysicalLocation" SortExpression="PhysicalLocation">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Specialty" HeaderText="Specialty" UniqueName="Specialty"
                                                SortExpression="Specialty">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings AllowDragToGroup="True">
                                        <%--<ClientEvents OnFilterMenuShowing="filterMenuShowing" />--%>
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
