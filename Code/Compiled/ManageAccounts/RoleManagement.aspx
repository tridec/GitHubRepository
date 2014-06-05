<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RoleManagement.aspx.vb"
    Inherits="VANS.RoleManagement" MasterPageFile="~/ManageAccounts/Admin.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
        width="100%">
        <tr valign="top">
            <td class="rightSubTitle">
                Manage Accounts
            </td>
        </tr>
        <tr>
            <td class="rightContent">
                <br />
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" CausesValidation="False">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="User Accounts" NavigateUrl="SupplierManagement.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="New User" NavigateUrl="SupplierCreate.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Roles" Selected="True">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div>
                </div>
                <br />
                <asp:Button ID="btnCreateRole" runat="server" Text="Create Role" />
                <asp:Panel Width="600px" runat="server" ID="pnlNewRole" Visible="false">
                    <table width="100%">
                        <tr>
                            <td class="rightSubTitle" colspan="2">
                                Create Role
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="formlabelalt">
                                <asp:Label ID="lblRoleName" runat="server" Text="" AssociatedControlID="txtNewRole">Role Name:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNewRole" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblRoleDescription" runat="server" Text="" AssociatedControlID="txtDescription">Role Description: </asp:Label>
                            </td>
                            <td class="formfield">
                                <asp:TextBox ID="txtDescription" runat="server" Width="350px" Rows="5" Text="" MaxLength="1000"
                                    TextMode="MultiLine"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="txtDescription"
                                    ValidationExpression="^[\s\S]{0,1000}$" ErrorMessage="Maximum 1000 characters are allowed in the Role Description"
                                    Text="Maximum 1000 characters are allowed in the Role Description." ValidationGroup="NewRole"
                                    Display="Dynamic"><br />Maximum 1000 characters are allowed in the Role Description.</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="btnAddRole" runat="server" Text="Add Role" CausesValidation="True"
                                    ValidationGroup="NewRole" />
                                <asp:Button ID="btnCancelRole" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="vsUserInfo" runat="server" ValidationGroup="NewRole" ShowMessageBox="True"
                        Visible="True" ShowSummary="False" />
                </asp:Panel>
                <br />
                <br />
                <telerik:RadGrid ID="rgRoles" runat="server" GridLines="None" AutoGenerateColumns="False"
                    Skin="WebBlue" Width="1000px">
                    <MasterTableView EditMode="EditForms">
                        <NestedViewTemplate>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="formlabelalt">
                                        Users in role:&nbsp;
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblUsers" runat="server"></asp:Label>
                                        <asp:Label ID="lblRoleNameNested" runat="server" Visible="false" Text='<%# Eval("Role Name") %>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </NestedViewTemplate>
                        <ExpandCollapseColumn Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridEditCommandColumn>
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle Width="30px"></ItemStyle>
                            </telerik:GridEditCommandColumn>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="lblViewPermissions" runat="server"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Role Name" UniqueName="RoleName">
                                <ItemTemplate>
                                    <asp:Label ID="lblRoleName" runat="server" Text='<%# Eval("Role Name") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblRoleName" runat="server" Text='<%# Eval("Role Name") %>'></asp:Label>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="User Count" UniqueName="UserCount">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserCount" runat="server" Text='<%# Eval("User Count") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Description" UniqueName="RoleDescription">
                                <ItemTemplate>
                                    <asp:Label ID="lblRoleDescription" runat="server" Text='<%# Eval("RoleDescription") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRoleDescription" runat="server" Text='<%# Eval("RoleDescription") %>'
                                        Width="350px" Rows="5" MaxLength="1000" TextMode="MultiLine"></asp:TextBox><br />
                                    <asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="txtRoleDescription"
                                        ValidationExpression="^[\s\S]{0,1000}$" ErrorMessage="Maximum 1000 characters are allowed in the Role Description."
                                        Text="Maximum 1000 characters are allowed in the Role Description." ValidationGroup="Update"
                                        Display="Dynamic"><br />Maximum 1000 characters are allowed in the Role Description.</asp:RegularExpressionValidator>
                                    <asp:ValidationSummary ID="vsUserUpdate" runat="server" ValidationGroup="Update"
                                        ShowMessageBox="True" Visible="True" ShowSummary="False" />
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button ID="btnDeleteRole" runat="server" OnClientClick="return confirm('Are you sure?')"
                                        Text="Delete" CommandName="Delete" />
                                    <asp:Label ID="lblRoleDataID" runat="server" Visible="false" Text='<%# Eval("RoleDataID") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <telerik:GridDropDownListColumnEditor ID="GridDropDownListColumnEditor1" runat="server">
                </telerik:GridDropDownListColumnEditor>
                <br />
                <div runat="server" id="ConfirmationMessage" style="color: #FF0000; font-family: Arial;
                    font-weight: bold">
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
