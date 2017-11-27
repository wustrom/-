using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class PayRecord
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 PayRecordId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? StoreID { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? UserId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Decimal? ShopMoney { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? ShopTime { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String ShopItem { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Decimal? NeedPayMoney { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "PayRecordId";
        }

    }
}
