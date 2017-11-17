$.extend($.fn.dataTable.defaults, {
    "dom": "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-12 text-center'i>>" +
            "<'row'<'col-sm-5'l><'col-sm-7'p>>",//默认是lfrtip
    "processing": true,//加载中
    "serverSide": true,//服务器模式
    "searching": false,//datatables自带的搜索
    "scrollX": true,//X滑动条
    //分页模式
    "pagingType": "full_numbers",
    //自动计算列宽
    "autoWidth": true,
    //是否延迟渲染
    "bDeferRender": false,
    //开启水平滚动
    "sScrollX": "100%",
    "ajax": {
        "type": "POST",
        "contentType": "application/json; charset=utf-8"
    },
    "language": {
        "processing": "加载中...",
        "lengthMenu": "每页显示 _MENU_ 条数据",
        "zeroRecords": "没有匹配结果",
        "info": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
        "infoEmpty": "显示第 0 至 0 项结果，共 0 项",
        "infoFiltered": "(从 _MAX_ 项结果过滤)",
        "infoPostFix": "",
        "search": "搜索:",
        "url": "",
        "emptyTable": "没有匹配结果",
        "loadingRecords": "载入中...",
        "thousands": ",",
        "paginate": {
            "first": "首页",
            "previous": "上一页",
            "next": "下一页",
            "last": "末页"
        },
        "aria": {
            "sortAscending": ": 以升序排列此列",
            "sortDescending": ": 以降序排列此列"
        }
    }
});

$(function () {
    $("#checkbox-all").click(function () {
        var checked = $("#checkbox-all")[0].checked;
        if (checked == true) {
            $("tbody tr :checkbox").each(function () {
                $(this)[0].checked = true;
            });
        }
        if (checked == false) {
            $("tbody tr :checkbox").each(function () {
                $(this)[0].checked = false;
            });
        }
    });
})

function repalceKey(str) {
    str = str.replace(/'/g, "Single_quotation&marks");
    str = str.replace(/\"/g, 'Double_quotation&marks');
    str = str.replace(/\\/g, 'BackSlash&marks');
    return str;
}
