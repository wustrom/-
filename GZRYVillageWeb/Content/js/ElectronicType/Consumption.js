/// <reference path="../alert.js" />
/// <reference path="../../lib/js/alert.js" />

//转换时间格式
function FormatDate(data) {
    var dateStr = data.substring(6, data.length - 2);
    var oDate = new Date(parseInt(dateStr));
    oYear = oDate.getFullYear();
    oMonth = oDate.getMonth() + 1;
    oDay = oDate.getDate();
    oHour = oDate.getHours();
    oMin = oDate.getMinutes();
    oSec = oDate.getSeconds();
    var str = oYear + "-" + oMonth + "-" + oDay + "  " + oHour + ":" + oMin + ":" + oSec;
    return str;
}
