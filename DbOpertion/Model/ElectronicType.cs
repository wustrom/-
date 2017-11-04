using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class ElectronicType
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 ElectronicTypeId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CardImage { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Decimal? CardMoney { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CardTypeName { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "ElectronicTypeId";
        }

    }
}
