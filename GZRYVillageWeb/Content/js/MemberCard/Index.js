/// <reference path="../ajax.js" />
/// <reference path="../../lib/js/alert.js" />
var NotEditAlert;
var ErrorAlert;
var DeleteSuccessAlert;
var AddSuccessAlert;
var DeleteAlert;
var InsertAlert;
var SuccessAlert;
//新增新类型卡
function add() {
    className = $(this).attr('class');
    $('#dialogBg').fadeIn(300);
    $('#dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
}
function InsertMemberCard() {
    var CardName = $('.editInfos #CardName').val();
    var MemberShipTypeId = $('.editInfos #MemberShipTypeId').val();
    jQuery.axpost("MemberCardAjax/Add_MemberCard", "CardName:'" + CardName + "',MemberShipTypeId:'" + MemberShipTypeId + "'"
        , function (data) {
            if (SuccessAlert) {
                SuccessAlert.show();
            }
            else {
                SuccessAlert = jqueryAlert({
                    'content': '新增会员卡成功',
                    'closeTime': 2000,

                });
            }
            //添加数据成功后清空文本框内容
            $('.editInfos #CardName').val("");
            $('.editInfos #MemberShipTypeId').val("");
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

//根据ID删除优惠券
function deleteCoupon(MemberShipTypeId, CouponId) {
    var coupon = CouponId;
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






