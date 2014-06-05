<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AppointmentsListMember.aspx.vb"
    Inherits="VANS.AppointmentsListMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Appointments List</title>
    <link href="../styles/mainstyle.css" rel="stylesheet" type="text/css" />
    <link href="../styles/calendar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/calendar.js"></script>
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
                                Loading Data...
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
                            <asp:Label ID="lblTitle" runat="server">Appointment List</asp:Label>
                            <asp:Label ID="lblName" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblSelectedPatientID" runat="server" Visible="false"></asp:Label>
                            <asp:Timer ID="tmrLoad" runat="server" Interval="100">
                            </asp:Timer>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="rightContent">
                            <table border="0" cellpadding="0" cellspacing="0" width="750px">
                                <asp:Panel ID="pnlWarning" runat="server" Visible="false">
                                    <asp:Label ID="lblWarning" runat="server">*Your account is currently not linked to a Vista user. Please contact the administrator.</asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="pnlAppointments" runat="server" Visible="false">
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="rgdGroupedAppointments" runat="server" Skin="WebBlue" GridLines="None"
                                                AutoGenerateColumns="false" EnableLinqExpressions="False" ShowGroupPanel="false"
                                                AllowSorting="true" AllowFilteringByColumn="false" ShowStatusBar="True" AllowPaging="true">
                                                <GroupPanel Enabled="false" PanelStyle-Height="50px" PanelStyle-Width="100%" PanelStyle-BackColor="#DFEEFF">
                                                    <PanelStyle BackColor="#DFEEFF" Height="50px" Width="100%"></PanelStyle>
                                                </GroupPanel>
                                                <GroupingSettings CaseSensitive="false" />
                                                <MasterTableView DataKeyNames="AppointmentGroupID" PageSize="10" PagerStyle-AlwaysVisible="true"
                                                    PagerStyle-PrevPageText="Previous Page" PagerStyle-NextPageText="Next Page" PagerStyle-LastPageText="Last Page"
                                                    PagerStyle-FirstPageText="First Page" ShowHeadersWhenNoRecords="true">
                                                    <NestedViewTemplate>
                                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td colspan="2" align="right">
                                                                    <telerik:RadGrid ID="rgdNestedItems" runat="server" Skin="WebBlue" AutoGenerateColumns="false"
                                                                        GridLines="None" OnItemDataBound="rgdNestedItems_ItemDatabound" AllowSorting="false">
                                                                        <MasterTableView AllowMultiColumnSorting="true">
                                                                            <SortExpressions>
                                                                                <telerik:GridSortExpression FieldName="AppointmentDate" SortOrder="Ascending" />
                                                                                <telerik:GridSortExpression FieldName="AppointmentTime" SortOrder="Ascending" />
                                                                            </SortExpressions>
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="VANSAppointmentID" HeaderText="Appointment ID"
                                                                                    Visible="false">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridDateTimeColumn AllowSorting="true" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                                                                                    DataField="AppointmentDate" SortExpression="AppointmentDate" UniqueName="AppointmentDate"
                                                                                    HeaderText="Date">
                                                                                </telerik:GridDateTimeColumn>
                                                                                <telerik:GridBoundColumn DataField="AppointmentTime" HeaderText="AppointmentTime"
                                                                                    UniqueName="AppointmentTime" SortExpression="AppointmentTime" Visible="true">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Clinic" UniqueName="Clinic" HeaderText="Clinic"
                                                                                    SortExpression="Clinic">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="ClinicID" UniqueName="ClinicID" HeaderText="ClinicID"
                                                                                    Visible="false">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="CurrentStatus" HeaderText="CurrentStatus" UniqueName="CurrentStatus"
                                                                                    SortExpression="CurrentStatus">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="type" HeaderText="Type" UniqueName="type" SortExpression="type">
                                                                                </telerik:GridBoundColumn>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                    </telerik:RadGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </NestedViewTemplate>
                                                    <SortExpressions>
                                                        <telerik:GridSortExpression FieldName="BeginDate" SortOrder="Descending" />
                                                    </SortExpressions>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbnRedirect" runat="server" CommandName="Update"><%#Eval ("AppointmentGroupID") %></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="AppointmentGroupID" Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridDateTimeColumn AllowSorting="true" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                                                            DataField="BeginDate" SortExpression="BeginDate" UniqueName="BeginDate" HeaderText="Begin Date">
                                                        </telerik:GridDateTimeColumn>
                                                        <telerik:GridDateTimeColumn AllowSorting="true" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                                                            DataField="EndDate" SortExpression="EndDate" UniqueName="EndDate" HeaderText="End Date">
                                                        </telerik:GridDateTimeColumn>
                                                        <telerik:GridBoundColumn DataField="DateSent" HeaderText="Date Sent" Visible="false">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <%--<ClientSettings AllowDragToGroup="True">
                                        <ClientEvents OnFilterMenuShowing="filterMenuShowing" />
                                    </ClientSettings>--%>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </asp:Panel>
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
