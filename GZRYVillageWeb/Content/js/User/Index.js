/// <reference path="../ajax.js" />
/// <reference path="../../lib/js/alert.js" />
var NotEditAlert;
var PassWordAlert;
var ErrorAlert;
var SuccessAlert;

//$(function () {
//    //点击添加数据按钮
//    $("#insert").click(function () {
//        className = $(this).attr('class');
//        $('.box1 #dialogBg').fadeIn(300);
//        $('.box1 #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
//    })
//})

//新增用户信息
function add() {
    className = $(this).attr('class');
    $('.box1 #dialogBg').fadeIn(300);
    $('.box1 #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
}
function InsertUserInfo() {
    var UserName = $('.box1 .editInfos #UserName').val();
    var UserNickName = $('.box1 .editInfos #UserNickName').val();
    var UserPhone = $('.box1 .editInfos #UserPhone').val();
    var UserEmail = $('.box1 .editInfos #UserEmail').val();
    var UserImage = $('.box1 .editInfos #UserImage').attr('src');
    var ConsumptionTime = $('.box1 .editInfos #ConsumptionTime').val();
    jQuery.axpost("UserAjax/Insert_UserInfo", "UserName:'" + UserName +
                                          "',UserNickName:'" + UserNickName +
                                          "',UserPhone:'" + UserPhone +
                                          "',UserEmail:'" + UserEmail +
                                          "',UserImage:'" + UserImage +
                                          "',ConsumptionTime:'" + ConsumptionTime + "'"
        , function (data) {
            if (SuccessAlert) {
                SuccessAlert.show();
            }
            else {
                SuccessAlert = jqueryAlert({
                    'content': '新增用户成功',
                    'closeTime': 2000,

                });
            }
            //添加数据成功后清空文本框内容
            //$('.editInfos #CardName').val("");
            //$('.editInfos #MemberShipTypeId').val("");
            CloseDialog();
            //调用 search 的点击事件的两种方法
            $("#search").trigger("click");
            return;
        }, ErrorAlert);
}

//修改用户信息
function modify(UserId) {
    jQuery.axpost("UserAjax/Get_User_ById", "UserId:'" + UserId + "'", function (data) {
        className = $(this).attr('class');
        $('.box #dialogBg').fadeIn(300);
        $('.box #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
        SetValue('.box .editInfos #UserName', data.Model1.UserName);
        SetValue('.box .editInfos #UserNickName', data.Model1.UserNickName);
        SetValue('.box .editInfos #UserPhone', data.Model1.UserPhone);
        SetValue('.box .editInfos #UserEmail', data.Model1.UserEmail);
        SetImgValue('.box .editInfos #UserImage', data.Model1.UserImage);
        SetValue('.box .editInfos #ConsumptionTime', data.Model1.ConsumptionTime);
        $('.box .editInfos #Modify_User').attr('onclick', 'modifyUserInfo(' + data.Model1.UserId + ')');
        return;
    }, ErrorAlert);
}
function modifyUserInfo(UserId) {
    var UserName = $('.box .editInfos #UserName').val();
    var UserNickName = $('.box .editInfos #UserNickName').val();
    var UserPhone = $('.box .editInfos #UserPhone').val();
    var UserEmail = $('.box .editInfos #UserEmail').val();
    var UserImage = $('.box .editInfos #UserImage').attr('src');
    var ConsumptionTime = $('.box .editInfos #ConsumptionTime').val();
    if (Compare('.box .editInfos #UserName') && Compare('.box .editInfos #UserNickName') && Compare('.box .editInfos #UserPhone') && Compare('.box .editInfos #UserEmail') && Compare('.box .editInfos #ConsumptionTime') && CompareImg('.box .editInfos #UserImage')) {
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
                                          "',UserImage:'" + UserImage +
                                          "',ConsumptionTime:'" + ConsumptionTime + "'"
        , function (data) {
            if (SuccessAlert) {
                SuccessAlert.show();
            }
            else {
                SuccessAlert = jqueryAlert({
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

function SetImgValue(str, value) {
    $(str).attr('src',value);
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

function CompareImg(str) {
    var BeforStr = $(str + '_Hidden').val();
    var AfterStr = $(str).attr('src');
    if (BeforStr == AfterStr) {
        return true;
    }
    else {
        return false;
    }
}