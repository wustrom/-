using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class MemberShipCard
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 MemberShipCardId { get; set; }
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
        public Int32? MemberShipTypeId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? UserId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? ReleaseDate { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? IsUser { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? IsDelete { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "MemberShipCardId";
        }

    }
}
