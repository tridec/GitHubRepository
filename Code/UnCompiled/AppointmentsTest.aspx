<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AppointmentsTest.aspx.vb"
    Inherits="VANS.AppointmentsTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Appointments</title>
    <link href="../styles/mainstyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        fieldset
        {
            border: 1px solid #666666;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" align="left" width="100%" summary="This table is for layout purposes only.">
            <tr>
                <td colspan="2" class="rightTitle">
                    <asp:Label ID="lblMainTitle" runat="server">Appointment Test</asp:Label>
                    <asp:Label ID="lblAppointmentID" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="rightContent">
                    <table border="0" cellpadding="0" cellspacing="0" width="750px">
                        <tr>
                            <td colspan="2" class="rightSubTitle">
                                <asp:Label ID="lblTitle" runat="server" Text="getClinics()"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgdClinics" runat="server" Skin="WebBlue" GridLines="None" AutoGenerateColumns="true">
                                    <MasterTableView>
                                        <Columns>
                                            <%--                                            <telerik:GridBoundColumn DataField="id" HeaderText="id" UniqueName="id" SortExpression="id">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="name" HeaderText="name" UniqueName="name" SortExpression="name">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="department" HeaderText="department" UniqueName="department"
                                                SortExpression="department">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="service" HeaderText="service" UniqueName="service"
                                                SortExpression="service">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="specialty" HeaderText="specialty" UniqueName="specialty"
                                                SortExpression="specialty">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="physicalLocation" HeaderText="physicalLocation" UniqueName="physicalLocation"
                                                SortExpression="physicalLocation">
                                            </telerik:GridBoundColumn>--%>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                            </td>
                        </tr>
                        <%-- Get Hospital Locations --%>
                        <tr>
                            <td colspan="2" class="rightSubTitle">
                                <asp:Label ID="Label1" runat="server" Text="getHospitalLocations()"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="rgdHospitals" runat="server" Skin="WebBlue" GridLines="None"
                                    AutoGenerateColumns="true">
                                    <MasterTableView>
                                        <Columns>
                                            <%--                                            <telerik:GridBoundColumn DataField="AppointmentNo" HeaderText="" UniqueName="AppointmentNo"
                                                SortExpression="AppointmentNo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentTime" HeaderText="Appointment Time"
                                                UniqueName="AppointmentTime" SortExpression="AppointmentTime">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentLocation" HeaderText="Appointment Location"
                                                UniqueName="AppointmentLocation" SortExpression="AppointmentLocation">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentService" HeaderText="Appointment Service"
                                                UniqueName="AppointmentService" SortExpression="AppointmentService">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Doctor" HeaderText="Doctor" UniqueName="Doctor"
                                                SortExpression="Doctor">
                                            </telerik:GridBoundColumn>--%>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                            </td>
                        </tr>
                        <%--Get Locations--%>
                        <tr>
                            <td colspan="2" class="rightSubTitle">
                                <asp:Label ID="Label2" runat="server" Text="getLocations()"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgdLocations" runat="server" Skin="WebBlue" GridLines="None"
                                    AutoGenerateColumns="true">
                                    <MasterTableView>
                                        <Columns>
                                            <%-- <telerik:GridBoundColumn DataField="AppointmentNo" HeaderText="" UniqueName="AppointmentNo"
                                                SortExpression="AppointmentNo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentTime" HeaderText="Appointment Time"
                                                UniqueName="AppointmentTime" SortExpression="AppointmentTime">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentLocation" HeaderText="Appointment Location"
                                                UniqueName="AppointmentLocation" SortExpression="AppointmentLocation">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentService" HeaderText="Appointment Service"
                                                UniqueName="AppointmentService" SortExpression="AppointmentService">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Doctor" HeaderText="Doctor" UniqueName="Doctor"
                                                SortExpression="Doctor">
                                            </telerik:GridBoundColumn>--%>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                            </td>
                        </tr>
                        <%--Get Specialties --%>
                        <tr>
                            <td colspan="2" class="rightSubTitle">
                                <asp:Label ID="Label3" runat="server" Text="getSpecialties()"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgdSpecialties" runat="server" Skin="WebBlue" GridLines="None"
                                    AutoGenerateColumns="true">
                                    <MasterTableView>
                                        <Columns>
                                            <%--<telerik:GridBoundColumn DataField="AppointmentNo" HeaderText="" UniqueName="AppointmentNo"
                                                SortExpression="AppointmentNo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentTime" HeaderText="Appointment Time"
                                                UniqueName="AppointmentTime" SortExpression="AppointmentTime">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentLocation" HeaderText="Appointment Location"
                                                UniqueName="AppointmentLocation" SortExpression="AppointmentLocation">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentService" HeaderText="Appointment Service"
                                                UniqueName="AppointmentService" SortExpression="AppointmentService">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Doctor" HeaderText="Doctor" UniqueName="Doctor"
                                                SortExpression="Doctor">
                                            </telerik:GridBoundColumn>--%>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                            </td>
                        </tr>
                        <%--Get Teams --%>
                        <tr>
                            <td colspan="2" class="rightSubTitle">
                                <asp:Label ID="Label4" runat="server" Text="getTeams()"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgdTeams" runat="server" Skin="WebBlue" GridLines="None" AutoGenerateColumns="true">
                                    <MasterTableView>
                                        <Columns>
                                            <%--<telerik:GridBoundColumn DataField="AppointmentNo" HeaderText="" UniqueName="AppointmentNo"
                                                SortExpression="AppointmentNo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentTime" HeaderText="Appointment Time"
                                                UniqueName="AppointmentTime" SortExpression="AppointmentTime">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentLocation" HeaderText="Appointment Location"
                                                UniqueName="AppointmentLocation" SortExpression="AppointmentLocation">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentService" HeaderText="Appointment Service"
                                                UniqueName="AppointmentService" SortExpression="AppointmentService">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Doctor" HeaderText="Doctor" UniqueName="Doctor"
                                                SortExpression="Doctor">
                                            </telerik:GridBoundColumn>--%>
                                        </Columns>
                                    </MasterTableView>
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
