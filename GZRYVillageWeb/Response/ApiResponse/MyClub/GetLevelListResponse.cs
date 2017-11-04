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
        public GetLevelListResponse(MemberShipLevel level, List<MemberCouponRelation> List_Relation)
        {
            //等级ID
            this.MembershipLevelId = level.MembershipLevelId;
            //等级名称
            this.LevelName = level.LevelName;
            //等级最低要求
            this.LevelMin = level.LevelMin;
            //当前等级所需
            this.LevelMax = level.LevelMax;
            //升级所需物品
            this.NeedThing = level.NeedThing;
            //优惠事项
            this.Options = List_Relation.Where(p => p.MembershipLevelId == level.MembershipLevelId).Select(p => p.CouponContains).ToList();
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
        /// 等级最低要求
        /// </summary>
        public Int32? LevelMin { get; set; }
        /// <summary>
        /// 当前等级所需
        /// </summary>
        public Int32? LevelMax { get; set; }
        /// <summary>
        /// 升级所需物品
        /// </summary>
        public String NeedThing { get; set; }
        /// <summary>
        /// 优惠事项
        /// </summary>
        public List<string> Options { get; set; }
    }
}