using Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest.DataTable
{
    /// <summary>
    /// 使用MembershipLevelId的DataTable
    /// </summary>
    public class MemberShipLevelDataTableRequest : DataTableRequest
    {
        /// <summary>
        /// 会员等级Id
        /// </summary>
        public int MembershipLevelId { get; set; }
    }
}