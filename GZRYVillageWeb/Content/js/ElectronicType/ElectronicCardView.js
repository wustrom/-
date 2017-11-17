/// <reference path="../ajax.js" />
/// <reference path="../../lib/js/alert.js" />
var BindSuccessAlert;
var NoCheckedAlert;
var DeleteSuccessAlert;
var DeleteAlert;
var ErrorAlert;
var InsertCardAlert;

//新增电子储值卡
function AddElectronicCard() {
    className = $(this).attr('class');
    $('.box #dialogBg').fadeIn(300);
    $('.box #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();

};
function Insert_ElectronicCard(ElectronicTypeId) {
    var CardName = $('.box table #CardName').val();
    var CardPassword = $('.box table #CardPassword').val();
    jQuery.axpost("ElectronicCardAjax/Insert_ElectronicCard", "CardName:'" + CardName + "',CardPassword:'" + CardPassword + "',ElectronicTypeId:'" + ElectronicTypeId + "'"
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
            $('.box table #CardName').val("");
            $('.box table #CardPassword').val("");
            CloseDialog1();
            //调用 search 的点击事件的两种方法
            $("#search").trigger("click");
            return;
        }, ErrorAlert);
}
function CloseDialog1() {
    $('.box #dialogBg').fadeOut(300, function () {
        $('.box #dialog').addClass('bounceOutUp').fadeOut();
    });
}

function CloseDialogByUserNickName() {
    $('.box1 #dialogBg').fadeOut(300, function () {
        $('.box1 #dialog').addClass('bounceOutUp').fadeOut();
    });
}


//转换时间格式
function FormatDate(data) {
            var str = "";
            if (data == "/Date(-62135596800000)/" ||data==null)
            {
                str = "暂时未使用此卡片";
                return str;
            }   
            else
            {
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


//根据ID删除电子储值卡
function deleteElectronicCard(ElectronicId) {
    DeleteAlert = jqueryAlert({
        'title': '删除提示',
        'content': '您确定要删除吗？',
        'modal': true,
        'buttons': {
            '确定': function () {
                DeleteAlert.close();
                jQuery.axpost("ElectronicCardAjax/Delete_ElectronicCardById", "ElectronicId:'" + ElectronicId + "'", function (data) {
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
};
//显示卡片的绑定人
function ShowUserNickName(ElectronicId, ElectronicTypeId) {
    jQuery.axpost("ElectronicCardAjax/Get_UserNickName", "ElectronicId:'" + ElectronicId + "',ElectronicTypeId:'" + ElectronicTypeId + "'", function (data) {
        if (BindSuccessAlert) {
            return BindSuccessAlert.show();
        }
        var name = data.Message;
        //截取除最后一位逗号的前面所有字符
        name = name.substring(0, name.length - 1);
        BindSuccessAlert = jqueryAlert({
            'content': name,
            'closeTime': 2000,

        }, ErrorAlert);
    })
    
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

        var ElectronicId = chkId.substring(0, chkId.length - 1);
        DeleteAlert = jqueryAlert({
            'title': '删除提示',
            'content': '您确定要删除吗？',
            'modal': true,
            'buttons': {
                '确定': function () {
                    DeleteAlert.close();
                    jQuery.axpost("ElectronicCardAjax/Delete_ElectronicCardByIds", "KeyIds:'" + ElectronicId + "'", function (data) {
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