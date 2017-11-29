using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 获得优惠卷请求
    /// </summary>
    public class GetCouponHtmlRequest
    {
        /// <summary>
        /// 优惠卷关系id
        /// </summary>
        [IntValid(ErrorMessage ="请上传优惠卷ID")]
        public int CouponRelationId { get; set; }
    }
}