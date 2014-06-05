<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ErrorPage.aspx.vb" Inherits="VANS.ErrorPage" MasterPageFile="~/vaLogin.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Error Page</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LoginContentPlaceHolder" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td height="80" minheight="80" maxheight="80" scrolling="none" style="background-color: rgb(194, 216, 232);">
                <img src="./img/header.png" id="prBanner" height="80px" alt="VOA"
                    title="VOA Projects" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
        frame="void" width="100%">
        <tr valign="top">
            <td style="background-color: rgb(194, 216, 232);">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <br />
                An Error Has Occurred in the Application. 
                <br /> Please contact your Systems Administrator for additional assistance.
                <br />
                <br />
            </td>
        </tr>
        <tr valign="top">
            <td style="background-color: rgb(194, 216, 232);">
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
