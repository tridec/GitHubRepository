<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AppointmentPrintPreview.aspx.vb"
    Inherits="VANS.AppointmentPrintPreview" %>

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
    <script type="text/javascript">
        function printPage() {
            document.getElementById("lbnEmail").style.display = 'none';
            document.getElementById("lbnBack").style.display = 'none';
            document.getElementById("lbnPrint").style.display = 'none';
            window.print();
            document.getElementById("lbnEmail").style.display = 'inline';
            document.getElementById("lbnBack").style.display = 'inline';
            document.getElementById("lbnPrint").style.display = 'inline';
            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" align="left" width="100%" summary="This table is for layout purposes only.">
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" class="rightContent">
                    <table border="0" cellpadding="0" cellspacing="0" width="650px">
                        <asp:Panel ID="pnlButtons" runat="server">
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:LinkButton ID="lbnBack" runat="server" Text="Back"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="lbnEmail" runat="server" Text="Release/Email" />
                                    &nbsp;&nbsp;
                                    <asp:LinkButton ID="lbnPrint" runat="server" Text="Print" OnClientClick="javascript: printPage();"></asp:LinkButton>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                                <asp:Label ID="lblReferURL" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblAppointmentGroupID" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-left: 30px;">
                                <asp:Label ID="lblNameAddressHeader" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblPatientNameValue" runat="server">Dear </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblTopMessage" runat="server"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgdAppointments" runat="server" GridLines="None" BackColor="Transparent"
                                    AutoGenerateColumns="false">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="VANSAppointmentID" HeaderText="Appointment ID"
                                                UniqueName="VANSAppointmentID" SortExpression="VANSAppointmentID" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CurrentStatus" HeaderText="Current Status" UniqueName="CurrentStatus"
                                                SortExpression="CurrentStatus" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Type" HeaderText="Type" UniqueName="type" SortExpression="type"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn AllowSorting="true" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                                                DataField="AppointmentDate" SortExpression="AppointmentDate" UniqueName="AppointmentDate"
                                                HeaderText="Date">
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentTime" HeaderText="Time" UniqueName="AppointmentTime"
                                                SortExpression="AppointmentTime">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ClinicName" UniqueName="ClinicName" HeaderText="Clinic">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ClinicID" UniqueName="ClinicID" HeaderText="ClinicID"
                                                Visible="false">
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
                            <td colspan="2">
                                <br />
                                <telerik:RadListView ID="rlvAppointments" runat="server" DataKeyNames="VANSAppointmentID,ClinicID"
                                    ItemPlaceholderID="AppointmentsContainer">
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <fieldset>
                                                        <legend>
                                                            <%# Eval("AppointmentDate") & ":" & Eval("AppointmentTime") & " " & Eval("ClinicName")%></legend>
                                                        <telerik:RadListView ID="rlvTemplate" runat="server" DataKeyNames="AppointmentTemplateID"
                                                            ItemPlaceholderID="TemplateContainer" OnNeedDataSource="rlvTemplate_NeedDataSource">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Label ID="lblClinicMessage" runat="server" Text='<%# Eval("TemplateMessage") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadListView>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadListView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <b>Additional Comments:</b>&nbsp;
                                <asp:Label ID="lblComments" runat="server"></asp:Label>
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
                            <td colspan="2">
                                <asp:Label ID="lblSignature" runat="server"></asp:Label>
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
