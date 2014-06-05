<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RadEditPage.aspx.vb" Inherits="VANS.RadEditPage"
    Theme="" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <title>RadEdit Title</title>
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
<body>
    <%--bgcolor="#DAE2E8"--%>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
            width="100%">
            <tr valign="top">
                <td class="rightTitle">
                    Public Pages Management
                    <table>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblVersionddl" AssociatedControlID="ddlVersion" runat="server">Select Version:&nbsp;</asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlVersion" runat="server" DataTextField="Title" DataValueField="Version"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <br />
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblTitle" AssociatedControlID="txtTitleText" runat="server">Page Title:&nbsp;</asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTitleText" runat="server" Text="" Width="75%"></asp:Label>
                                <asp:TextBox ID="txtTitleText" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblVersionText" runat="server" Text="  Version: "></asp:Label>
                                <asp:Label ID="lblVersion" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="rightContent">
                    <asp:Label ID="lblWarning" runat="server" class="valalert"></asp:Label>
                    <asp:Panel ID="pnlUpload" runat="server">
                        <asp:Label ID="lblUpload" AssociatedControlID="rulUpload" runat="server">Upload Picture for Content:
				            <span class="small">(JPG Format and less than 150kb)</span>
				            <br />
                        </asp:Label>
                        <br />
                        <telerik:RadUpload ID="rulUpload" runat="server" ControlObjectsVisibility="ClearButtons"
                            InputSize="50" MaxFileInputsCount="1" ReadOnlyFileInputs="False" Height="25px"
                            Width="498px" Localization-Select="Browse" select="Browse" MaxFileSize="153600"
                            AllowedFileExtensions=".jpg" FocusOnLoad="True" EnableFileInputSkinning="False"
                            EnableEmbeddedSkins="True">
                        </telerik:RadUpload>
                        <asp:Label ID="lblALT" AssociatedControlID="txtTitle" runat="server">Image Alt Text:&nbsp;
                        <span class="hidden">Upload Picture for Content: (JPG Format and less than 150kb)</span></asp:Label>
                        <asp:TextBox ID="txtTitle" runat="server">
                        </asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="rightContent">
                    <asp:Label ID="lblContentlbl" AssociatedControlID="reContentText" runat="server"><b>Page Content:&nbsp;</b><br /></asp:Label>
                    <asp:Label ID="lblContentText" runat="server" Text="" Width="100%"></asp:Label>
                    <telerik:RadEditor ID="reContentText" runat="server" OnClientCommandExecuting="OnClientCommandExecuting">
                        <Tools>
                            <telerik:EditorToolGroup>
                                <telerik:EditorTool Name="Bold" />
                                <telerik:EditorTool Name="Italic" />
                                <telerik:EditorTool Name="Underline" />
                                <telerik:EditorTool Name="Copy" />
                                <telerik:EditorTool Name="Cut" />
                                <telerik:EditorTool Name="Paste" />
                                <telerik:EditorTool Name="FontName" />
                                <telerik:EditorTool Name="FontSize" />
                                <telerik:EditorTool Name="ForeColor" />
                                <telerik:EditorTool Name="AjaxSpellCheck" />
                                <telerik:EditorTool Name="InsertTable" />
                                <telerik:EditorTool Name="LinkManager" />
                                <telerik:EditorTool Name="Unlink" />
                            </telerik:EditorToolGroup>
                        </Tools>
                        <Content>
                    
                        </Content>
                    </telerik:RadEditor>
                    <br />
                    <asp:CheckBox ID="cbxPublish" runat="server" Text="Published" Enabled="false" OnCheckedChanged="ChangePublish" />
                </td>
            </tr>
            <tr>
                <td class="rightContent">
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNewVersion" runat="server" Text="New Version" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblRadEditId" runat="server" Text="" Visible="false"></asp:Label><br />
        <asp:Label ID="lblNodeId" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblNewVersion" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblEditOnly" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblPublish" runat="server" Text="" Visible="false"></asp:Label><br />
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    //<![CDATA[
    function OnClientCommandExecuting(editor, args) {
        var name = args.get_name(); //The command name
        var val = args.get_value(); //The tool that initiated the command


        if (name == "Uploaded Images") {
            //Set the background image to the head of the tool depending on the selected toolstrip item
            var tool = args.get_tool();
            var span = tool.get_element().getElementsByTagName("SPAN")[0];
            span.style.backgroundImage = "url(" + val + ")";

            //Paste the selected in the dropdown emoticon 
            editor.pasteHtml(val);

            //Cancel the further execution of the command
            args.set_cancel(true);
        }

        if (name == "Documents") {
            //Set the background image to the head of the tool depending on the selected toolstrip item
            var tool = args.get_tool();
            var span = tool.get_element().getElementsByTagName("SPAN")[0];
            span.style.backgroundImage = "url(" + val + ")";

            //Paste the selected in the dropdown emoticon 
            editor.pasteHtml(val);

            //Cancel the further execution of the command
            args.set_cancel(true);
        }

        var elem = editor.getSelectedElement(); //Get a reference to the selected element 


        var elem = editor.getSelectedElement(); //Get a reference to the selected element 

    }
    //]]>



</script>
