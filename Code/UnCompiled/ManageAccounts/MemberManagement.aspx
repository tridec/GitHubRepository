<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MemberManagement.aspx.vb"
    Inherits="VANS.MemberManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Member Accounts</title>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <link href="../styles/mainstyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .RadTabStrip
        {
            margin: 0;
            padding: 0;
        }
        .RadTabStripTop_Default .rtsLevel
        {
            background-color: transparent;
        }
        .RadTabStrip .rtsLevel1
        {
            padding-top: 0;
        }
        .RadTabStrip .rtsLevel
        {
            clear: both;
            overflow: hidden;
            width: 100%;
            position: relative;
            padding-top: 1px;
        }
        .RadTabStrip .rtsUL
        {
            overflow: hidden;
            float: left;
            margin: 0;
            padding: 0;
        }
        .RadTabStrip_Default .rtsLI
        {
            color: #000;
            font: 12px/26px "Segoe UI" , Arial, sans-serif;
        }
        .RadTabStrip .rtsLI
        {
            overflow: hidden;
            list-style-type: none;
            margin: 0;
        }
        .RadTabStripTop_Default .rtsLI .rtsSelected
        {
            background-position: 0 -26px;
        }
        .RadTabStripTop_Default .rtsFirst .rtsLink
        {
            background-position: 0 0;
        }
        .RadTabStripTop_Default .rtsLevel .rtsLink
        {
            background-image: url( 'mvwres://Telerik.Web.UI, Version=2009.2.701.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.TabStrip.TabStripStates.png' );
        }
        .RadTabStripTop_Default .rtsLink
        {
            background-position: 0 -52px;
        }
        .RadTabStrip_Default .rtsLink
        {
            color: #000;
            font: 12px/26px "Segoe UI" , Arial, sans-serif;
        }
        .RadTabStrip .rtsLink
        {
            text-align: center;
        }
        .RadTabStrip .rtsLink
        {
            display: block;
            outline: none;
            cursor: pointer;
            text-decoration: none;
            white-space: nowrap;
            padding-left: 9px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lblRoleName" runat="server" Visible="false"></asp:Label>
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
                <%--**** Main TABS--%>
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
                    </Tabs>
                </telerik:RadTabStrip>
                <div>
                </div>
                <asp:SqlDataSource ID="sqlPriority" runat="server" ConnectionString="<%$ ConnectionStrings:VOAConnectionString %>"
                    SelectCommand="RoleTypeSelect" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <br />
                <asp:Label ID="lblFilter" AssociatedControlID="ddlSearchType" runat="server">Filter:</asp:Label>&nbsp;
                <asp:DropDownList ID="ddlSearchType" runat="server">
                    <asp:ListItem Selected="True" Text="Last Name" Value="1"></asp:ListItem>
                    <asp:ListItem Text="First Name" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Email" Value="3"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <asp:Label ID="lblFiltervalue" AssociatedControlID="txtSearchTerm" runat="server">Filter Value:</asp:Label>
                <asp:TextBox ID="txtSearchTerm" runat="server"></asp:TextBox>
                &nbsp;
                <asp:Label ID="lblRole" AssociatedControlID="ddlRoleSearch" runat="server">Role:</asp:Label>
                <asp:DropDownList ID="ddlRoleSearch" runat="server" DataTextField="RoleName" DataValueField="RoleName">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Button ID="btnSearchSubmit" runat="server" Text="Filter" />
                &nbsp;&nbsp;
                <asp:Button ID="btnSearchClear" runat="server" Text="Clear Filter" />
                <br />
                <br />
                <telerik:RadGrid ID="rgdMember" runat="server" GridLines="None" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="True" Skin="WebBlue">
                    <MasterTableView GridLines="None" DataKeyNames="PersonID, UserName" AllowSorting="True"
                        PageSize="20" PagerStyle-PrevPageText="Previous Page" PagerStyle-NextPageText="Next Page"
                        PagerStyle-LastPageText="Last Page" PagerStyle-FirstPageText="First Page">
                        <NestedViewTemplate>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td class="formlabelalt" width="250">
                                        <asp:Label ID="lblUserName" runat="server" Text="User Name:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue" width="450">
                                        <asp:Label ID="lblUserNameValue" runat="server" Text='<%# Bind( "UserName" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt" width="250">
                                        <asp:Label ID="lblEmailView" runat="server" Text="Email:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue" width="450">
                                        <asp:Label ID="lblEmailViewValue" runat="server" Text='<%# Bind( "Email" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblCompanyNameView" runat="server" Text="Company Name:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblCompanyNameViewValue" runat="server" Text='<%# Bind( "CompanyName" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblDeptOrgNameView" runat="server" Text="Department/Organization Name:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblDeptOrgNameViewValue" runat="server" Text='<%# Bind( "DeptOrgName" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblFirstNameView" runat="server" Text=" First Name:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblFirstNameViewValue" runat="server" Text='<%# Bind( "FirstName" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblLastNameView" runat="server" Text="Last Name:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblLastNameViewValue" runat="server" Text='<%# Bind( "LastName" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblTitleView" runat="server" Text="Title:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblTitleViewValue" runat="server" Text='<%# Bind( "Title" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblAddress1View" runat="server" Text="Address: "></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblAddress1ViewValue" runat="server" Text='<%# Bind( "Address1" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblCityView" runat="server" Text="City:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblCityViewValue" runat="server" Text='<%# Bind( "City" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblStateNameView" runat="server" Text="State:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblStateNameViewValue" runat="server" Text='<%# Bind( "StateName" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblZipCodeView" runat="server" Text="Zip Code:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblZipCodeViewValue" runat="server" Text='<%# Bind( "ZipCode" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblPhoneNumberView" runat="server" Text="Phone Number:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblPhoneNumberViewValue" runat="server" Text='<%# Bind( "PhoneNumber" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblIsAuthorized" runat="server" Text="Account Authorized:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblIsAuthorizedValue" runat="server" Text='<%# Bind( "IsApproved" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        Member of Roles:
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblUserRoles" runat="server" Text=""></asp:Label>&nbsp;
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
                        </NestedViewTemplate>
                        <ExpandCollapseColumn Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="lblEdit" runat="server"></asp:Label>
                                    <asp:Label ID="lblUserName" runat="server" Visible="false" Text='<%# Bind("UserName") %>'></asp:Label>
                                    <asp:Label ID="lblPersonID" runat="server" Visible="false" Text='<%# Bind("PersonID") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="PersonID" HeaderText="PersonId" UniqueName="PersonID"
                                Visible="False">
                                <HeaderStyle ForeColor="Silver" Width="20px" />
                                <ItemStyle ForeColor="Gray" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UserName" HeaderText="User Name" UniqueName="UserName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FirstName" HeaderText="First Name" UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="LastName" HeaderText="Last Name" UniqueName="LastName"
                                Reorderable="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="email">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PhoneNumber" HeaderText="Phone" UniqueName="phone">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" UniqueName="companyname">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NewRequest" HeaderText="New Request" UniqueName="newrequest"
                                Visible="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle AlwaysVisible="True" Position="Top" />
                    </MasterTableView>
                </telerik:RadGrid>
                <br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
