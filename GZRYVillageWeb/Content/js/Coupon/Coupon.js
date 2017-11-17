/// <reference path="../ajax.js" />
/// <reference path="../../lib/js/alert.js" />
var SuccessAlert;
var ErrorAlert;
var DeleteSuccessAlert;
var DeleteAlert;
var NoCheckedAlert;
var NotEditAlert;
var SuccessUpdateAlert;

//新增优惠券
function add() {
    className = $(this).attr('class');
    $('.box #dialogBg').fadeIn(300);
    $('.box #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();

}

function InsertCoupon() {
    var CouponName = $('.box table #CouponName').val();
    var CouponDescribe = $('.box table #CouponDescribe').val();
    var ExpirationDay = $('.box table #ExpirationDay').val();
    jQuery.axpost("CouponAjax/Insert_Coupon", "CouponName:'" + CouponName + "',CouponDescribe:'" + CouponDescribe + "',ExpirationDay:'" + ExpirationDay + "'"
        , function (data) {
            if (SuccessAlert) {
                SuccessAlert.show();
            }
            else {
                SuccessAlert = jqueryAlert({
                    'content': '新增优惠券成功',
                    'closeTime': 2000,

                });
            }
            //添加数据成功后清空文本框内容
            $('.box table #CouponName').val("");
            $('.box table #CouponDescribe').val("");
            $('.box table #ExpirationDay').val("");
            CloseDialog();
            //调用 search 的点击事件的两种方法
            $("#search").trigger("click");
            return;
        }, ErrorAlert);
}

function CloseDialog() {
    $('#dialogBg').fadeOut(300, function () {
        $('#dialog').addClass('bounceOutUp').fadeOut();
    });
}

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

        var CouponId = chkId.substring(0, chkId.length - 1);
        DeleteAlert = jqueryAlert({
            'title': '删除提示',
            'content': '您确定要删除吗？',
            'modal': true,
            'buttons': {
                '确定': function () {
                    DeleteAlert.close();
                    jQuery.axpost("CouponAjax/Delete_CouponByIds", "KeyIds:'" + CouponId + "'", function (data) {
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
})

//修改优惠券
function Modify_Coupon(CouponId) {
    jQuery.axpost("CouponAjax/Get_CouponInfo_ById", "CouponId:'" + CouponId + "'", function (data) {
        className = $(this).attr('class');
        $('.box1 #dialogBg').fadeIn(300);
        $('.box1 #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
        SetValue('.box1 table #CouponName', data.Model1.CouponName);
        SetValue('.box1 table #CouponDescribe', data.Model1.CouponDescribe);
        SetValue('.box1 table #ExpirationDay', data.Model1.ExpirationDay);
        $('.box1 table #Update_Coupon').attr('onclick', 'Update_CouponById(' + data.Model1.CouponId + ')');
        return;
    }, ErrorAlert);

}
function Update_CouponById(CouponId) {
    var CouponName = $('.box1 table #CouponName').val();
    var CouponDescribe = $('.box1 table #CouponDescribe').val();
    var ExpirationDay = $('.box1 table #ExpirationDay').val();
    if (Compare('.box1 table #CouponName') && Compare('.box1 table #CouponDescribe') && Compare('.box1 table #ExpirationDay')) {
        if (NotEditAlert) {
            NotEditAlert.show();
        }
        else {
            NotEditAlert = jqueryAlert({
                'content': '并未修改任何信息',
                'closeTime': 2000,
            });
        }
        return;
    }
    jQuery.axpost("CouponAjax/Update_CouponById", "CouponId:'" + CouponId +
                                          "',CouponName:'" + CouponName +
                                          "',CouponDescribe:'" + CouponDescribe +
                                          "',ExpirationDay:'" + ExpirationDay + "'"
        , function (data) {
            if (SuccessUpdateAlert) {
                SuccessUpdateAlert.show();
            }
            else {
                SuccessUpdateAlert = jqueryAlert({
                    'content': '修改用户信息成功',
                    'closeTime': 2000,
                });
            }
            CloseCouponDialog();
            //调用 search 的点击事件的两种方法
            $("#search").trigger("click");
            return;
        }, ErrorAlert);

}

function SetValue(str, value) {
    $(str).val(value);
    $(str + '_Hidden').val(value);
}
function Compare(str) {
    var BeforStr = $(str + '_Hidden').val();
    var AfterStr = $(str).val();
    if (BeforStr == AfterStr) {
        return true;
    }
    else {
        return false;
    }
}

function CloseCouponDialog() {
    $('.box1 #dialogBg').fadeOut(300, function () {
        $('.box1 #dialog').addClass('bounceOutUp').fadeOut();
    });
}

