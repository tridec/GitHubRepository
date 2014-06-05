<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MemberManagementUpdate.aspx.vb"
    Inherits="NOAATEAMSAdmin.MemberManagementUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <link href="../styles/mainstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="rightTitle">
                    Manage Accounts
                </td>
            </tr>
            <tr>
                <td class="rightContent">
                    <br />
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" CausesValidation="False">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="Supplier Accounts" NavigateUrl="SupplierManagement.aspx">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="New Supplier" NavigateUrl="SupplierCreate.aspx">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Roles" NavigateUrl="RoleManagement.aspx">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Member Accounts" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="New VA Member" NavigateUrl="MemberCreate.aspx">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="Access Requests" NavigateUrl="MemberApprove.aspx">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <br />
                    <table width="750px" bgcolor="#dae2e8" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td class="rightSubTitle" colspan="2">
                                Domain Information
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt" width="225px">
                                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" class="formlabelalt">Email:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblEmailVal" runat="server" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                                <asp:Button ID="btnChangeEmail" runat="server" Text="Change Email" CommandName="Update"
                                    CommandArgument="ChangeEmail" />
                                <asp:Label ID="lblEditFormNewRequest" runat="server" Visible="false" Text='<%# Bind( "NewRequest" ) %>'></asp:Label>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlWarning" runat="server" Visible="false">
                            <tr>
                                <td class="formlabelalt" style="text-align: center" colspan="2">
                                    <hr />
                                    <div class="valalert">
                                        <asp:Label ID="lblWarning" runat="server" Text="In order to complete this action another user will be inactivated.<br />  Confirmation is necessary to disable this conflicting user."></asp:Label><br />
                                    </div>
                                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                    <hr />
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td class="formlabelalt" align="right">
                                <asp:Label ID="lblUserName" runat="server" AssociatedControlID="lblUserNameVal">User Name:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblUserNameVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblCompanyName" AssociatedControlID="lblCompanyNameVal" runat="server">Company Name: </asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblCompanyNameVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblDeptOrgName" AssociatedControlID="lblDeptOrgNameVal" runat="server">Department/<br />Organization Name: </asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblDeptOrgNameVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblFirstName" AssociatedControlID="lblFirstNameVal" runat="server">First Name:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblFirstNameVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblLastName" AssociatedControlID="lblLastNameVal" runat="server">Last Name:</asp:Label><br />
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblLastNameVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblTitle" AssociatedControlID="lblTitleVal" runat="server">Title:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblTitleVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblAddress1" AssociatedControlID="lblAddress1Val" runat="server">Address: </asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblAddress1Val" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblCity" AssociatedControlID="lblCityVal" runat="server">City: </asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblCityVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblState" AssociatedControlID="lblStateVal" runat="server">State:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblStateVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblZipCode" AssociatedControlID="lblZipCodeVal" runat="server">Zip Code:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblZipCodeVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="lblPhone" AssociatedControlID="lblPhoneVal" runat="server">Phone Number:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblPhoneVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightSubTitle" colspan="2">
                                User Authorization
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                &nbsp
                            </td>
                            <td align="left" class="formlabelvalue" valign="middle">
                                <br />
                                <fieldset style="width: 180px; text-align: left">
                                    <legend>
                                        <asp:Label ID="lblIsAuthorized" AssociatedControlID="rblIsAuthorizedMember" runat="server"
                                            Text="Account Authorized:"></asp:Label></legend>
                                    <asp:RadioButtonList runat="server" ID="rblIsAuthorizedMember" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                            </td>
                            <td class="formlabelvalue" style="text-align: right;">
                                <asp:Button ID="btnSaveAuth" runat="server" Text="Save Authorization" />
                            </td>
                        </tr>
                        <tr>
                            <td class="valalert" align="center" colspan="2">
                                <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="rightSubTitle" colspan="2">
                                User Roles
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelvalue" colspan="2">
                                <telerik:RadTabStrip ID="rtsRoles" runat="server" MultiPageID="RMPRoles" SelectedIndex="0"
                                    CausesValidation="False">
                                    <Tabs>
                                        <telerik:RadTab runat="server" Text="Other" Selected="True">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" Text="Idea Portal">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" Text="Customer Portal">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" Text="ECTOS">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" Text="Innovation Portal">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" Text="Major Initiative">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" Text="Acquisition Tracker">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" Text="Internal Innovations">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" Text="Proposals">
                                        </telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                            </td>
                        </tr>
                        <tr>
                            <%--<td class="formlabelalt">
                                <span class="hidden">Only one of the following roles can be granted to a single user:
                                    Admin, Contract Officer, Idea Manager<br />
                                    <br />
                                    If one of these roles is selected the user also cannot be assigned IdeaReviewer
                                    or Idea Reader roles.</span>
                                <table>
                                    <tr>
                                        <td class="small">
                                            Only one of the following roles can be granted to a single user: Admin, Contract
                                            Officer, Idea Manager<br />
                                            <br />
                                            If one of these roles is selected the user also cannot be assigned IdeaReviewer
                                            or Idea Reader roles.
                                        </td>
                                    </tr>
                                </table>
                            </td>--%>
                            <td class="formlabelvalue" id="tdRoles" runat="server" colspan="2">
                                <fieldset style="width: 360px; text-align: left">
                                    <legend>
                                        <asp:Label ID="lblUserRoles" runat="server" AssociatedControlID="rlbUserRoles">Roles:</asp:Label></legend>
                                    <telerik:RadListBox ID="rlbUserRoles" runat="server" CheckBoxes="True" Skin="WebBlue"
                                        Width="250px" DataTextField="RoleName" DataValueField="RoleID">
                                    </telerik:RadListBox>
                                </fieldset>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlReviewer" runat="server">
                            <tr>
                                <td class="formlabelvalue" colspan="2">
                                    <asp:Label ID="lblReviewerTypeID" runat="server" Visible="false"></asp:Label>
                                    <fieldset style="width: 360px; text-align: left">
                                        <legend>
                                            <asp:Label ID="lblReviewerType" AssociatedControlID="ddlReviewerType" runat="server"
                                                Text="Reviewer Type:"></asp:Label>
                                        </legend>
                                        <asp:DropDownList ID="ddlReviewerType" runat="server" DataTextField="ReviewerTypeTitle"
                                            DataValueField="IdeaReviewerTypeID">
                                        </asp:DropDownList>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="formlabelvalue" colspan="2">
                                    <fieldset style="width: 360px; text-align: left">
                                        <legend>
                                            <asp:Label ID="lblChallenge" runat="server" AssociatedControlID="rlbChallenge">Challenges:</asp:Label>
                                        </legend>
                                        <telerik:RadListBox ID="rlbChallenge" runat="server" CheckBoxes="True" Skin="WebBlue"
                                            Width="350px" DataTextField="Challenge" DataValueField="IdeaChallengeID">
                                        </telerik:RadListBox>
                                    </fieldset>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlRequiringActivity" runat="server">
                            <tr>
                                <td align="left" class="formlabelvalue" colspan="2">
                                    <fieldset style="width: 360px; text-align: left">
                                        <legend>
                                            <asp:Label ID="lblRequiringActivity" runat="server" AssociatedControlID="rlbRequiringActivity">Requiring Activity:</asp:Label>
                                        </legend>
                                        <telerik:RadListBox ID="rlbRequiringActivity" runat="server" CheckBoxes="true" Skin="WebBlue"
                                            Width="350px" DataTextField="RequiringActivityName" DataValueField="AcqRequestRequiringActivityID">
                                        </telerik:RadListBox>
                                    </fieldset>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlMajorInitiative" runat="server">
                            <tr>
                                <td align="left" class="formlabelvalue" colspan="2">
                                    <fieldset style="width: 360px; text-align: left">
                                        <legend>
                                            <asp:Label ID="Label1" runat="server" AssociatedControlID="rlbChallenge">Major Initiatives:</asp:Label>
                                        </legend>
                                        <telerik:RadListBox ID="rlbMI" runat="server" CheckBoxes="True" Skin="WebBlue" Width="350px"
                                            DataTextField="Initiative" DataValueField="AcqRequestVAInitiativesID">
                                        </telerik:RadListBox>
                                    </fieldset>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlInnovationReviewer" runat="server" Visible="false">
                            <tr>
                                <td align="left" class="formlabelvalue" colspan="2">
                                    <fieldset style="width: 360px; text-align: left">
                                        <legend>
                                            <asp:Label ID="lblTopic" runat="server" AssociatedControlID="rlbTopic">Topics:</asp:Label>
                                        </legend>
                                        <telerik:RadListBox ID="rlbTopic" runat="server" CheckBoxes="True" Skin="WebBlue"
                                            Width="350px" DataTextField="TopicFY" DataValueField="InnovationTopicID">
                                        </telerik:RadListBox>
                                    </fieldset>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlInternalInnovationPanel" runat="server" Visible="false">
                            <tr>
                                <td align="left" class="formlabelvalue" colspan="2">
                                    <fieldset style="width: 360px; text-align: left">
                                        <legend>
                                            <asp:Label ID="lblPanel" runat="server" AssociatedControlID="rlbInnovationPanel">Innovation Panel:</asp:Label>
                                        </legend>
                                        <telerik:RadListBox ID="rlbInnovationPanel" runat="server" CheckBoxes="True" Skin="WebBlue"
                                            Width="350px" DataTextField="Panel" DataValueField="InternalInnovationPanelID">
                                        </telerik:RadListBox>
                                    </fieldset>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlProposalsType" runat="server" Visible="false">
                            <tr>
                                <td align="left" class="formlabelvalue" colspan="2">
                                    <fieldset style="width: 360px; text-align: left">
                                        <legend>
                                            <asp:Label ID="lblProposalsType" runat="server" AssociatedControlID="rlbProposalsType">Proposals Type:</asp:Label>
                                        </legend>
                                        <telerik:RadListBox ID="rlbProposalsType" runat="server" CheckBoxes="True" Skin="WebBlue"
                                            Width="350px" DataTextField="ProposalType" DataValueField="ProposalsTypeID">
                                        </telerik:RadListBox>
                                    </fieldset>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td class="formlabelvalue" style="text-align: right;" id="tdSubmit" runat="server"
                                colspan="2">
                                <asp:Button ID="btnSaveRole" runat="server" Text="Save Roles" /><asp:Button ID="btnReturn"
                                    runat="server" Text="Close" />
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
