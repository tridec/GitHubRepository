var initialValue = 0.0;
function cleanUpCurrency(s) {
    var expression = /^\(\$?[\d,\.]*\)$/;

    //Check if it is in the proper format
    if (s.match(expression)) {
        //It matched - strip out parentheses and append - at front      
        return parseFloat('-' + s.replace(/[\$\(\),]/g, ''));
    }
    else {
        return parseFloat(s.replace(/[\$,]/g, ''));
    }
}
function validateBalance(sender, args) {
    var balanceLabelControl = $get(sender.balanceLabel);
    args.IsValid = cleanUpCurrency(balanceLabelControl.innerText) >= 0;
}
function updateODCTravelTotal(totalLabel, changedBox, budgetLabel, balanceLabel) {
    var totalLabelControl = $get(totalLabel);
    var changedBoxControl = $get(changedBox);
    var tempValue = cleanUpCurrency(totalLabelControl.innerText) - initialValue;
    var newValue = parseFloat(tempValue);
    if (!isNaN(cleanUpCurrency(changedBoxControl.value))) {
        newValue = newValue + cleanUpCurrency(changedBoxControl.value);
    }
    totalLabelControl.innerText = addCommas(newValue.toFixed(2));
    if (arguments[3]) {
        var budgetLabelControl = $get(budgetLabel);
        var balanceLabelControl = $get(balanceLabel);
        newValue = cleanUpCurrency(budgetLabelControl.innerText) - newValue;
        balanceLabelControl.innerText = addCommas(newValue.toFixed(2));
    }
}
function getInitialValue(changedBox) {
    var changedBoxControl = document.getElementById(changedBox);
    initialValue = cleanUpCurrency(changedBoxControl.value);
    if (isNaN(initialValue)) {
        initialValue = 0.0;
    }
}
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    if (x1.substr(0, 1) == '-') {
        return '($' + x1.substr(1, x1.length - 1) + x2 + ')';
    }
    else {
        return '$' + x1 + x2;
    }
}
