<%@ Page Title="" Language="VB" MasterPageFile="~/vaLogin.Master" AutoEventWireup="false"
    Inherits="VANS.RecoverUserName" CodeBehind="RecoverUserName.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style6
        {
            height: 29px;
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
                Forget Your Username?
            </td>
        </tr>
        <!-- END CONTENT AREA ROW -->
        <tr>
            <td class="rightContent" align="left">
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
                                    <td align="left" colspan="2" class="rightSubTitle" style="vertical-align: top">
                                        <asp:Label ID="lblUserNameInfo" runat="server" AssociatedControlID="txtEmailAddress">Enter your Email address to receive your Username.</asp:Label>
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
                                        <asp:Label ID="lblEmailAddress" runat="server" AssociatedControlID="txtEmailAddress"
                                            Style="vertical-align: middle">Email Address: <font color="Red">*</font></asp:Label>
                                    </td>
                                    <td align="left" class="style9">
                                        &nbsp;
                                        <asp:TextBox ID="txtEmailAddress" runat="server" Style="text-align: left" Width="255px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp
                                    </td>
                                    <td align="left">
                                        <asp:RequiredFieldValidator ID="EmailAddressRequired" runat="server" ControlToValidate="txtEmailAddress"
                                            ErrorMessage="Email Address is required." ToolTip="Email Address is required."
                                            ValidationGroup="EmailCheck">&nbsp;&nbsp;&nbsp;Email Address is required.</asp:RequiredFieldValidator>
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
                                        <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="EmailCheck" />
                                        <br />
                                        <br />
                                        <a href="Login.aspx">Return to Login Page</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="valalert" colspan="2">
                                        <asp:Literal ID="SuccessFailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="EmailCheck"
                        ShowMessageBox="True" Visible="True" ShowSummary="False" />
                </table>
                <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
