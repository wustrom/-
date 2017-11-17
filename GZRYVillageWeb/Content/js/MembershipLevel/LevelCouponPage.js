$(function () {
    var E = window.wangEditor;
    var editor = new E('#divDemo');
    editor.customConfig.menus = [
        'head',  // 标题
        'bold',  // 粗体
        'italic',  // 斜体
        'underline',  // 下划线
        'strikeThrough',  // 删除线
        'foreColor',  // 文字颜色
        'link',  // 插入链接
        'list',  // 列表
        'justify',  // 对齐方式
        'image',  // 插入图片
        'table',  // 表格
        'undo',  // 撤销
        'redo'  // 重复
    ];
    editor.customConfig.showLinkImg = false;
    editor.customConfig.uploadImgShowBase64 = true;
    editor.create();
    $("#btn_Text").bind("click", function () {
        $("#divDemo img").css("width", "100%");
        alert('<head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"></head><body style="color:white;background:black; width:96%;height:100%;padding:0 2%;">' + editor.txt.html() + '</body>');
    });
});
