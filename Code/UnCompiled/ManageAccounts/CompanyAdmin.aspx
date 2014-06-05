<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CompanyAdmin.aspx.vb"
    Inherits="VANS.CompanyAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Company Admin</title>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <link href="../styles/mainstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
            width="100%">
            <tr valign="top">
                <td class="rightTitle">
                    Contracting Office Admin
                </td>
            </tr>
            <tr>
                <td class="rightContent">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlInsertEdit" runat="server" Visible="false">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="rightSubTitle" colspan="2">
                                                <asp:Label ID="lblCompanyTitle" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <asp:Label ID="lblCompanyID" runat="server" Visible="false"></asp:Label>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCompanyName" AssociatedControlID="txtCompanyName" runat="server">Company Name:<span class="valalert">*</span></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCompanyName" runat="server" MaxLength="150" Width="400px" TabIndex="3"></asp:TextBox>
                                              
                                                <asp:RequiredFieldValidator ID="rfvCompanyName" ValidationGroup="Submit" runat="server"
                                                    ControlToValidate="txtCompanyName" ErrorMessage="<br/>Company Name is required." ToolTip="Company Name is required."
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" />
                                                &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                </telerik:RadScriptManager>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:VOAConnectionString %>"
                                    SelectCommand="CompanySelect" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                <telerik:RadGrid ID="rgCompany" runat="server" DataSourceID="SqlDataSource1" Skin="WebBlue">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="CompanyID" DataSourceID="SqlDataSource1">
                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="EditColumn">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="false" CommandName="Update"
                                                        Text="Edit">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="CompanyID" DataType="System.Int32" FilterControlAltText="Filter CompanyID column"
                                                HeaderText="CompanyID" ReadOnly="True" SortExpression="CompanyID" UniqueName="CompanyID">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CompanyName" FilterControlAltText="Filter CompanyName column"
                                                HeaderText="CompanyName" SortExpression="CompanyName" UniqueName="CompanyName">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <FilterMenu EnableImageSprites="False">
                                    </FilterMenu>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Button ID="btnAddCompany" runat="server" Text="Add Company" />
                                <br />
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
