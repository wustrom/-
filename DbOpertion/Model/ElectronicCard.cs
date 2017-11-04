using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class ElectronicCard
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 ElectronicId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CardName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CardPassword { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Decimal? CaerMoney { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? ElectronicTypeId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? IsUser { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? IsDelete { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? UserId { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "ElectronicId";
        }

    }
}
