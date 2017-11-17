/// <reference path="../ajax.js" />
/// <reference path="../../lib/js/alert.js" />
var NotEditAlert;
var PassWordAlert;
var ErrorAlert;
var SuccessUpdateAlert;
var SuccessAddAlert;
var NotSuccessAlert;
var DeleteSuccessAlert;
var DeleteAlert;
var NoCheckedAlert;

//新增用户信息
function add() {
    className = $(this).attr('class');
    $('.box1 #dialogBg').fadeIn(300);
    $('.box1 #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
}
function InsertUserInfo() {
    var UserName = $('.box1 table #UserName').val();
    var UserNickName = $('.box1 table #UserNickName').val();
    var UserPhone = $('.box1 table #UserPhone').val();
    var UserEmail = $('.box1 table #UserEmail').val();
    var Sex = GetRedioValue('.box1 table input[name="sex-insert"]');
    var ConsumptionTime = $('.box1 table #ConsumptionTime').val();
    jQuery.axpost("UserAjax/Insert_UserInfo", "UserName:'" + UserName +
                                          "',UserNickName:'" + UserNickName +
                                          "',UserPhone:'" + UserPhone +
                                          "',UserEmail:'" + UserEmail +
                                          "',sex:'" + Sex +
                                          "',ConsumptionTime:'" + ConsumptionTime + "'"
        , function (data) {
            if (SuccessAddAlert) {
                SuccessAddAlert.show();
            }
            else {
                SuccessAddAlert = jqueryAlert({
                    'content': '新增用户成功',
                    'closeTime': 2000,

                });
            }
            //添加数据成功后清空文本框内容
            $('.box1 table #UserName').val("");
            $('.box1 table #UserNickName').val("");
            $('.box1 table #UserPhone').val("");
            $('.box1 table #UserEmail').val("");
            $('.box1 table input[name="sex-insert"]').removeAttr("checked");
            $('.box1 table #ConsumptionTime').val("");
            CloseDialog1();
            //调用 search 的点击事件的两种方法
            $("#search").trigger("click");
            return;
        }, ErrorAlert);
}
function CloseDialog1() {
    $('.box1 #dialogBg').fadeOut(300, function () {
        $('.box1 #dialog').addClass('bounceOutUp').fadeOut();
    });
};


//修改用户信息
function modify(UserId) {
    jQuery.axpost("UserAjax/Get_User_ById", "UserId:'" + UserId + "'", function (data) {
        className = $(this).attr('class');
        $('.box #dialogBg').fadeIn(300);
        $('.box #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
        SetValue('.box table #UserName', data.Model1.UserName);
        SetValue('.box table #UserNickName', data.Model1.UserNickName);
        SetValue('.box table #UserPhone', data.Model1.UserPhone);
        SetValue('.box table #UserEmail', data.Model1.UserEmail);
        SetSexValue('.box table input[name="sex"]', data.Model1.Sex);
        SetValue('.box table #ConsumptionTime', data.Model1.ConsumptionTime);
        $('.box table #Modify_User').attr('onclick', 'modifyUserInfo(' + data.Model1.UserId + ')');
        return;
    }, ErrorAlert);
}
function modifyUserInfo(UserId) {
    var UserName = $('.box table #UserName').val();
    var UserNickName = $('.box table #UserNickName').val();
    var UserPhone = $('.box table #UserPhone').val();
    var UserEmail = $('.box table #UserEmail').val();
    var Sex = GetRedioValue('.box table input[name="sex"]');
    var ConsumptionTime = $('.box table #ConsumptionTime').val();
    if (Compare('.box table #UserName') && Compare('.box table #UserNickName') && Compare('.box table #UserPhone') && Compare('.box table #UserEmail') && Compare('.box table #ConsumptionTime') && CompareSex('.box table input[name="sex"]')) {
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
    jQuery.axpost("UserAjax/Modify_User", "UserId:'" + UserId +
                                          "',UserName:'" + UserName +
                                          "',UserNickName:'" + UserNickName +
                                          "',UserPhone:'" + UserPhone +
                                          "',UserEmail:'" + UserEmail +
                                          "',sex:'" + Sex +
                                          "',ConsumptionTime:'" + ConsumptionTime + "'"
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
            CloseDialog();
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

function SetValue(str, value) {
    $(str).val(value);
    $(str + '_Hidden').val(value);
}
function SetSexValue(str, value) {
    if (value == true) {
        $(str)[1].checked = false;
        $(str)[0].checked = true;
    }
    else if (value == false) {
        $(str)[1].checked = true;
        $(str)[0].checked = false;
    }
    else if (value == null) {
        $(str)[1].checked = false;
        $(str)[0].checked = false;
    }
    $(str + '_Hidden').val(value);
}

function GetRedioValue(str) {
    var value;
    $(str).each(function () {
        if ($(this)[0].checked == true) {
            if ($(this)[0].value == "1") {
                value = true;
            }
            else if ($(this)[0].value == "0") {
                value = false;
            }
            else {
                value = $(this)[0].value;
            }
        }
    });
    return value;
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

function CompareSex(str) {
    var BeforStr = $(str + '_Hidden').val();
    var AfterStr = GetRedioValue('.box table input[name="sex"]');
    if (BeforStr == AfterStr) {
        return true;
    }
    else {
        return false;
    }
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

        var UserId = chkId.substring(0, chkId.length - 1);
        DeleteAlert = jqueryAlert({
            'title': '删除提示',
            'content': '您确定要删除吗？',
            'modal': true,
            'buttons': {
                '确定': function () {
                    DeleteAlert.close();
                    jQuery.axpost("UserAjax/Delete_UserByIds", "KeyIds:'" + UserId + "'", function (data) {
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
