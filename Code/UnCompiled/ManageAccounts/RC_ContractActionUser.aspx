<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RC_ContractActionUser.aspx.vb"
    Inherits="NOAATEAMSAdmin.RC_ContractActionUser" MasterPageFile="~/ManageAccounts/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr valign="top">
            <td class="rightTitle">
                Contract Action List User Administration
            </td>
        </tr>
        <tr>
            <td class="rightContent">
            <br />
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td colspan="3">
                            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" CausesValidation="False">
                                <Tabs>
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
                                    <telerik:RadTab runat="server" Text="User Administration" Selected="true" NavigateUrl="RC_ContractActionUser.aspx">
                                    </telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                        </td>
                    </tr>
                </table>
                <telerik:RadAjaxPanel ID="pnlAdmin" runat="server">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Label ID="lbl1" runat="server" Text="Admin" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAdmin_SortCommand" runat="server" Text="ALL" Visible="False"></asp:Label>
                                <div>
                                    <asp:LinkButton ID="Admin_ALL" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="ALL">ALL</asp:LinkButton>&nbsp;|&nbsp;
                                    <asp:LinkButton ID="Admin_A" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="A">A</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_B" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="B">B</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_C" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="C">C</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_D" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="D">D</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_E" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="E">E</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_F" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="F">F</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_G" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="G">G</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_H" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="H">H</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_I" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="I">I</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_J" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="J">J</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_K" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="K">K</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_L" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="L">L</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_M" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="M">M</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_N" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="N">N</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_O" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="O">O</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_P" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="P">P</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_Q" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="Q">Q</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_R" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="R">R</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_S" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="S">S</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_T" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="T">T</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_U" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="U">U</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_V" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="V">V</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_W" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="W">W</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_X" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="X">X</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_Y" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="Y">Y</asp:LinkButton>
                                    <asp:LinkButton ID="Admin_Z" runat="server" Font-Bold="True" Font-Size="Smaller"
                                        CommandName="Z">Z</asp:LinkButton>
                                </div>
                                <telerik:RadListBox ID="lstAdminUsersAll" runat="server" Width="304px" Height="211px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                            <td>
                                <asp:Button ID="btnAdminAdd" runat="server" Width="85px" Text="Add >>"></asp:Button><br />
                                <asp:Button ID="btnAdminRemove" runat="server" Width="85px" Text="<< Remove"></asp:Button>
                            </td>
                            <td>
                                <telerik:RadListBox ID="lstAdminUsers" runat="server" Width="304px" Height="211px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                        </tr>
                    </table>
                </telerik:RadAjaxPanel>
                <telerik:RadAjaxPanel ID="pnlHeadquarters" runat="server">
                    <table>
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Label ID="Label1" runat="server" Text="Headquarters" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblHQ_SortCommand" runat="server" Text="ALL" Visible="false"></asp:Label>
                                <div>
                                    <asp:LinkButton ID="HQ_ALL" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="ALL">ALL</asp:LinkButton>&nbsp;|&nbsp;
                                    <asp:LinkButton ID="HQ_A" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="A">A</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_B" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="B">B</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_C" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="C">C</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_D" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="D">D</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_E" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="E">E</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_F" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="F">F</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_G" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="G">G</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_H" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="H">H</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_I" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="I">I</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_J" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="J">J</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_K" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="K">K</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_L" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="L">L</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_M" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="M">M</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_N" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="N">N</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_O" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="O">O</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_P" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="P">P</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_Q" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Q">Q</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_R" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="R">R</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_S" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="S">S</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_T" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="T">T</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_U" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="U">U</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_V" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="V">V</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_W" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="W">W</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_X" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="X">X</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_Y" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Y">Y</asp:LinkButton>
                                    <asp:LinkButton ID="HQ_Z" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Z">Z</asp:LinkButton>
                                </div>
                                <telerik:RadListBox ID="lstHeadquartersAll" runat="server" Width="304px" Height="211px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                            <td>
                                <asp:Button ID="btnHeadquartersAdd" runat="server" Width="85px" Text="Add >>"></asp:Button><br />
                                <asp:Button ID="btnHeadquartersRemove" runat="server" Width="85px" Text="<< Remove">
                                </asp:Button>
                            </td>
                            <td>
                                <telerik:RadListBox ID="lstHeadquarters" runat="server" Width="304px" Height="211px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                        </tr>
                    </table>
                </telerik:RadAjaxPanel>
                <telerik:RadAjaxPanel ID="pnlNorthEast" runat="server">
                    <table>
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Label ID="Label2" runat="server" Text="North East" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNE_SortCommand" runat="server" Text="ALL" Visible="false"></asp:Label>
                                <div>
                                    <asp:LinkButton ID="NE_ALL" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="ALL">ALL</asp:LinkButton>&nbsp;|&nbsp;
                                    <asp:LinkButton ID="NE_A" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="A">A</asp:LinkButton>
                                    <asp:LinkButton ID="NE_B" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="B">B</asp:LinkButton>
                                    <asp:LinkButton ID="NE_C" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="C">C</asp:LinkButton>
                                    <asp:LinkButton ID="NE_D" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="D">D</asp:LinkButton>
                                    <asp:LinkButton ID="NE_E" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="E">E</asp:LinkButton>
                                    <asp:LinkButton ID="NE_F" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="F">F</asp:LinkButton>
                                    <asp:LinkButton ID="NE_G" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="G">G</asp:LinkButton>
                                    <asp:LinkButton ID="NE_H" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="H">H</asp:LinkButton>
                                    <asp:LinkButton ID="NE_I" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="I">I</asp:LinkButton>
                                    <asp:LinkButton ID="NE_J" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="J">J</asp:LinkButton>
                                    <asp:LinkButton ID="NE_K" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="K">K</asp:LinkButton>
                                    <asp:LinkButton ID="NE_L" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="L">L</asp:LinkButton>
                                    <asp:LinkButton ID="NE_M" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="M">M</asp:LinkButton>
                                    <asp:LinkButton ID="NE_N" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="N">N</asp:LinkButton>
                                    <asp:LinkButton ID="NE_O" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="O">O</asp:LinkButton>
                                    <asp:LinkButton ID="NE_P" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="P">P</asp:LinkButton>
                                    <asp:LinkButton ID="NE_Q" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Q">Q</asp:LinkButton>
                                    <asp:LinkButton ID="NE_R" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="R">R</asp:LinkButton>
                                    <asp:LinkButton ID="NE_S" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="S">S</asp:LinkButton>
                                    <asp:LinkButton ID="NE_T" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="T">T</asp:LinkButton>
                                    <asp:LinkButton ID="NE_U" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="U">U</asp:LinkButton>
                                    <asp:LinkButton ID="NE_V" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="V">V</asp:LinkButton>
                                    <asp:LinkButton ID="NE_W" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="W">W</asp:LinkButton>
                                    <asp:LinkButton ID="NE_X" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="X">X</asp:LinkButton>
                                    <asp:LinkButton ID="NE_Y" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Y">Y</asp:LinkButton>
                                    <asp:LinkButton ID="NE_Z" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Z">Z</asp:LinkButton>
                                </div>
                                <telerik:RadListBox ID="lstNorthEastAll" runat="server" Width="304px" Height="211px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                            <td>
                                <asp:Button ID="btnNorthEastAdd" runat="server" Width="85px" Text="Add >>"></asp:Button><br />
                                <asp:Button ID="btnNorthEastRemove" runat="server" Width="85px" Text="<< Remove">
                                </asp:Button>
                            </td>
                            <td>
                                <telerik:RadListBox ID="lstNorthEast" runat="server" Width="304px" Height="211px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                        </tr>
                    </table>
                </telerik:RadAjaxPanel>
                <telerik:RadAjaxPanel ID="pnlNorthWest" runat="server">
                    <table>
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Label ID="Label3" runat="server" Text="North West" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNW_SortCommand" runat="server" Text="ALL" Visible="false"></asp:Label>
                                <div>
                                    <asp:LinkButton ID="NW_ALL" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="ALL">ALL</asp:LinkButton>&nbsp;|&nbsp;
                                    <asp:LinkButton ID="NW_A" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="A">A</asp:LinkButton>
                                    <asp:LinkButton ID="NW_B" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="B">B</asp:LinkButton>
                                    <asp:LinkButton ID="NW_C" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="C">C</asp:LinkButton>
                                    <asp:LinkButton ID="NW_D" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="D">D</asp:LinkButton>
                                    <asp:LinkButton ID="NW_E" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="E">E</asp:LinkButton>
                                    <asp:LinkButton ID="NW_F" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="F">F</asp:LinkButton>
                                    <asp:LinkButton ID="NW_G" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="G">G</asp:LinkButton>
                                    <asp:LinkButton ID="NW_H" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="H">H</asp:LinkButton>
                                    <asp:LinkButton ID="NW_I" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="I">I</asp:LinkButton>
                                    <asp:LinkButton ID="NW_J" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="J">J</asp:LinkButton>
                                    <asp:LinkButton ID="NW_K" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="K">K</asp:LinkButton>
                                    <asp:LinkButton ID="NW_L" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="L">L</asp:LinkButton>
                                    <asp:LinkButton ID="NW_M" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="M">M</asp:LinkButton>
                                    <asp:LinkButton ID="NW_N" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="N">N</asp:LinkButton>
                                    <asp:LinkButton ID="NW_O" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="O">O</asp:LinkButton>
                                    <asp:LinkButton ID="NW_P" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="P">P</asp:LinkButton>
                                    <asp:LinkButton ID="NW_Q" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Q">Q</asp:LinkButton>
                                    <asp:LinkButton ID="NW_R" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="R">R</asp:LinkButton>
                                    <asp:LinkButton ID="NW_S" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="S">S</asp:LinkButton>
                                    <asp:LinkButton ID="NW_T" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="T">T</asp:LinkButton>
                                    <asp:LinkButton ID="NW_U" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="U">U</asp:LinkButton>
                                    <asp:LinkButton ID="NW_V" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="V">V</asp:LinkButton>
                                    <asp:LinkButton ID="NW_W" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="W">W</asp:LinkButton>
                                    <asp:LinkButton ID="NW_X" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="X">X</asp:LinkButton>
                                    <asp:LinkButton ID="NW_Y" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Y">Y</asp:LinkButton>
                                    <asp:LinkButton ID="NW_Z" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Z">Z</asp:LinkButton>
                                </div>
                                <telerik:RadListBox ID="lstNorthWestAll" runat="server" Width="304px" Height="211px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                            <td>
                                <asp:Button ID="btnNorthWestAdd" runat="server" Width="85px" Text="Add >>"></asp:Button><br />
                                <asp:Button ID="btnNorthWestRemove" runat="server" Width="85px" Text="<< Remove">
                                </asp:Button>
                            </td>
                            <td>
                                <telerik:RadListBox ID="lstNorthWest" runat="server" Width="304px" Height="211px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                        </tr>
                    </table>
                </telerik:RadAjaxPanel>
                <telerik:RadAjaxPanel ID="pnlSouthWest" runat="server">
                    <table>
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Label ID="Label4" runat="server" Text="South West" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSW_SortCommand" runat="server" Text="ALL" Visible="false"></asp:Label>
                                <div>
                                    <asp:LinkButton ID="SW_ALL" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="ALL">ALL</asp:LinkButton>&nbsp;|&nbsp;
                                    <asp:LinkButton ID="SW_A" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="A">A</asp:LinkButton>
                                    <asp:LinkButton ID="SW_B" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="B">B</asp:LinkButton>
                                    <asp:LinkButton ID="SW_C" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="C">C</asp:LinkButton>
                                    <asp:LinkButton ID="SW_D" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="D">D</asp:LinkButton>
                                    <asp:LinkButton ID="SW_E" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="E">E</asp:LinkButton>
                                    <asp:LinkButton ID="SW_F" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="F">F</asp:LinkButton>
                                    <asp:LinkButton ID="SW_G" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="G">G</asp:LinkButton>
                                    <asp:LinkButton ID="SW_H" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="H">H</asp:LinkButton>
                                    <asp:LinkButton ID="SW_I" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="I">I</asp:LinkButton>
                                    <asp:LinkButton ID="SW_J" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="J">J</asp:LinkButton>
                                    <asp:LinkButton ID="SW_K" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="K">K</asp:LinkButton>
                                    <asp:LinkButton ID="SW_L" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="L">L</asp:LinkButton>
                                    <asp:LinkButton ID="SW_M" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="M">M</asp:LinkButton>
                                    <asp:LinkButton ID="SW_N" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="N">N</asp:LinkButton>
                                    <asp:LinkButton ID="SW_O" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="O">O</asp:LinkButton>
                                    <asp:LinkButton ID="SW_P" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="P">P</asp:LinkButton>
                                    <asp:LinkButton ID="SW_Q" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Q">Q</asp:LinkButton>
                                    <asp:LinkButton ID="SW_R" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="R">R</asp:LinkButton>
                                    <asp:LinkButton ID="SW_S" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="S">S</asp:LinkButton>
                                    <asp:LinkButton ID="SW_T" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="T">T</asp:LinkButton>
                                    <asp:LinkButton ID="SW_U" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="U">U</asp:LinkButton>
                                    <asp:LinkButton ID="SW_V" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="V">V</asp:LinkButton>
                                    <asp:LinkButton ID="SW_W" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="W">W</asp:LinkButton>
                                    <asp:LinkButton ID="SW_X" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="X">X</asp:LinkButton>
                                    <asp:LinkButton ID="SW_Y" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Y">Y</asp:LinkButton>
                                    <asp:LinkButton ID="SW_Z" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Z">Z</asp:LinkButton>
                                </div>
                                <telerik:RadListBox ID="lstSouthWestAll" runat="server" Width="304px" Height="211px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSouthWestAdd" runat="server" Width="85px" Text="Add >>"></asp:Button><br />
                                <asp:Button ID="btnSouthWestRemove" runat="server" Width="85px" Text="<< Remove">
                                </asp:Button>
                            </td>
                            <td>
                                <telerik:RadListBox ID="lstSouthWest" runat="server" Width="304px" Height="211px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                        </tr>
                    </table>
                </telerik:RadAjaxPanel>
                <telerik:RadAjaxPanel ID="pnlSouthEast" runat="server">
                    <table>
                        <tr>
                            <td colspan="3" align="center">
                                <br />
                                <asp:Label ID="Label5" runat="server" Text="South East" Font-Bold="true" Font-Size="Larger"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSE_SortCommand" runat="server" Text="ALL" Visible="false"></asp:Label>
                                <div>
                                    <asp:LinkButton ID="SE_ALL" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="ALL">ALL</asp:LinkButton>&nbsp;|&nbsp;
                                    <asp:LinkButton ID="SE_A" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="A">A</asp:LinkButton>
                                    <asp:LinkButton ID="SE_B" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="B">B</asp:LinkButton>
                                    <asp:LinkButton ID="SE_C" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="C">C</asp:LinkButton>
                                    <asp:LinkButton ID="SE_D" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="D">D</asp:LinkButton>
                                    <asp:LinkButton ID="SE_E" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="E">E</asp:LinkButton>
                                    <asp:LinkButton ID="SE_F" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="F">F</asp:LinkButton>
                                    <asp:LinkButton ID="SE_G" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="G">G</asp:LinkButton>
                                    <asp:LinkButton ID="SE_H" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="H">H</asp:LinkButton>
                                    <asp:LinkButton ID="SE_I" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="I">I</asp:LinkButton>
                                    <asp:LinkButton ID="SE_J" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="J">J</asp:LinkButton>
                                    <asp:LinkButton ID="SE_K" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="K">K</asp:LinkButton>
                                    <asp:LinkButton ID="SE_L" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="L">L</asp:LinkButton>
                                    <asp:LinkButton ID="SE_M" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="M">M</asp:LinkButton>
                                    <asp:LinkButton ID="SE_N" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="N">N</asp:LinkButton>
                                    <asp:LinkButton ID="SE_O" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="O">O</asp:LinkButton>
                                    <asp:LinkButton ID="SE_P" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="P">P</asp:LinkButton>
                                    <asp:LinkButton ID="SE_Q" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Q">Q</asp:LinkButton>
                                    <asp:LinkButton ID="SE_R" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="R">R</asp:LinkButton>
                                    <asp:LinkButton ID="SE_S" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="S">S</asp:LinkButton>
                                    <asp:LinkButton ID="SE_T" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="T">T</asp:LinkButton>
                                    <asp:LinkButton ID="SE_U" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="U">U</asp:LinkButton>
                                    <asp:LinkButton ID="SE_V" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="V">V</asp:LinkButton>
                                    <asp:LinkButton ID="SE_W" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="W">W</asp:LinkButton>
                                    <asp:LinkButton ID="SE_X" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="X">X</asp:LinkButton>
                                    <asp:LinkButton ID="SE_Y" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Y">Y</asp:LinkButton>
                                    <asp:LinkButton ID="SE_Z" runat="server" Font-Bold="True" Font-Size="Smaller" CommandName="Z">Z</asp:LinkButton>
                                </div>
                                <telerik:RadListBox ID="lstSouthEastAll" runat="server" Width="304px" Height="207px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSouthEastAdd" runat="server" Width="85px" Text="Add >>"></asp:Button><br />
                                <asp:Button ID="btnSouthEastRemove" runat="server" Width="85px" Text="<< Remove">
                                </asp:Button>
                            </td>
                            <td>
                                <telerik:RadListBox ID="lstSouthEast" runat="server" Width="300px" Height="211px"
                                    DataValueField="LoginID">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.LastName")%>,
                                        <%# DataBinder.Eval(Container, "DataItem.FirstName")%>
                                    </ItemTemplate>
                                </telerik:RadListBox>
                            </td>
                        </tr>
                    </table>
                </telerik:RadAjaxPanel>
                <%--<asp:Button ID="btnReturn" runat="server" Text="Return to Action List" />--%>
                <%--</div>--%>
            </td>
        </tr>
    </table>
</asp:Content>
