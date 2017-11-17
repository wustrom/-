using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest
{
    public class MessageRequest
    {
        /// <summary>
        /// 消息名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "消息名称不能为空")]
        public string MessageName { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        //[Required(AllowEmptyStrings = false, ErrorMessage = "消息内容不能为空")]
        public string MessageContains { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "消息描述不能为空")]
        public string MessageDescribe { get; set; }
        /// <summary>
        /// 消息图案
        /// </summary>
        [Required(ErrorMessage = "请选择消息图案", AllowEmptyStrings = false)]
        public string MessageImage { get; set; }
     
    }
}