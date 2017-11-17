using Common.Attribute.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Request.ApiRequest
{
    /// <summary>
    /// 电子储值卡关系请求
    /// </summary>
    public class ElecRelationRequest : UserTokenRequest
    {
        /// <summary>
        /// 关系Id
        /// </summary>
        [IntValid(ErrorMessage = "关系Id不能为空")]
        public int? RelationId { get; set; }
    }
}