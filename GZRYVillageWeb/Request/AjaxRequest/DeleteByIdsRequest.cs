using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest
{
    /// <summary>
    /// 多选删除请求
    /// </summary>
    public class DeleteByIdsRequest
    {
        /// <summary>
        /// 数据Id
        /// </summary>
        public string KeyIds { get; set; }
    }
}