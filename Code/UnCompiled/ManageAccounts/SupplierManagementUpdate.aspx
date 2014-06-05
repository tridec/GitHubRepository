<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SupplierManagementUpdate.aspx.vb"
    Inherits="VANS.SupplierManagementUpdate" MasterPageFile="~/ManageAccounts/Admin.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <asp:Label ID="lblUserName" runat="server" Visible="false"></asp:Label>
            <div>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td class="rightSubTitle">
                            Manage Accounts
                        </td>
                    </tr>
                    <tr>
                        <td class="rightContent">
                            <br />
                            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" CausesValidation="False">
                                <Tabs>
                                    <telerik:RadTab runat="server" Text="User Accounts" Selected="True">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" Text="New User" NavigateUrl="SupplierCreate.aspx">
                                    </telerik:RadTab>
                                    <telerik:RadTab runat="server" Text="Roles" NavigateUrl="RoleManagement.aspx">
                                    </telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <br />
                            <table cellpadding="3" cellspacing="0" border="0" width="500px">
                                <tr>
                                    <td class="rightSubTitle" colspan="2">
                                        User Information
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 304px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblUserNameEdit" runat="server">User Name:&nbsp;&nbsp;</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserNameEditValue" runat="server"></asp:Label>
                                        <asp:Label ID="lblEditFormNewRequest" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblFirstNameEdit" AssociatedControlID="txtFirstName" runat="server">First Name:<font color="Red">*</font></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName" runat="server" Width="304px" MaxLength="50"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ControlToValidate="txtFirstName" ErrorMessage="First Name is required."
                                            ID="FirstNameRequired" runat="server" ToolTip="First Name is required." ValidationGroup="UserInfo"
                                            Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblLastNameEdit" AssociatedControlID="txtLastName" runat="server">Last Name:<font color="Red">*</font></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName" runat="server" Width="304px" MaxLength="50"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ControlToValidate="txtLastName" ErrorMessage="Last Name is required."
                                            ID="LastNameRequired" runat="server" ToolTip="Last Name is required." ValidationGroup="UserInfo"
                                            Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblEmailEdit" AssociatedControlID="txtEmail" runat="server">Email:&nbsp;&nbsp;</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server" Width="304px" MaxLength="255"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtEmail" ErrorMessage="Email is required."
                                            ID="rfvEmail" runat="server" ToolTip="Email is required." ValidationGroup="UserInfo"
                                            Display="Dynamic"><br />
                                Email is required. </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmailFormat" runat="server" ControlToValidate="txtEmail"
                                            ValidationExpression=".*@.*\..*" ErrorMessage="Invalid e-mail address." ValidationGroup="UserInfo"
                                            Display="dynamic"><br />Invalid e-mail address.
                                        </asp:RegularExpressionValidator>
                                        <asp:Label ID="lblEmailDuplicate" runat="server" Visible="false" CssClass="valalert"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td>
                                        <fieldset style="width: 255px; text-align: left">
                                            <legend>
                                                <asp:Label ID="lblIsLockedOut" AssociatedControlID="rblIsLockedOut" runat="server"
                                                    Text="Account Locked Out:&nbsp;&nbsp;&nbsp;"></asp:Label></legend>
                                            <asp:RadioButtonList runat="server" ID="rblIsLockedOut" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Yes" Value="True" Enabled="false"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td>
                                        <asp:Label ID="lblIsAuthorizedOld" runat="server" Visible="false"></asp:Label>
                                        <fieldset style="width: 255px; text-align: left">
                                            <legend>
                                                <asp:Label ID="lblIsAuthorized" AssociatedControlID="rblIsAuthorized" runat="server"
                                                    Text="Account Enabled:&nbsp;&nbsp;&nbsp;"></asp:Label></legend>
                                            <asp:RadioButtonList runat="server" ID="rblIsAuthorized" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <span class="valalert">* Required Field </span>
                                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="valalert" align="center" colspan="2">
                                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td id="tdRoles" runat="server">
                                        <fieldset style="width: 255px">
                                            <legend>
                                                <asp:Label ID="lblUserRoles" runat="server" AssociatedControlID="rlbUserRoles">
                                            <span class="hidden"></span>Roles:</asp:Label></legend>
                                            <telerik:RadListBox ID="rlbUserRoles" runat="server" CheckBoxes="True" Skin="WebBlue"
                                                Width="250px" DataTextField="RoleName" DataValueField="RoleID" AutoPostBack="true">
                                            </telerik:RadListBox>
                                            <br />
                                            <br />
                                        </fieldset>
                                        <asp:Label ID="lblVewNodeTree" runat="server" />&nbsp;
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="rightSubTitle" colspan="2">
                                        <asp:Label ID="lblFindTitle" runat="server">Link User to Vista</asp:Label>
                                    </td>
                                </tr>
                                <asp:Panel ID="pnlFindUser" runat="server">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblLinkHelp" runat="server"><p style=" color:Red; font-size:small;">*A user must be linked to the vista system before they can see their appointments. 
                                    Please search for and select the appropriate user below.</p></asp:Label>
                                            <br />
                                            <asp:Label ID="lblSSN" runat="server">Enter Patient by SSN, 'LAST,FIRST', A1234(Last initial + last four SSN): </asp:Label>
                                            <br />
                                            <br />
                                            <asp:TextBox ID="txtSSN" runat="server" Width="250px"></asp:TextBox>&nbsp;&nbsp;<asp:Button
                                                ID="btnFind" runat="server" Width="80px" Text="Find" ValidationGroup="Find" /><br />
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
                                <asp:Panel ID="pnlExistingUser" runat="server">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblVistaUser" runat="server">Vista User:&nbsp;&nbsp;</asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblVistaUserValue" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:Button runat="server" ID="btnResetLink" Text="Reset Linked User" />
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUserAccountUpdate" Text='Update' runat="server" CausesValidation="True"
                                            ValidationGroup="UserInfo"></asp:Button>&nbsp;
                                        <asp:Button ID="btnUserAccountCancel" Text="Cancel" runat="server" CausesValidation="False">
                                        </asp:Button>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblResetHidden" AssociatedControlID="btnReset" runat="server"><span class="hidden">This button will generate a new password and email it to this user</span></asp:Label>
                                        <asp:Button ID="btnReset" runat="server" Text="Reset Password" />
                                        <br />
                                        <asp:Label ID="lblReset" runat="server" AssociatedControlID="btnReset" Style="font-family: Calibri;">
                                        <i>This button will generate a new password and email it to this user</i></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="vsUserInfo" runat="server" ValidationGroup="UserInfo"
                    ShowMessageBox="True" Visible="True" ShowSummary="False" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
