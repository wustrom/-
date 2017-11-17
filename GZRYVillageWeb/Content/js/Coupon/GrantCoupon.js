/// <reference path="../alert.js" />
/// <reference path="../../lib/js/alert.js" />
var NoCheckedAlert;
var SuccessAlert;
var ErrorAlert;
var DeleteSuccessAlert;
var DeleteAlert;
var NoCheckedAlert;

//发放优惠券
function InsertCoupon(CouponId) {
    var chkId = "";
    $("tbody tr :checkbox").each(function () {
        var box = $(this)[0];
        if (box.checked == true) {
            chkId = chkId + box.value + ",";
        }
    })
    if (chkId.length == 0) {
        if (NoCheckedAlert) {
            return NoCheckedAlert.show();
        }
        NoCheckedAlert = jqueryAlert({
            'content': "请选择用户",
            'closeTime': 2000,
        })
        return;
    }

    var UserId = chkId.substring(0, chkId.length - 1);
    jQuery.axpost("CouponAjax/GrantCouponToUser", "CouponId:'" + CouponId + "',KeyIds:'" + UserId + "'"
        , function (data) {
            if (SuccessAlert) {
                SuccessAlert.show();
                $("tbody tr :checkbox").each(function () {
                    var box = $(this)[0];
                    if (box.checked == true) {
                        box.checked = false;
                    }
                })
            }
            else {
                SuccessAlert = jqueryAlert({
                    'content': '发送优惠券成功',
                    'closeTime': 2000,

                });
            }
            return;
        }, ErrorAlert);
}

//多选删除数据
$(function () {
    $("#DeleteAll").click(function () {
        var chkId = "";
        $("tbody tr :checkbox").each(function () {
            var box = $(this)[0];
            if (box.checked == true) {
                chkId = chkId + box.value + ",";
            }
        })
        if (chkId.length == 0) {
            if (NoCheckedAlert) {
                return NoCheckedAlert.show();
            }
            NoCheckedAlert = jqueryAlert({
                'content': "请选择要删除的数据",
                'closeTime': 2000,
            })
            return;
        }

        var CouponUserRelationId = chkId.substring(0, chkId.length - 1);
        DeleteAlert = jqueryAlert({
            'title': '删除提示',
            'content': '您确定要删除吗？',
            'modal': true,
            'buttons': {
                '确定': function () {
                    DeleteAlert.close();
                    jQuery.axpost("CouponAjax/Delete_CouponUserRelationByIds", "KeyIds:'" + CouponUserRelationId + "'", function (data) {
                        $("#Hidden_Search").trigger("click");
                        // 删除成功提示
                        if (DeleteSuccessAlert) {
                            return DeleteSuccessAlert.show();
                        }
                        DeleteSuccessAlert = jqueryAlert({
                            'content': data.Message,
                            'closeTime': 2000,
                        })
                    })
                },
                '取消': function () {
                    DeleteAlert.close();
                }
            }

        });
    })
});

//更多搜索条件
$(function () {
    $(".form-group .MoreInfo a").click(function () {
        var display = $('#SearchTime').css('display');
        if (display == 'none') {
            $("#SearchTime").css('display', 'block');
        }
        else {
            $("#SearchTime").css('display', 'none');
        }
    })
})

//转换时间格式
function FormatDate(data) {
    var str = "";
    if (data == "/Date(-62135596800000)/" || data == null) {
        str = "";
        return str;
    }
    else {
        var dateStr = data.substring(6, data.length - 2);
        var oDate = new Date(parseInt(dateStr));
        oYear = oDate.getFullYear();
        oMonth = oDate.getMonth() + 1;
        oDay = oDate.getDate();
        oHour = oDate.getHours();
        oMin = oDate.getMinutes();
        oSec = oDate.getSeconds();
        str = oYear + "-" + oMonth + "-" + oDay + "  " + oHour + ":" + oMin + ":" + oSec;
        return str;
    }
}
