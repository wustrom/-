/// <reference path="../ajax.js" />
/// <reference path="../DataTable.js" />
/// <reference path="../../lib/js/alert.js" />
var NotEditAlert;
var PassWordAlert;
var ErrorAlert;
var SuccessAlert;
//修改等级所需条件
function modify(MembershipLevelId) {
    jQuery.axpost("MemberShipLevelAjax/Get_MembershipLevel_ById", "MembershipLevelId:'" + MembershipLevelId + "'", function (data) {
        className = $(this).attr('class');
        $('.box #dialogBg').fadeIn(500);
        $('.box #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
        SetValue('.box .editInfos #LevelName', data.Model1.LevelName);
        SetValue('.box .editInfos #LevelMin', data.Model1.LevelMin);
        SetValue('.box .editInfos #LevelMax', data.Model1.LevelMax);
        $('.box .editInfos #Modify_MemberShipLevel').attr('onclick', 'modifyLevelInfo(' + data.Model1.MembershipLevelId + ')');
        return;
    }, ErrorAlert);
}

//修改会员等级所需条件
function modifyLevelInfo(MembershipLevelId) {
    var LevelName = $('.editInfos #LevelName').val();
    var LevelMin = $('.editInfos #LevelMin').val();
    var LevelMax = $('.editInfos #LevelMax').val();
    if (Compare('.editInfos #LevelName') && Compare('.editInfos #LevelMin') && Compare('.editInfos #LevelMax')) {
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
    jQuery.axpost("MemberShipLevelAjax/Update_MembershipLevel", "MembershipLevelId:'" + MembershipLevelId +
                                              "',LevelName:'" + LevelName +
                                              "',LevelMin:'" + LevelMin +
                                              "',LevelMax:'" + LevelMax + "'"
            , function (data) {
                if (SuccessAlert) {
                    SuccessAlert.show();
                }
                else {
                    SuccessAlert = jqueryAlert({
                        'content': '修改会员等级信息成功',
                        'closeTime': 2000,
                    });
                }
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
};


//修改等级优惠内容
function LevelCouponInfo(MembershipLevelId) {
    jQuery.axpost("MemberShipLevelAjax/Get_MemberCouponRelationInfo", "MembershipLevelId:'" + MembershipLevelId + "'", function (data) {
        className = $(this).attr('class');
        $('.box1 #dialogBg').fadeIn(500);
        $('.box1 #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
        $('.box_Editor #divDemo .w-e-text').html(data.Model1.CouponContains);
        $('.box1 #Update_MemberShipLevel').attr('onclick', 'modifyContainsInfo(' + MembershipLevelId + ')');
        return;
    }, ErrorAlert);
}


//修改
function modifyContainsInfo(MembershipLevelId) {
    var CouponContains = $('.box_Editor #divDemo .w-e-text').html();
    if (CouponContains.indexOf('<meta http-equiv="Content-Type" content="text/html; charset=utf-8">') >= 0) {
        CouponContains = '<body style="color:white;background:black;  width:96%;height:100%;padding:0 2%;">' + CouponContains + '</body>';
    }
    else {
        CouponContains = '<head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"></head><body style="color:white;background:black; width:96%;height:100%;padding:0 2%;">' + CouponContains + '</body>';
    }
    CouponContains = repalceKey(CouponContains);
  
    debugger;
    var contentArry = "'CouponContains':'" + CouponContains + "','MembershipLevelId':'" + MembershipLevelId + "'";
    debugger;
    jQuery.axpost("MemberShipLevelAjax/Update_MemberCouponRelationInfo", contentArry
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
                                            CloseLevelCouponInfoDialog();
                                            //调用 search 的点击事件的两种方法
                                            $("#search").trigger("click");
                                            return;
                                        }, ErrorAlert);
}

function CloseLevelCouponInfoDialog() {
    $('.box1 #dialogBg').fadeOut(300, function () {
        $('.box1 #dialog').addClass('bounceOutUp').fadeOut();
    });
}

// 转为unicode 编码  
function encodeUnicode(str) {
    var res = [];
    for (var i = 0; i < str.length; i++) {
        res[i] = ("00" + str.charCodeAt(i).toString(16)).slice(-4);
    }
    return "\\u" + res.join("\\u");
}

// 解码  
function decodeUnicode(str) {
    str = str.replace(/\\/g, "%");
    return unescape(str);
}