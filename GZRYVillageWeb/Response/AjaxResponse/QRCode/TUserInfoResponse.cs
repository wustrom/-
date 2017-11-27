using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Response.AjaxResponse
{
    /// <summary>
    /// 用户返回
    /// </summary>
    public class TUserInfoResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="List_Level"></param>
        public TUserInfoResponse(TUser user, List<MemberShipLevel> List_Level)
        {
            //用户ID
            this.UserId = user.UserId;
            //用户昵称
            this.UserNickName = user.UserNickName;
            //用户手机号码
            this.UserPhone = user.UserPhone;
            //消费次数
            this.ConsumptionTime = user.ConsumptionTime;
            //钻石金额
            this.DiamondsMoney = user.DiamondsMoney;
            //用户等级
            this.UserLevel = List_Level.Where(p => p.LevelMin <= user.LevelScore && (p.LevelMax + p.LevelMin) > user.LevelScore).FirstOrDefault().LevelName;
            //用户性别
            this.Sex = user.Sex;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Int32 UserId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public String UserNickName { get; set; }
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public String UserPhone { get; set; }
        /// <summary>
        /// 消费次数
        /// </summary>
        public Int32? ConsumptionTime { get; set; }
        /// <summary>
        /// 用户等级
        /// </summary>
        public string UserLevel { get; set; }
        /// <summary>
        /// 钻石金额
        /// </summary>
        public Decimal? DiamondsMoney { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public Boolean? Sex { get; set; }
    }
}