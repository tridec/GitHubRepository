<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SupplierManagement.aspx.vb"
    Inherits="VANS.SupplierManagement" MasterPageFile="~/ManageAccounts/Admin.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblRoleName" runat="server" Visible="false"></asp:Label>
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
                <%--<asp:Label ID="lblIsAuthorizedOld" runat="server" Visible="false"></asp:Label>--%>
                <%-- <asp:Label ID="lblNewRequest" runat="server" Visible="false"></asp:Label>--%>
                <%--**** Main TABS--%>
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
                <asp:Button ID="btnSearchSubmit" runat="server" Text="Filter" />
                &nbsp;&nbsp;
                <asp:Button ID="btnSearchClear" runat="server" Text="Clear Filter" />
                &nbsp;&nbsp;
                <asp:Label ID="lblRole" AssociatedControlID="ddlRoleSearch" runat="server">Role:</asp:Label>
                <asp:DropDownList ID="ddlRoleSearch" AutoPostBack="true" runat="server" DataTextField="RoleName"
                    DataValueField="RoleName">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Label ID="lblShowDisabled" runat="server" AssociatedControlID="cbShowDisabled">Show Only Disabled:</asp:Label>
                &nbsp;
                <asp:CheckBox ID="cbShowDisabled" runat="server" AutoPostBack="true" />
                <br />
                <br />
                <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="True" Skin="WebBlue">
                    <MasterTableView GridLines="None" DataKeyNames="PersonID" AllowSorting="True" PageSize="20"
                        PagerStyle-PrevPageText="Previous Page" PagerStyle-NextPageText="Next Page" PagerStyle-LastPageText="Last Page"
                        PagerStyle-FirstPageText="First Page">
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
                                <%--                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblTitleView" runat="server" Text="Title:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblTitleViewValue" runat="server" Text='<%# Bind( "Title" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblCompanyNameView" runat="server" Text="Company Name:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblCompanyNameViewValue" runat="server" Text='<%# Bind( "CompanyName" ) %>'></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="formlabelalt" width="250">
                                        <asp:Label ID="lblEmailView" runat="server" Text="Email:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue" width="450">
                                        <asp:Label ID="lblEmailViewValue" runat="server" Text='<%# Bind( "Email" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <%--   <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblAddress1View" runat="server" Text="Address 1:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblAddress1ViewValue" runat="server" Text='<%# Bind( "Address1" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblAddress2View" runat="server" Text="Address 2:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblAddress2ViewValue" runat="server" Text='<%# Bind( "Address2" ) %>'></asp:Label>
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
                                        <asp:Label ID="lblContactRegionView" runat="server" Text="Contact Region:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblRegionViewValue" runat="server" Text='<%# Bind( "Region" ) %>'></asp:Label>
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
                                        <asp:Label ID="lblAltPhoneNumberView" runat="server" Text="Alternate Phone Number:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblAltPhoneNumberViewValue" runat="server" Text='<%# Bind( "AltPhone" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblFaxPhoneNumberView" runat="server" Text="Fax Number:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblFaxPhoneNumberViewValue" runat="server" Text='<%# Bind( "FaxNumber" ) %>'></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblIsLockedOut" runat="server" Text="Account Locked Out:"></asp:Label>
                                    </td>
                                    <td class="formlabelvalue">
                                        <asp:Label ID="lblIsLockedOutValue" runat="server" Text='<%# Bind( "IsLockedOut" ) %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formlabelalt">
                                        <asp:Label ID="lblIsAuthorized" runat="server" Text="Account Enabled:"></asp:Label>
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
                                    <asp:LinkButton ID="lbnEdit" runat="server" Text="Edit" CommandName="Edit"></asp:LinkButton>
                                    <asp:Label ID="lblUserName" runat="server" Visible="false" Text='<%# Bind("UserName") %>'></asp:Label>
                                    <asp:Label ID="lblPersonID" runat="server" Visible="false" Text='<%# Bind("PersonID") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="lblViewPermissions" runat="server"><a href="UserAccessReview.aspx?UserName=<%# eval("UserName") %> ">View User Access</a></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="PersonID" HeaderText="PersonId" UniqueName="PersonID"
                                Visible="False">
                                <HeaderStyle ForeColor="Silver" Width="20px" />
                                <ItemStyle ForeColor="Gray" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UserName" HeaderText="User Name" UniqueName="column1">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FirstName" HeaderText="First Name" UniqueName="column">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="LastName" HeaderText="Last Name" UniqueName="LastName"
                                Reorderable="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="email">
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn DataField="PhoneNumber" HeaderText="Phone" UniqueName="phone">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" UniqueName="CompanyName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NewRequest" HeaderText="New Request" UniqueName="newrequest"
                                Visible="false">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="IsApproved" HeaderText="Account Enabled" UniqueName="IsApproved">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle AlwaysVisible="True" Position="Top" />
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
