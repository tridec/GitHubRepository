<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MemberCreate.aspx.vb"
    Inherits="NOAATEAMSAdmin.MemberCreate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New VA Member</title>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <link href="../styles/mainstyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ConfirmSubmit() {
            var name = eval("lblNewMemberUserName.innerText");

            return confirm('This action will create user ' + name + '.  Do you want to continue?');

            return false;
        }
    </script>

</head>
<body style="background-color: #DAE2E8">
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
        width="100%">
        <tr valign="top">
            <td class="rightTitle">
                Manage Accounts
            </td>
        </tr>
        <tr>
            <td class="rightContent">
                <br />
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                </telerik:RadScriptManager>
                <%--Label to hold the original value of account locked out radio button to email if activated--%>
                <asp:Label ID="lblIsAuthorizedOld" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblNewRequest" runat="server" Visible="false"></asp:Label>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" CausesValidation="False">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="Supplier Accounts" NavigateUrl="SupplierManagement.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="New Supplier" NavigateUrl="SupplierCreate.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Roles" NavigateUrl="RoleManagement.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Member Accounts" NavigateUrl="MemberManagement.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="New VA Member" Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Access Requests" NavigateUrl="MemberApprove.aspx">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <div>
                </div>
                <asp:SqlDataSource ID="sqlPriority" runat="server" ConnectionString="<%$ ConnectionStrings:VOAConnectionString %>"
                    SelectCommand="RoleTypeSelect" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:Panel ID="pnlSearch" runat="server">
                    <table style="width: 575px;">
                        <tr>
                            <td colspan="2" class="formlabelalt">
                                Enter the email address of the desired new user
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt" >
                                <asp:Label ID="lblAddMember" AssociatedControlID="txtAddMember" runat="server">Email: </asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:TextBox ID="txtAddMember" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="formlabelalt" align="right">
                                <asp:Button ID="btnAddUser" runat="server" Text="Search for User" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
                    <table class="formlabelalt" style="width: 575px; height: 250px;">
                        <tr>
                            <td align="center" valign="top">
                                <br />
                                <br />
                                <table border="0" style="background-color: #DAE2E8; font-size: 100%; height: 134px;
                                    width: 446px; font-family: Calibri" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="rightTitle">
                                            Account Created
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblDisplayUserName" runat="server"></asp:Label>
                                            <br />
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Button ID="btnComplete" runat="server" Text="Continue" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlNoUser" runat="server" Visible="false" style="width: 575px;">
                <table style="width: 575px;">
                    <tr>
                        <td class="formlabelalt">
                            <asp:Label ID="lblNoUser" runat="server" Text="No user was found in Active Directory with this email address."
                                CssClass="valalert" Visible="false"></asp:Label><br />
                            <asp:Label ID="lblDupEmail" runat="server" Text="A user with that email address already exists in the system."
                                CssClass="valalert" Visible="false"></asp:Label><br />
                            <asp:Label ID="lblDupUserName" runat="server" Text="A user with that username already exists in the system."
                                CssClass="valalert" Visible="false"></asp:Label><br />
                            <asp:Label ID="lblDupBoth" runat="server" Text="A user with that username and email already exists in the system."
                                CssClass="valalert" Visible="false"></asp:Label><br />
                        </td>
                    </tr>
                </table>
                    
                </asp:Panel>
                <asp:Panel ID="pnlEditExistingUser" runat="server" Visible="false">
                    <table style="width: 575px;">
                        <tr>
                            <td class="formlabelalt" style="width: 100%">
                                <asp:Button ID="btnEditExistingUser" runat="server" Text="Edit Existing User" Visible="false" />
                                <asp:Label ID="lblPersonID" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlInfo" runat="server" Visible="false" Width="675px">
                    <table width="615px" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="rightTitle" colspan="2">
                                User Name:
                                <asp:Label ID="lblNewMemberUserName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt" align="right">
                                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="lblEmailVal">E-mail:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblEmailVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="Label2" AssociatedControlID="lblCompanyNameVal" runat="server">Company Name: </asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblCompanyNameVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="Label3" AssociatedControlID="lblDeptOrgNameVal" runat="server">Department/Organization Name: </asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblDeptOrgNameVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="Label4" AssociatedControlID="lblFirstNameVal" runat="server">First Name:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblFirstNameVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="Label5" AssociatedControlID="lblLastNameVal" runat="server">Last Name:</asp:Label><br />
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblLastNameVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="Label6" AssociatedControlID="lblTitleVal" runat="server">Title:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblTitleVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="Label7" AssociatedControlID="lblAddress1Val" runat="server">Address: </asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblAddress1Val" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="Label8" AssociatedControlID="lblCityVal" runat="server">City: </asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblCityVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="Label9" AssociatedControlID="lblStateVal" runat="server">State:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblStateVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="Label10" AssociatedControlID="lblZipCodeVal" runat="server">Zip Code:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblZipCodeVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                <asp:Label ID="Label11" AssociatedControlID="lblPhoneVal" runat="server">Phone Number:</asp:Label>
                            </td>
                            <td class="formlabelvalue">
                                <asp:Label ID="lblPhoneVal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="formlabelalt">
                                &nbsp
                            </td>
                            <td class="formlabelaltvalue">
                                <fieldset style="width: 360px; text-align: left">
                                    <legend>
                                        <asp:Label ID="lblIsAuthorized" AssociatedControlID="rblIsAuthorizedNewMember" runat="server"
                                            Text="Account Authorized:"></asp:Label></legend>
                                    <asp:RadioButtonList runat="server" ID="rblIsAuthorizedNewMember" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Yes" Value="True" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </fieldset>
                            </td>
                        </tr>
                        
                        
                        
                        <tr>
                            <td class="formlabelalt">
                            </td>
                            <td class="formlabelalt">
                                <asp:Button ID="btnSaveMember" Text="Create Member" runat="server" OnClientClick="return ConfirmSubmit()">
                                </asp:Button>&nbsp;
                                <asp:Button ID="btnUserAccountCancel" Text="Cancel" runat="server" CausesValidation="False">
                                </asp:Button>&nbsp;
                                <asp:Label ID="lblEmpty" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
