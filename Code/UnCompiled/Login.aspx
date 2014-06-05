<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/vaLogin.Master"
    CodeBehind="Login.aspx.vb" Inherits="VANS.Login" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Login Page</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LoginContentPlaceHolder" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td height="80" style="background-color: rgb(194, 216, 232);">
                <img src="./img/header.png" id="prBanner" height="80px" alt="VOA" title="VOA Projects" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
        frame="void">
        <tr valign="top">
            <td style="background-color: rgb(7, 0, 72);">
                &nbsp;
            </td>
        </tr>
        <!-- END CONTENT AREA ROW -->
        <tr>
            <td class="rightContent" align="center" style="height: 267px; width: 913px;">
                <asp:Login ID="Login1" runat="server" BorderStyle="None" Font-Names="Verdana" Font-Size="10pt"
                    CreateUserText="If you don't have a login please register first." CreateUserUrl="CreateAccount.aspx"
                    DestinationPageUrl="HomeMain.aspx" PasswordRecoveryText="Forgot your password?"
                    PasswordRecoveryUrl="RecoverPassword.aspx" Height="212px" Style="margin-top: 0px"
                    Width="435px" FailureAction="Refresh" DisplayRememberMe="False">
                    <TitleTextStyle Font-Bold="True" ForeColor="#FFFFFF" />
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;">
                            <tr>
                                <td align="center" colspan="2" class="valalert">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <fieldset>
                                        <legend>login</legend>
                                        <table border="0" cellpadding="0" style="height: 177px; width: 450px;" align="center">
                                            <tr>
                                                <td align="right" valign="bottom">
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"><b>User Name:</b></asp:Label>
                                                </td>
                                                <td valign="bottom">
                                                    &nbsp;
                                                    <asp:TextBox ID="UserName" runat="server" Width="150px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="bottom">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password"><b>Password:</b></asp:Label>
                                                </td>
                                                <td valign="bottom">
                                                    &nbsp;
                                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <%-- <asp:HyperLink ID="CreateUserLink" runat="server" NavigateUrl="~/CreateAccount.aspx">If you do not have a login, please register first.</asp:HyperLink>
                                                    <br />--%>
                                                    <asp:HyperLink ID="PasswordRecoveryLink" runat="server" NavigateUrl="~/RecoverPassword.aspx">Forgot your password?</asp:HyperLink>
                                                    <br />
                                                    <asp:HyperLink ID="UsernameRecoveryLink" runat="server" NavigateUrl="~/RecoverUsername.aspx">Forgot your username?</asp:HyperLink>
                                                    <%-- <br />
                                                    <br />
                                                    If you need assistance contact<a href="mailto:voahelp@va.gov?    subject=VOA Support&amp;">
                                                        voahelp@va.gov</a>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:Login>
                <asp:ValidationSummary ID="ValidationLogin1" runat="server" ValidationGroup="Login1"
                    ShowMessageBox="True" Visible="True" ShowSummary="False" />
            </td>
        </tr>
        <tr>
            <td class="loginMessage">
                <p style="text-align: center;">
                    <strong>**WARNING**WARNING**WARNING**</strong>
                </p>
                <p style="text-align: center;">
                    This is a United States (Agency) computer system, which may be accessed and used
                    only for official Government business by authorized personnel. Unauthorized access
                    or use of this computer system may subject violators to criminal, civil, and/or
                    administrative action.
                </p>
                <p style="text-align: center;">
                    All information on this computer system may be intercepted, recorded, read, copied,
                    and disclosed by and to authorized personnel for official purposes, including criminal
                    investigations. Access or use of this computer system by any person whether authorized
                    or unauthorized, constitutes consent to these terms.
                </p>
                <p style="text-align: center;">
                    <strong>**WARNING**WARNING**WARNING**</strong></p>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
