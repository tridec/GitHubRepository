<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DocumentAdd.aspx.vb" Inherits="VANS.DocumentAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add/Update Documents</title>
    <style type="text/css">
        html, body, form
        {
            height: 100%;
            margin: 0px;
            padding: 0px;
        }
    </style>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <link href="styles/mainstyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        <!--
        function btnSaveChecks() 
        { 
            var isValid = false;
            isValid = (Page_ClientValidate("Submit"));
                if (isValid) 
                {
                   isValid = (Page_ClientValidate("Save"));
                } 
                 return isValid;
        }

        function validateRUL(source, arguments) {
            try {

                arguments.IsValid = $find("<telerik:RadCodeBlock runat="server"><%= rulUpload.ClientID %></telerik:RadCodeBlock>").validateExtensions();

            }
            catch (e) {
                //catch and just suppress error
                arguments.IsValid = true;
            } 
        }

        function checkRequiredUpload(source, arguments)  {
        try{ 
                var radUpload = $find("<telerik:RadCodeBlock runat="server"><%= rulUpload.ClientID %></telerik:RadCodeBlock>");
                var fileInputs = radUpload.getFileInputs();
                if (fileInputs[0].value==0) {
                    arguments.IsValid = false;
                } else {
                    arguments.IsValid = true;
                }}
        catch(e){
        //catch and just suppress error
        arguments.IsValid = true;
        }}

        // -->
    </script>
</head>
<body bgcolor="#DAE2E8">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" align="left" summary="This table is for layout purposes only."
        width="100%">
        <tr valign="top">
            <td class="rightTitle" colspan="2">
                <b>Add/Update Documents</b>
            </td>
        </tr>
        <tr>
            <td class="rightContent">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2">
                            <br />
                            <asp:Label ID="lblWarning" runat="server" class="valalert"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="right">
                            <asp:Label ID="lblNodeID" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblTitle" runat="server" For="txtTitle" Font-Size="Larger">Title:&nbsp;&nbsp;</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" MaxLength="250" Width="305px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            <asp:Label ID="lblDescription" runat="server" For="txtDescription" Font-Size="Larger">Description:&nbsp;&nbsp;</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="305px" MaxLength="250" Rows="3"
                                TextMode="MultiLine"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="txtDescription"
                                ValidationExpression="^[\s\S]{0,250}$" ErrorMessage="Maximum 250 characters are allowed for the Description."
                                Text="Maximum 250 characters are allowed for the Description.<br />" ValidationGroup="Submit"><br />Maximum 250 characters are allowed for the Description.</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblVersion" runat="server" For="lblVersionID" Font-Size="Larger">Version:&nbsp;&nbsp;</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblVersionID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <%-- set publish to visible false  not used --%>
                    <%--   <asp:Panel ID="pnlPublish" runat="server" Visible="false">
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblPublish" runat="server" For="cbxPublish" Text="Published: "></asp:Label>
                                <asp:Label ID="lblPublishValue" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbxPublish" runat="server" />
                            </td>
                        </tr>
                    </asp:Panel>--%>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblFile" runat="server" Font-Size="Larger" for="rulUpload">Select File: <span class="hidden">(HTML, Adobe PDF or Microsoft Word Format and less than 100MB)</span>&nbsp;</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFileRules" runat="server"><span class="small">(Must be .htm, .html, .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .mpp, .zip, or .vsd and be less than 100Mb.)</span></asp:Label>
                            <asp:CustomValidator runat="server" ID="CustomValidatorRUL" Display="Dynamic" ClientValidationFunction="validateRUL"
                                ValidationGroup="Save" ErrorMessage="Must be .htm, .html, .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .mpp, .zip, or .vsd and be less than 100Mb."
                                OnServerValidate="validateRULServerValidate">&nbsp;&nbsp;Invalid file for upload (.htm, .html, .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx, .mpp, .zip, or .vsd and less than 100MB)
                            </asp:CustomValidator>
                            <asp:CustomValidator runat="server" ID="CustomValidatorRULRequired" Display="Dynamic"
                                ClientValidationFunction="checkRequiredUpload" ValidationGroup="Save" ErrorMessage="Document required.">&nbsp;&nbsp;Document is required.</asp:CustomValidator>
                            <telerik:RadUpload ID="rulUpload" runat="server" ControlObjectsVisibility="ClearButtons"
                                InputSize="50" MaxFileInputsCount="1" ReadOnlyFileInputs="False" Height="25px"
                                Width="498px" Localization-Select="Browse" select="Browse" AllowedFileExtensions=".htm,.html,.pdf,.doc,.docx,.ppt,.pptx,.xls,.xlsx,.zip,.vsd,.mpp"
                                AllowedMimeTypes="application/pdf,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/vnd.ms-powerpoint,application/vnd.openxmlformats-officedocument.presentationml.presentation,application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/x-zip-compressed,application/octet-stream,application/vnd.ms-visio.viewer,text/html"
                                MaxFileSize="102400000" EnableFileInputSkinning="False" EnableEmbeddedSkins="True">
                            </telerik:RadUpload>
                            <asp:Label ID="lblDocumentUploadID" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CausesValidation="true" ValidationGroup="Save"
                                OnClientClick="return btnSaveChecks();" />
                            &nbsp;<asp:Button ID="btnClose" runat="server" OnClientClick="Page_BlockSubmit = false;" Text="Close" />
                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation="true"
                                ValidationGroup="Submit" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummarySave" runat="server" ValidationGroup="Save"
        ShowMessageBox="True" Visible="True" ShowSummary="False" />
    <asp:ValidationSummary ID="ValidationSummarySubmit" runat="server" ValidationGroup="Submit"
        ShowMessageBox="True" Visible="True" ShowSummary="False" />
    </form>
</body>
</html>
