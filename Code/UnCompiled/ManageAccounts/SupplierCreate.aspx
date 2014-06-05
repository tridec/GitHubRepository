<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SupplierCreate.aspx.vb"
    Inherits="VANS.SupplierCreate" MasterPageFile="~/ManageAccounts/Admin.Master" %>

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
                <%--Label to hold the original value of account locked out radio button to email if activated--%>
                <asp:Label ID="lblIsAuthorizedOld" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblNewRequest" runat="server" Visible="false"></asp:Label>
                <%--**** Main TABS--%>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" CausesValidation="False">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="User Accounts" NavigateUrl="SupplierManagement.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="New User" Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Roles" NavigateUrl="RoleManagement.aspx">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div>
                </div>
                <asp:SqlDataSource ID="sqlPriority" runat="server" ConnectionString="<%$ ConnectionStrings:VOAConnectionString %>"
                    SelectCommand="RoleTypeSelect" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <%--**** Main TABS Multi Page--%>
                <asp:Panel ID="pnlComplete" runat="server" Visible="false">
                    <table style="width: 500px; height: 450px;">
                        <tr>
                            <td align="center" valign="top">
                                <br />
                                <table border="0" style="height: 134px; width: 450px;">
                                    <tr>
                                        <td class="rightContent">
                                            <asp:Label ID="lblNewUserSaved" runat="server" Text="The new user has been created successfully."
                                                Visible="false"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnResetNewUser" runat="server" Text="Continue" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlAll" runat="server">
                    <table style="width: 500px">
                        <tr>
                            <td width="175px">
                                &nbsp;
                            </td>
                            <td width="304px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:<font color="Red">*</font>&nbsp;</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server" Width="140px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" ValidationGroup="New" runat="server"
                                    ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required.">* User Name is required.</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:<font color="Red">*</font>
                            <span class="hidden">Must be at least 8 characters, contain both upper and lower case letters, at least one number and one special character</span></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="140px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" ValidationGroup="New" runat="server"
                                    ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required."><br />* Password is required.</asp:RequiredFieldValidator>
                                <asp:Label ID="lblPasswordInfo" runat="server" AssociatedControlID="Password">
                            <br /><i>Must be at least 8 characters, contain both upper and lower case letters, at least<br /> one number and one special character</i></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">
                                        Confirm Password:<font color="Red">*</font>&nbsp;</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" Width="140px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" ValidationGroup="New" runat="server"
                                    ControlToValidate="ConfirmPassword" ErrorMessage="Confirm Password is required."
                                    ToolTip="Confirm Password is required."><br />* Confirm Password is required.</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:<font color="Red">*</font>&nbsp;</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Email" runat="server" Width="140px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" ValidationGroup="New" runat="server"
                                    ControlToValidate="Email" ErrorMessage="E-mail is required." ToolTip="E-mail is required."><br />* E-mail is required.
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmailFormat" runat="server" ControlToValidate="Email"
                                    ValidationExpression=".*@.*\..*" ErrorMessage="Invalid e-mail address." ValidationGroup="New"
                                    Display="dynamic"><br />Invalid e-mail address.
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:CompareValidator ID="PasswordCompare" ValidationGroup="New" runat="server" ControlToCompare="Password"
                                    ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match.">* The Password and Confirmation Password must match.</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" class="valalert">
                                <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="valalert">
                                * Required Field
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <div runat="server" id="ConfirmationMessage2" class="valalert">
                                </div>
                                <br />
                                <asp:Label ID="lblCreateStatus" runat="server" class="valalert"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 500px">
                        <tr>
                            <td align="right" width="175px">
                                <asp:Label ID="lblFirstNameEdit" AssociatedControlID="txtFirstName" runat="server">First Name:<font color="Red">*</font></asp:Label>
                            </td>
                            <td width="304px">
                                <asp:TextBox ID="txtFirstName" runat="server" Width="304px" MaxLength="50"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ControlToValidate="txtFirstName" ErrorMessage="First Name is required."
                                    ID="FirstNameRequired" runat="server" ToolTip="First Name is required." ValidationGroup="New"
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
                                    ID="LastNameRequired" runat="server" ToolTip="Last Name is required." ValidationGroup="New"
                                    Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 500px">
                        <tr>
                            <td width="350">
                                &nbsp;
                            </td>
                            <td width="450">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
                            </td>
                            <td>
                                <fieldset style="width: 255px">
                                    <legend>
                                        <asp:Label ID="lblRoles" runat="server" AssociatedControlID="rlbUserRoles">
                                 <span class="hidden"></span>Roles:</asp:Label></legend>
                                    <telerik:RadListBox ID="rlbUserRoles" runat="server" CheckBoxes="True" Skin="WebBlue"
                                        Width="250px" DataTextField="RoleName" DataValueField="RoleID" AutoPostBack="true">
                                    </telerik:RadListBox>
                                </fieldset>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <asp:Panel ID="pnlDivisions" Visible="false" runat="server">
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                                <td>
                                    <fieldset style="width: 275px; text-align: left">
                                        <legend>
                                            <asp:Label ID="lblDivisions" runat="server" AssociatedControlID="rlbDivisions">Divisions:</asp:Label>
                                        </legend>
                                        <telerik:RadListBox ID="rlbDivisions" runat="server" CheckBoxes="true" Skin="WebBlue"
                                            Width="275px" DataTextField="Division" DataValueField="DivisionID">
                                        </telerik:RadListBox>
                                    </fieldset>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                            </td>
                            <td style="padding-left: 61px">
                                <asp:Button ID="btnSaveNewUser" runat="server" Text="Save New User" ValidationGroup="New"
                                    Width="140px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ValidationSummary ID="vsNew" runat="server" ValidationGroup="New" ShowMessageBox="True"
                    Visible="True" ShowSummary="False" />
            </td>
        </tr>
    </table>
</asp:Content>
