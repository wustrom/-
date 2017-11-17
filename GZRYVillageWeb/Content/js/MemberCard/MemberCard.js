/// <reference path="../ajax.js" />
/// <reference path="../../lib/js/alert.js" />
var ErrorAlert;
var InsertCardAlert;
var AddSuccessAlert;
var InsertAlert;
var DeleteSuccessAlert;
var DeleteAlert;
var NoCheckedAlert;

//根据ID删除优惠券
function deleteCoupon(MemberShipTypeId, CouponId) {
    DeleteAlert = jqueryAlert({
        'title': '删除提示',
        'content': '您确定要删除吗？',
        'modal': true,
        'buttons': {
            '确定': function () {
                DeleteAlert.close();
                jQuery.axpost("MemberCardAjax/Delete_CouponById", "MemberShipTypeId:'" + MemberShipTypeId + "',CouponId:'" + CouponId + "'", function (data) {
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
    })
}
//添加优惠券
function InsertCoupon(MemberShipTypeId, CouponId) {
    InsertAlert = jqueryAlert({
        'title': '添加提示',
        'content': '您确定要添加吗？',
        'modal': true,
        'buttons': {
            '确定': function () {
                InsertAlert.close();
                jQuery.axpost("MemberCardAjax/Insert_CouponById", "MemberShipTypeId:'" + MemberShipTypeId + "',CouponId:'" + CouponId + "'", function (data) {
                    $("#Hidden_Search").trigger("click");
                    // 添加成功提示
                    if (AddSuccessAlert) {
                        return AddSuccessAlert.show();
                    }
                    AddSuccessAlert = jqueryAlert({
                        'content': data.Message,
                        'closeTime': 2000,
                    })
                })
            },
            '取消': function () {
                InsertAlert.close();
            }
        }
    })
}


//添加会员卡
function InsertMemberCard() {
    className = $(this).attr('class');
    $('#dialogBg').fadeIn(300);
    $('#dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
}
function Insert_MemberCard(MemberShipTypeId) {
    var CardName = $('table #CardName').val();
    var CardPassword = $('table #CardPassword').val();
    jQuery.axpost("MemberCardAjax/Insert_MemberCard", "CardName:'" + CardName + "',CardPassword:'" + CardPassword + "',MemberShipTypeId:'" + MemberShipTypeId + "'"
        , function (data) {
            if (InsertCardAlert) {
                InsertCardAlert.show();
            }
            else {
                InsertCardAlert = jqueryAlert({
                    'content': '新增会员卡成功',
                    'closeTime': 2000,

                });
            }
            //添加数据成功后清空文本框内容
            $('table #CardName').val("");
            $('table #CardPassword').val("");
            CloseDialog1();
            //调用 search 的点击事件的两种方法
            $("#search").trigger("click");
            return;
        }, ErrorAlert);
}

function CloseDialog1() {
    $('#dialogBg').fadeOut(300, function () {
        $('#dialog').addClass('bounceOutUp').fadeOut();
    });
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

        var MemberShipCardId = chkId.substring(0, chkId.length - 1);
        DeleteAlert = jqueryAlert({
            'title': '删除提示',
            'content': '您确定要删除吗？',
            'modal': true,
            'buttons': {
                '确定': function () {
                    DeleteAlert.close();
                    jQuery.axpost("MemberCardAjax/Delete_MemberCardByIds", "KeyIds:'" + MemberShipCardId + "'", function (data) {
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


