using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class MemberShipType
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 MemberShipTypeId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CardName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String CardImage { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "MemberShipTypeId";
        }

    }
}
