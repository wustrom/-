using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Response.ApiResponse
{
    /// <summary>
    /// 获得我的卡片的应答
    /// </summary>
    public class GetLevelListResponse
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GetLevelListResponse(MemberShipLevel level, List<MemberLevelRelation> List_Relation)
        {
            //等级ID
            this.MembershipLevelId = level.MembershipLevelId;
            //等级名称
            this.LevelName = level.LevelName;
            //优惠事项
            this.Options = List_Relation.Where(p => p.MembershipLevelId == level.MembershipLevelId).Select(p => p.CouponContains).ToList().FirstOrDefault();
        }
        /// <summary>
        /// 等级ID
        /// </summary>
        public Int32 MembershipLevelId { get; set; }
        /// <summary>
        /// 等级名称
        /// </summary>
        public String LevelName { get; set; }
        /// <summary>
        /// 优惠事项
        /// </summary>
        public string Options { get; set; }
    }
}