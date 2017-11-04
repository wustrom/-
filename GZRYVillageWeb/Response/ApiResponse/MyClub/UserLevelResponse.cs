using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Response.ApiResponse
{
    /// <summary>
    /// 用户等级应答
    /// </summary>
    public class UserLevelResponse
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="levelBefore">之前等级</param>
        /// <param name="levelAfter">之后等级</param>
        /// <param name="UserScore">用户分数</param>
        /// <param name="DiamondsMoney">用户砖石金额</param> 
        public UserLevelResponse(MemberShipLevel levelBefore, MemberShipLevel levelAfter, int UserScore, decimal DiamondsMoney)
        {
            //用户等级名称
            this.UserLevelName = levelBefore.LevelName;
            //下个等级名称
            this.NextLevelName = levelAfter.LevelName;
            //下个等级所需物品
            this.NeedThing = levelBefore.NeedThing;
            //钻石数量
            this.Diamonds = int.Parse(Math.Floor((DiamondsMoney / 30)).ToString());
            //当前物品个数
            this.CurrentThingCount = UserScore - levelBefore.LevelMin.Value;
            //达到下级物品所需个数
            this.NextThingCount = levelBefore.LevelMax.Value;
        }

        /// <summary>
        /// 用户等级名称
        /// </summary>
        public string UserLevelName { get; set; }

        /// <summary>
        /// 下个等级名称
        /// </summary>
        public string NextLevelName { get; set; }

        /// <summary>
        /// 下个等级所需物品
        /// </summary>
        public string NeedThing { get; set; }

        /// <summary>
        /// 钻石个数
        /// </summary>
        public int Diamonds { get; set; }

        /// <summary>
        /// 当前物品个数
        /// </summary>
        public int CurrentThingCount { get; set; }

        /// <summary>
        /// 达到下级物品所需个数
        /// </summary>
        public int NextThingCount { get; set; }

    }
}