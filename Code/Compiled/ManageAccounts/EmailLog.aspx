<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EmailLog.aspx.vb" Inherits="VANS.EmailLog"
    MasterPageFile="~/ManageAccounts/Admin.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <asp:SqlDataSource ID="sqlDatasource1" runat="server" ConnectionString="<%$ ConnectionStrings:VOAConnectionString %>"
        SelectCommand="EmailViewSelect" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="StartDate" Type="DateTime" />
            <asp:Parameter Name="EndDate" Type="DateTime" />
        </SelectParameters>
    </asp:SqlDataSource>
    <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
        style="width: 100%">
        <tr valign="top">
            <td class="rightSubTitle">
                <asp:Label ID="lblPageTitle" runat="server" Text="Sent Email Log"></asp:Label>
            </td>
        </tr>
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
                                    <asp:ListItem Value="RecordType">Record Type</asp:ListItem>
                                    <asp:ListItem Value="EmailSubject">Email Subject</asp:ListItem>
                                    <asp:ListItem Value="SentDate">Sent Date</asp:ListItem>
                                    <asp:ListItem Value="SentDateTime">Sent Time</asp:ListItem>
                                    <asp:ListItem Value="Email">User</asp:ListItem>
                                    <asp:ListItem Value="LastName">Last Name</asp:ListItem>
                                    <asp:ListItem Value="FirstName">First Name</asp:ListItem>
                                    <asp:ListItem Value="EmailFrom">Email From</asp:ListItem>
                                    <asp:ListItem Value="EmailTo">Email To</asp:ListItem>
                                    <asp:ListItem Value="EmailCC">Email CC</asp:ListItem>
                                    <asp:ListItem Value="EmailBCC">Email BCC</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="btnGroupBy" runat="server" Text="Group" Visible="true" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblFilter" AssociatedControlID="ddlFilter" runat="server" Visible="true">Filter Column:</asp:Label>
                                <asp:DropDownList ID="ddlFilter" runat="server" Visible="true">
                                    <asp:ListItem Value="None">None</asp:ListItem>
                                    <asp:ListItem Value="RecordType">Record Type</asp:ListItem>
                                    <asp:ListItem Value="EmailSubject">Email Subject</asp:ListItem>
                                    <asp:ListItem Value="SentDate">Sent Date</asp:ListItem>
                                    <asp:ListItem Value="Email">User</asp:ListItem>
                                    <asp:ListItem Value="LastName">Last Name</asp:ListItem>
                                    <asp:ListItem Value="FirstName">First Name</asp:ListItem>
                                    <asp:ListItem Value="EmailFrom">Email From</asp:ListItem>
                                    <asp:ListItem Value="EmailTo">Email To</asp:ListItem>
                                    <asp:ListItem Value="EmailCC">Email CC</asp:ListItem>
                                    <asp:ListItem Value="EmailBCC">Email BCC</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblFilterValue" AssociatedControlID="txtFilter" runat="server" Visible="true">Filter Value:</asp:Label>
                                <asp:TextBox ID="txtFilter" runat="server" Visible="true"></asp:TextBox>
                                <asp:Button ID="btnFilter" runat="server" Text="Filter" Visible="true" />
                            </asp:Panel>
                            <br />
                            <br />
                            <asp:Panel ID="pnlSelectEmailSentDate" runat="server">
                                <asp:Label ID="lblSelectEmailSentDateVal" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblSelectEmailSentDate" AssociatedControlID="txtSelectEmailSentDate"
                                    runat="server">Select Email Sent Date (mm/dd/yyyy):</asp:Label><font color="red"><asp:Label
                                        ID="lblSelectEmailSentDateReq" runat="server" Text="*"></asp:Label></font>
                                <asp:TextBox runat="server" ID="txtSelectEmailSentDate" name="strDate0" MaxLength="10"
                                    Width="85px" />
                                <a href="#calendar" onclick="javascript:calendar(null,null,null, '<%=txtSelectEmailSentDate.ClientID %>', 'divSelectEmailSentDate',null,null);return false;">
                                    <asp:Image runat="server" ID="Image5" ImageUrl="/images/calendar.gif" Height="21"
                                        Width="24" ToolTip="Date Picker Calendar - click to open" /></a> &nbsp;
                                &nbsp;
                                <a href="#Reset" onclick="javascript:document:getElementById('<%=txtSelectEmailSentDate.ClientID %>').value=''; document:getElementById('divSelectEmailSentDate').style.display='none';">
                                    <asp:Image runat="server" ID="imgReset" ImageUrl="../images/ResetButton.png" Height="22px"
                                        Width="48px" ToolTip="Click to clear date" /></a>
                                <div id="divSelectEmailSentDate" style="display: none">
                                </div>
                                <asp:RangeValidator runat="server" ID="RangeValidator1" ControlToValidate="txtSelectEmailSentDate"
                                    ValidationGroup="Submit" Type="Date" ErrorMessage="Please enter a valid date."
                                    ToolTip="Please enter a valid date." Display="Dynamic" MinimumValue="1950-1-1"
                                    MaximumValue="9999-12-31" Text="Please enter a valid date.">
                                </asp:RangeValidator>
                                <asp:RequiredFieldValidator runat="server" ID="RangeValidator2" ControlToValidate="txtSelectEmailSentDate"
                                    ValidationGroup="Submit" ErrorMessage="Email Sent Date is required." Text="Email Sent Date is required."
                                    Display="Dynamic">Email Sent Date is required.</asp:RequiredFieldValidator>
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
                            <telerik:RadGrid ID="rgEmail" runat="server" AutoGenerateColumns="False" GridLines="None"
                                DataSourceID="sqlDatasource1" Skin="WebBlue" MasterTableView-PagerStyle-Position="Top"
                                ShowGroupPanel="True" AllowSorting="True" EnableLinqExpressions="False" AllowFilteringByColumn="True"
                                AllowPaging="True">
                                <pagerstyle position="TopAndBottom" />
                                <grouppanel enabled="true" panelstyle-height="50px" panelstyle-width="100%" panelstyle-backcolor="#DFEEFF">
                                        <PanelStyle BackColor="#DFEEFF" Height="50px" Width="100%"></PanelStyle>
                                    </grouppanel>
                                <groupingsettings casesensitive="false" />
                                <mastertableview autogeneratecolumns="False" pagerstyle-prevpagetext="Previous Page"
                                    pagerstyle-nextpagetext="Next Page" pagerstyle-lastpagetext="Last Page" pagerstyle-firstpagetext="First Page"
                                    allowfilteringbycolumn="true">
                                        <NestedViewTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="formlabelalt" width="100%">
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td class="formlabelalt">
                                                                    <asp:Label ID="lblEmailBody" runat="server">Email Body:&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                                                                </td>
                                                                <td class="formlabelvalue" colspan="3">
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblEmailBodyVal" runat="server" Text='<%# Bind( "EmailBody" ) %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </NestedViewTemplate>
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="RecordType" HeaderText="Record Type" SortExpression="RecordType"
                                                UniqueName="RecordType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailSubject" HeaderText="Email Subject" SortExpression="EmailSubject"
                                                UniqueName="EmailSubject">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SentDate" DataFormatString="{0:d}" HeaderText="Sent Date"
                                                SortExpression="SentDate" UniqueName="SentDate">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SentDateTime" DataFormatString="{0:T}" DataType="System.DateTime"
                                                AllowFiltering="False" ItemStyle-Wrap="false" HeaderText="Sent Time" SortExpression="SentDateTime"
                                                UniqueName="SentDateTime">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Email" HeaderText="User" SortExpression="Email"
                                                UniqueName="Email">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                                                UniqueName="LastName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                                                UniqueName="FirstName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailFrom" HeaderText="Email From" SortExpression="EmailFrom"
                                                UniqueName="EmailFrom">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailTo" HeaderText="Email To" SortExpression="EmailTo"
                                                UniqueName="EmailTo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailCC" HeaderText="Email CC" SortExpression="EmailCC"
                                                UniqueName="EmailCC">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmailBCC" HeaderText="Email BCC" SortExpression="EmailBCC"
                                                UniqueName="EmailBCC">
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
