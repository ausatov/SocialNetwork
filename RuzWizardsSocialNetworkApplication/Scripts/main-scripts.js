function TabsFnc(divID) {
    $(divID).tabs({
        // Change selected tab om mouse over event.
        event: "mouseover",
        // Save current tab in jquery-cookie and restore that after reload.
        cookie: { expires: 7, name: "tabID Cookie" }
    });
}

function DatepickerFnc(tbxName) {
    var minDate = new Date('1/1/1900');
    var defaultDateFormat = 'mm.dd.yy';
    var todaysDate = new Date();
    var maxDate = new Date(todaysDate.getFullYear(),
                               todaysDate.getMonth(),
                               todaysDate.getDate() - 1);
    var currentsYear = todaysDate.getFullYear();

    var range = '1900:' + currentsYear
    $("[name $= " + tbxName + "]").datepicker({
        dateFormat: defaultDateFormat,
        minDate: minDate,
        maxDate: maxDate,
        changeMonth: true,
        changeYear: true,
        yearRange: range
    });
}

var defaultCity = "-- Select City --";

function FillCountryXCitiesCascadeDropList(ddlCountry, ddlCity, webServiceURL) {
    // Web-service URL.
    var pageUrl = webServiceURL;
    // Control ddlCountry OnChange function.
    $("[name $= " + ddlCountry + "]").change(function () {
        // Get ID of selected item in ddlCountry.
        var countryID = $("[name $= " + ddlCountry + "]").find(':selected').val();
        // If that ID equal 'not selected' then append default value.
        if (countryID == '0') {
            $("[name $= " + ddlCity + "]").empty();
            $("[name $= " + ddlCity + "]").append($("<option></option>").val("0").html(defaultCity)).attr('selected', 'selected');
        }
        // Else get list if cities of current country.
        else {
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetAllCountryCities",
                data: "{countryID:'" + countryID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    BindCityDropList(msg.d, ddlCity)
                }
            });
        } 
    });
}

// Bing droplist by cities of current country.
function BindCityDropList(msg, ddlCity) {
    $("[name $= " + ddlCity + "]").empty();
    $("[name $= " + ddlCity + "]").append($("<option></option>").val("0").html(defaultCity));
    $.each(msg, function () {
        $("[name $= " + ddlCity + "]").append($("<option></option>").val(this['Key']).html(this['Value']));
    });
}

// Function executed after the file successfully uploaded.
function AsyncUploadComplete(sender, args, webServiceURL, imgAvatarID) {
    var fileName = args.get_fileName();
    var extensionPointIndex = fileName.lastIndexOf('.');
    var fileExtension = '-1';
    if (extensionPointIndex != -1) {
        fileExtension = fileName.substring(extensionPointIndex).toLowerCase();
    }

    var fileLength = parseInt(args.get_length());
    var defaultUploadLimit = 5242880;

    if (fileLength > defaultUploadLimit) {
        alert("Error. File size too big.");
        return;
    }

    if (fileExtension != '-1'
            && (fileExtension == '.jpg' || fileExtension == '.png' || fileExtension == '.jpeg')) {
        GetAvatarImage(webServiceURL, imgAvatarID, '00000000-0000-0000-0000-000000000000'); 
    }
    else {
        alert("Error. Incorrect file format. Upload only: jpg, jpeg, png.");

        return;
    }
}

function GetAvatarImage(webServiceURL, imgAvatarID, userID) {
    $.ajax({
        type: "POST",
        url: webServiceURL + "/GetUploadedAvatarImage",
        data: "{id:'" + userID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            BindAvatarImage(msg.d, imgAvatarID)
        }
    });
}

// Change image without reload.
function BindAvatarImage(msg, imgAvatarID) {
    var imagePath = "Uploads/Photo/";
    $("img[id$='" + imgAvatarID + "']").attr("src", imagePath + msg + "?" + Math.random());
}

// Executed if the file uploading failed.
function OnAsyncUploadError(sender, args) {
    alert("Upload error.");
}

// Select first item in droplist.
function SelectFirstItem(ddlItemID) {
    $("Select[id$='" + ddlItemID + "'] :first").attr("selected", "selected");
}

function HideSmallAvatar(imgAvatarID) {
    var imagePath = "Uploads/Photo/";
    var imageEmpty = "empty_small.png";
    $("img[id$='" + imgAvatarID + "']").attr("src", imagePath + imageEmpty + "?" + Math.random());
}
