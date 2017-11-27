/// <reference path="../wangEditor.min.js" />
$(function () {
    var E = window.wangEditor;
    var editorLeft = new E('.divDemoLeft');
    editorLeft.customConfig.menus = [
        'head',  // 标题
        'bold',  // 粗体
        'italic',  // 斜体
        'underline',  // 下划线
        'strikeThrough',  // 删除线
        'list',  // 列表
        'justify',  // 对齐方式
        'undo',  // 撤销
        'redo'  // 重复
    ];
    editorLeft.customConfig.showLinkImg = false;
    editorLeft.customConfig.uploadImgShowBase64 = true;
    editorLeft.create();

    var E = window.wangEditor;
    var editorRight = new E('.divDemoRight');
    editorRight.customConfig.menus = [
        'head',  // 标题
        'bold',  // 粗体
        'italic',  // 斜体
        'underline',  // 下划线
        'strikeThrough',  // 删除线
        'list',  // 列表
        'justify',  // 对齐方式
        'undo',  // 撤销
        'redo'  // 重复
    ];
    editorRight.customConfig.showLinkImg = false;
    editorRight.customConfig.uploadImgShowBase64 = true;
    editorRight.create();
    var SuccessAlert;

    jQuery.axpost("CommonHtml/CommonProblem", "", function (data) {
        $('.Editor_Content .Content .divDemoLeft .w-e-text').html(data.Model1.HtmlContent);
        $('.Editor_Content .Content .divDemoRight .w-e-text').html(data.Model2.HtmlContent);
    }, ErrorAlert);
});
var ErrorAlert;
var SuccessAlert1;
var SuccessAlert2;
function ContentHtml(str) {
    if (str.indexOf('<meta http-equiv="Content-Type" content="text/html; charset=utf-8">') >= 0) {
        str = '<head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"></head><body style="color:white;background:black;  width:96%;height:100%;padding:0 2%;">' + str + '</body>';
    }
    else {
        str = '<head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"></head><body style="color:white;background:black; width:96%;height:100%;padding:0 2%;">' + str + '</body>';
    }
    str = str.replace(/'/g, "Single_quotation&marks");
    str = str.replace(/\"/g, 'Double_quotation&marks');
    str = str.replace(/\\/g, 'BackSlash&marks');
    return str;
}

function UpdateCommonProblem() {
    var content = $('.Editor_Content .Content .divDemoLeft .w-e-text').html();
    content = ContentHtml(content);
    jQuery.axpost("CommonHtml/UpdateCommonProblem", "'HtmlContent':'" + content + "'", function (data) {
        if (SuccessAlert1) {
            SuccessAlert1.show();
        }
        else {
            SuccessAlert1 = jqueryAlert({
                'content': '修改常见问题成功',
                'closeTime': 2000,
            });
        }
    }, ErrorAlert);
}

function UpdateTermsOfUse() {
    var content = $('.Editor_Content .Content .divDemoRight .w-e-text').html();
    content = ContentHtml(content);
    jQuery.axpost("CommonHtml/UpdateTermsOfUse", "HtmlContent:'" + content + "'", function (data) {
        if (SuccessAlert2) {
            SuccessAlert2.show();
        }
        else {
            SuccessAlert2 = jqueryAlert({
                'content': '修改使用条款成功',
                'closeTime': 2000,
            });
        }
    }, ErrorAlert);
}
