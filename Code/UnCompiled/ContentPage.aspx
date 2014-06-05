<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ContentPage.aspx.vb" Inherits="VANS.ContentPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <title>Content Title</title>
    <style type="text/css">  
    html, body, form 
    {  
        height: 100%;  
        margin: 0px;  
        padding: 0px;  
    }  
    </style>  
    <link href="styles/mainstyle.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#DAE2E8">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only." width="100%">
		    <tr valign="top"><td class="rightTitle"><asp:Label ID="lblTitleText" runat="server" Text="" Width="100%"></asp:Label>
            </td></tr><tr><td class="rightContent" height="100%"><asp:Label ID="lblContentText" runat="server" 
            Width="100%" Height="100%"></asp:Label></td></tr>
	    </table>
    </div>
    </form>
</body>
</html>
