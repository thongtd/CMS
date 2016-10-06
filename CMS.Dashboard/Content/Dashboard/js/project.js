$(function () {
    jsDivAutoCenter('.ui-dialog');

    /*=============================================Tags============================================*/
    $('#btAddTags').click(function () {
        var vTag = $('#txtTag').val();
        jsAddNewTag(vTag);
    });
    $("#txtTag").bind('keyup', function (e) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var vTag = $('#txtTag').val();
            jsAddNewTag(vTag);
            return event.defaultPrevented || event.returnValue == true;
        }
        return event.defaultPrevented || event.returnValue == false;
    });
    $('#div-tags a').click(function () {
        AddTags(this.id, this.text);
    });
    /*=============================================End Tags============================================*/

    //Them moi the loai tren form popup
    $('button:eq(0)', $('.ui-dialog-buttonset')).click(function () {
        var text = $('input[name=fname]', $('#b_popup_4')).val();
        if (text == '') {
            $('.ui-widget-overlay').show();
            $('.ui-dialog').css('display', 'block');
        }
    });

    $('button:eq(1)', $('.ui-dialog-buttonset')).click(function () {
        $('.ui-widget-overlay').hide();
    });
});

$(window).resize(function () {
    jsDivAutoCenter('.ui-dialog');
});

function jsShowPopup() {
    $('.ui-widget-overlay').css({ display: 'block' });
    $('.lstContent').css({ display: 'block' });
}

function jsClosePopup() {
    $('.ui-widget-overlay').css({ display: 'none' });
    $('.lstContent').css({ display: 'none' });
}

function jsDivAutoCenter(divName) {
    $(divName).css({
        position: 'fixed',
        left: ($(window).width() - $(divName).outerWidth()) / 2,
        top: ($(window).height() - $(divName).outerHeight()) / 2,
        height: 'auto'
    });
}

/*=====================Name To Tag=================================*/
function jsNameToTag(input) {
    if (input.trim().length > 0) {
        $.post("/Admin/Helper/NameToTag", { name: "" + input + "" },
        function (data) {
            $('#Slug').val(data);
        });
    }
}

//Validate number and format number
//Format dinh dang cho cac control kieu input co thuoc tinh val
function jsFormatNumber(nStr, txtControlId) {
    nStr = nStr.replace(/[,]/g, '');
    x = nStr.split(',');
    x1 = x[0];
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    var result = x1 + x2;
    $("input[id=" + txtControlId + "]").val(result);
}

function RemoveImage(imgUrl) {
    var arr = $('#HiddenImages').val().split('|');
    $('.lstImages').html("");
    $('#HiddenImages').val("");
    var lstImg = "";
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] != imgUrl && arr[i] != "") {
            $('.lstImages').append('<img ondblclick=RemoveImage("' + arr[i] + '") src="' + arr[i] + '" title="Click đúp để xóa ảnh."/>');
            lstImg += arr[i] + "|";
        }
    }
    $('#HiddenImages').val(lstImg);
}