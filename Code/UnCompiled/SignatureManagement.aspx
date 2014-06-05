<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SignatureManagement.aspx.vb"
    Inherits="VANS.SignatureManagement" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signature Management</title>
    <link href="../styles/mainstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <table border="0" cellpadding="0" cellspacing="0" align="left" width="100%" summary="This table is for layout purposes only.">
            <tr>
                <td colspan="2" class="rightTitle">
                    <asp:Label ID="lblTopTitle" runat="server">Signature Management</asp:Label>
                    <asp:Label ID="lblSignatureID" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="rightContent">
                    <table width="750px" cellpadding="0" cellspacing="0" border="0">
                        <asp:Panel ID="pnlSignatureList" runat="server">
                            <tr>
                                <td class="rightSubTitle" colspan="2">
                                    <asp:Label ID="lblSignatureListTitle" runat="server" Text="Signature List"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <telerik:RadGrid ID="rgdSignature" runat="server" Skin="WebBlue" GridLines="None"
                                        AutoGenerateColumns="false" EnableLinqExpressions="False" ShowGroupPanel="false"
                                        AllowSorting="true" AllowFilteringByColumn="true" ShowStatusBar="True" AllowPaging="true">
                                        <MasterTableView PageSize="10" PagerStyle-PrevPageText="Previous Page" PagerStyle-NextPageText="Next Page"
                                            PagerStyle-LastPageText="Last Page" PagerStyle-FirstPageText="First Page" DataKeyNames="SignatureID">
                                            <NestedViewTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="formlabelalt" style="width: 150px;">
                                                            <asp:Label ID="lblMessage" runat="server" Text="Signature Message: "></asp:Label>
                                                        </td>
                                                        <td class="formlabelvalue">
                                                            <asp:Label ID="lblMessageValue" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                                                        </td>
                                                </table>
                                            </NestedViewTemplate>
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="Edit" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbnEdit" runat="server" CommandName="Update" Text="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="SignatureID" HeaderText="Signature ID" UniqueName="SignatureID"
                                                    SortExpression="SignatureID" Groupable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Title" HeaderText="Title" UniqueName="Title"
                                                    SortExpression="Title" Groupable="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn UniqueName="DefaultEnabled" HeaderText="Default" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox Enabled="false" ID="chkEnabledGrid" runat="server" Checked='<%# Eval("DefaultEnabled") * -1 %>'
                                                            Visible='<%# Eval("DefaultEnabled") * -1 %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="Delete" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbnDelete" runat="server" CommandName="Delete" Text="Delete"
                                                            OnClientClick="return confirm('Are you sure you want to delete this signature?')"></asp:LinkButton></ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add Signature" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlSignatureEdit" runat="server" Visible="false">
                            <tr>
                                <td class="rightSubTitle" colspan="2">
                                    <asp:Label ID="lblSignatureEditTitle" runat="server" Text="Edit Signature"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="formlabelalt">
                                    <asp:Label ID="lblDefaultEnabled" runat="server">Default Signature:&nbsp;&nbsp;&nbsp;</asp:Label>
                                </td>
                                <td class="formlabelvalue">
                                    <asp:CheckBox ID="chkDefaultEnabled" runat="server" />
                                    <asp:Label ID="lblDefaultWarning" runat="server"><span class="valalert">*Checking this will make this signature the new default for appointments.</span></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="formlabelalt">
                                    <asp:Label ID="lblTitle" runat="server">Title:&nbsp<font color="red">*</font></asp:Label>
                                </td>
                                <td class="formlabelvalue">
                                    <asp:TextBox ID="txtTitle" runat="server" Width="400px" MaxLength="250"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtTitle" ErrorMessage="Title is required."
                                        ID="rfvTitle" runat="server" ValidationGroup="Save" Text="Title is required."
                                        ToolTip="Title is required." Display="Dynamic"><br />Title is required.</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="formlabelalt">
                                    <asp:Label ID="lblSignature" runat="server">Signature Message:&nbsp<font color="red">*</font></asp:Label>
                                </td>
                                <td class="formlabelvalue">
                                    <telerik:RadEditor ID="reSignature" runat="server" EnableResize="false" Width="100%"
                                        MaxHtmlLength="2000" Height="350px">
                                        <Tools>
                                            <telerik:EditorToolGroup>
                                                <telerik:EditorTool Name="Bold" />
                                                <telerik:EditorTool Name="Italic" />
                                                <telerik:EditorTool Name="Underline" />
                                                <telerik:EditorTool Name="Copy" />
                                                <telerik:EditorTool Name="Cut" />
                                                <telerik:EditorTool Name="Paste" />
                                                <telerik:EditorTool Name="FontSize" />
                                                <telerik:EditorTool Name="ForeColor" />
                                            </telerik:EditorToolGroup>
                                        </Tools>
                                    </telerik:RadEditor>
                                    <asp:RequiredFieldValidator ControlToValidate="reSignature" ErrorMessage="Signature Message is required."
                                        ID="rfvMessage" runat="server" ValidationGroup="Save" Text="Signature Message is required."
                                        ToolTip="Signature Message is required." Display="Dynamic">Signature Message is required.</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" />
                                    &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                    <br />
                                    <asp:ValidationSummary ID="vsSave" runat="server" ValidationGroup="Save" ShowMessageBox="True"
                                        Visible="True" ShowSummary="False" />
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
