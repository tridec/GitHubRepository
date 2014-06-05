<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AppointmentsList.aspx.vb"
    Inherits="VANS.AppointmentsList" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Appointments List</title>
    <link href="../styles/mainstyle.css" rel="stylesheet" type="text/css" />
    <link href="../styles/calendar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/calendar.js"></script>
    <script type="text/javascript">
        function PleaseWaitButton() {
            //document.forms[0].submit();
            window.setTimeout("DisableButton()", 0);
        }
        function DisableButton() {
            if (Page_ClientValidate("Save")) {
                //alert("Valid");
                document.getElementById('btnCreateLetter').disabled = true;
            }
        } 
    </script>
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
            <table border="0" cellpadding="0" cellspacing="0" align="left" width="100%" summary="This table is for layout purposes only.">
                <tr>
                    <td colspan="2" class="rightTitle">
                        <asp:Label ID="lblTitle" runat="server">Appointment List</asp:Label>
                        <asp:LinkButton ID="lbnTitle" runat="server" Text="Appointment List" Visible="false"
                            ForeColor="White"></asp:LinkButton>
                        <asp:Label ID="lblTitleName" runat="server"></asp:Label>
                        <asp:Label ID="lblName" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblSelectedPatientID" runat="server" Visible="false"></asp:Label>
                        <%--<asp:DropDownList ID="ddlClinics" runat="server" DataTextField="name" DataValueField="id"
                                Visible="false" />--%>
                    </td>
                    <tr>
                        <td colspan="2" class="rightContent">
                            <table border="0" cellpadding="0" cellspacing="0" width="750px">
                                <asp:Panel ID="pnlPatient" runat="server">
                                    <tr>
                                        <td colspan="2" class="rightSubTitle">
                                            <asp:Label ID="lblPatient" runat="server" Text="Select Patient"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblSSN" runat="server">Enter Patient by SSN, 'LAST,FIRST', A1234(Last initial + last four SSN): </asp:Label>
                                            <br />
                                            <br />
                                            <asp:TextBox ID="txtSSN" runat="server" Width="250px"></asp:TextBox>&nbsp;&nbsp;<asp:Button
                                                ID="btnFind" runat="server" Width="80px" Text="Find" ValidationGroup="Find" />
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtSSN" ID="revSSN"
                                                ValidationGroup="Find" ValidationExpression="^[\s\S]{2,}$" runat="server" ErrorMessage="Minimum 2 characters required."
                                                Text="Minimum 2 characters required." ToolTip="Minimum 2 characters required."></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="rfvSSN" runat="server" Display="Dynamic" ControlToValidate="txtSSN"
                                                ValidationGroup="Find" ErrorMessage="Field is required." Text="Field is required."
                                                ToolTip="Field is required." />
                                            <br />
                                            <br />
                                            <asp:ValidationSummary ID="vsFind" runat="server" ValidationGroup="Find" ShowMessageBox="True"
                                                Visible="True" ShowSummary="False" />
                                            <telerik:RadGrid ID="rgdPerson" runat="server" Skin="WebBlue" GridLines="None" AutoGenerateColumns="false"
                                                EnableLinqExpressions="False" ShowGroupPanel="false" AllowSorting="true" AllowFilteringByColumn="False"
                                                ShowStatusBar="True" AllowPaging="true">
                                                <ClientSettings Selecting-AllowRowSelect="true" EnablePostBackOnRowClick="true">
                                                </ClientSettings>
                                                <MasterTableView ShowHeadersWhenNoRecords="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn Visible="false" DataField="localPID" HeaderText="PID">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="name" HeaderText="Name">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="dob" HeaderText="DOB">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pnlAppointments" runat="server">
                                    <tr>
                                        <td colspan="2" class="rightSubTitle">
                                            <asp:Label ID="lblAppointment" runat="server" Text="Patient Appointments without Letters"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                            <asp:CheckBox ID="chkShowAll" runat="server" Text="Show Appointments with Letters."
                                                AutoPostBack="true" Checked="true" /><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <br />
                                            <asp:Label ID="lblDateRange" runat="server" Text="Begin/End Date: (mm/dd/yyyy)"></asp:Label>
                                            &nbsp;
                                            <asp:TextBox ID="txtBeginDate" runat="server" ToolTip="mm/dd/yyyy" Width="90px"></asp:TextBox>
                                            <a href="#calendar" onclick="javascript:calendar(null,null,null, '<%=txtBeginDate.ClientID %>', 'divBeginDate',null,null,'Begin Date');return false;">
                                                <asp:Image runat="server" ID="Image5" ImageUrl="/images/calendar.gif" Height="17"
                                                    Width="22" ToolTip="Date Picker Calendar - click to open" /></a> &nbsp;-&nbsp;
                                            <asp:TextBox ID="txtEndDate" runat="server" ToolTip="mm/dd/yyyy" Width="90px"></asp:TextBox>
                                            <a href="#calendar" onclick="javascript:calendar(null,null,null, '<%=txtEndDate.ClientID %>', 'divEndDate',null,null,'End Date');return false;">
                                                <asp:Image runat="server" ID="Image1" ImageUrl="/images/calendar.gif" Height="17"
                                                    Width="22" ToolTip="Date Picker Calendar - click to open" /></a> &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnFilter" runat="server" Text="Filter" ValidationGroup="Filter" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnReset" runat="server" Text="Reset" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 315px">
                                            <div id="divBeginDate" style="display: inline-block">
                                            </div>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEndDateWidth" runat="server" Width="25px"></asp:Label>
                                            <div id="divEndDate" style="display: inline-block">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <%--<asp:RequiredFieldValidator runat="server" ID="rfvBeginDate" ControlToValidate="txtBeginDate"
                                                ValidationGroup="Filter" ErrorMessage="Begin Date is required." Text="Begin Date is required."
                                                Display="Dynamic"><br />Begin Date is required.</asp:RequiredFieldValidator>--%>
                                            <asp:RangeValidator runat="server" ID="rvBeginDate" ControlToValidate="txtBeginDate"
                                                ValidationGroup="Filter" Type="Date" ErrorMessage="Please enter a valid Begin Date."
                                                ToolTip="Please enter a valid Begin Date." Display="Dynamic" MinimumValue="1950-1-1"
                                                MaximumValue="9999-12-31" Text="Please enter a valid Begin Date."><br />Please enter a valid Begin Date.</asp:RangeValidator>
                                            <%--<asp:RequiredFieldValidator runat="server" ID="rfvEndDate" ControlToValidate="txtEndDate"
                                                ValidationGroup="Filter" ErrorMessage="End Date is required." Text="End Date is required."
                                                Display="Dynamic"><br />End Date is required.</asp:RequiredFieldValidator>--%>
                                            <asp:RangeValidator runat="server" ID="rvEndDate" ControlToValidate="txtEndDate"
                                                ValidationGroup="Filter" Type="Date" ErrorMessage="Please enter a valid End Date."
                                                ToolTip="Please enter a valid End Date." Display="Dynamic" MinimumValue="1950-1-1"
                                                MaximumValue="9999-12-31" Text="Please enter a valid End Date."><br />Please enter a valid End Date.</asp:RangeValidator>
                                            <%--<asp:CompareValidator ID="cmpDate" runat="server" ControlToCompare="txtBeginDate"
                                                ControlToValidate="txtEndDate" Type="Date" ValidationGroup="Filter" Display="Dynamic"
                                                Operator="GreaterThanEqual" ErrorMessage="End Date must be Greater Than Begin Date."><br />End Date must be Greater Than Begin Date.</asp:CompareValidator>--%>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="Filter" ShowMessageBox="True"
                                                Visible="True" ShowSummary="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadGrid ID="rgdAppointments" runat="server" Skin="WebBlue" GridLines="None"
                                                AutoGenerateColumns="false" EnableLinqExpressions="False" ShowGroupPanel="false"
                                                AllowSorting="true" AllowFilteringByColumn="false" ShowStatusBar="True" AllowPaging="true">
                                                <GroupPanel Enabled="false" PanelStyle-Height="50px" PanelStyle-Width="100%" PanelStyle-BackColor="#DFEEFF">
                                                    <PanelStyle BackColor="#DFEEFF" Height="50px" Width="100%"></PanelStyle>
                                                </GroupPanel>
                                                <GroupingSettings CaseSensitive="false" />
                                                <MasterTableView PageSize="10" PagerStyle-AlwaysVisible="true" PagerStyle-PrevPageText="Previous Page"
                                                    PagerStyle-NextPageText="Next Page" PagerStyle-LastPageText="Last Page" PagerStyle-FirstPageText="First Page"
                                                    ShowHeadersWhenNoRecords="true">
                                                    <SortExpressions>
                                                        <telerik:GridSortExpression FieldName="AppointmentDate" SortOrder="Descending" />
                                                    </SortExpressions>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkAppointment" runat="server" AutoPostBack="true" OnCheckedChanged="chkAppointment_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="VANSAppointmentID" HeaderText="Appointment ID"
                                                            UniqueName="VANSAppointmentID" SortExpression="VANSAppointmentID" Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridDateTimeColumn AllowSorting="true" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                                                            DataField="AppointmentDate" SortExpression="AppointmentDate" UniqueName="AppointmentDate"
                                                            HeaderText="Date">
                                                        </telerik:GridDateTimeColumn>
                                                        <telerik:GridBoundColumn DataField="AppointmentTime" HeaderText="Time" UniqueName="AppointmentTime"
                                                            SortExpression="AppointmentTime" Visible="true">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ClinicName" UniqueName="Clinic" HeaderText="Clinic">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ClinicID" UniqueName="ClinicID" Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CurrentStatus" HeaderText="Current Status" UniqueName="CurrentStatus"
                                                            SortExpression="CurrentStatus">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="type" HeaderText="Type" UniqueName="type" SortExpression="type">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <asp:Button ID="btnCreateLetter" runat="server" Text="Create Letter" OnClientClick='PleaseWaitButton()' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="rightSubTitle">
                                            <asp:Label ID="lblGroupedAppointments" runat="server" Text="Patient Appointments with Letters"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                            <asp:CheckBox ID="chkPreviousLetters" runat="server" Text="Show Past Appointments."
                                                AutoPostBack="true" />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
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
                                                                                <telerik:GridBoundColumn DataField="AppointmentTime" HeaderText="Time" UniqueName="AppointmentTime"
                                                                                    SortExpression="AppointmentTime" Visible="true">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Clinic" UniqueName="Clinic" HeaderText="Clinic"
                                                                                    SortExpression="Clinic">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="ClinicID" UniqueName="ClinicID" HeaderText="ClinicID"
                                                                                    Visible="false">
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="CurrentStatus" HeaderText="Current Status" UniqueName="CurrentStatus"
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
                                                        <telerik:GridSortExpression FieldName="AppointmentGroupID" SortOrder="Descending" />
                                                    </SortExpressions>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="Letter ID" SortExpression="AppointmentGroupID">
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
                                                        <telerik:GridBoundColumn DataField="DateSent" HeaderText="Date Sent">
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
