using Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.AjaxRequest.DataTable
{
    public class MemberCouponRelationDataTableRequest:DataTableRequest
    {
        /// <summary>
        /// 会员等级Id
        /// </summary>
        public int MembershipLevelId { get; set; }
    }
}