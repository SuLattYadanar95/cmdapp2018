
function ADD(modalname) {
    var modal = $("#" + modalname)
    modal.modal('show');
    $('.datepicker').datepicker({
        format: 'mm/dd/yyyy'
    });
}

function GetPagingInfo(e) {
    var page = 1;
    var $a = $(e).attr("href");
    if ($a != null) {
        var regex = /[?&]([^=#]+)=([^&#]*)/g,
        params = {},
        match;
        while (match = regex.exec($a)) {
            params[match[1]] = match[2];
        }
        page = params.page;
    }
    return page;
}

function EDITWITHKEY(id, source, url, div, formid, modalid, modalname) {    
    $.ajax(
    {
        url: url,
        data: { id: id, source: source },
        beforeSend: function () {
            $(div).empty();
        },
        success: function (data) {
            $(div).empty().html(data);
        },
        complete: function () {
            attachValidator(formid, modalid);
            $(modalname).modal('show');
        }
    })
}

function DELETE(id, url, div) {
    $.ajax(
    {
        url: url,
        data: { id: id },
        beforeSend: function () {

        },
        success: function (data) {
            $(div + id).empty();
        }
    })
}

function DELETEWITHKEY(id, source, url, div) {
    $.ajax(
    {
        url: url,
        data: { id: id, source: source },
        beforeSend: function () {

        },
        success: function (data) {
            $(div + id).empty();
        }
    })
}

function LIST(url, div, pushStateStr, replaceStateStr, push, replace) {
    $.ajax({
        url: url,
        beforeSend: function () {
            $(div).empty().append("<div class='spinnerr' style='margin-top:0px!important'><div class='bounce1'></div><div class='bounce2'></div><div class='bounce3'></div></div>");
            $('.btn').prop("disabled", true);
        },
        success: function (myData) {
            $(div).empty().append(myData);
            //if (replace == "Yes") {
            //    if (push == "Yes") {
            //        history.pushState("","",pushStateStr);
            //    }
            //    history.replaceState("","",replaceStateStr);
            //}
        },
        complete: function () {
            $('.btn').prop("disabled", false);
        }
    });
}

//function getStates(state, hstate, township, htownship) {
    
//    $.ajax({
//        url: '@Url.Action("GetStates", "Home")',
//        type: 'Get',
//        cache: false,
//        success: function (result) {
//            $(state).empty();
//            for (i = 0; i < result.length; i++) {
//                $(state).append($('<option></option>').text(result[i]).attr('ID', result[i]));
//            }
//        },
//        complete: function () {
//            $(state).val($(hstate).val());
//            alert($(state).val());
//            $.ajax({
//                url: '@Url.Action("GetTownships","Home")',
//                type: 'Get',
//                cache: false,
//                data: { selectedState: $(hstate).val() },
//                success: function (result) {
//                    $(township).empty();
//                    for (i = 0; i < result.length; i++) {
//                        $(township).append($('<option></option>').text(result[i]).attr('ID', result[i]));
//                    }
//                },
//                complete: function () {
//                    $(township).val($(htownship).val());
//                }
//            })
//        }
//    })
//}