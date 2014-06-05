// NOTES:
// HiddenField hfDirty must be defined on the page.
// Include "jquery-latest.js" on the page in addition to this script.
// If server page validation fails, hfDirty should be set to "true".
// Any controls that cause a postback but do not navigate away from the page must call client function "overrideConfirm()".
// Save button must have ID "btnSave" if using onunload event (doesn't matter if using onbeforeunload).
// btnSave and all other controls that themselves save or discard changes should call client function "setDirty(false)" or "validateSave(validationGroup)".
// If using "validateSave" function, the control calling the function should not have a ValidationGroup defined or it will cause the validation to fire twice.
var bDirty = false;
function overrideConfirm() {
    window.onunload = null;
    window.onbeforeunload = null;
}
function loadRadEditor(editor) {

    var htmlArea = editor.get_textArea();
    var contentArea = editor.get_contentArea();

    if (htmlArea) {
        htmlArea.attachEvent('onmousedown', function () { setDirty(true); });
        htmlArea.attachEvent('onkeydown', function () { setDirty(true); });
    }

    if (contentArea) {
        contentArea.attachEvent('onmousedown', function () { setDirty(true); });
        contentArea.attachEvent('onkeydown', function () { setDirty(true); });
    }
}
function hfDirtyBool() {
    return document.getElementById("hfDirty").value == "true";
}
function setDirty(b) {
    bDirty = b;
    document.getElementById("hfDirty").value = b;
}
function validateSave(validationGroup, bReturn) {
    var b = Page_ClientValidate(validationGroup);
    setDirty(!b);
    if (arguments[1]) {
        if (bReturn) {
            return b;
        }
    }
}
//window.onunload = function (e) {
//    if (bDirty) {
//        var bSave = confirm('You have unsaved changes.  Click "OK" to save changes or "Cancel" to discard.');
//        if (bSave) {
//            document.getElementById("btnSave").click();
//        }
//    }
//}
window.onbeforeunload = function (e) {
    if (bDirty) {
        return ('You have unsaved changes.  Do you want to discard your changes and leave the page, or stay on the page and continue working?');
    }
}
$(function () {
    bDirty = hfDirtyBool();
    $(":input").change(function () {
        setDirty(true);
    });
});
