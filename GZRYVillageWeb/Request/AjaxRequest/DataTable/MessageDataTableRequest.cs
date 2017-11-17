using Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest.DataTable
{
    public class MessageDataTableRequest:DataTableRequest
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public int MessageID { get; set; }
    }
}