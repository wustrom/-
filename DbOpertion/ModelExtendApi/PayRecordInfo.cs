using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOpertion.Models
{
    public class PayRecordInfo
    {
        /// <summary>
        /// 消费记录ID
        /// </summary>
        public Int32 ConsumptionRecordId { get; set; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public Decimal? PayMoney { get; set; }

        /// <summary>
        /// 销售时间
        /// </summary>
        public DateTime? ShopTime { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string UserPhone { get; set; }

        /// <summary>
        /// 商店名称
        /// </summary>
        public string StoreName { get; set; }
    }
}
