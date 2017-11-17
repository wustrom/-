using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest
{
    public class UpdateMessageContainsRequest
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "消息内容不能为空")]
        public string MessageContains { get; set; }
        /// <summary>
        /// 消息Id
        /// </summary>
        public int MessageID { get; set; }
    }
}