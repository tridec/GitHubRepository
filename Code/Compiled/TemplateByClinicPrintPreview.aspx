<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TemplateByClinicPrintPreview.aspx.vb"
    Inherits="VANS.TemplateByClinicPrintPreview" %>

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
                <td colspan="2">
                    &nbsp;
                    <asp:Label ID="lblClinicID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblClinicName" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="rightContent">
                    <table border="0" cellpadding="0" cellspacing="0" width="650px">
                        <tr>
                            <td align="left" colspan="2">
                                <asp:LinkButton ID="lbnBack" runat="server" Text="Back"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                                <asp:Label ID="lblReferURL" runat="server" Visible="false"></asp:Label>
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
                                <asp:Label ID="lblNameAddressHeader" runat="server">Dwyer, Andy<br />123 North Street<br />Pawnee, IN, 32123</asp:Label>
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
                                <asp:Label ID="lblPatientNameValue" runat="server">Dear Dwyer, Andy,</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblTopMessage" runat="server">You have the following appoinment(s) scheduled:</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgdAppointments" runat="server" GridLines="None" BackColor="Transparent"
                                    AutoGenerateColumns="false">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridDateTimeColumn AllowSorting="true" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}"
                                                DataField="AppointmentDate" SortExpression="AppointmentDate" UniqueName="AppointmentDate"
                                                HeaderText="Date">
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn DataField="AppointmentTime" HeaderText="Time" UniqueName="AppointmentTime"
                                                SortExpression="AppointmentTime">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ClinicName" UniqueName="ClinicName" HeaderText="Clinic">
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
                        <asp:Panel ID="pnlTemplate" runat="server" Visible="true">
                            <tr>
                                <td colspan="2">
                                    <br />
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="lblLegend" runat="server"></asp:Label>
                                        </legend>
                                        <telerik:RadListView ID="rlvTemplate" runat="server" DataKeyNames="TemplateID" ItemPlaceholderID="TemplateContainer"
                                            OnNeedDataSource="rlvTemplate_NeedDataSource">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblClinicMessage" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadListView>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="2">
                                <b>Additional Comments:</b>&nbsp;
                                <asp:Label ID="lblComments" runat="server">The comments for a letter will go here.</asp:Label>
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
