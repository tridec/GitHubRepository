<%@ Page Title="" Language="VB" MasterPageFile="~/vaLogin.Master" AutoEventWireup="false"
    Inherits="VANS.RecoverPassword" CodeBehind="RecoverPassword.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style11
        {
            font-size: small;
        }
        .style9
        {
            height: 24px;
            width: 300px;
        }
        .styletitle
        {
            padding: 3px 5px 3px 0px;
            text-align: center;
            background-color: #495A70;
            color: #FFFFFF;
            font-size: 16px;
            font-family: Calibri;
            font-weight: bold;
            letter-spacing: 1px;
            height: 20px;
            width: 470px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LoginContentPlaceHolder" runat="Server">
    <table border="0" cellpadding="1" cellspacing="0" align="left" summary="This table is for layout purposes only."
        width="100%">
        <tr valign="top">
            <td class="rightTitle">
                Forget Your Password?
            </td>
        </tr>
        <!-- END CONTENT AREA ROW -->
        <tr>
            <td class="rightContent" align="left">
                <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" SuccessText="Your password has been sent to the email address of record for your account."
                    Style="text-align: left">
                    <QuestionTemplate>
                        <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse;
                            width: 525px;" align="left">
                            <tr>
                                <td colspan="2" align="center">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <table border="0" cellpadding="0" frame="border">
                                </td>
                                <tr>
                                    <td align="center" colspan="2" class="rightSubTitle">
                                        Answer the following question to receive your password.
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style9">
                                        <b>Identity Confirmation&nbsp;&nbsp;&nbsp;&nbsp;</b>
                                    </td>
                                    <td align="left">
                                        &nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblUserName" runat="server">User Name:&nbsp;&nbsp;</asp:Label>
                                    </td>
                                    <td align="left" style="width: 300;">
                                        <asp:Literal ID="UserName" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblQuestion" runat="server">Question:&nbsp;&nbsp;</asp:Label>
                                    </td>
                                    <td align="left" style="width: 300;">
                                        <asp:Literal ID="Question" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style9">
                                        <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Answer:&nbsp;<font color="Red">*</font>&nbsp;</asp:Label>
                                    </td>
                                    <td align="left" class="style9">
                                        <asp:TextBox ID="Answer" runat="server" Style="text-align: left" Width="255px"></asp:TextBox><br />
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                                            ErrorMessage="Answer is required." ToolTip="Answer is required." ValidationGroup="PasswordRecovery1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                    <td align="left" class="valalert">
                                        &nbsp;&nbsp;* Required Field<br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2" style="padding: 0px 12px;">
                                        <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1" />
                                        <br /><br />
                                        <a href="Login.aspx">Return to Login Page</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" class="valalert">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PasswordRecovery1"
                                    ShowMessageBox="True" Visible="True" ShowSummary="False" />
                        </table>
                    </QuestionTemplate>
                    <UserNameTemplate>
                        <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse;
                            width: 475px;" align="left">
                            <tr>
                                <td colspan="2" align="center">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <table border="0" cellpadding="0" frame="border">
                                        <tr>
                                            <td align="right" colspan="2" class="rightSubTitle" style="vertical-align: top">
                                                <asp:Label ID="lblUserNameInfo" runat="server" AssociatedControlID="UserName">Enter your User Name to receive your password.</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style9">
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name: <font color="Red">*</font></asp:Label>
                                            </td>
                                            <td align="left" class="style9">
                                                &nbsp;
                                                <asp:TextBox ID="UserName" runat="server" Style="text-align: left" Width="255px"></asp:TextBox><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                &nbsp
                                            </td>
                                            <td align="left">
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="PasswordRecovery1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td align="left" class="valalert">
                                                &nbsp;&nbsp;* Required Field<br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2" style="padding: 0px 12px;">
                                                <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1"
                                                    OnClick="SubmitButton_Click" /><br /><br />
                                                <a href="Login.aspx">Return to Login Page</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" class="valalert">
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="PasswordRecovery1"
                                ShowMessageBox="True" Visible="True" ShowSummary="False" />
                        </table>
                    </UserNameTemplate>
                    <SuccessTemplate>
                        <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" style="height: 134px; width: 446px;">
                                        <tr>
                                            <td>
                                                <b style="font-size: medium">Your password has been sent
                                                    <br />
                                                    to the email address on record.</b><br /><br />
                                                <a href="Login.aspx">Return to Login Page</a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </SuccessTemplate>
                </asp:PasswordRecovery>
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
