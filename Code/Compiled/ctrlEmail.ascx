<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlEmail.ascx.vb" Inherits="VANS.ctrlEmail" %>
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

    // The code below can be used for the focus if the M&D and Q&A pages will need focus for postback
    //    try {
    //        var FocusClientID = $get("ctrlEMail_lblFocusClientID").innerText;
    //    }
    //    catch (e) {
    //        try {
    //            var FocusClientID = $get("ctrlMessageDocuments_ctrlEMail_lblFocusClientID").innerText;
    //        }
    //        catch (e) {
    //            try {
    //                var FocusClientID = $get("ctrlQuestionAnswer_ctrlEMail_lblFocusClientID").innerText;
    //            }
    //            catch (e) {
    //            }
    //        }
    //    }
    function focus() {
        try {
            var FocusClientID = $get("ctrlEMail_lblFocusClientID").innerText;
            if (FocusClientID != "") {
                setTimeout(function () {
                    $get(FocusClientID).focus();
                }, 0);
            }
            FocusClientID.innerText = "";
        }
        catch (e) {
        }
    }

    //]]>

</script>
<asp:Panel ID="pnlcheck" runat="server">
    <asp:CheckBox ID="chkEmail" runat="server" Text="Send Email" AutoPostBack="true"
        Checked="true" />
    <asp:Label ID="lblEmailModuleType" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblProjectVendorID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblEmailRecordID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblRecordTypeID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblRecordID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblOriginalBodyText" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblEmailTo" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblProjectNumber" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblIDIQName" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblProjectTItle" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblQADate" runat="server" Visible="false"></asp:Label>
</asp:Panel>
<asp:Panel ID="pnlEmail" runat="server" Visible="true">
    <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
        width="100%">
        <tr valign="top">
            <td class="rightSubTitle">
                Vendor Email
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEmailAlert" runat="server" CssClass="valalert">  Please verify the default message below and append any additional
                    information that may describe this change/submission.</asp:Label><br />
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lbltitle" runat="server" AssociatedControlID="txtTitle">Title: </asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTitle" runat="server" Width="600px" MaxLength="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="lblBody" runat="server" AssociatedControlID="reContentText">Body: <span class="hidden">Please verify the default message below and append any additional
                                information that may describe this change/submission.</span></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <telerik:RadEditor ID="reContentText" runat="server" OnClientCommandExecuting="OnClientCommandExecuting"
                                EnableResize="false" OnClientLoad="focus">
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
                                    </telerik:EditorToolGroup>
                                </Tools>
                                <Content>                    
                                </Content>
                            </telerik:RadEditor>
                            <asp:Label ID="lblEmailWarning" runat="server" CssClass="valalert" Visible="false">Fields ##ProjectNumber, ##ProjectTitle and  will be filled in upon save with the new Project details.</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="hidden">
                            <asp:Label ID="lblFocusClientID" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<script type="text/javascript">
    try {
        Telerik.Web.UI.Editor.CommandList.InsertTab = function (commandName, editor, oTool) {
            setTimeout(function () {

                $get(editor.get_id() + "_ModesWrapper").focus();
            }, 0);
        };
    }
    catch (e) {
    }
</script>
