$(document).ready(function () {
    var ddlBans = $('#<%=ddlBans.ClientID%>');
    ddlBans.hide();
    $("#<%=imgBtnSave.ClientID%>").hide();
    $("#<%=imgBtnCancel.ClientID%>").hide();
    $("#<%=tbReason.ClientID%>").hide();
    $("#<%=tbToDate.ClientID%>").datepicker();
    $("#<%=tbToDate.ClientID%>").hide();
});
$(function () {
    $(".tb").autocomplete({
        source: function (request, response) {
            var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
            $.ajax({
                url: pageUrl + "/FetchEmailList",
                data: "{ 'mail': '" + request.term + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            value: item.Email
                        }
                    }))
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    alert(err.Message);
                }
            });
        },
        minLength: 2
    });
});


ddlBans.change(function (e) {

    var ddlBans = $('#<%=ddlBans.ClientID%>');
    var banId = ddlBans.val();
    if (banId != -1) {
        // Get Ban Details
        GetBanDetails(banId);
    }
    else {
        $("#outputTable").hide();
    }
});
// });
function Start() {
    var tbUserMail = $("#<%=tbAuto.ClientID%>");
    var userMail = tbUserMail.val();
    var ddlBans = $('#<%=ddlBans.ClientID%>');
    $("#outputTable").hide();
    ddlBans.empty();
    ddlBans.append('<option value="-1">--Select from ad to ban date --</option>');
    GetAllBans(userMail);

    ddlBans.change(function (e) {
        OnCancelClick();
        var banId = ddlBans.val();
        if (banId != -1) {
            // Get Ban Details
            GetBanDetails(banId.toString());
        }
        else {
            $("#outputTable").hide();
        }
    });
}



function GetAllBans(userMail) {
    var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
    var ddlBans = $('#<%=ddlBans.ClientID%>');
    $.ajax({
        type: "POST",
        url: pageUrl + "/GetAllBans",
        data: "{'userMail':'" + userMail + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (response) {
            var Bans = response.d;
            $.each(Bans, function (index, ban) {
                ddlBans.append('<option value="' + ban.Key + '">' + ban.Value + '</option>');
            });
            ddlBans.show();
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }

    });
}



function GetBanDetails(banId) {
    var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
    $.ajax({
        type: "POST",
        url: pageUrl + "/GetBan",
        data: "{'banId':'" + banId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (response) {
            var ban = response.d;
            $("#spnBanId").html(ban.ID);
            $("#spnBanReason").html(ban.Reason);
            //$("#<%=tbReason.ClientID%>").val(ban.Reason.toString());
            var parsedFromDate = new Date(parseInt(ban.FromDate.toString().substr(6)));
            $("#spnFromDate").html(parsedFromDate.toString());
            var parsedToDate = new Date(parseInt(ban.ToDate.toString().substr(6)));
            $("#spnToDate").html(parsedToDate.toString());
            $("#outputTable").show();
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }

    });
}


function Edit() {
    var reason = $('#spnBanReason').html();
    $("#<%=imgBtnEditBan.ClientID%>").hide();
    $("#<%=imgBtnSave.ClientID%>").show();
    $("#<%=imgBtnCancel.ClientID%>").show();
    $("#<%=tbReason.ClientID%>").val(reason.toString());
    $("#<%=tbReason.ClientID%>").show();
    $("#<%=tbToDate.ClientID%>").val("");
    $("#<%=tbToDate.ClientID%>").show();

}
function OnSaveClick() {
    var banReason = $("#<%=tbReason.ClientID%>").val();
    var toDate = $("#<%=tbToDate.ClientID%>").val();
    var banId = $('#<%=ddlBans.ClientID%>').val();
    SaveParams(banReason, toDate, banId);

}
function SaveParams(banReason, toDate, banId) {
    var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
    $.ajax({
        type: "POST",
        url: pageUrl + "/UpdateBan",
        data: "{'banReason': '" + banReason + "', 'toDate': '" + toDate + "','banId':'" + banId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (response) {
            OnCancelClick();
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    });
}

function OnCancelClick() {
    $("#<%=imgBtnEditBan.ClientID%>").show();
    $("#<%=imgBtnSave.ClientID%>").hide();
    $("#<%=imgBtnCancel.ClientID%>").hide();
    $("#<%=tbReason.ClientID%>").hide();
    $("#<%=tbToDate.ClientID%>").hide();
}

function OnDeleteClick() {
    if (confirm('Are you sure to delete this ban?') == true) {
        var banId = $('#<%=ddlBans.ClientID%>').val();
        Deleteban(banId);
    }
    else {
        return false;
    }
}
function Deleteban(banId) {
    var pageUrl = '<%=ResolveUrl("~/WebServices/SocialNetworkService.asmx")%>';
    $.ajax({
        type: "POST",
        url: pageUrl + "/DeleteBan",
        data: "{'banId':'" + banId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (response) {
            alert("success");
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    });
}