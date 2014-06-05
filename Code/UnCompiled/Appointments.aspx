<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Appointments.aspx.vb"
    Inherits="VANS.Appointments" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    <asp:Label ID="lblTitle" runat="server">Appointment Details</asp:Label>
                    <asp:Label ID="lblAppointmentGroupID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblVANSAppointmentID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblAppointmentID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblIsEdit" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblSignatureIDOriginal" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblSignatureMessageOriginal" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="rightContent">
                    <table border="0" cellpadding="0" cellspacing="0" width="750px">
                        <tr>
                            <td colspan="2" class="rightSubTitle">
                                <asp:Label ID="lblInformation" runat="server" Text="Information"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblPatientName" runat="server">Patient Name:&nbsp;</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblPatientNameValue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblPatientAddress" runat="server">Patient Address:&nbsp;</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblPatientAddressValue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblCityStateZip" runat="server">City, State, Zip:&nbsp;</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblCityStateZipValue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblSendDate" runat="server">Last Email/Printed Date:&nbsp;</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblSendDateValue" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgdAppointments" runat="server" Skin="WebBlue" GridLines="None"
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
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="rightSubTitle">
                                <asp:Label ID="lblTemplateTitle" runat="server" Text="Email/Letter Toggle"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <asp:Label ID="lblMessage" runat="server" Text="*Select the messages you wish to send."></asp:Label><br />
                                <telerik:RadListView ID="rlvAppointments" runat="server" DataKeyNames="VANSAppointmentID,ClinicID"
                                    ItemPlaceholderID="AppointmentsContainer">
                                    <ItemTemplate>
                                        <table width="100%">
                                            <%--<tr>
                                                <td>
                                                    <asp:Label ID="lblAppointmentID" runat="server" Visible="false"><%# Eval("VANSAppointmentID") %></asp:Label>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                    <fieldset>
                                                        <legend style="padding-bottom: 5px;">
                                                            <asp:LinkButton ID="lbnExpand" runat="Server" OnClick="lbnExpand_Click">
                                                                <img style="border-style: none" id="imgExpandCollapse" runat="server" alt="Expand/Collapse"
                                                                    src="../images/chevronup.png" /></asp:LinkButton><%# Eval("AppointmentDate") & ":" & Eval("AppointmentTime") & " " & Eval("ClinicName")%></legend>
                                                        <asp:Label ID="lblNoTemplates" runat="server" Visible="false" Font-Bold="true"></asp:Label>
                                                        <telerik:RadListView ID="rlvTemplate" runat="server" DataKeyNames="TemplateID" ItemPlaceholderID="TemplateContainer"
                                                            OnNeedDataSource="rlvTemplate_NeedDataSource" OnPreRender="rlvTemplate_PreRender">
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td class="formlabelalt">
                                                                            <asp:CheckBox ID="chkAppointment" runat="server" Checked='<%# Eval("DefaultEnabled") * -1 %>' />
                                                                        </td>
                                                                        <td class="formlabelvalue" style="width: 100%">
                                                                            <asp:Label ID="lblAppointmentTemplateID" runat="server" Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblTemplateMessageHidden" runat="server" Visible="false" Text='<%# Eval("Message")%>'></asp:Label>
                                                                            <asp:Label ID="lblTemplateMessage" runat="server" Visible='<%# IIF(lblIsEdit.Text = "False", True, False)%>'
                                                                                Text='<%# Eval("Message")%>'></asp:Label>
                                                                            <asp:TextBox ID="txtTemplateMessage" runat="server" Visible='<%# IIF(lblIsEdit.Text = "True", True, False)%>'
                                                                                TextMode="MultiLine" Height="85px" Width="650px" Text='<%# Eval("Message")%>'></asp:TextBox>
                                                                            <asp:Label ID="lblTemplateID" runat="server" Visible="false" Text='<%# Eval("TemplateID") %>' />
                                                                            <asp:RegularExpressionValidator ID="revMessage" runat="server" ControlToValidate="txtTemplateMessage"
                                                                                ValidationExpression="^[\s\S]{0,2000}$" ErrorMessage="Maximum 2000 characters are allowed in the Message."
                                                                                Text="Maximum 2000 characters are allowed in the Message." ValidationGroup="Save"
                                                                                Display="Dynamic">Maximum 2000 characters are allowed in the Message.</asp:RegularExpressionValidator>
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
                        <asp:Panel ID="pnlComments" runat="server">
                            <tr>
                                <td colspan="2" class="rightSubTitle">
                                    <asp:Label ID="lblCommentsHeader" runat="server" Text="Additional Comments"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblComments" runat="server">Additional Comments:&nbsp;</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="100px"
                                        Width="750px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revComments" runat="server" ControlToValidate="txtComments"
                                        ValidationExpression="^[\s\S]{0,1000}$" ErrorMessage="Maximum 1000 characters are allowed in the Comments."
                                        Text="Maximum 1000 characters are allowed in the Comments." ValidationGroup="Save"
                                        Display="Dynamic">Maximum 1000 characters are allowed in the Comments.</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlSignature" runat="server">
                            <tr>
                                <td colspan="2" class="rightSubTitle">
                                    <asp:Label ID="lblSignatureTitle" runat="server" Text="Signature Toggle"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblSignature" runat="server" Text="Select Signature: "></asp:Label>
                                    <asp:DropDownList ID="ddlSignature" AutoPostBack="True" runat="server" DataTextField="Title"
                                        DataValueField="SignatureID">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;<asp:Button ID="btnResetSignature" runat="server" Text="Reset Signature" /><span
                                        class="valalert"> *Resets signature to default value.</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblSignatureValue" runat="server"></asp:Label>
                                    <asp:Label ID="lblSignatureValueHidden" Visible="false" runat="server"></asp:Label>
                                    <telerik:RadEditor ID="reSignature" runat="server" EnableResize="false" Width="100%"
                                        Height="350px" MaxHtmlLength="2000">
                                        <Tools>
                                            <telerik:EditorToolGroup>
                                                <telerik:EditorTool Name="Bold" />
                                                <telerik:EditorTool Name="Italic" />
                                                <telerik:EditorTool Name="Underline" />
                                                <telerik:EditorTool Name="Copy" />
                                                <telerik:EditorTool Name="Cut" />
                                                <telerik:EditorTool Name="Paste" />
                                                <telerik:EditorTool Name="FontSize" />
                                                <telerik:EditorTool Name="ForeColor" />
                                            </telerik:EditorToolGroup>
                                        </Tools>
                                    </telerik:RadEditor>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you wish to delete this letter?')" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnPreview" runat="server" Text="Preview" ValidationGroup="Save" />
                                <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="Save" ShowMessageBox="true"
                                    Visible="true" ShowSummary="false" />
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
