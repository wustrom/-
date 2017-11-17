/// <reference path="../ajax.js" />
/// <reference path="../../lib/js/alert.js" />
var DeleteSuccessAlert;
var DeleteAlert;
var SuccessAlert;
var ErrorAlert;
var NoCheckedAlert;
//新增会员类型卡
function add() {
    className = $(this).attr('class');
    $('.box #dialogBg').fadeIn(300);
    $('.box #dialog').removeAttr('class').addClass('animated ' + className + '').fadeIn();
}
function InsertMemberTypeCard() {
    var CardName = $('.box .editInfos #CardName').val();
    var CardImage = $('.box .editInfos #result').attr('src');
    jQuery.axpost("MemberCardAjax/Insert_MemberTypeCard", "CardName:'" + CardName + "',CardImage:'" + CardImage + "'"
        , function (data) {
            if (SuccessAlert) {
                SuccessAlert.show();
            }
            else {
                SuccessAlert = jqueryAlert({
                    'content': '新增会员类型卡成功',
                    'closeTime': 2000,

                });
            }
            //添加数据成功后清空文本框内容
            $(".box .editInfos #CardName").val("");
            $(".box .Image_Li #result").css("display", "none");
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

//上传图片
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

    //上传Excel
    $("#btn_showExcel").bind("click", function () {
        $("#form_uploadExcel #upExcel").trigger("click");
        var options = {
            success: function (responseText, statusText, xhr, $form) {
                var ExcelPath = responseText.pic;
                if (ExcelPath == "") {
                    alert(responseText.error);
                }
                else {
                    $("#form_uploadExcel").hide();
                    $("#Excelresult").attr("src", ExcelPath).show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus);
                console.log(errorThrown);
            }
        };

        $("#form_uploadExcel").ajaxForm(options);
    });
    $("#form_uploadExcel #upExcel").bind("change", function () {
        if ($("#form_uploadExcel #upExcel").val() != "") {
            $("#form_uploadExcel #Sumbit_Excel").trigger("click");
        }
    });
    $("#btn_showExcel").bind();
});

//批量导入卡,导入Excel
function InsertExcel(MemberShipTypeId) {
    $('#form_uploadExcel #MemberShipTypeId').val(MemberShipTypeId);
    $("#btn_showExcel").trigger("click");
    return;
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

        var MemberShipTypeId = chkId.substring(0, chkId.length - 1);
        DeleteAlert = jqueryAlert({
            'title': '删除提示',
            'content': '您确定要删除吗？',
            'modal': true,
            'buttons': {
                '确定': function () {
                    DeleteAlert.close();
                    jQuery.axpost("MemberCardAjax/Delete_MemberTypeCardByIds", "KeyIds:'" + MemberShipTypeId + "'", function (data) {
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
