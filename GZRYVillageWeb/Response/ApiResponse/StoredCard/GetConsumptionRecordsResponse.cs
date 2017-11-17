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
    public class GetConsumptionRecordsResponse
    {
        /// <summary>
        /// 获得电子储值卡列表请求
        /// </summary>
        public GetConsumptionRecordsResponse(PayRecord record, Store store)
        {
            //商铺名称
            this.ShopName = store.StoreName;
            //消费时间
            this.ShopTime = record.ShopTime == null ? "" : record.ShopTime.Value.ToString("yyyy-MM-dd HH:mm");
            //消费金额
            this.CardMoney = record.ShopMoney;
        }

        /// <summary>
        /// 获得电子储值卡列表请求
        /// </summary>
        public GetConsumptionRecordsResponse(PayRecordInformation Info)
        {
            //商铺名称
            this.ShopName = Info.StoreName;
            //消费时间
            this.ShopTime = Info.ShopTime == null ? "" : Info.ShopTime.Value.ToString("yyyy-MM-dd HH:mm");
            //消费金额
            this.CardMoney = Info.PayMoney;
            //用户手机号码
            this.PhoneNumber = Info.UserPhone;
        }

        /// <summary>
        /// 商铺名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 消费时间
        /// </summary>
        public string ShopTime { get; set; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public Decimal? CardMoney { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

    }
}