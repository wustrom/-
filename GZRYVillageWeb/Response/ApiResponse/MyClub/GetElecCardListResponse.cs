using DbOpertion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GZRYVillageWeb.Response.ApiResponse
{
    /// <summary>
    /// 获得电子储值卡列表请求
    /// </summary>
    public class GetElecCardListResponse
    {
        /// <summary>
        /// 获得电子储值卡列表请求
        /// </summary>
        public GetElecCardListResponse(ElectronicCardRelation Relation, List<ElectronicCard> ListElecCard, List<ElectronicType> ListElecType, string Host)
        {
            //电子储存卡关系Id
            this.ElectronicCardRelationId = Relation.ElectronicCardRelationId;
            //电子储值卡
            var ElecCard = ListElecCard.Where(p => p.ElectronicId == Relation.ElectronicCardId).ToList().FirstOrDefault();
            ElectronicType ElecType = null;
            if (ElecCard != null)
            {
                ElecType = ListElecType.Where(p => p.ElectronicTypeId == ElecCard.ElectronicTypeId).ToList().FirstOrDefault();
            }
            if (ElecType != null)
            {
                this.CardId = ElecCard.ElectronicId;
                //卡名
                this.CardName = ElecCard.CardName;
                //图片
                this.CardImage = "http://" + Host + ElecType.CardImage;
                //卡片类型名称
                this.CardTypeName = ElecType.CardTypeName;
                //卡片金额
                this.CardMoney = ElecCard.CaerMoney;
                //有效时间
                this.EffectiveTime = ElecCard.CardExpirationDay == null ? "" : ElecCard.CardExpirationDay.Value.ToString("yyyy-MM-dd");
            }


        }

        /// <summary>
        /// 电子储存卡关系Id
        /// </summary>
        public int ElectronicCardRelationId { get; set; }

        /// <summary>
        /// 电子储存卡Id
        /// </summary>
        public int CardId { get; set; }

        /// <summary>
        /// 卡片图片
        /// </summary>
        public String CardImage { get; set; }

        /// <summary>
        /// 卡片金额
        /// </summary>
        public Decimal? CardMoney { get; set; }

        /// <summary>
        /// 卡片类型名称
        /// </summary>
        public String CardTypeName { get; set; }

        /// <summary>
        /// 卡名
        /// </summary>
        public String CardName { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        public string EffectiveTime { get; set; }
    }
}