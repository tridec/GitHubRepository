<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ModuleTypeAdmin.aspx.vb"
    Inherits="VANS.ModuleTypeAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Module Type Adaministration</title>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <link href="styles/mainstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="rightTitle">
                Module Type Administration
            </td>
        </tr>
        <tr>
            <td class="rightContent">
                <form id="form1" runat="server">
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableTheming="true">
                </telerik:RadScriptManager>
                <div>
                    <telerik:RadGrid ID="rgModuleType" runat="server" GridLines="None" AllowAutomaticDeletes="True"
                        AllowFilteringByColumn="true" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                        AutoGenerateColumns="False" EnableLinqExpressions="False" AllowSorting="true">
                        <AlternatingItemStyle BackColor="LightGray"></AlternatingItemStyle>
                        <MasterTableView DataKeyNames="ModuleTypeId" CommandItemDisplay="Top" AllowAutomaticInserts="False"
                            AllowAutomaticDeletes="False" AllowAutomaticUpdates="False" EditMode="PopUp">
                            <Columns>
                                <telerik:GridEditCommandColumn>
                                </telerik:GridEditCommandColumn>
                                <telerik:GridTemplateColumn DataField="ModuleTypeId" DataType="System.Int32" HeaderText="Module Type Id"
                                    SortExpression="ModuleTypeId" UniqueName="ModuleTypeId">
                                    <ItemTemplate>
                                        <asp:Label ID="ModuleTypeIdLabel" runat="server" Text='<%# Eval("ModuleTypeId") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ModuleName" HeaderText="Module Name" SortExpression="ModuleName"
                                    UniqueName="ModuleName" DataType="System.String">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ModuleDescription" HeaderText="Module Description"
                                    SortExpression="ModuleDescription" UniqueName="ModuleDescription">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ModuleURL" HeaderText="Module URL" SortExpression="ModuleURL"
                                    UniqueName="ModuleURL">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="IsPublic" DataType="System.Int32" HeaderText="Is Public"
                                    SortExpression="IsPublic" UniqueName="IsPublic" AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:Label ID="IsPublicLabel" runat="server" Text='<%# Eval("IsPublic") %>' Visible="False"></asp:Label>
                                        <asp:CheckBox ID="cbIsPublic" runat="server" Enabled="False"></asp:CheckBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn" AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?')"
                                            Text="Delete" ID="btnDelete"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <EditFormSettings EditFormType="Template">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                                <FormTemplate>
                                    <asp:Label ID="lblModuleTypeId" runat="server" Text='<%# Eval("ModuleTypeId") %>'
                                        Visible="false"></asp:Label>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblModuleName" AssociatedControlID="txtModuleName" runat="server">Module Name:&nbsp;&nbsp;</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtModuleName" runat="server" Text='<%# Bind( "ModuleName") %>'
                                                    MaxLength="255" TabIndex="3" Width="225px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ModuleNameRequired" ValidationGroup='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "SaveGroup", "Update")%>'
                                                    runat="server" ControlToValidate="txtModuleName" ErrorMessage="Module Name is required."
                                                    ToolTip="Module Name is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblModuleDescription" AssociatedControlID="txtModuleDescription" runat="server">Module Description:&nbsp;&nbsp;</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtModuleDescription" runat="server" Text='<%# Bind( "ModuleDescription") %>'
                                                    MaxLength="1000" TabIndex="3" Width="225px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ModuleDescriptionRequired" ValidationGroup='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "SaveGroup", "Update")%>'
                                                    runat="server" ControlToValidate="txtModuleDescription" ErrorMessage="Module Description is required."
                                                    ToolTip="Module Description is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblModuleURL" AssociatedControlID="txtModuleURL" runat="server">Module URL:&nbsp;&nbsp;</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtModuleURL" runat="server" Text='<%# Bind( "ModuleURL") %>' MaxLength="255"
                                                    TabIndex="3" Width="225px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ModuleURLRequired" ValidationGroup='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "SaveGroup", "Update")%>'
                                                    runat="server" ControlToValidate="txtModuleURL" ErrorMessage="Module URL is required."
                                                    ToolTip="Module URL is required." Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblIsPublic" AssociatedControlID="cbIsPublicValue" runat="server">Is Public:&nbsp;&nbsp;</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="cbIsPublicValue" runat="server"></asp:CheckBox>
                                                <asp:Label ID="lblIsPublicValue" runat="server" Text='<%# Bind( "isPublic") %>' Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnInsertUpdate" ValidationGroup='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "SaveGroup", "Update")%>'
                                                    CausesValidation="true" Text='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "Insert", "Update") %>'
                                                    runat="server" CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'>
                                                </asp:Button>&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "SaveGroup", "Update")%>'
                                        ShowMessageBox="True" Visible="True" ShowSummary="False" />
                                </FormTemplate>
                                <PopUpSettings Modal="True" Width="450px"></PopUpSettings>
                            </EditFormSettings>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="false" />
                    </telerik:RadGrid>
                </div>
                </form>
            </td>
        </tr>
    </table>
</body>
</html>
