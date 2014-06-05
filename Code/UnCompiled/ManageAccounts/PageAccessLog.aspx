<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PageAccessLog.aspx.vb"
    Inherits="VANS.PageAccessLog" MasterPageFile="~/ManageAccounts/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
        style="width: 100%">
        <tr valign="top">
            <td class="rightSubTitle">
                <asp:Label ID="lblPageLogTitle" runat="server" Text="Page Log"></asp:Label>
            </td>
        </tr>
        <asp:SqlDataSource ID="sqlDatasource1" runat="server" ConnectionString="<%$ ConnectionStrings:VOAConnectionString %>"
            SelectCommand="PageLogSelect" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="StartDate" Type="DateTime" />
                <asp:Parameter Name="EndDate" Type="DateTime" />
            </SelectParameters>
        </asp:SqlDataSource>
        <tr>
            <td class="rightContent">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btn508" runat="server" Text="Keyboard Accessible View" Visible="true"
                                Enabled="true" />
                            <asp:Panel ID="pnl508" runat="server" Visible="false">
                                <asp:Label ID="lblGroup" AssociatedControlID="ddlGroup" runat="server">Group By:</asp:Label>
                                <asp:DropDownList ID="ddlGroup" runat="server" Visible="true">
                                    <asp:ListItem Value="None">None</asp:ListItem>
                                    <asp:ListItem Value="PageName">Page Name</asp:ListItem>
                                    <asp:ListItem Value="FirstName">First Name</asp:ListItem>
                                    <asp:ListItem Value="LastName">Last Name</asp:ListItem>
                                    <asp:ListItem Value="AccessDate">Access Date</asp:ListItem>
                                    <asp:ListItem Value="AccessTime">Access Time</asp:ListItem>
                                    <asp:ListItem Value="Email">Email</asp:ListItem>
                                    <asp:ListItem Value="UserName">User Name</asp:ListItem>
                                    <asp:ListItem Value="FullURL">Full URL</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="btnGroupBy" runat="server" Text="Group" Visible="true" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFilter" AssociatedControlID="ddlFilter" runat="server" Visible="true">Filter Column:</asp:Label>
                                <asp:DropDownList ID="ddlFilter" runat="server" Visible="true">
                                    <asp:ListItem Value="None">None</asp:ListItem>
                                    <asp:ListItem Value="PageName">Page Name</asp:ListItem>
                                    <asp:ListItem Value="FirstName">First Name</asp:ListItem>
                                    <asp:ListItem Value="LastName">Last Name</asp:ListItem>
                                    <asp:ListItem Value="AccessDate">Access Date</asp:ListItem>
                                    <asp:ListItem Value="Email">Email</asp:ListItem>
                                    <asp:ListItem Value="UserName">User Name</asp:ListItem>
                                    <asp:ListItem Value="FullURL">Full URL</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblFilterValue" AssociatedControlID="txtFilter" runat="server" Visible="true">Filter Value:</asp:Label>
                                <asp:TextBox ID="txtFilter" runat="server" Visible="true"></asp:TextBox>
                                <asp:Button ID="btnFilter" runat="server" Text="Filter" Visible="true" />
                            </asp:Panel>
                            <br />
                            <br />
                            <asp:Panel ID="pnlSelectPageAccessDate" runat="server">
                                <asp:Label ID="lblSelectPageAccessDateVal" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblSelectPageAccessDate" AssociatedControlID="txtSelectPageAccessDate"
                                    runat="server">Select Page Access Date (mm/dd/yyyy):</asp:Label><font color="red"><asp:Label
                                        ID="lblSelectPageAccessDateReq" runat="server" Text="*"></asp:Label></font>
                                <asp:TextBox runat="server" ID="txtSelectPageAccessDate" name="strDate0" MaxLength="10"
                                    Width="85px" />
                                <a href="#calendar" onclick="javascript:calendar(null,null,null, '<%=txtSelectPageAccessDate.ClientID %>', 'divSelectPageAccessDate',null,null);return false;">
                                    <asp:Image runat="server" ID="Image5" ImageUrl="/images/calendar.gif" Height="21"
                                        Width="24" ToolTip="Date Picker Calendar - click to open" /></a> &nbsp;
                                &nbsp;
                                <a href="#Reset" onclick="javascript:document:getElementById('<%=txtSelectPageAccessDate.ClientID %>').value=''; document:getElementById('divSelectPageAccessDate').style.display='none';">
                                    <asp:Image runat="server" ID="imgReset" ImageUrl="../images/ResetButton.png" Height="22px"
                                        Width="48px" ToolTip="Click to clear date" /></a>
                                <div id="divSelectPageAccessDate" style="display: none">
                                </div>
                                <asp:RangeValidator runat="server" ID="RangeValidator1" ControlToValidate="txtSelectPageAccessDate"
                                    ValidationGroup="Submit" Type="Date" ErrorMessage="Please enter a valid date."
                                    ToolTip="Please enter a valid date." Display="Dynamic" MinimumValue="1950-1-1"
                                    MaximumValue="9999-12-31" Text="Please enter a valid date.">
                                </asp:RangeValidator>
                                <asp:RequiredFieldValidator runat="server" ID="RangeValidator2" ControlToValidate="txtSelectPageAccessDate"
                                    ValidationGroup="Submit" ErrorMessage="Page Access Date is required." Text="Page Access Date is required."
                                    Display="Dynamic">Page Access Date is required.</asp:RequiredFieldValidator>
                                <asp:Label ID="lblddlDaysVal" runat="server" Visible="false"> </asp:Label>
                                <asp:Label ID="lblddlDays" AssociatedControlID="ddlDays" runat="server">Select Date Range + / - </asp:Label>
                                <asp:DropDownList ID="ddlDays" runat="server" Visible="true">
                                    <asp:ListItem Value="3" Text="3 days"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7 days"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14 days"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                                    Visible="true" Enabled="true" CausesValidation="true" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" ValidationGroup="Clear" Visible="true"
                                    Enabled="true" CausesValidation="true" />
                                <br />
                                <asp:ValidationSummary ID="vsSubmit" runat="server" ValidationGroup="Submit" ShowMessageBox="True"
                                    Visible="True" ShowSummary="False" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="rgdPageLog" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" GridLines="None" DataSourceID="sqlDatasource1" ShowGroupPanel="True"
                                Skin="WebBlue" AllowFilteringByColumn="True" EnableLinqExpressions="False">
                                <groupingsettings casesensitive="false" />
                                <grouppanel enabled="true" panelstyle-height="50px" panelstyle-width="100%" panelstyle-backcolor="#DFEEFF">
                                        <PanelStyle BackColor="#DFEEFF" Height="50px" Width="100%"></PanelStyle>
                                    </grouppanel>
                                <mastertableview autogeneratecolumns="False" datakeynames="PageLogID" allowsorting="true"
                                    pagesize="20" pagerstyle-prevpagetext="Previous Page" pagerstyle-nextpagetext="Next Page"
                                    pagerstyle-lastpagetext="Last Page" pagerstyle-firstpagetext="First Page">
                                        <PagerStyle AlwaysVisible="true" Position="Top" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="PageName" UniqueName="PageName" HeaderText="Page Name"
                                                SortExpression="PageName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FirstName" UniqueName="FirstName" HeaderText="First Name"
                                                SortExpression="FirstName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="LastName" UniqueName="LastName" HeaderText="Last Name"
                                                SortExpression="LastName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AccessDate" UniqueName="AccessDate" HeaderText="Access Date"
                                                SortExpression="AccessDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AccessTime" UniqueName="AccessTime" HeaderText="Access Time"
                                                SortExpression="AccessTime" DataType="System.DateTime" DataFormatString="{0:T}"
                                                AllowFiltering="false" HeaderStyle-Wrap="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Email" UniqueName="Email" HeaderText="Email"
                                                SortExpression="Email">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UserName" UniqueName="UserName" HeaderText="User Name"
                                                SortExpression="UserName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FullURL" HeaderText="Full URL" SortExpression="FullURL"
                                                UniqueName="FullURL">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <PagerStyle NextPageText="Next Page" LastPageText="Last Page" PrevPageText="Previous Page"
                                            FirstPageText="First Page"></PagerStyle>
                                    </mastertableview>
                                <clientsettings allowdragtogroup="True">
                                    </clientsettings>
                            </telerik:RadGrid>
                            <br />
                            <br />
                            <asp:CheckBox ID="IgnoreCheckBox" Text="Ignore paging (exports all pages)" runat="server">
                            </asp:CheckBox><asp:Label ID="lblIgnoreCheckBox" runat="server" AssociatedControlID="IgnoreCheckBox"
                                class="formlabelalt"><span class="hidden">Ignore paging (exports all pages)</span></asp:Label>
                            <br />
                            <br />
                            <asp:Button ID="ExportButton" Width="150px" Text="Export to Excel" OnClick="Export_Click"
                                runat="server"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
