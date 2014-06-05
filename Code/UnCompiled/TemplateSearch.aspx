<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TemplateSearch.aspx.vb"
    Inherits="VANS.TemplateSearch" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Template List</title>
    <link href="../styles/mainstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <asp:UpdatePanel ID="upTest" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="prgLoadingStatus" runat="server" DynamicLayout="true">
                <ProgressTemplate>
                    <div id="overlay">
                        <div id="modalprogress">
                            <div id="theprogress">
                                Please Wait...
                                <asp:Image ID="imgLoading" runat="server" ImageAlign="AbsMiddle" ImageUrl="/images/page-loader.gif" />
                            </div>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" align="left" width="100%" summary="This table is for layout purposes only.">
                    <tr>
                        <td colspan="2" class="rightTitle">
                            <asp:Label ID="lblTitle" runat="server">Template - Search by Clinic</asp:Label>
                            <asp:Label ID="lblClinicID" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rightContent">
                            <table>
                                <tr>
                                    <td colspan="2" align="left">
                                        <%--<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true">
                                    <asp:ListItem Text="All" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="New" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp; --%>
                                        <asp:Button ID="btnAddTemplate" runat="server" Text="Add Template" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" Text="Search by Template" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnPreview" runat="server" Text="Preview Template" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnUpdateClinics" runat="server" Text="Download Latest Clinic Information" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="rightSubTitle">
                                        <asp:Label ID="lblClinicList" runat="server" Text="Clinics"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadGrid ID="rgdClinics" runat="server" Skin="WebBlue" GridLines="None" AutoGenerateColumns="false"
                                            EnableLinqExpressions="False" ShowGroupPanel="false" AllowSorting="true" AllowFilteringByColumn="true"
                                            ShowStatusBar="True" AllowPaging="true">
                                            <%-- <GroupPanel Enabled="true" PanelStyle-Height="50px" PanelStyle-Width="100%" PanelStyle-BackColor="#DFEEFF">
                                        <PanelStyle BackColor="#DFEEFF" Height="50px" Width="100%"></PanelStyle>
                                    </GroupPanel>
                                    <GroupingSettings CaseSensitive="false" />--%>
                                            <MasterTableView PageSize="10" PagerStyle-PrevPageText="Previous Page" PagerStyle-NextPageText="Next Page"
                                                PagerStyle-LastPageText="Last Page" PagerStyle-FirstPageText="First Page">
                                                <SortExpressions>
                                                    <telerik:GridSortExpression FieldName="Name" />
                                                </SortExpressions>
                                                <Columns>
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
                                            <ClientSettings AllowDragToGroup="True" Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
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
                                <%--                        <tr>
                            <td class="rightSubTitle" colspan="2">
                                <asp:Label ID="lblSubTitle" runat="server" Text="Templates"></asp:Label>
                            </td>
                        </tr>--%>
                                <tr>
                                    <td colspan="2" class="rightSubTitle">
                                        <asp:Label ID="lblTemplatesAssigned" runat="server" Text="Assigned Templates"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadGrid ID="rgdTemplatesAssigned" runat="server" Skin="WebBlue" GridLines="None"
                                            AutoGenerateColumns="false" EnableLinqExpressions="False" ShowGroupPanel="false"
                                            AllowSorting="true" AllowFilteringByColumn="true" ShowStatusBar="True" AllowPaging="true">
                                            <%--<GroupPanel Enabled="true" PanelStyle-Height="50px" PanelStyle-Width="100%" PanelStyle-BackColor="#DFEEFF">
                                        <PanelStyle BackColor="#DFEEFF" Height="50px" Width="100%"></PanelStyle>
                                    </GroupPanel>
                                    <GroupingSettings CaseSensitive="false" />--%>
                                            <MasterTableView PageSize="10" PagerStyle-PrevPageText="Previous Page" PagerStyle-NextPageText="Next Page"
                                                PagerStyle-LastPageText="Last Page" PagerStyle-FirstPageText="First Page">
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" Groupable="false">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbnRemove" runat="server" Text="Remove" CommandName="Update"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="TemplateClinicID" HeaderText="Template ID" UniqueName="TemplateClinicID"
                                                        SortExpression="TemplateClinicID" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TemplateID" HeaderText="Template ID" UniqueName="TemplateID"
                                                        SortExpression="TemplateID" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" Groupable="false" HeaderText="Template ID">
                                                        <HeaderStyle Width="90px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTemplateLink" runat="server"><a href='TemplateAddEdit.aspx?TemplateID=<%# Eval("TemplateID") %>'><%# Eval("TemplateID") %></a></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="Message" HeaderText="Message" UniqueName="Message"
                                                        SortExpression="Message">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Enabled by default?" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkEnabledDefault" runat="server" OnCheckedChanged="chkEnabledDefault_UpdateEnabled"
                                                                AutoPostBack="true" Checked='<%# Eval("DefaultEnabled") * -1 %>' />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <%--<telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"
                                                SortExpression="Status">
                                            </telerik:GridBoundColumn>--%>
                                                </Columns>
                                            </MasterTableView>
                                            <%-- <ClientSettings AllowDragToGroup="True">
                                        <ClientEvents OnFilterMenuShowing="filterMenuShowing" />
                                    </ClientSettings>--%>
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
                                        <asp:Label ID="lblTemplatesAvailable" runat="server" Text="Available Templates"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadGrid ID="rgdTemplatesAvailable" runat="server" Skin="WebBlue" GridLines="None"
                                            AutoGenerateColumns="false" EnableLinqExpressions="False" ShowGroupPanel="false"
                                            AllowSorting="true" AllowFilteringByColumn="true" ShowStatusBar="True" AllowPaging="true">
                                            <%--<GroupPanel Enabled="true" PanelStyle-Height="50px" PanelStyle-Width="100%" PanelStyle-BackColor="#DFEEFF">
                                        <PanelStyle BackColor="#DFEEFF" Height="50px" Width="100%"></PanelStyle>
                                    </GroupPanel>
                                    <GroupingSettings CaseSensitive="false" />--%>
                                            <MasterTableView PageSize="10" PagerStyle-PrevPageText="Previous Page" PagerStyle-NextPageText="Next Page"
                                                PagerStyle-LastPageText="Last Page" PagerStyle-FirstPageText="First Page">
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" Groupable="false">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbnAdd" runat="server" CommandName="Update" Text="Add"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="TemplateID" HeaderText="Template ID" UniqueName="TemplateID"
                                                        SortExpression="TemplateID" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" Groupable="false" HeaderText="Template ID">
                                                        <HeaderStyle Width="90px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTemplateLink" runat="server"><a href='TemplateAddEdit.aspx?TemplateID=<%# Eval("TemplateID") %>'><%# Eval("TemplateID") %></a></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="Message" HeaderText="Message" UniqueName="Message"
                                                        SortExpression="Message">
                                                    </telerik:GridBoundColumn>
                                                    <%--<telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"
                                                SortExpression="Status">
                                            </telerik:GridBoundColumn>--%>
                                                </Columns>
                                            </MasterTableView>
                                            <%-- <ClientSettings AllowDragToGroup="True">
                                        <ClientEvents OnFilterMenuShowing="filterMenuShowing" />
                                    </ClientSettings>--%>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
