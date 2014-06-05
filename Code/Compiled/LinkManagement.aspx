<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LinkManagement.aspx.vb"
    Inherits="VANS.LinkManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Link Management</title>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <link href="styles/mainstyle.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#DAE2E8">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
            width="100%">
            <tr valign="top">
                <td class="rightTitle">
                    Links Management
                </td>
            </tr>
            <tr>
                <td class="rightContent">
                    <br />
                    <table width="100%">
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:VOAConnectionString %>"
                            DeleteCommand="LinksDelete" DeleteCommandType="StoredProcedure" InsertCommand="LinksInsert"
                            InsertCommandType="StoredProcedure" SelectCommand="LinksSelect" SelectCommandType="StoredProcedure"
                            UpdateCommand="LinksUpdate" UpdateCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter Name="NodeID" Type="Int32" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="LinkID" Type="Int32" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="LinkID" Type="Int32" />
                                <asp:Parameter Name="NodeID" Type="Int32" />
                                <asp:Parameter Name="URL" Type="String" />
                                <asp:Parameter Name="Title" Type="String" />
                                <asp:Parameter Name="Description" Type="String" />
                                <asp:Parameter Name="CategoryID" Type="Int32" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="NodeID" Type="Int32" />
                                <asp:Parameter Name="URL" Type="String" />
                                <asp:Parameter Name="Title" Type="String" />
                                <asp:Parameter Name="Description" Type="String" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:VOAConnectionString %>"
                            DeleteCommand="LinkCategoriesDelete" DeleteCommandType="StoredProcedure" InsertCommand="LinkCategoriesInsert"
                            InsertCommandType="StoredProcedure" SelectCommand="LinkCategoriesSelect" SelectCommandType="StoredProcedure"
                            UpdateCommand="LinkCategoriesUpdate" UpdateCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="1" Name="NodeID" Type="Int32" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="CategoryID" Type="Int32" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="CategoryID" Type="Int32" />
                                <asp:Parameter Name="NodeID" Type="Int32" />
                                <asp:Parameter Name="Title" Type="String" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="NodeID" Type="Int32" />
                                <asp:Parameter Name="Title" Type="String" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
                                    width="100%">
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
                                                width="800px">
                                                <tr valign="top">
                                                    <td class="rightSubTitle">
                                                        <b>Links</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblWarning" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="lblNodeID" runat="server" Visible="false"></asp:Label>
                                                        <input id="inputSelect" runat="server" type="hidden" />
                                                        <telerik:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource1" GridLines="None"
                                                            Skin="WebBlue" AutoGenerateColumns="False">
                                                            <MasterTableView DataKeyNames="LinkID" DataSourceID="SqlDataSource1" CommandItemDisplay="Top"
                                                                EditMode="PopUp">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="LinkID" DataType="System.Int32" HeaderText="LinkID"
                                                                        ReadOnly="True" SortExpression="LinkID" UniqueName="LinkID" Visible="False">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="NodeID" DataType="System.Int32" HeaderText="NodeID"
                                                                        SortExpression="NodeID" UniqueName="NodeID" Visible="False">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="URL" HeaderText="URL" SortExpression="URL" UniqueName="URL"
                                                                        Visible="False">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Title" HeaderText="Title" SortExpression="Title"
                                                                        UniqueName="Title" Visible="False">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridHyperLinkColumn DataNavigateUrlFields="URL" DataTextField="Title" HeaderText="Link"
                                                                        UniqueName="column1" Target="blank">
                                                                    </telerik:GridHyperLinkColumn>
                                                                    <telerik:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                                                                        UniqueName="Description">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="CategoryID" HeaderText="CategoryID" UniqueName="CategoryID"
                                                                        SortExpression="CategoryID" DataType="System.Int32" Visible="False">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Title1" HeaderText="Category" SortExpression="Title1"
                                                                        UniqueName="Title1">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridEditCommandColumn UniqueName="EditColumn1">
                                                                    </telerik:GridEditCommandColumn>
                                                                    <telerik:GridTemplateColumn UniqueName="DeleteColumn1">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Delete"
                                                                                OnClientClick="return confirm('Are you sure you want to delete this record?')"
                                                                                Text="Delete"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                                <EditFormSettings EditFormType="Template" PopUpSettings-Modal="true" PopUpSettings-Width="900px">
                                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                                    </EditColumn>
                                                                    <FormTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td class="formfield" width="450px">
                                                                                    <asp:Label ID="lblEditFormLinkID" runat="server" Text='<%# Bind( "LinkID" ) %>' Visible="False"></asp:Label>
                                                                                    <asp:TextBox ID="LinkID" runat="server" Width="200px" MaxLength="250" Text='<%# Bind( "LinkID" ) %>'
                                                                                        Visible="False"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="formlabelalt" width="450px">
                                                                                    <asp:Label ID="URLLabel" runat="server" AssociatedControlID="URL">URL:&nbsp;&nbsp;&nbsp;</asp:Label>
                                                                                </td>
                                                                                <td class="formlabelvalue" width="450px">
                                                                                    <asp:TextBox ID="URL" runat="server" Width="200px" MaxLength="250" Text='<%# Bind( "URL" ) %>'
                                                                                        Visible="True"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ControlToValidate="URL" ErrorMessage="URL is required."
                                                                                        ID="rfvURL" runat="server" ToolTip="URL is required." ValidationGroup='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "LinkInfo", "Update")%>'
                                                                                        Display="Dynamic"><br /> URL is required.
                                                                                    </asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="formlabelalt" width="450px">
                                                                                    <asp:Label ID="TitleLabel" runat="server" AssociatedControldID="Title">Title:&nbsp&nbsp&nbsp</asp:Label>
                                                                                </td>
                                                                                <td class="formlabelvalue" width="450px">
                                                                                    <asp:TextBox ID="Title" runat="server" Width="200px" MaxLength="250" Text='<%# Bind( "Title" ) %>'
                                                                                        Visible="True"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ControlToValidate="Title" ErrorMessage="Title is required."
                                                                                        ID="rfvTitle" runat="server" ToolTip="Title is required." ValidationGroup='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "LinkInfo", "Update")%>'
                                                                                        Display="Dynamic"><br /> Title is required.
                                                                                    </asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="formlabelalt" width="450px">
                                                                                    <asp:Label ID="DescriptionLabel" runat="server" AssociatedControldID="Description">Description:&nbsp&nbsp&nbsp</asp:Label>
                                                                                </td>
                                                                                <td class="formlabelvalue" width="450px">
                                                                                    <asp:TextBox ID="Description" runat="server" Width="200px" Text='<%# Bind( "Description" ) %>'
                                                                                        Visible="True" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="valDescription" runat="server" ControlToValidate="Description"
                                                                                        ValidationExpression="^[\s\S]{0,1000}$" ErrorMessage="Description cannot be greater than 1000 characters."
                                                                                        ValidationGroup='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "LinkInfo", "Update")%>'
                                                                                        Display="Dynamic"><br /> Description cannot be greater than 1000 characters.
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="formlabelalt">
                                                                                    <asp:Label ID="lblCategory" for="ddlCategory" runat="server" Text="Category:">Category: <font color="Red"></font></asp:Label>
                                                                                </td>
                                                                                <td class="formlabelvalue">
                                                                                    <asp:Label ID="lblCategoryID" runat="server" Text='<%# Bind( "CategoryID" ) %>' Visible="false"></asp:Label>
                                                                                    <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="Title" DataValueField="CategoryID"
                                                                                        Display="Dynamic">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="formlabelalt">
                                                                                </td>
                                                                                <td class="formlabelvalue">
                                                                                    <asp:Button ID="btnLinkUpdate" ValidationGroup='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "LinkInfo", "Update")%>'
                                                                                        CausesValidation="true" Text='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "Insert", "Update") %>'
                                                                                        runat="server" CommandName="Update" />
                                                                                    <asp:Button ID="btnLinkCancel" Text='Cancel' runat="server" CausesValidation="false"
                                                                                        CommandName="Cancel" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <asp:ValidationSummary ID="vsLinkInfo" runat="server" ValidationGroup='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "LinkInfo", "Update")%>'
                                                                            ShowMessageBox="true" Visible="true" ShowSummary="false" />
                                                                    </FormTemplate>
                                                                    <PopUpSettings Modal="True" Width="900px"></PopUpSettings>
                                                                </EditFormSettings>
                                                            </MasterTableView>
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="True" />
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%-- -------------------------------------------
        --
        --
        --
        --  RadGrid for Categories below
        --
        --
        -- ---------------------------------------------%>
                        <tr>
                            <td>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlTable3" runat="server">
                                    <table id="table3" border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
                                        width="800px">
                                        <tr valign="top">
                                            <td class="rightSubTitle">
                                                <b>Link Categories</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerik:RadGrid ID="RadGrid2" runat="server" DataSourceID="SqlDataSource2" GridLines="None"
                                                    Skin="WebBlue" AutoGenerateColumns="False">
                                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="CategoryID" DataSourceID="SqlDataSource2"
                                                        EditMode="PopUp" CommandItemDisplay="Top">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="CategoryID" DataType="System.Int32" HeaderText="CategoryID"
                                                                ReadOnly="True" SortExpression="CategoryID" UniqueName="CategoryID" Visible="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="NodeID" DataType="System.Int32" HeaderText="NodeID"
                                                                SortExpression="NodeID" UniqueName="NodeID" Visible="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Title" HeaderText="Title" SortExpression="Title"
                                                                UniqueName="Title">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridEditCommandColumn UniqueName="EditColumn2">
                                                            </telerik:GridEditCommandColumn>
                                                            <telerik:GridTemplateColumn UniqueName="DeleteColumn2">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Delete"
                                                                        OnClientClick="return confirm('Are you sure you want to delete this record?')"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                        <EditFormSettings EditFormType="Template" PopUpSettings-Modal="true" PopUpSettings-Width="900px">
                                                            <EditColumn UniqueName="EditCommandColumn2">
                                                            </EditColumn>
                                                            <FormTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td class="formlabelvalue" width="450px">
                                                                            <asp:Label ID="lblEditFormCategoryID" runat="server" Text='<%# Bind( "CategoryID" ) %>'
                                                                                Visible="False"></asp:Label>
                                                                            <asp:TextBox ID="txtCategoryID" runat="server" Width="200px" MaxLength="250" Text='<%# Bind( "CategoryID" ) %>'
                                                                                Visible="False"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="formlabelalt" width="450px">
                                                                            <asp:Label ID="lblCategoryTitle" runat="server" AssociatedControlID="lblCategoryTitle">Title:&nbsp;&nbsp;&nbsp;</asp:Label>
                                                                        </td>
                                                                        <td class="formlabelvalue" width="450px">
                                                                            <asp:TextBox ID="txtCategoryTitle" runat="server" Width="200px" MaxLength="250" Text='<%# Bind( "Title" ) %>'
                                                                                Visible="True"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ControlToValidate="txtCategoryTitle" ErrorMessage="Title is required."
                                                                                ID="rfvCategoryTitle" runat="server" ToolTip="Title is required." ValidationGroup="CategoryInfo"
                                                                                Display="Dynamic"><br /> Title is required.
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="formlabelalt">
                                                                        </td>
                                                                        <td class="formlabelvalue">
                                                                            <asp:Button ID="btnCategoryUpdate" Text='Update' runat="server" CommandName="Update"
                                                                                CausesValidation="true" ValidationGroup="CategoryInfo" />
                                                                            <asp:Button ID="btnCategoryCancel" Text='Cancel' runat="server" CausesValidation="false"
                                                                                CommandName="Cancel" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <asp:ValidationSummary ID="vsCategoryInfo" runat="server" ValidationGroup="CategoryInfo"
                                                                    ShowMessageBox="true" Visible="true" ShowSummary="false" />
                                                            </FormTemplate>
                                                            <PopUpSettings Modal="True" Width="900px"></PopUpSettings>
                                                        </EditFormSettings>
                                                    </MasterTableView>
                                                    <ClientSettings>
                                                        <Selecting AllowRowSelect="True" />
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
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
