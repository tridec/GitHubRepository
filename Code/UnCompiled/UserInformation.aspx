<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserInformation.aspx.vb"
    Inherits="VANS.UserInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Information</title>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <link href="styles/mainstyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        html, body, form
        {
            height: 100%;
            margin: 0px;
            padding: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
            width="100%">
            <tr valign="top">
                <td class="rightTitle" align="right">
                    <asp:Label ID="lblUserDisplay" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr valign="top">
                <td class="rightContent" colspan="2">
                    <span class="hidden">Press Alt + A to activate the Account Information Tab</span>
                    <span class="hidden">Press Alt + C to activate the Contact Information Tab</span>
                    <telerik:RadAjaxPanel ID="rapUserInfo" runat="server" EnableAJAX="False" Visible="true">
                        <telerik:RadTabStrip ID="rtsUserInfo" runat="server" MultiPageID="rmpUserInfo" SelectedIndex="0"
                            BackColor="#9FB3C8" Skin="WebBlue">
                            <Tabs>
                                <telerik:RadTab runat="server" Text="Contact Information" TabIndex="1" AccessKey="C"
                                    Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="Account Information" TabIndex="2" AccessKey="A">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="rmpUserInfo" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="rpvContactInfo" runat="server" Selected="True">
                                <telerik:RadAjaxPanel ID="rapUserInfoView" runat="server" EnableAJAX="False">
                                    <table cellpadding="5px" cellspacing="0" border="0">
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
                                                <asp:Label ID="lblUserNameView" runat="server" Text="User Name:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblUserNameViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblFirstNameView" runat="server" Text=" First Name:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFirstNameViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblLastNameView" runat="server" Text="Last Name:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblLastNameViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
<%--                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblTitleView" runat="server" Text="Title:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblTitleViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblCompanyNameView" runat="server" Text="Company Name:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblCompanyNameViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
--%>                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblEmailView" runat="server" Text="Email:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblEmailViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
<%--                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblAddress1View" runat="server" Text="Address 1:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblAddress1ViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblAddress2View" runat="server" Text="Address 2:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblAddress2ViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblCityView" runat="server" Text="City:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblCityViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblStateView" runat="server" Text="State:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblStateViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblZipCodeView" runat="server" Text="Zip Code:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblZipCodeViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblContactRegionView" runat="server" Text="Contact Region:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblRegionViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblPhoneView" runat="server" Text="Phone Number:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblPhoneViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblAltPhoneView" runat="server" Text="Alt Phone:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblAltPhoneViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblFaxView" runat="server" Text="Fax Number:"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblFaxViewValue" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td align="left" style="color: Red; font-size: medium; font-family: Calibri;">
                                            </td>
                                        </tr>
--%>                                        <tr>
                                            <td align="left" style="color: #FF0000">
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="75px" Visible="true" />
                                            </td>
                                        </tr>
                                    </table>
                                    <hr />
                                </telerik:RadAjaxPanel>
                                <telerik:RadAjaxPanel ID="rapUserInfoEdit" runat="server" Visible="False" EnableAJAX="False">
                                    <table style="width: 615px" cellpadding="0" cellspacing="0" border="0">
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
<%--                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblTitleEdit" AssociatedControlID="txtTitle" runat="server">Title:&nbsp;&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTitle" runat="server" Width="304px" MaxLength="255"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblCompanyNameEdit" AssociatedControlID="ddlCompany" runat="server">Company Name:<font color="Red">*</font></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCompany" runat="server" DataValueField="CompanyID" DataTextField="CompanyName" />
                                                <asp:CompareValidator Enabled="false" ID="cvCategory" ValidationGroup="UserInfo"
                                                    ControlToValidate="ddlCompany" ValueToCompare="0" Operator="NotEqual" Type="String"
                                                    runat="server" ErrorMessage="Comapny Name is required." Display="Dynamic"><br />Comapny Name is required.</asp:CompareValidator>
                                            </td>
                                        </tr>
--%>                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblEmailEdit" AssociatedControlID="txtEmail" runat="server">Email:&nbsp;&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmail" runat="server" Width="304px" MaxLength="255"></asp:TextBox>
                                            </td>
                                        </tr>
<%--                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblAddress1Edit" AssociatedControlID="txtAddress1" runat="server">Address 1:<font color="Red">*</font></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAddress1" runat="server" Width="304px" MaxLength="255"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ControlToValidate="txtAddress1" ErrorMessage="Address 1 is required."
                                                    ID="Address1Required" runat="server" ToolTip="Address 1 is required." ValidationGroup="UserInfo"
                                                    Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblAddress2Edit" AssociatedControlID="txtAddress2" runat="server">Address 2:&nbsp;&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAddress2" runat="server" Width="304px" MaxLength="255"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblCityEdit" AssociatedControlID="txtCity" runat="server">City:<font color="Red">*</font></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCity" runat="server" Width="304px" MaxLength="100"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ControlToValidate="txtCity" ErrorMessage="City is required."
                                                    ID="CityRequired" runat="server" ToolTip="City is required." ValidationGroup="UserInfo"
                                                    Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblStateEdit" AssociatedControlID="ddlState" runat="server">State:<font color="Red">*</font></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlState" runat="server" DataTextField="StateName" DataValueField="StateId"
                                                    Width="308px">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RangeValidator ID="RangeValidator1" Type="Integer" ValidationGroup="UserInfo"
                                                    ControlToValidate="ddlState" MaximumValue="99" MinimumValue="1" runat="server"
                                                    Display="Dynamic" ErrorMessage="State is required."></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblZipCodeEdit" AssociatedControlID="txtZipCode" runat="server">Zip Code:<font color="Red">*</font></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtZipCode" runat="server" Width="304px" MaxLength="50"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ControlToValidate="txtZipCode" ErrorMessage="Zip Code is required."
                                                    ID="ZipCodeRequired" runat="server" ToolTip="Zip Code is required." ValidationGroup="UserInfo"
                                                    Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblContactregionEdit" AssociatedControlID="ddlRegion" runat="server">Contact Region:&nbsp;&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegion" runat="server" DataValueField="UserRegionID" DataTextField="Region" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblPhoneEdit" AssociatedControlID="txtPhone" runat="server">Phone Number:<font color="Red">*</font></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPhone" runat="server" Width="304px" MaxLength="100"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPhone" ErrorMessage="Phone is required."
                                                    ID="PhoneRequired" runat="server" ToolTip="Phone is required." ValidationGroup="UserInfo"
                                                    Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblAltPhoneEdit" AssociatedControlID="txtAltPhone" runat="server">Alt Phone:&nbsp;&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAltPhone" runat="server" Width="304px" MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblFaxEdit" AssociatedControlID="txtFax" runat="server">Fax Number:&nbsp;&nbsp;</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFax" runat="server" Width="304px" MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
--%>                                        <tr>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td align="left" class="valalert">
                                                * Required Field
                                                <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="color: #FF0000">
                                            </td>
                                            <td align="right" style="width: 304px">
                                                <asp:Button ID="btnSave" runat="server" ValidationGroup="UserInfo" CausesValidation="true"
                                                    Text="Save" Width="75px" />
                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel"
                                                    Width="75px" />
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
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="UserInfo"
                                            ShowMessageBox="True" Visible="True" ShowSummary="False" />
                                    </table>
                                </telerik:RadAjaxPanel>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="rpvAccountInfo" runat="server" TabIndex="1">
                                <table border="0" style="width: 575px">
                                    <tr>
                                        <td align="right" width="191px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblUserName" runat="server">User Name:&nbsp;&nbsp;</asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUserNameValue" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:ChangePassword ID="ChangePassword1" runat="server" ContinueDestinationPageUrl="UserInformation.aspx"
                                    ChangePasswordFailureText="Password Incorrect or New Password Invalid">
                                    <ChangePasswordTemplate>
                                        <table style="width: 575px">
                                            <tr>
                                                <td align="right" width="191px">
                                                    <asp:Label AssociatedControlID="CurrentPassword" ID="CurrentPasswordLabel" runat="server">Password:<font color="Red">*</font><span class="hidden">Must be at least 8 characters, contain both upper and lower case letters, at least one number and one special character</span></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="CurrentPassword" ErrorMessage="Password is required."
                                                        ID="CurrentPasswordRequired" runat="server" ToolTip="Password is required." ValidationGroup="ValidateChangePassword"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="PasswordRequirements" runat="server">
                                                <i>(Must be at least 8 characters, contain both upper and lower
                                                   case letters, at least one number and one special character)</i></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label AssociatedControlID="NewPassword" ID="NewPasswordLabel" runat="server">New Password:<font color="Red">*</font>
                                                                <span class="hidden">Must be at least 8 characters, contain both upper and lower case letters, at least one number and one special character</span>
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="NewPassword" runat="server" TextMode="Password">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="NewPassword" ErrorMessage="New Password is required."
                                                        ID="NewPasswordRequired" runat="server" ToolTip="New Password is required." ValidationGroup="ValidateChangePassword">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label AssociatedControlID="ConfirmNewPassword" ID="ConfirmNewPasswordLabel"
                                                        runat="server">Confirm New Password:<font color="Red">*</font>
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ControlToValidate="ConfirmNewPassword" ErrorMessage="Confirm New Password is required."
                                                        ID="ConfirmNewPasswordRequired" runat="server" ToolTip="Confirm New Password is required."
                                                        ValidationGroup="ValidateChangePassword">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:CompareValidator ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                                        Display="Dynamic" ErrorMessage="The confirm New Password must match the New Password entry."
                                                        ID="NewPasswordCompare" runat="server" ValidationGroup="ValidateChangePassword">
                                                    </asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                                <td align="left" class="valalert">
                                                    * Required Field
                                                    <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                                                    <br />
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="ChangePasswordPushButton" runat="server" Text="Change Password" CommandName="ChangePassword"
                                                        ValidationGroup="ValidateChangePassword" />
                                                    <asp:Button ID="CancelPushButton" runat="server" CommandName="Cancel" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ChangePasswordTemplate>
                                </asp:ChangePassword>
                                <hr />
                                <table style="width: 575px">
                                    <tr>
                                        <td align="right" valign="top" width="190px">
                                            &nbsp;
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblCurrentPassword" runat="server">Current Password:<font color="Red">*</font></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="txtCurrentPassword" ErrorMessage="Current password is required."
                                                ID="rfvCurrentPasswordRequired" runat="server" ToolTip="Current password is required."
                                                ValidationGroup="ChangeSecurity" Display="Dynamic">Current password is required.</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="lblQuestion" runat="server">Security Question:&nbsp;
                                            <span class="hidden">If you forget your password you will be asked 
                                            the security question you choose here and prompted to enter the answer 
                                            you specify below.</span>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlQuestion" runat="server">
                                                <asp:ListItem>What is your favorite sports team?</asp:ListItem>
                                                <asp:ListItem>In what city were you born?</asp:ListItem>
                                                <asp:ListItem>What is your favorite sport?</asp:ListItem>
                                                <asp:ListItem>Who was your childhood hero?</asp:ListItem>
                                                <asp:ListItem>What is the name of your favorite pet?</asp:ListItem>
                                                <asp:ListItem>Where do you work?</asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <i>If you forget your password you will be asked the security question you choose here
                                                and prompted to enter the answer you specify below.</i>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;&nbsp;&nbsp;<asp:Label ID="lblAnswer" runat="server">Security Answer:<font color="Red">*</font></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAnswer" runat="server" Width="140px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="txtAnswer" ErrorMessage="Security Answer is required."
                                                ID="rfvSecurityAnswer" runat="server" ToolTip="Security Answer is required."
                                                ValidationGroup="ChangeSecurity" Display="Dynamic">Security Answer is required.</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td style="color: Red;">
                                            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnUpdateSecurity" runat="server" Text="Update Security" Width="150px"
                                                ValidationGroup="ChangeSecurity" />
                                        </td>
                                    </tr>
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="ChangeSecurity"
                                        ShowMessageBox="True" Visible="True" ShowSummary="False" />
                                    <asp:ValidationSummary ID="ValidateSummary2" runat="server" ValidationGroup="ValidateChangePassword"
                                        ShowMessageBox="True" Visible="True" ShowSummary="False" />
                                </table>
                                <hr />
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </telerik:RadAjaxPanel>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfNewUser" Value="False" runat="server" />
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="False">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="lbExpandColapse">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rapUserInfoEdit" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rapUserInfoView">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rapUserInfoView" />
                        <telerik:AjaxUpdatedControl ControlID="rapUserInfoEdit" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rapUserInfoEdit">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rapUserInfoView" />
                        <telerik:AjaxUpdatedControl ControlID="rapUserInfoEdit" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
    </div>
    </form>
</body>
</html>
