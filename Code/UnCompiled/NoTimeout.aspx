<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NoTimeout.aspx.vb" Inherits="VANS.NoTimeout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<%--29 minutes in milliseconds = 1740000
Javascript to prompt for refresh

<script type="text/javascript">
    function timeMsg() {
        var t = setTimeout("alert('Your Session is about to expire, click ok to continue working.'); document.form1.submit();", 240000);
    }
</script>

--%>
    <title></title>
</head>
<body <%--onload="timeMsg();"--%>  >
    <form id="form1" runat="server">
    <div>
    </div>
</form>
</body>
</html>