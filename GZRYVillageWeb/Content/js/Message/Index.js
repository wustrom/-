/// <reference path="../ajax.js" />
/// <reference path="../../lib/js/alert.js" />
var SuccessAlert;
var ErrorAlert;
var NoCheckedAlert;
var DeleteSuccessAlert;
var DeleteAlert;
//新增消息
function add() {
    className = $(this).attr('class');
    $('#dialogBg').fadeIn(300);
    $('#dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();

}

function InsertMessage() {
    var MessageName = $('.editInfos #MessageName').val();
    var MessageDescribe = $('.editInfos #MessageDescribe').val();
    var MessageImage = $('.editInfos #result').attr('src');
    jQuery.axpost("MessageAjax/Insert_Message", "MessageName:'" + MessageName + "',MessageDescribe:'" + MessageDescribe + "',MessageImage:'" + MessageImage + "'"
        , function (data) {
            if (SuccessAlert) {
                SuccessAlert.show();
            }
            else {
                SuccessAlert = jqueryAlert({
                    'content': '新增消息成功',
                    'closeTime': 2000,

                });
            }
            //添加数据成功后清空文本框内容
            $('.editInfos #MessageName').val("");
           // $('.editInfos #MessageContains').val("");
            $('.editInfos #MessageDescribe').val("");
            $(".Image_Li #result").css("display", "none");
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


//上传消息图片
$(function () {
    $("#btn_show").bind("click", function () {
        $("#form_upload #upImg").trigger("click");
        var options = {
            success: function (responseText, statusText, xhr, $form) {
                var picPath = responseText.pic;
                if (picPath == "") {
                    alert(responseText.error);
                }
                else {
                    $("#form_upload").hide();
                    $("#result").attr("src", picPath).show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus);
                console.log(errorThrown);
            }
        };

        $("#form_upload").ajaxForm(options);
    });
    $("#upImg").bind("change", function () {
        $("#form_upload #Sumbit_Img").trigger("click");
        $(".Image_Li #result").css("display", "block");
    });
    $("#btn_show").bind();

});

//修改等级优惠内容
function MessageContainsInfo(MessageID) {
    jQuery.axpost("MessageAjax/GetMessageInfoById", "MessageID:'" + MessageID + "'", function (data) {
        className = $(this).attr('class');
        $('.box1 #dialogBg').fadeIn(500);
        $('.box1 #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
        $('.box_Editor #divDemo .w-e-text').html(data.Model1.MessageContains);
        $('.box1 #Update_Message').attr('onclick', 'modifyMessage(' + MessageID + ')');
        return;
    }, ErrorAlert);
}


//修改
function modifyMessage(MessageID) {
    var MessageContains = $('.box_Editor #divDemo .w-e-text').html();
    if (MessageContains.indexOf('<meta http-equiv="Content-Type" content="text/html; charset=utf-8">') >= 0) {
        MessageContains = '<body style="color:white;background:black;  width:96%;height:100%;padding:0 2%;">' + MessageContains + '</body>';
    }
    else {
        MessageContains = '<head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"></head><body style="color:white;background:black; width:96%;height:100%;padding:0 2%;">' + MessageContains + '</body>';
    }
    MessageContains = repalceKey(MessageContains);

    debugger;
    var contentArry = "'MessageContains':'" + MessageContains + "','MessageID':'" + MessageID + "'";
    debugger;
    jQuery.axpost("MessageAjax/Update_MessageContains", contentArry
                                        , function (data) {
                                            if (SuccessAlert) {
                                                SuccessAlert.show();
                                            }
                                            else {
                                                SuccessAlert = jqueryAlert({
                                                    'content': '更新消息内容成功',
                                                    'closeTime': 2000,
                                                });
                                            }
                                            CloseMessageContainsDialog();
                                            //调用 search 的点击事件的两种方法
                                            $("#search").trigger("click");
                                            return;
                                        }, ErrorAlert);
}

function CloseMessageContainsDialog() {
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

        var MessageID = chkId.substring(0, chkId.length - 1);
        DeleteAlert = jqueryAlert({
            'title': '删除提示',
            'content': '您确定要删除吗？',
            'modal': true,
            'buttons': {
                '确定': function () {
                    DeleteAlert.close();
                    jQuery.axpost("MessageAjax/Delete_MessageByIds", "KeyIds:'" + MessageID + "'", function (data) {
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