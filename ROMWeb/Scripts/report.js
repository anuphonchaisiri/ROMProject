//var sidebarContent;
//$(document).ready(function () {
//    rebindDefaultReport();
//});

function rebindDefaultReport()
{
    $("#numPages").html(" of " + $(".report-form").length);
    
    //View Show
    $("#presentationMode").on('click', function () {
        var docElement, request;
        docElement = document.documentElement;
        request = docElement.requestFullScreen || docElement.webkitRequestFullScreen || docElement.mozRequestFullScreen || docElement.msRequestFullScreen;
        if (typeof request != "undefined" && request) {
            request.call(docElement);
        }
    });
    //Close View
    $("#presentationModeClose").on('click', function () {
        var docElement, request;
        docElement = document;
        request = docElement.cancelFullScreen || docElement.webkitCancelFullScreen || docElement.mozCancelFullScreen || docElement.msCancelFullScreen || docElement.exitFullscreen;
        if (typeof request != "undefined" && request) {
            request.call(docElement);
        }
    });

    //Slice show sidebar
    $("#sidebarToggle").click(function () {
        var outerContainer = $("#outerContainer");
        outerContainer.toggleClass("sidebarClose");
    });

    $("#sidebarContent").html("");
    var rform = $(".report-form");
    for (var i = 0; i < rform.length; i++) {
        var id = i + 1;
        $(rform[i]).attr("A4-page-number", id);
        captureHtml2Canvas($(rform[i]), id);
    }
}
function printPageformToPrinter(obj) {
    var divToPrint = $('.report-form');
    var body = $('<div />');
    for (var i = 0; i < divToPrint.length; i++) {
        $(body).append($(divToPrint[i]).clone());
    }
    var newWin = window.open('', 'Print-Window');
    var header = $("<div />");
    $("head").clone().appendTo($(header));
    var landscape = $("#viewerContainer").attr("layout");
    if (landscape == "landscape")
    {
        $(header).append("<style type='text/css' media='print'> @page { size: landscape; } </style>");
    }

    //var strPrint = "<>"
    newWin.document.open();
    newWin.document.write('<html>' + $(header).html()
                         + '<body onload="window.print()">' + $(body).html()
                         + '</body>'
                      + '</html>');
    newWin.document.close();
    setTimeout(function () { newWin.close(); }, 100);
}

function exportReportWord() {

    if (!window.Blob) {
        alert('Your legacy browser does not support this action.');
        return;
    }

    var html, link, blob, url, css;

    // EU A4 use: size: 841.95pt 595.35pt;
    // US Letter use: size:11.0in 8.5in;

    css = (
      '@page report-form{size: 841.95pt 595.35pt;mso-page-orientation: landscape;}' +
      'div.report-form {page: report-form;}' +
      'table{border-collapse:collapse;width:100%;}td,th{border:1px gray solid;width:5em;padding:2px;}'
    );

    //html = window.docx.innerHTML;

    var divToPrint = $('.report-form');
    var body = $('<div />');
    for (var i = 0; i < divToPrint.length; i++) {
        $(body).append($(divToPrint[i]).html());
    }

    var elementStyle = "";
    var style = $('style');
    for (var i = 0; i < style.length; i++) {
        elementStyle += $(style[i]).html();
    }
    var head = ('<head><style>' + elementStyle + css + '</style></head>');
    html = ('<html>' + head
                     + '<body>' + $(body).html()
                     + '</body>'
                      + '</html>');

    blob = new Blob(['\ufeff', html], {
        type: 'application/msword'
    });
    url = URL.createObjectURL(blob);
    link = document.createElement('A');
    link.href = url;
    // Set default file name. 
    // Word will append file extension - do not add an extension here.
    link.download = 'Document';
    document.body.appendChild(link);
    if (navigator.msSaveOrOpenBlob) navigator.msSaveOrOpenBlob(blob, 'Document.doc'); // IE10-11
    else link.click();  // other browsers
    document.body.removeChild(link);
}

function zoomDataPageA4ForSelected(obj)
{
    var zOld = convertToDecimalReport($(obj).attr("data-zoom"));
    var zNew = convertToDecimalReport($(obj).val());
    var report = $(".report-form");
    for (var i = 0; i < report.length; i++) {
        var bodyA4 = $(report[i]);
        var wWidth = $(bodyA4).css("width").split("px").join("");
        var nWidth = convertToDecimalReport(wWidth);
        var hSize = $(bodyA4).css("height").split("px").join("");
        var nHeight = convertToDecimalReport(hSize);
        $(bodyA4).css({ "width": "" + ((nWidth / zOld) * zNew) + "px", "min-height": "" + ((nHeight / zOld) * zNew) + "px" });

        var div = bodyA4.find("*").not("thead , tbody , tr, th , td");
        for (var j = 0; j < div.length; j++) {
            //var fSize = $(div[j]).css("font-size");
            //fSize = fSize.split("px").join("");
            //var nSize = convertToDecimalReport(fSize);
            //$(div[j]).css({ "font-size": "" + ((nSize / zOld) * zNew) + "px" });
            $(div[j]).css({ "font-size": "" + (14 * zNew) + "px" });
        } 
    }
    $(".report-header").css({ "font-size": "" + (16 * zNew) + "px" });
    $(obj).attr("data-zoom", ""+zNew);
}

//scoll top textbox
function scollingpagereportSelectedClick(mode)
{
    var pageCuurent = convertToDecimalReport($("#pageNumber").val());
    $("#pageNumber").val(pageCuurent + mode);
    scollingpagereportSelected($("#pageNumber"));
   
}


function scollingpagereportSelected(obj)
{
    var index = convertToDecimalReport($(obj).val());
    if (index > $(".report-form").length)
    {
        index = 1;
        $(obj).val(index);
    }
    else if (index <= 0)
    {
        index = $(".report-form").length;
        $(obj).val(index);
    }
    $("#viewerContainer").scrollTop($("#viewerContainer").scrollTop() + $("[A4-page-number='" + index + "']").position().top);
    $(".thumbnailImage").removeClass("active");
    $($(".thumbnailImage")[index-1]).addClass("active");
}


//Properties data
function convertToDecimalReport(val) {
    if (val == "" || val == undefined || val.replace(/[^0-9\.-]+/g, "") == "") {
        return 0;
    }
    return parseFloat(val.replace(/[^0-9\.-]+/g, ""));
}

function captureHtml2Canvas(objImgConvert, id) {
    var ring = document.createElement('div');
    ring.className = "thumbnailSelectionRing";
    ring.setAttribute("data-npage", id);
    $("#sidebarContent").append(ring);

    html2canvas($(".report-form")[id - 1]).then(canvas => {
        var newImg = document.createElement('img');

        var titlepange = $(objImgConvert).find(".report-header") == undefined ? "" : " : " + $(objImgConvert).find(".report-header").html();
        newImg.src = canvas.toDataURL();
        newImg.id = "thumbnail" + id;
        newImg.setAttribute("img-number", id);
        newImg.setAttribute("title", "page " + id + titlepange);
        newImg.className = "c-pointer thumbnailImage" + (id == 1 ? " active" : " ");
        newImg.style.width = '100px';
        newImg.style.height = '126px';
        newImg.addEventListener('click', function (eventObj) {
            var number = $(this).attr("img-number");
            $("#pageNumber").val(number);
            $("#viewerContainer").scrollTop($("#viewerContainer").scrollTop() + $("[A4-page-number='" + number + "']").position().top - 5);
            $(".thumbnailImage").removeClass("active");
            $(this).addClass("active");
        });
        $("#sidebarContent").find(".thumbnailSelectionRing[data-npage='" + id + "']").append(newImg);
        canvas.width = 0;
        canvas.height = 0;
        delete canvas;
    });
}

//function ConvertElement2Canvas(objImgConvert, id) {

//    if (!HTMLCanvasElement.prototype.toBlob) {
//        Object.defineProperty(HTMLCanvasElement.prototype, 'toBlob', {
//            value: function (callback, type, quality) {
//                var canvas = this;
//                setTimeout(function () {
//                    var binStr = atob(canvas.toDataURL(type, quality).split(',')[1]),
//                    len = binStr.length,
//                    arr = new Uint8Array(len);

//                    for (var i = 0; i < len; i++) {
//                        arr[i] = binStr.charCodeAt(i);
//                    }

//                    callback(new Blob([arr], { type: type || 'image/png' }));
//                });
//            }
//        });
//    }

//    var canvas = document.createElement('canvas');
//    var ctx = canvas.getContext('2d');

//    var ring = document.createElement('div');
//    ring.className = "thumbnailSelectionRing";
//    ring.setAttribute("data-npage", id);
//    $("#sidebarContent").append(ring);

//        //Content
//        var temp = $("<div />");
//        $(objImgConvert).clone().appendTo($(temp));
//        var data = '<svg xmlns="http://www.w3.org/2000/svg" width="200" height="200">' +
//                   '<foreignObject width="100%" height="100%">' +
//                   '<div xmlns="http://www.w3.org/1999/xhtml" style="font-size:10px;padding: 5px 10px;">'
//                     + $(temp).html() +
//                   '</div>' +
//                   '</foreignObject>' +
//                   '</svg>';

//        data = encodeURIComponent(data);

//        var img = new Image();
//        img.onload = function () {
//            ctx.drawImage(img, 0, 0);

//            canvas.toBlob(function (blob) {
//                var newImg = document.createElement('img');
           
//                var titlepange = $(objImgConvert).find(".report-header") == undefined ? "" : " : "+$(objImgConvert).find(".report-header").html();
//                newImg.src = canvas.toDataURL();
//                newImg.id = "thumbnail" + id;
//                newImg.setAttribute("img-number", id);
//                newImg.setAttribute("title", "page " + id + titlepange);
//                newImg.className = "c-pointer thumbnailImage" + (id == 1 ? " active" : " ");
//                newImg.style.width = '100px';
//                newImg.style.height = '126px';
//                newImg.addEventListener('click', function (eventObj) {
//                    var number = $(this).attr("img-number");
//                    $("#pageNumber").val(number);
//                    $("#viewerContainer").scrollTop($("#viewerContainer").scrollTop() + $("[A4-page-number='" + number + "']").position().top - 5);
//                    $(".thumbnailImage").removeClass("active");
//                    $(this).addClass("active");
//                });
//                $("#sidebarContent").find(".thumbnailSelectionRing[data-npage='"+id+"']").append(newImg);
//                canvas.width = 0;
//                canvas.height = 0;
//                delete canvas;
//            });
//        }
//        img.src = "data:image/svg+xml," + data
//}






