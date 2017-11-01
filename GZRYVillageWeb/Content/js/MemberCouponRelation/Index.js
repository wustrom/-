/// <reference path="../ajax.js" />
/// <reference path="../../lib/js/alert.js" />
var NotEditAlert;
var PassWordAlert;
var ErrorAlert;
var SuccessAlert;
//新增优惠信息
function add() {
    className = $(this).attr('class');
    $('.box #dialogBg').fadeIn(300);
    $('.box #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();

}

function InsertMemberCouponRelation(MembershipLevelId) {
    var CouponContains = $('.box .editInfos #CouponContains').val();
   
    jQuery.axpost("MemberShipLevelAjax/Insert_MemberCouponRelation", "CouponContains:'" + CouponContains + "',MembershipLevelId:'" + MembershipLevelId + "'"
        , function (data) {
            if (SuccessAlert) {
                SuccessAlert.show();
            }
            else {
                SuccessAlert = jqueryAlert({
                    'content': '新增优惠信息成功',
                    'closeTime': 2000,

                });
            }
            //添加数据成功后清空文本框内容
            $('.box .editInfos #CardName').val("");
            CloseDialog();
            //调用 search 的点击事件的两种方法
            $("#search").trigger("click");
            return;
        }, ErrorAlert);
}



//获取详细信息
function modify(CouponContainsId) {
    jQuery.axpost("MemberShipLevelAjax/Get_MemberCouponRelationInfo", "CouponContainsId:'" + CouponContainsId + "'", function (data) {
        className = $(this).attr('class');
        $('.box1 #dialogBg').fadeIn(500);
        $('.box1 #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
        SetValue('.box1 table #CouponContains', data.Model1.CouponContains);
        SetValue('.box1 table #MembershipLevelId', data.Model1.MembershipLevelId);
       
        $('.box1 table #Modify_MemberShipLevel').attr('onclick', 'modifyContainsInfo(' + data.Model1.CouponContainsId + ')');
        return;
    }, ErrorAlert);
}
//修改
function modifyContainsInfo(CouponContainsId) {
    var MembershipLevelId = $('.box1 table #MembershipLevelId').val();
    var CouponContains = $('.box1 table #CouponContains').val();
    if (Compare('.box1 table #MembershipLevelId') && Compare('.box1 table #CouponContains')) {
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
    jQuery.axpost("MemberShipLevelAjax/Update_MemberCouponRelationInfo", "CouponContains:'" + CouponContains + "',CouponContainsId:'" + CouponContainsId + "'"
                                        , function (data) {
            if (SuccessAlert) {
                SuccessAlert.show();
            }
            else {
                SuccessAlert = jqueryAlert({
                    'content': '修改会员等级优惠内容信息成功',
                    'closeTime': 2000,
                });
            }
            CloseDialog1();
            //调用 search 的点击事件的两种方法
            $("#search").trigger("click");
            return;
        }, ErrorAlert);
}
function CloseDialog() {
    $('.box #dialogBg').fadeOut(300, function () {
        $('.box #dialog').addClass('bounceOutUp').fadeOut();
    });
}
function CloseDialog1() {
    $('.box1 #dialogBg').fadeOut(300, function () {
        $('.box1 #dialog').addClass('bounceOutUp').fadeOut();
    });
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

//根据ID删除优惠信息
function deleteMemberContains(CouponContainsId) {
    DeleteAlert = jqueryAlert({
        'title': '删除提示',
        'content': '您确定要删除吗？',
        'modal': true,
        'buttons': {
            '确定': function () {
                DeleteAlert.close();
                jQuery.axpost("MemberShipLevelAjax/Delete_MemberCouponRelationById", "CouponContainsId:'" + CouponContainsId + "'", function (data) {
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
