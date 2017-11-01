using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Models
{
  public  class ConsumptionInfo
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        //public  ConsumptionInfo1(Consumption consumption)
        //{
        //    this.ShopName = consumption.ShopName;
        //    this.ConsumptionId = consumption.ConsumptionId;
        //    this.ShopMoney = consumption.ShopMoney;
        //    this.ElectronicId = consumption.ElectronicId;
        //    this.ShopTime = consumption.ShopTime;
        //    this.IsDelete = consumption.IsDelete;
        //    if (consumption.ShopType == 1)
        //    {
        //        this.ShopType = "现金";
        //    }
        //    else if (consumption.ShopType == 2)
        //    {
        //        this.ShopType = "刷卡";
        //    }
        //    else
        //    {
        //        this.ShopType = "未知支付方式";
        //    }
        //}
        /// <summary>
        /// 消费记录表Id
        /// </summary>
        public Int32 ConsumptionId { get; set; }
        /// <summary>
        ///电子储值卡Id
        /// </summary>
        public Int32? ElectronicId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public String UserNickName { get; set; }
        /// <summary>
        ///商店名称
        /// </summary>
        public String ShopName { get; set; }
        /// <summary>
        ///消费金额
        /// </summary>
        public Decimal? ShopMoney { get; set; }
        /// <summary>
        ///消费时间
        /// </summary>
        public DateTime? ShopTime { get; set; }
        /// <summary>
        ///支付方式
        /// </summary>
        public string ShopType { get; set; }
        /// <summary>
        ///是否失效
        /// </summary>
        public Boolean? IsDelete { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "ConsumptionId";
        }
    }
}
