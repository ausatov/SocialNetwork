
function isValidEmail(email) {
    var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    return pattern.test(email);
}

function vfIsValidEmail(oSrc, args) {
    if ($.trim(args.Value) == '' || !isValidEmail(args.Value)) {
        args.IsValid = false;
        $("[name $= 'tbxEmail']").removeClass("tbxNormal");
        $("[name $= 'tbxEmail']").addClass("tbxSelected");
    }
    else {
        args.IsValid = true;
        $("[name $= 'tbxEmail']").removeClass("tbxSelected");
        $("[name $= 'tbxEmail']").addClass("tbxNormal");
    }
}

function vfIsValidRegEmail(oSrc, args) {
    if ($.trim(args.Value) == '' || !isValidEmail(args.Value)) {
        args.IsValid = false;
    }
}

function vfIsValidPassword(oSrc, args) {
    if ($.trim(args.Value) == '' || args.Value.length < 4) {
        args.IsValid = false;
        $("[name $= 'tbxPassword']").removeClass("tbxNormal");
        $("[name $= 'tbxPassword']").addClass("tbxSelected");
    }
    else {
        args.IsValid = true;
        $("[name $= 'tbxPassword']").removeClass("tbxSelected");
        $("[name $= 'tbxPassword']").addClass("tbxNormal");
    }
}

function vfIsValidRegPassword(oSrc, args) {
    var password1 = $.trim(args.Value);
    if (password1.length < 4) {
        args.IsValid = false;
    }
    else {
        args.IsValid = true;
    }
}

// Измнить стиль поля Photo при отображении поля City 
// (в ddlCountry выбрано значение отличное от 0)
$(document).change(function () {
    var ddlSelectedValue = $("#<%= ddlCountry.ClientID %>").val();
    if (ddlSelectedValue > 0) {
        $('#trPhoto').removeClass("tblNormalRow");
        $('#trPhoto').addClass("tblAlternativeRow");
    }
    else {
        $('#trPhoto').removeClass("tblAlternativeRow");
        $('#trPhoto').addClass("tblNormalRow");
    }
});

    