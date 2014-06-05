<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MemberApprove.aspx.vb"
    MasterPageFile="~/ManageAccounts/Admin.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
        width="100%">
        <tr valign="top">
            <td class="rightTitle">
                Approve Accounts
            </td>
        </tr>
        <tr>
            <td class="rightContent">
                <br />
                <%--Label to hold the original value of account locked out radio button to email if activated--%>
                <asp:Label ID="lblIsAuthorizedOld" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblNewRequest" runat="server" Visible="false"></asp:Label>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" CausesValidation="False">
                    <tabs>
                        <telerik:RadTab runat="server" Text="User Accounts" NavigateUrl="SupplierManagement.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="New User" NavigateUrl="SupplierCreate.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Roles" NavigateUrl="RoleManagement.aspx">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="Member Accounts" NavigateUrl="MemberManagement.aspx"
                            Visible="false">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="New VA Member" NavigateUrl="MemberCreate.aspx"
                            Visible="false">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Selected="true" Text="Access Requests">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="User Administration" NavigateUrl="RC_ContractActionUser.aspx">
                        </telerik:RadTab>
                    </tabs>
                </telerik:RadTabStrip>
                <div>
                </div>
                <br />
                <table>
                    <tr>
                        <td>
                            <br />
                            <asp:Label ID="lblFilter" runat="server" AssociatedControlID="ddlFilterType">Filter by:&nbsp</asp:Label>
                            <asp:DropDownList ID="ddlFilterType" runat="server" AutoPostBack="true">
                                <asp:ListItem Selected="True" Text="Unprocessed" Value="1"></asp:ListItem>
                                <asp:ListItem Text="All" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="rdgAccessRequest" runat="server" GridLines="None" Skin="WebBlue">
                                <mastertableview autogeneratecolumns="False" datakeynames="AccessRequestID">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="AccessRequestID" HeaderText="AccessRequestID"
                                            SortExpression="AccessRequestID" UniqueName="AccessRequestID" DataType="System.Int32"
                                            ReadOnly="True" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email" SortExpression="Email"
                                            UniqueName="Email">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Comment" HeaderText="Comment" SortExpression="Comment"
                                            UniqueName="Comment">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RequestDate" HeaderText="RequestDate" ReadOnly="True"
                                            SortExpression="RequestDate" UniqueName="RequestDate">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="Processed" HeaderText="Processed" SortExpression="Processed"
                                            UniqueName="Processed">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProcess" runat="server" Visible="True" Text='<%# Eval("Processed") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="ProcessedDate" HeaderText="Processed Date" SortExpression="ProcessedDate"
                                            UniqueName="ProcessedDate">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" Groupable="false" HeaderText=""
                                            DataField="Title" SortExpression="Title">
                                            <ItemTemplate>
                                                <asp:Button ID="btnProcess" runat="server" Text="Process" CommandName="Update" />
                                                <asp:Label ID="lblAccessRequestID" runat="server" Visible="false" Text='<%# Eval("AccessRequestID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </mastertableview>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
