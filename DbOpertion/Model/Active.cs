using System;

namespace DbOpertion.Models
{
    [Serializable]
    public class Active
    {
        /// <summary>
        ///
        /// </summary>
        public Int32 ActiveId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? ActiveCardId { get; set; }
        /// <summary>
        ///
        /// </summary>
        public Int32? ActiceUserId { get; set; }
        /// <summary>
        /// 获取对应主键
        /// </summary>
        public string GetBuilderPrimaryKey()
        {
            return "ActiveId";
        }

    }
}
