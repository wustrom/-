/*****************************************************************
                  jQuery Ajax封装通用类  (linjq)       
*****************************************************************/
$(function () {
    var baseUrl = "/";
    /**
     * ajax封装
     * url 发送请求的地址
     * data 发送到服务器的数据，数组存储，如：{"date": new Date().getTime(), "state": 1}
     * async 默认值: true。默认设置下，所有请求均为异步请求。如果需要发送同步请求，请将此选项设置为 false。
     *       注意，同步请求将锁住浏览器，用户其它操作必须等待请求完成才可以执行。
     * type 请求方式("POST" 或 "GET")， 默认为 "GET"
     * dataType 预期服务器返回的数据类型，常用的如：xml、html、json、text
     * successfn 成功回调函数
     * errorfn 失败回调函数
     */
    jQuery.ax = function (url, data, async, type, dataType, successfn) {
        async = (async == null || async == "" || typeof (async) == "undefined") ? "true" : async;
        type = (type == null || type == "" || typeof (type) == "undefined") ? "post" : type;
        dataType = (dataType == null || dataType == "" || typeof (dataType) == "undefined") ? "json" : dataType;
        data = (data == null || data == "" || typeof (data) == "undefined") ? { "date": new Date().getTime() } : data;
        $.ajax({
            type: type,
            async: async,
            data: data,
            url: baseUrl + url,
            dataType: dataType,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.HttpCode != 200) {
                    ErrorAlert = jqueryAlert({
                        'content': data.Message,
                        'closeTime': 3000,
                    });
                }
                else {
                    successfn(data);
                }
            },
            error: function (e) {
                ErrorAlert = jqueryAlert({
                    'content': '请求处理出错',
                    'closeTime': 3000,
                });
            }
        });
    };

    /**
     * ajax封装
     * url 发送请求的地址
     * data 发送到服务器的数据，数组存储，如：{"date": new Date().getTime(), "state": 1}
     * successfn 成功回调函数
     */
    jQuery.axpost = function (url, data, successfn, ErrorAlert) {
        data = (data == null || data == "" || typeof (data) == "undefined") ? { "date": new Date().getTime() } : data;
        $.ajax({
            type: "post",
            data: "{" + data + "}",
            url: baseUrl + url,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.HttpCode != 200) {
                    ErrorAlert = jqueryAlert({
                        'content': data.Message,
                        'closeTime': 3000,
                    });
                }
                else {
                    successfn(data);
                }
            },
            error: function (e) {
                if (ErrorAlert) {
                    ErrorAlert.content = '请求处理出错';
                    return ErrorAlert.show();
                }
                ErrorAlert = jqueryAlert({
                    'content': '请求处理出错',
                    'closeTime': 3000,
                });
            }
        });
    };

    /**
     * ajax封装
     * url 发送请求的地址
     * data 发送到服务器的数据，数组存储，如：{"date": new Date().getTime(), "state": 1}
     * dataType 预期服务器返回的数据类型，常用的如：xml、html、json、text
     * successfn 成功回调函数
     * errorfn 失败回调函数
     */
    jQuery.axspost = function (url, data, successfn) {
        data = (data == null || data == "" || typeof (data) == "undefined") ? { "date": new Date().getTime() } : data;
        $.ajax({
            type: "post",
            data: data,
            url: baseUrl + url,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.HttpCode != 200) {
                    ErrorAlert = jqueryAlert({
                        'content': data.Message,
                        'closeTime': 3000,
                    });
                }
                else {
                    successfn(data);
                }
            },
            error: function (e) {
                ErrorAlert = jqueryAlert({
                    'content': '请求处理出错',
                    'closeTime': 3000,
                });
            }
        });
    };
});