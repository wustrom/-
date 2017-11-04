using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class MemberShipLevel
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 MembershipLevelId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String LevelName { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? LevelMin { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? LevelMax { get; set; }
        /// <summary>
        ///
        /// </summary>
        public String NeedThing { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Boolean? IsDelete { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? NextLevelId { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "MembershipLevelId";
        }

    }
}
