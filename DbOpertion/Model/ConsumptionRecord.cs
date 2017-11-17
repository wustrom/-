using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class ConsumptionRecord
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 ConsumptionRecordId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? ElectronicId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? PayRecordId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Decimal? PayMoney { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? ShopType { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "ConsumptionRecordId";
        }

    }
}
